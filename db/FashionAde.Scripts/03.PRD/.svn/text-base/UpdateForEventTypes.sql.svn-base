ALTER TABLE eventtypes
 ADD ShortName VARCHAR(7) AFTER BinaryNumber;

UPDATE eventtypes SET ShortName = 'Evening' WHERE EventTypeId = 1;
UPDATE eventtypes SET ShortName = 'Evening' WHERE EventTypeId = 2;
UPDATE eventtypes SET ShortName = 'Work' WHERE EventTypeId = 3;
UPDATE eventtypes SET ShortName = 'Work' WHERE EventTypeId = 4;
UPDATE eventtypes SET ShortName = 'Play' WHERE EventTypeId = 5;

ALTER TABLE eventtypes
 CHANGE ShortName ShortName VARCHAR(7) NOT NULL;
 