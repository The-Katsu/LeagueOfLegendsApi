
    drop table if exists ability cascade

    drop table if exists champion cascade

    drop table if exists champion_comic cascade

    drop table if exists champion_story cascade

    drop table if exists champion_video cascade

    drop table if exists champion_races cascade

    drop table if exists champion_roles cascade

    drop table if exists comic cascade

    drop table if exists race cascade

    drop table if exists region cascade

    drop table if exists related_champion cascade

    drop table if exists related_story cascade

    drop table if exists role cascade

    drop table if exists skin cascade

    drop table if exists story cascade

    drop table if exists cinematic cascade

    create table ability (
        id uuid not null,
       description varchar(255),
       icon_url varchar(255),
       name varchar(255),
       mp4_url varchar(255),
       webm_url varchar(255),
       champion_id uuid,
       primary key (id)
    )

    create table champion (
        id uuid not null,
       image_url varchar(255),
       animated_image_url varchar(255),
       name varchar(255),
       nickname varchar(255),
       biography varchar(255),
       region_id uuid,
       primary key (id)
    )

    create table champion_comic (
        comic_id uuid not null,
       champion_id uuid not null,
       primary key (champion_id, comic_id)
    )

    create table champion_story (
        story_id uuid not null,
       champion_id uuid not null,
       primary key (champion_id, story_id)
    )

    create table champion_video (
        video_id uuid not null,
       champion_id uuid not null,
       primary key (champion_id, video_id)
    )

    create table champion_races (
        champion_id uuid not null,
       race_id uuid not null,
       primary key (race_id, champion_id)
    )

    create table champion_roles (
        champion_id uuid not null,
       role_id uuid not null,
       primary key (role_id, champion_id)
    )

    create table comic (
        id uuid not null,
       title varchar(255),
       series varchar(255),
       pages varchar(255),
       scaled_pages varchar(255),
       credits varchar(255),
       description varchar(255),
       primary key (id)
    )

    create table race (
        id uuid not null,
       name varchar(255),
       primary key (id)
    )

    create table region (
        id uuid not null,
       name varchar(255),
       image_url varchar(255),
       animated_image_url varchar(255),
       overview varchar(255),
       primary key (id)
    )

    create table related_champion (
        id uuid not null,
       champion_id uuid,
       related_champion_id uuid,
       primary key (id)
    )

    create table related_story (
        id uuid not null,
       champion_id uuid,
       story_id uuid,
       primary key (id)
    )

    create table role (
        id uuid not null,
       name varchar(255),
       primary key (id)
    )

    create table skin (
        id uuid not null,
       name varchar(255),
       image_url varchar(255),
       champion_id uuid,
       primary key (id)
    )

    create table story (
        id uuid not null,
       release_date timestamp,
       title varchar(255),
       word_count int4,
       minutes_to_read int4,
       subtitle varchar(255),
       image_url varchar(255),
       content varchar(255),
       region_id uuid,
       primary key (id)
    )

    create table cinematic (
        id uuid not null,
       title varchar(255),
       subtitle varchar(255),
       description varchar(255),
       image_url varchar(255),
       url varchar(255),
       type varchar(255),
       primary key (id)
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
