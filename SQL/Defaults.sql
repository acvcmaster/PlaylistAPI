-- Default values for PROPERTIES

insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (1, 'ALBUM', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (2, 'ALBUM_ARTIST', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (3, 'ARTIST', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (4, 'BIT_RATE', 'INTEGER');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (5, 'CATEGORY', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (6, 'COMPOSER', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (7, 'DATE_ADDED', 'DATETIME');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (8, 'DATE_MODIFIED', 'DATETIME');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (9, 'DESCRIPTION', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (10, 'DISC_NUMBER', 'INTEGER');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (11, 'GENRE', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (12, 'HAS_ARTWORK', 'BOOLEAN');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (13, 'NAME', 'STRING');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (14, 'SAMPLE_RATE', 'INTEGER');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (15, 'SIZE', 'INTEGER');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (16, 'TRACK_NUMER', 'INTEGER');
insert into "PROPERTIES" ("ID", "NAME", "TYPE") values (17, 'YEAR', 'INTEGER');

-- Default values for COMPARATORS

insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (1, 'greater than', '>');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (2, 'greater than or equals', '>=');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (3, 'smaller than', '<');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (4, 'smaller than or equals', '<=');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (5, 'equals', '==');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (6, 'not equals', '!=');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (7, 'contains', 'c');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (8, 'does not contain', '!c');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (9, 'in', 'i'); -- In playlist
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (10, 'not in', '!i'); -- Not in playlist
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (11, 'is true', 't');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (12, 'is false', '!t');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (13, 'is before', 'b');
insert into "COMPARATORS" ("ID", "DESCRIPTION", "OPERATOR") values (14, 'is after', '!b');

-- Default values for RULES

-- ARTIST
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (1, 8);

-- ALBUM_ARTIST
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (2, 8);

-- BIT RATE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (3, 6);

-- CATEGORY
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (4, 8);

-- COMPOSER
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (5, 8);

-- DATE_ADDED
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 13);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (6, 14);

-- DATE_MODIFIED
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 13);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (7, 14);

-- DESCRIPTION
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (8, 8);

-- DISC_NUMBER
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (9, 6);

-- GENRE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (10, 8);

-- HAS_ARTWORK
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (11, 11);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (11, 12);

-- GENRE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (12, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (12, 6);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (12, 7);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (12, 8);

-- SAMPLE_RATE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (13, 6);

-- SIZE
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (14, 6);

-- TRACK_NUMBER
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (15, 6);

-- YEAR
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 1);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 2);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 3);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 4);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 5);
insert into "RULES" ("PROPERTY_ID", "COMPARATOR_ID") values (16, 6);
