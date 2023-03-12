
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
        champion_id int4 not null,
       comic_id int4 not null,
       primary key (comic_id, champion_id)
    )

    create table champion_story (
        champion_id int4 not null,
       story_id int4 not null,
       primary key (story_id, champion_id)
    )

    create table champion_video (
        champion_id int4 not null,
       video_id int4 not null,
       primary key (video_id, champion_id)
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
       url varchar(255),
       content text,
       credits text,
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
       url varchar(255),
       content text,
       primary key (Id)
    )

    create table cinematic (
        Id int4 not null,
       title varchar(255) unique,
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
        add constraint FK_61004C3D 
        foreign key (comic_id) 
        references comic

    alter table champion_comic 
        add constraint FK_43F92BC6 
        foreign key (champion_id) 
        references champion

    alter table champion_story 
        add constraint FK_BFFD538F 
        foreign key (story_id) 
        references story

    alter table champion_story 
        add constraint FK_C2736032 
        foreign key (champion_id) 
        references champion

    alter table champion_video 
        add constraint FK_FBE8D0D4 
        foreign key (video_id) 
        references cinematic

    alter table champion_video 
        add constraint FK_95431E02 
        foreign key (champion_id) 
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

    alter table skin 
        add constraint FK_BCE54059 
        foreign key (champion_id) 
        references champion

    create sequence hibernate_sequence
