-- Create trigger functions

create or replace function trigger_on_create()
returns TRIGGER as $$
begin
  new."CREATION" = now();
  return new;
end;
$$ language plpgsql;

create or replace function trigger_on_update()
returns TRIGGER as $$
begin
  new."LAST_MODIFICATION" = now();
  return new;
end;
$$ language plpgsql;

-- Create triggers

drop trigger if exists on_create on "USERS";
drop trigger if exists on_update on "USERS";
drop trigger if exists on_create on "PLAYLISTS";
drop trigger if exists on_update on "PLAYLISTS";
drop trigger if exists on_create on "RULES";
drop trigger if exists on_update on "RULES";
drop trigger if exists on_create on "SONG_PROPERTIES";
drop trigger if exists on_update on "SONG_PROPERTIES";
drop trigger if exists on_create on "PLAYLIST_RULES";
drop trigger if exists on_update on "PLAYLIST_RULES";
drop trigger if exists on_create on "SONGS";
drop trigger if exists on_update on "SONGS";
drop trigger if exists on_create on "COMPARATORS";
drop trigger if exists on_update on "COMPARATORS";
drop trigger if exists on_create on "PROPERTIES";
drop trigger if exists on_update on "PROPERTIES";

create trigger on_create
before insert on "USERS"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "USERS"
for each row
execute procedure trigger_on_update();

create trigger on_create
before insert on "PLAYLISTS"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "PLAYLISTS"
for each row
execute procedure trigger_on_update();

create trigger on_create
before insert on "RULES"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "RULES"
for each row
execute procedure trigger_on_update();

create trigger on_create
before insert on "SONG_PROPERTIES"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "SONG_PROPERTIES"
for each row
execute procedure trigger_on_update();

create trigger on_create
before insert on "PLAYLIST_RULES"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "PLAYLIST_RULES"
for each row
execute procedure trigger_on_update();

create trigger on_create
before insert on "SONGS"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "SONGS"
for each row
execute procedure trigger_on_update();

create trigger on_create
before insert on "COMPARATORS"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "COMPARATORS"
for each row
execute procedure trigger_on_update();

create trigger on_create
before insert on "PROPERTIES"
for each row
execute procedure trigger_on_create();

create trigger on_update
before update on "PROPERTIES"
for each row
execute procedure trigger_on_update();