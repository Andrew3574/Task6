create table ElementTypes(
	id serial primary key,
	name varchar(20) not null
);

create table Elements(
	id serial primary key,
	name varchar(20) not null,
	typeid int references ElementTypes(id) on delete restrict	
);

create table Slides(
	id serial primary key,
	background varchar(32) default 'white'
);

create table Presentations(
	id serial primary key,
	title varchar(32) default 'Title',
	author varchar(32) not null
);

create table SharedSlideElements(
	id serial primary key,
	slideid int references Slides(id) on delete cascade,
	elementid int references elements(id) on delete restrict,
	element_x int default 0,
	element_y int default 0,
	element_width int default 100,
	element_height int default 30,
	element_content text default ''
);

create table SharedPresentationSlides(
	id serial primary key,
	presentationid int references Presentations(id) on delete cascade,
	slideid int references Slides(id) on delete restrict
);


create index on SharedSlideElements(slideid,elementid);
create index on SharedPresentationSlides(presentationid,slideid);