
    drop table ability cascade

    drop table champion cascade

    drop table champion_comic cascade

    drop table champion_story cascade

    drop table champion_video cascade

    drop table champion_races cascade

    drop table champion_roles cascade

    drop table comic cascade

    drop table race cascade

    drop table region cascade

    drop table related_champion cascade

    drop table related_story cascade

    drop table role cascade

    drop table skin cascade

    drop table story cascade

    drop table cinematic cascade

    drop sequence hibernate_sequence

    create table ability (
        Id int4 not null,
       description text,
       icon_url varchar(255),
       name varchar(255) unique,
       mp4_url varchar(255),
       webm_url varchar(255),
       champion_id int4,
       primary key (Id)
    )

    create table champion (
        Id int4 not null,
       image_url varchar(255),
       animated_image_url varchar(255),
       name varchar(255) unique,
       nickname varchar(255),
       biography text,
       release_date timestamp,
       region_id int4,
       primary key (Id)
    )

    create table champion_comic (
        comic_id int4 not null,
       champion_id int4 not null,
       primary key (champion_id, comic_id)
    )

    create table champion_story (
        story_id int4 not null,
       champion_id int4 not null,
       primary key (champion_id, story_id)
    )

    create table champion_video (
        video_id int4 not null,
       champion_id int4 not null,
       primary key (champion_id, video_id)
    )

    create table champion_races (
        champion_id int4 not null,
       race_id int4 not null,
       primary key (race_id, champion_id)
    )

    create table champion_roles (
        champion_id int4 not null,
       role_id int4 not null,
       primary key (role_id, champion_id)
    )

    create table comic (
        Id int4 not null,
       title varchar(255) unique,
       series varchar(255),
       pages varchar(255),
       scaled_pages varchar(255),
       credits varchar(255),
       description text,
       primary key (Id)
    )

    create table race (
        Id int4 not null,
       name varchar(255) unique,
       primary key (Id)
    )

    create table region (
        Id int4 not null,
       name varchar(255) unique,
       image_url varchar(255),
       animated_image_url varchar(255),
       overview text,
       primary key (Id)
    )

    create table related_champion (
        Id int4 not null,
       champion_id int4,
       related_champion_id int4,
       primary key (Id)
    )

    create table related_story (
        Id int4 not null,
       champion_id int4,
       story_id int4,
       primary key (Id)
    )

    create table role (
        Id int4 not null,
       name varchar(255) unique,
       primary key (Id)
    )

    create table skin (
        Id int4 not null,
       name varchar(255),
       image_url varchar(255),
       champion_id int4,
       primary key (Id)
    )

    create table story (
        Id int4 not null,
       release_date timestamp,
       title varchar(255) unique,
       word_count int4,
       minutes_to_read int4,
       subtitle varchar(255),
       image_url varchar(255),
       content text,
       region_id int4,
       primary key (Id)
    )

    create table cinematic (
        Id int4 not null,
       title varchar(255) unique,
       subtitle varchar(255),
       description varchar(255),
       image_url varchar(255),
       url varchar(255),
       type varchar(255),
       primary key (Id)
    )

    alter table ability 
        add constraint FK_E2F2296E 
        foreign key (champion_id) 
        references champion

    alter table champion 
        add constraint FK_87E51405 
        foreign key (region_id) 
        references region

    alter table champion_comic 
        add constraint FK_4D44E9DC 
        foreign key (champion_id) 
        references comic

    alter table champion_comic 
        add constraint FK_27A75AF0 
        foreign key (comic_id) 
        references champion

    alter table champion_story 
        add constraint FK_C66D2DBC 
        foreign key (champion_id) 
        references story

    alter table champion_story 
        add constraint FK_448D07AB 
        foreign key (story_id) 
        references champion

    alter table champion_video 
        add constraint FK_9F4E4D58 
        foreign key (champion_id) 
        references cinematic

    alter table champion_video 
        add constraint FK_51C5E00F 
        foreign key (video_id) 
        references champion

    alter table champion_races 
        add constraint FK_EF6229AE 
        foreign key (race_id) 
        references race

    alter table champion_races 
        add constraint FK_7E1E4BD0 
        foreign key (champion_id) 
        references champion

    alter table champion_roles 
        add constraint FK_EBBCDCE 
        foreign key (role_id) 
        references role

    alter table champion_roles 
        add constraint FK_CB3F99FE 
        foreign key (champion_id) 
        references champion

    alter table related_champion 
        add constraint FK_CA31F441 
        foreign key (champion_id) 
        references champion

    alter table related_champion 
        add constraint FK_D6300E76 
        foreign key (related_champion_id) 
        references champion

    alter table related_story 
        add constraint FK_5856CC23 
        foreign key (champion_id) 
        references story

    alter table related_story 
        add constraint FK_C364CF79 
        foreign key (story_id) 
        references region

    alter table skin 
        add constraint FK_BCE54059 
        foreign key (champion_id) 
        references champion

    alter table story 
        add constraint FK_C4B86FBB 
        foreign key (region_id) 
        references region

    create sequence hibernate_sequence
