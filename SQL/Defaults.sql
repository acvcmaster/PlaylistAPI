-- Default values for PROPERTIES

insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (1, 'ALBUM', 'STRING', 'Album');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (2, 'ALBUM_ARTIST', 'STRING', 'Album artist');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (3, 'ARTIST', 'STRING', 'Artist');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (4, 'BIT_RATE', 'INTEGER', 'Bit rate');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (5, 'CATEGORY', 'STRING', 'Category');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (6, 'COMPOSER', 'STRING', 'Composer');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (7, 'DATE_ADDED', 'DATETIME', 'Date added');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (8, 'DATE_MODIFIED', 'DATETIME', 'Date modified');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (9, 'DESCRIPTION', 'STRING', 'Description');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (10, 'DISC_NUMBER', 'INTEGER', 'Disc number');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (11, 'GENRE', 'STRING', 'Genre');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (12, 'HAS_ARTWORK', 'BOOLEAN', 'Has artwork');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (13, 'NAME', 'STRING', 'Title');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (14, 'SAMPLE_RATE', 'INTEGER', 'Sample rate');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (15, 'SIZE', 'INTEGER', 'File size');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (16, 'TRACK_NUMER', 'INTEGER', 'Track number');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (17, 'YEAR', 'INTEGER', 'Year');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (18, 'LYRICS', 'STRING', 'Lyrics');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (19, 'MIME', 'STRING', 'MIME Type');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (20, 'PLAYLIST', 'PLAYLIST', 'Playlist');
insert into "PROPERTIES" ("ID", "NAME", "TYPE", "DESCRIPTION") values (21, 'GROUPING', 'STRING', 'Playlist');


-- Default values for COMPARATORS

insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (1, 'greater than', '>');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (2, 'greater than or equals', '>=');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (3, 'smaller than', '<');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (4, 'smaller than or equals', '<=');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (5, 'equals', '==');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (6, 'not equals', '!=');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (7, 'contains', 'c');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (8, 'does not contain', '!c');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (11, 'is true', 't');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (12, 'is false', '!t');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (13, 'is before', 'b');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (14, 'is after', '!b');

-- Default values for RULES

-- ALBUM
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 8);

-- ALBUM_ARTIST
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 8);

-- ARTIST
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 8);

-- BIT RATE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 6);

-- CATEGORY
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 8);

-- COMPOSER
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 8);

-- DATE_ADDED
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 13);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 14);

-- DATE_MODIFIED
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 13);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 14);

-- DESCRIPTION
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 8);

-- DISC_NUMBER
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 6);

-- GENRE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (11, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (11, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (11, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (11, 8);

-- GROUPING
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (21, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (21, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (21, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (21, 8);

-- HAS_ARTWORK
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (12, 11);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (12, 12);

-- GENRE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 8);

-- SAMPLE_RATE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 6);

-- SIZE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 6);

-- TRACK_NUMBER
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 6);

-- YEAR
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (17, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (17, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (17, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (17, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (17, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (17, 6);

-- LYRICS
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (18, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (18, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (18, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (18, 8);

-- MIME
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (19, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (19, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (19, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (19, 8);

-- SONG
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (20, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (20, 6);