using Microsoft.AspNetCore.SignalR;

namespace EditorAPP
{
    public class PresentationHub : Hub
    {
        private static Dictionary<int, HashSet<string>> PresentationUsers = new Dictionary<int, HashSet<string>>();

        public async Task JoinSession(int presentationId, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{presentationId}");
            lock (PresentationUsers)
            {
                if (!PresentationUsers.ContainsKey(presentationId))
                    PresentationUsers[presentationId] = new HashSet<string>();

                PresentationUsers[presentationId].Add(username);
            }

            await Clients.Group($"{presentationId}")
                .SendAsync("UserListUpdated", PresentationUsers[presentationId].ToList());

            Context.Items["presentationId"] = presentationId;
            Context.Items["username"] = username;

        }

        public async Task OnElementChanged(int slideId, int elementIndex, string newContent)
        {
            await Clients.Others.SendAsync("OnElementChanged", slideId, elementIndex, newContent);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.Items.TryGetValue("presentationId", out var pidObj) &&
                 Context.Items.TryGetValue("username", out var unameObj))
            {
                int presentationId = (int)pidObj!;
                string username = (string)unameObj!;

                lock (PresentationUsers)
                {
                    if (PresentationUsers.ContainsKey(presentationId))
                    {
                        PresentationUsers[presentationId].Remove(username);

                        if (!PresentationUsers[presentationId].Any())
                            PresentationUsers.Remove(presentationId);
                    }
                }

                await Clients.Group($"{presentationId}")
                    .SendAsync("UserListUpdated", PresentationUsers.ContainsKey(presentationId)
                        ? PresentationUsers[presentationId].ToList()
                        : new List<string>());

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{presentationId}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
