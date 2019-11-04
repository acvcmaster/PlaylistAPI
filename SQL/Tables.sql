-- drop all tables

drop table if exists "USERS";
drop table if exists "PLAYLISTS";
drop table if exists "RULES";
drop table if exists "SONG_PROPERTIES";
drop table if exists "PLAYLIST_RULES";
drop table if exists "SONGS";
drop table if exists "COMPARATORS";
drop table if exists "PROPERTIES";
drop table if exists "BASE";

-- create all tables (all tables inherit the 'BASE' table)

create table "BASE" (
	"ID" serial primary key,
	"CREATION" timestamp not null,
	"LAST_MODIFICATION" timestamp null,
	"ACTIVE" boolean not null
	);

create table "USERS" (
	"NAME" varchar(255) not null,
	"EMAIL" varchar(255) null,
	"PASSWORD" varchar(255) not null
	) inherits ("BASE");

create table "PLAYLISTS" (
	"NAME" varchar(255) not null,
	"OWNER_ID" integer not null,
    "IS_SMART" boolean not null
) inherits ("BASE");

create table "RULES" (
	"PROPERTY_ID" varchar(255) not null,
    "ID_COMPARATOR" integer not null
) inherits ("BASE");

create table "SONG_PROPERTIES" (
	"NAME" varchar(255) not null,
    "SONG_ID" integer not null,
    "VALUE" varchar
) inherits ("BASE");

create table "PLAYLIST_RULES" (
    "PLAYLIST_ID" integer not null,
    "RULE_ID" integer not null,
    "DATA" varchar null
) inherits ("BASE");

create table "SONGS" (
    "URL" varchar not null
) inherits ("BASE");

create table "COMPARATORS" (
    "DESCRIPTION" varchar(255) null,
    "OPERATOR" varchar(2) not null
) inherits ("BASE");

create table "PROPERTIES" (
    "NAME" varchar(255) not null,
	"TYPE" varchar(25) not null
) inherits ("BASE");