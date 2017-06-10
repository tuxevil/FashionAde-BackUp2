CREATE TABLE `silouhettekeywordsbypartners` (
  `silouhettekeywordsbypartnerid` int(11) NOT NULL AUTO_INCREMENT,
  `silouhetteid` int(11) NOT NULL,
  `partnerid` int(11) NOT NULL,
  `keywords` varchar(1000) NOT NULL,
  PRIMARY KEY (`silouhettekeywordsbypartnerid`),
  KEY `silouhetteid` (`silouhetteid`),
  KEY `partnerid` (`partnerid`),
  CONSTRAINT `silouhettekeywordsbypartners_ibfk_1` FOREIGN KEY (`silouhetteid`) REFERENCES `silouhettes` (`SilouhetteId`),
  CONSTRAINT `silouhettekeywordsbypartners_ibfk_2` FOREIGN KEY (`partnerid`) REFERENCES `partners` (`PartnerId`)
) ENGINE=InnoDB AUTO_INCREMENT=257 DEFAULT CHARSET=latin1;

INSERT INTO silouhettekeywordsbypartners (silouhetteid, partnerid, keywords) select silouhetteid, 2, keywords from silouhettes;

ALTER TABLE fashionade_dev.silouhettes
 DROP FOREIGN KEY FK36381626D90B4B4,
 DROP FOREIGN KEY FK3638162D795D853,
 DROP FOREIGN KEY FK3638162EA106C17,
 DROP Keywords;
ALTER TABLE fashionade_dev.silouhettes
 ADD CONSTRAINT FK36381626D90B4B4 FOREIGN KEY (CategoryId) REFERENCES fashionade_dev.categories (CategoryId) ON UPDATE RESTRICT ON DELETE RESTRICT,
 ADD CONSTRAINT FK3638162D795D853 FOREIGN KEY (ShapeId) REFERENCES fashionade_dev.shapes (ShapeId) ON UPDATE RESTRICT ON DELETE RESTRICT,
 ADD CONSTRAINT FK3638162EA106C17 FOREIGN KEY (StructureId) REFERENCES fashionade_dev.structures (StructureId) ON UPDATE RESTRICT ON DELETE RESTRICT;
