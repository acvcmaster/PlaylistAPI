-- drop all tables

drop table if exists "PLAYLIST_RULES";
drop table if exists "SONG_PROPERTIES";
drop table if exists "RULES";
drop table if exists "PLAYLISTS";
drop table if exists "COMPARATORS";
drop table if exists "SONGS";
drop table if exists "USERS";
drop table if exists "PROPERTIES";
-- create all tables

create table "USERS" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
	"NAME" varchar(255) not null,
	"EMAIL" varchar(255) null,
	"PASSWORD" varchar(255)
);

create table "SONGS" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
    "URL" varchar not null
);

create table "COMPARATORS" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
    "DESCRIPTION" varchar(255) null,
    "OPERATOR" varchar(2) not null
);

create table "PROPERTIES" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
    "NAME" varchar(255) not null,
	"TYPE" varchar(25) not null
);

create table "PLAYLISTS" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
	"NAME" varchar(255) not null,
	"OWNER_ID" integer not null references "USERS"("ID") on delete cascade,
    "IS_SMART" boolean not null
);

create table "RULES" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
	"PROPERTY_ID" integer not null references "PROPERTIES"("ID") on delete cascade,
    "COMPARATOR_ID" integer not null references "COMPARATORS"("ID") on delete cascade
);

create table "SONG_PROPERTIES" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
	"NAME" varchar(255) not null,
    "SONG_ID" integer not null references "SONGS"("ID") on delete cascade,
    "VALUE" varchar
);

create table "PLAYLIST_RULES" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null,
    "PLAYLIST_ID" integer not null references "PLAYLISTS"("ID") on delete cascade,
    "RULE_ID" integer not null references "RULES"("ID") on delete cascade,
    "DATA" varchar null
);