using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace EditorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentationsController : ControllerBase
    {
        private readonly PresentationRepository _presentationRepository;
        private readonly PresentationSlideRepoitory _presentationSlideRepoitory;
        private readonly ILogger<PresentationsController> _logger;

        public PresentationsController(PresentationRepository presentationRepository, PresentationSlideRepoitory presentationSlideRepoitory, ILogger<PresentationsController> logger)
        {
            _presentationRepository = presentationRepository;
            _presentationSlideRepoitory = presentationSlideRepoitory;
            _logger = logger;
        }

        [HttpGet("GetByBatch/{batch:int}")]
        public async Task<ActionResult> GetByBatch(int batch)
        {
            try
            {
                return Ok(await _presentationRepository.GetByBatch(batch));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _presentationRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(Presentation presentation)
        {
            try
            {
                await _presentationRepository.Create(presentation);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody]Presentation presentation)
        {
            try
            {
                await _presentationRepository.Update(presentation);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при обновлении презентации: " + ex.Message +"INNER:"+ex.InnerException);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Presentation presentation)
        {
            try
            {
                await _presentationRepository.Delete(presentation);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }

        [HttpGet("GetPresentationSlides/{id:int}")]
        public async Task<ActionResult> GetPresentationSlides(int id)
        {
            try
            {
                return Ok(await _presentationSlideRepoitory.GetByPresentationId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
