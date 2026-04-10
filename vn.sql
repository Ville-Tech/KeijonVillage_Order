-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema vn
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `vn` ;

-- -----------------------------------------------------
-- Schema vn
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `vn` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin ;
USE `vn` ;

-- -----------------------------------------------------
-- Table `vn`.`alue`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`alue` ;

CREATE TABLE IF NOT EXISTS `vn`.`alue` (
  `alue_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nimi` VARCHAR(40) NULL DEFAULT NULL,
  PRIMARY KEY (`alue_id`),
  INDEX `alue_nimi_index` (`nimi` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


-- -----------------------------------------------------
-- Table `vn`.`posti`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`posti` ;

CREATE TABLE IF NOT EXISTS `vn`.`posti` (
  `postinro` CHAR(5) NOT NULL,
  `toimipaikka` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`postinro`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;

-- -----------------------------------------------------
-- Data for table `vn`.`posti`
-- -----------------------------------------------------
INSERT INTO `vn`.`posti` (`postinro`, `toimipaikka`) VALUES
('00100', 'Helsinki'),('00200', 'Helsinki'),('00300', 'Helsinki'),
('00400', 'Helsinki'),('00500', 'Helsinki'),('00600', 'Helsinki'),
('00700', 'Helsinki'),('00800', 'Helsinki'),('00900', 'Helsinki'),
('01200', 'Vantaa'),('01300', 'Vantaa'),('01400', 'Vantaa'),
('01600', 'Vantaa'),('01700', 'Vantaa'),('01800', 'Klaukkala'),
('02100', 'Espoo'),('02200', 'Espoo'),('02300', 'Espoo'),
('02600', 'Espoo'),('02700', 'Kauniainen'),('04200', 'Kerava'),
('04400', 'Järvenpää'),('04600', 'Mäntsälä'),('06100', 'Porvoo'),
('08100', 'Lohja'),('09220', 'Karkkila'),('10120', 'Tammisaari'),
('10600', 'Tammisaari'),('10900', 'Hanko'),('11100', 'Riihimäki'),
('11200', 'Riihimäki'),('12100', 'Oitti'),('12700', 'Loppi'),
('13100', 'Hämeenlinna'),('13200', 'Hämeenlinna'),('13300', 'Hämeenlinna'),
('13500', 'Hämeenlinna'),('14200', 'Tuulos'),('14500', 'Iittala'),
('15100', 'Lahti'),('15200', 'Lahti'),('15300', 'Lahti'),
('15500', 'Lahti'),('15700', 'Lahti'),('15900', 'Lahti'),
('16300', 'Orimattila'),('17100', 'Heinola'),('17200', 'Vääksy'),
('18100', 'Heinola'),('19200', 'Hartola'),('20100', 'Turku'),
('20200', 'Turku'),('20300', 'Turku'),('20500', 'Turku'),
('20700', 'Turku'),('20800', 'Turku'),('21100', 'Naantali'),
('21200', 'Raisio'),('21500', 'Piikkiö'),('21600', 'Parainen'),
('23100', 'Mynämäki'),('23500', 'Uusikaupunki'),('24100', 'Salo'),
('25110', 'Salo'),('25200', 'Salo'),('26100', 'Rauma'),
('26200', 'Rauma'),('26500', 'Rauma'),('27100', 'Eurajoki'),
('28100', 'Pori'),('28200', 'Pori'),('28500', 'Pori'),
('28900', 'Pori'),('29200', 'Harjavalta'),('29900', 'Luvia'),
('30100', 'Forssa'),('30200', 'Forssa'),('31100', 'Tammela'),
('31200', 'Tammela'),('32200', 'Loimaa'),('33100', 'Tampere'),
('33200', 'Tampere'),('33300', 'Tampere'),('33500', 'Tampere'),
('33700', 'Tampere'),('33900', 'Tampere'),('36200', 'Kangasala'),
('37500', 'Lempäälä'),('37800', 'Akaa'),('38200', 'Sastamala'),
('39500', 'Ikaalinen'),('40100', 'Jyväskylä'),('40200', 'Jyväskylä'),
('40500', 'Jyväskylä'),('40700', 'Jyväskylä'),('40900', 'Jyväskylä'),
('41160', 'Tikkakoski'),('42100', 'Jämsä'),('42300', 'Himos'),
('43100', 'Saarijärvi'),('44100', 'Äänekoski'),('45100', 'Kouvola'),
('45200', 'Kouvola'),('45500', 'Kouvola'),('45700', 'Kouvola'),
('46400', 'Elimäki'),('47400', 'Kymi'),('48100', 'Kotka'),
('48200', 'Kotka'),('48400', 'Kotka'),('48700', 'Kotka'),
('49400', 'Hamina'),('50100', 'Mikkeli'),('50200', 'Mikkeli'),
('50500', 'Mikkeli'),('50700', 'Mikkeli'),('51200', 'Kangasniemi'),
('51900', 'Juva'),('53100', 'Lappeenranta'),('53200', 'Lappeenranta'),
('53500', 'Lappeenranta'),('53900', 'Lappeenranta'),('54100', 'Joutseno'),
('55100', 'Imatra'),('55300', 'Imatra'),('57100', 'Savonlinna'),
('57200', 'Savonlinna'),('58100', 'Kerimäki'),('58500', 'Enonkoski'),
('60100', 'Seinäjoki'),('60200', 'Seinäjoki'),('60500', 'Seinäjoki'),
('60800', 'Seinäjoki'),('61300', 'Kurikka'),('62100', 'Lapua'),
('65100', 'Vaasa'),('65200', 'Vaasa'),('65300', 'Vaasa'),
('66400', 'Laihia'),('67100', 'Kokkola'),('67200', 'Kokkola'),
('67300', 'Kokkola'),('70100', 'Kuopio'),('70200', 'Kuopio'),
('70300', 'Kuopio'),('70500', 'Kuopio'),('70700', 'Kuopio'),
('70900', 'Kuopio'),('71800', 'Siilinjärvi'),('72100', 'Karttula'),
('73100', 'Lapinlahti'),('73300', 'Nilsiä'),('73310', 'Tahkovuori'),
('74100', 'Iisalmi'),('74200', 'Iisalmi'),('76100', 'Pieksämäki'),
('80100', 'Joensuu'),('80200', 'Joensuu'),('80500', 'Joensuu'),
('80700', 'Joensuu'),('82100', 'Liperi'),('82500', 'Kitee'),
('83500', 'Outokumpu'),('84100', 'Ylivieska'),('85100', 'Ylivieska'),
('87100', 'Kajaani'),('87200', 'Kajaani'),('87500', 'Kajaani'),
('88600', 'Sotkamo'),('88610', 'Vuokatti'),('88900', 'Kuhmo'),
('90100', 'Oulu'),('90200', 'Oulu'),('90300', 'Oulu'),
('90500', 'Oulu'),('90700', 'Oulu'),('90900', 'Oulu'),
('91100', 'Ii'),('91500', 'Muhos'),('91800', 'Tyrnävä'),
('92100', 'Raahe'),('92200', 'Raahe'),('93100', 'Pudasjärvi'),
('93200', 'Syöte'),('93400', 'Taivalkoski'),('93600', 'Kuusamo'),
('93999', 'Ruka'),('95100', 'Kemi'),('95400', 'Tornio'),
('95700', 'Pello'),('95900', 'Kolari'),('95950', 'Ylläs'),
('95980', 'Ylläsjärvi'),('96100', 'Rovaniemi'),('96200', 'Rovaniemi'),
('96300', 'Rovaniemi'),('97700', 'Ranua'),('98100', 'Kemijärvi'),
('98530', 'Pyhätunturi'),('98550', 'Luosto'),('99100', 'Kittilä'),
('99130', 'Sirkka'),('99300', 'Muonio'),('99400', 'Enontekiö'),
('99600', 'Sodankylä'),('99800', 'Ivalo'),('99830', 'Saariselkä'),
('99870', 'Inari'),('00102', 'Helsinki'),('00103', 'Helsinki'),('00104', 'Helsinki'),
('00105', 'Helsinki'),('00106', 'Helsinki'),('00107', 'Helsinki'),
('00108', 'Helsinki'),('00109', 'Helsinki'),('00110', 'Helsinki'),
('00120', 'Helsinki'),('00130', 'Helsinki'),('00140', 'Helsinki'),
('00150', 'Helsinki'),('00160', 'Helsinki'),('00170', 'Helsinki'),
('00180', 'Helsinki'),('00190', 'Helsinki'),('00210', 'Helsinki'),
('00220', 'Helsinki'),('00230', 'Helsinki'),('00240', 'Helsinki'),
('00250', 'Helsinki'),('00260', 'Helsinki'),('00270', 'Helsinki'),
('00280', 'Helsinki'),('00290', 'Helsinki'),('00310', 'Helsinki'),
('00320', 'Helsinki'),('00330', 'Helsinki'),('00340', 'Helsinki'),
('00350', 'Helsinki'),('00360', 'Helsinki'),('00370', 'Helsinki'),
('00380', 'Helsinki'),('00390', 'Helsinki'),('00410', 'Helsinki'),
('00420', 'Helsinki'),('00430', 'Helsinki'),('00440', 'Helsinki'),
('00510', 'Helsinki'),('00520', 'Helsinki'),
('00530', 'Helsinki'),('00540', 'Helsinki'),('00550', 'Helsinki'),
('00560', 'Helsinki'),('00570', 'Helsinki'),('00580', 'Helsinki'),
('00590', 'Helsinki'),('00610', 'Helsinki'),('00620', 'Helsinki'),
('00630', 'Helsinki'),('00640', 'Helsinki'),('00650', 'Helsinki'),
('00660', 'Helsinki'),('00670', 'Helsinki'),('00680', 'Helsinki'),
('00690', 'Helsinki'),('00710', 'Helsinki'),
('00720', 'Helsinki'),('00730', 'Helsinki'),('00740', 'Helsinki'),
('00750', 'Helsinki'),('00760', 'Helsinki'),('00770', 'Helsinki'),
('00780', 'Helsinki'),('00790', 'Helsinki'),
('00810', 'Helsinki'),('00820', 'Helsinki'),('00830', 'Helsinki'),
('00840', 'Helsinki'),('00850', 'Helsinki'),('00860', 'Helsinki'),
('00870', 'Helsinki'),('00880', 'Helsinki'),('00890', 'Helsinki'),
('00910', 'Helsinki'),('00920', 'Helsinki'),
('00930', 'Helsinki'),('00940', 'Helsinki'),('00950', 'Helsinki'),
('00960', 'Helsinki'),('00970', 'Helsinki'),('00980', 'Helsinki'),
('00990', 'Helsinki'),
('33010', 'Tampere'),('33014', 'Tampere'),
('33180', 'Tampere'),('33210', 'Tampere'),('33230', 'Tampere'),
('33240', 'Tampere'),('33250', 'Tampere'),('33270', 'Tampere'),
('33280', 'Tampere'),('33310', 'Tampere'),('33330', 'Tampere'),
('33340', 'Tampere'),('33410', 'Tampere'),('33420', 'Tampere'),
('33430', 'Tampere'),('33470', 'Tampere'),('33480', 'Tampere'),
('33520', 'Tampere'),('33540', 'Tampere'),('33560', 'Tampere'),
('33580', 'Tampere'),('33610', 'Tampere'),('33620', 'Tampere'),
('33630', 'Tampere'),('33640', 'Tampere'),('33650', 'Tampere'),
('33660', 'Tampere'),('33680', 'Tampere'),('33710', 'Tampere'),
('33720', 'Tampere'),('33730', 'Tampere'),('33800', 'Tampere'),
('33820', 'Tampere'),('33840', 'Tampere'),('33850', 'Tampere'),
('33870', 'Tampere'),('33920', 'Tampere'),
('33950', 'Tampere'),('33960', 'Tampere'),('90007', 'Oulu'),
('90014', 'Oulu'),('90015', 'Oulu'),
('90016', 'Oulu'),('90017', 'Oulu'),('90018', 'Oulu'),
('90019', 'Oulu'),('90020', 'Oulu'),('90029', 'Oulu'),
('90030', 'Oulu'),('90040', 'Oulu'),('90050', 'Oulu'),
('90060', 'Oulu'),('90061', 'Oulu'),('90062', 'Oulu'),
('90063', 'Oulu'),('90064', 'Oulu'),('90065', 'Oulu'),
('90066', 'Oulu'),('90067', 'Oulu'),('90068', 'Oulu'),
('90069', 'Oulu'),('90070', 'Oulu'),('90071', 'Oulu'),
('90072', 'Oulu'),('90073', 'Oulu'),('90074', 'Oulu'),
('90075', 'Oulu'),('90076', 'Oulu'),('90077', 'Oulu'),
('90110', 'Oulu'),('90120', 'Oulu'),('90130', 'Oulu'),
('90140', 'Oulu'),('90150', 'Oulu'),('90160', 'Oulu'),
('90170', 'Oulu'),('90210', 'Oulu'),('90220', 'Oulu'),
('90230', 'Oulu'),('90240', 'Oulu'),('90250', 'Oulu'),
('90260', 'Oulu'),('90310', 'Oulu'),('90320', 'Oulu'),
('90330', 'Oulu'),('90340', 'Oulu'),('90350', 'Oulu'),
('90360', 'Oulu'),('90370', 'Oulu'),('90380', 'Oulu'),
('90390', 'Oulu'),('90400', 'Oulu'),('90410', 'Oulu'),
('90420', 'Oulu'),('90430', 'Oulu'),('90440', 'Oulu'),
('90450', 'Oulu'),('90460', 'Oulu'),('90470', 'Oulu'),
('90480', 'Oulu'),('90490', 'Oulu'),('90510', 'Oulu'),
('90520', 'Oulu'),('90530', 'Oulu'),('90540', 'Oulu'),
('90550', 'Oulu'),('90560', 'Oulu'),('90570', 'Oulu'),
('90580', 'Oulu'),('90590', 'Oulu'),('90610', 'Oulu'),
('90620', 'Oulu'),('90630', 'Oulu'),('90640', 'Oulu'),
('90650', 'Oulu'),('90660', 'Oulu'),('90670', 'Oulu'),
('90680', 'Oulu'),('90690', 'Oulu'),('90710', 'Oulu'),
('90720', 'Oulu'),('90730', 'Oulu'),('90740', 'Oulu'),
('90750', 'Oulu'),('90760', 'Oulu'),('90770', 'Oulu'),
('90780', 'Oulu'),('90790', 'Oulu'),('90810', 'Oulu'),
('90820', 'Oulu'),('90830', 'Oulu'),('90840', 'Oulu'),
('90850', 'Oulu'),('90860', 'Oulu'),('90870', 'Oulu'),
('90880', 'Oulu'),('90890', 'Oulu'),('70110', 'Kuopio'),
('70150', 'Kuopio'),('70160', 'Kuopio'),
('70170', 'Kuopio'),('70180', 'Kuopio'),('70190', 'Kuopio'),
('70210', 'Kuopio'),('70211', 'Kuopio'),('70240', 'Kuopio'),
('70260', 'Kuopio'),('70280', 'Kuopio'),('70290', 'Kuopio'),
('70340', 'Kuopio'),('70420', 'Kuopio'),('70460', 'Kuopio'),
('70600', 'Kuopio'),('70610', 'Kuopio'),('70620', 'Kuopio'),
('70630', 'Kuopio'),('70640', 'Kuopio'),('70650', 'Kuopio'),
('70660', 'Kuopio'),('70670', 'Kuopio'),('70680', 'Kuopio'),
('70690', 'Kuopio'),('70780', 'Kuopio'),
('20101', 'Turku'),('20110', 'Turku'),('20111', 'Turku'),
('20120', 'Turku'),('20130', 'Turku'),('20140', 'Turku'),
('20160', 'Turku'),('20180', 'Turku'),('20210', 'Turku'),
('20220', 'Turku'),('20240', 'Turku'),('20250', 'Turku'),
('20260', 'Turku'),('20270', 'Turku'),('20280', 'Turku'),
('20320', 'Turku'),('20360', 'Turku'),('20380', 'Turku'),
('20400', 'Turku'),('20460', 'Turku'),('20520', 'Turku'),
('20540', 'Turku'),('20560', 'Turku'),('20610', 'Turku'),
('20660', 'Turku'),('20720', 'Turku'),('20740', 'Turku'),
('20760', 'Turku'),('20780', 'Turku'),('20810', 'Turku'),
('20880', 'Turku'),('20900', 'Turku'),('20960', 'Turku'),
('40014', 'Jyväskylä'),('40018', 'Jyväskylä'),('40019', 'Jyväskylä'),
('40250', 'Jyväskylä'),('40270', 'Jyväskylä'),('40320', 'Jyväskylä'),
('40340', 'Jyväskylä'),('40350', 'Jyväskylä'),('40360', 'Jyväskylä'),
('40420', 'Jyväskylä'),('40520', 'Jyväskylä'),('40530', 'Jyväskylä'),
('40600', 'Jyväskylä'),('40620', 'Jyväskylä'),('40630', 'Jyväskylä'),
('40640', 'Jyväskylä'),('40660', 'Jyväskylä'),('40720', 'Jyväskylä'),
('40740', 'Jyväskylä'),('40820', 'Jyväskylä'),('40830', 'Jyväskylä'),
('40930', 'Jyväskylä'),('40950', 'Jyväskylä'),
('96101', 'Rovaniemi'),('96130', 'Rovaniemi'),('96140', 'Rovaniemi'),
('96160', 'Rovaniemi'),('96190', 'Rovaniemi'),('96210', 'Rovaniemi'),
('96230', 'Rovaniemi'),('96240', 'Rovaniemi'),('96260', 'Rovaniemi'),
('96270', 'Rovaniemi'),('96280', 'Rovaniemi'),('96290', 'Rovaniemi'),
('96320', 'Rovaniemi'),('96370', 'Rovaniemi'),('96380', 'Rovaniemi'),
('96400', 'Rovaniemi'),('96440', 'Rovaniemi'),('96460', 'Rovaniemi'),
('96480', 'Rovaniemi'),('96500', 'Rovaniemi'),('96600', 'Rovaniemi'),
('96700', 'Rovaniemi'),('96800', 'Rovaniemi'),('96900', 'Rovaniemi'),
('31600', 'Jokioinen'),('31700', 'Urjala'),('31800', 'Urjala'),
('31900', 'Urjala'),('34110', 'Ruovesi'),('34130', 'Ruovesi'),
('34200', 'Ruovesi'),('34210', 'Ruovesi'),('34220', 'Ruovesi'),
('34230', 'Ruovesi'),('34240', 'Ruovesi'),('34250', 'Ruovesi'),
('34260', 'Ruovesi'),('34270', 'Ruovesi'),('35100', 'Orivesi'),
('35200', 'Orivesi'),('35220', 'Orivesi'),('35270', 'Orivesi'),
('35300', 'Orivesi'),('35400', 'Orivesi'),('35420', 'Orivesi'),
('35430', 'Orivesi'),('35470', 'Orivesi'),('35500', 'Orivesi'),
('35530', 'Orivesi'),('35540', 'Orivesi'),('35600', 'Orivesi'),
('35700', 'Orivesi'),('35800', 'Orivesi'),('35820', 'Orivesi'),
('35900', 'Orivesi'),('36100', 'Kangasala'),('36110', 'Kangasala'),
('36120', 'Kangasala'),('36130', 'Kangasala'),('36140', 'Kangasala'),
('36150', 'Kangasala'),('36160', 'Kangasala'),('36170', 'Kangasala'),
('36180', 'Kangasala'),('36190', 'Kangasala'),('36210', 'Kangasala'),
('36220', 'Kangasala'),('36230', 'Kangasala'),('36240', 'Kangasala'),
('52200', 'Puumala'),('52300', 'Ristiina'),('52400', 'Ristiina'),
('52420', 'Ristiina'),('52430', 'Ristiina'),('52440', 'Ristiina'),
('52450', 'Ristiina'),('52460', 'Ristiina'),('52470', 'Ristiina'),
('52480', 'Ristiina'),('52490', 'Ristiina'),('52500', 'Mikkeli'),
('52510', 'Mikkeli'),('52520', 'Mikkeli'),('52530', 'Mikkeli'),
('52540', 'Mikkeli'),('52550', 'Mikkeli'),('52560', 'Mikkeli'),
('52570', 'Mikkeli'),('52580', 'Mikkeli'),('52590', 'Mikkeli'),
('56100', 'Imatra'),('56120', 'Imatra'),('56130', 'Imatra'),
('56140', 'Imatra'),('56150', 'Imatra'),('56160', 'Imatra'),
('56170', 'Imatra'),('56180', 'Imatra'),('56190', 'Imatra'),
('56210', 'Imatra'),('56220', 'Imatra'),('56230', 'Imatra'),
('56240', 'Imatra'),('56250', 'Imatra'),('56260', 'Imatra'),
('56270', 'Imatra'),('56280', 'Imatra'),('56290', 'Imatra'),
('56310', 'Imatra'),('56320', 'Imatra'),('56330', 'Imatra'),
('56340', 'Imatra'),('56350', 'Imatra'),('56360', 'Imatra'),
('56370', 'Imatra'),('56380', 'Imatra'),('56390', 'Imatra'),
('64100', 'Kristiinankaupunki'),('64200', 'Närpiö'),('64300', 'Lapväärtti'),
('64400', 'Isojoki'),('64500', 'Kristiinankaupunki'),('64600', 'Karijoki'),
('64700', 'Teuva'),('64800', 'Teuva'),('64900', 'Teuva'),
('68100', 'Kokkola'),('68150', 'Kokkola'),('68200', 'Kokkola'),
('68210', 'Kokkola'),('68220', 'Kokkola'),('68230', 'Kokkola'),
('68240', 'Kokkola'),('68250', 'Kokkola'),('68260', 'Kokkola'),
('68270', 'Kokkola'),('68280', 'Kokkola'),('68290', 'Kokkola'),
('68300', 'Kokkola'),('68310', 'Kokkola'),('68320', 'Kokkola'),
('68330', 'Kokkola'),('68340', 'Kokkola'),('68350', 'Kokkola'),
('68360', 'Kokkola'),('68370', 'Kokkola'),('68380', 'Kokkola'),
('68390', 'Kokkola'),('68410', 'Kokkola'),('68420', 'Kokkola'),
('68430', 'Kokkola'),('68440', 'Kokkola'),('68450', 'Kokkola'),
('68460', 'Kokkola'),('68470', 'Kokkola'),('68480', 'Kokkola'),
('68490', 'Kokkola'),('68510', 'Kokkola'),('68520', 'Kokkola'),
('68530', 'Kokkola'),('68540', 'Kokkola'),('68550', 'Kokkola'),
('68560', 'Kokkola'),('68570', 'Kokkola'),('68580', 'Kokkola'),
('68590', 'Kokkola'),('68600', 'Pietarsaari'),('68620', 'Pietarsaari'),
('68630', 'Pietarsaari'),('68640', 'Pietarsaari'),('68650', 'Pietarsaari'),
('68660', 'Pietarsaari'),('68670', 'Pietarsaari'),('68680', 'Pietarsaari'),
('68690', 'Pietarsaari'),('68700', 'Pietarsaari'),('68710', 'Pietarsaari'),
('68720', 'Pietarsaari'),('68730', 'Pietarsaari'),('68740', 'Pietarsaari'),
('68750', 'Pietarsaari'),('68760', 'Pietarsaari'),('68770', 'Pietarsaari'),
('68780', 'Pietarsaari'),('68790', 'Pietarsaari'),('68800', 'Pietarsaari'),
('68810', 'Pietarsaari'),('68820', 'Pietarsaari'),('68830', 'Pietarsaari'),
('68840', 'Pietarsaari'),('68850', 'Pietarsaari'),('68860', 'Pietarsaari'),
('68870', 'Pietarsaari'),('68880', 'Pietarsaari'),('68890', 'Pietarsaari'),
('69100', 'Kannus'),('69150', 'Kannus'),('69200', 'Kannus'),
('69210', 'Kannus'),('69220', 'Kannus'),('69230', 'Kannus'),
('69240', 'Kannus'),('69250', 'Kannus'),('69260', 'Kannus'),
('69270', 'Kannus'),('69280', 'Kannus'),('69290', 'Kannus'),
('69300', 'Toholampi'),('69310', 'Toholampi'),('69320', 'Toholampi'),
('69330', 'Toholampi'),('69340', 'Toholampi'),('69350', 'Toholampi'),
('69360', 'Toholampi'),('69370', 'Toholampi'),('69380', 'Toholampi'),
('69390', 'Toholampi'),('69400', 'Toholampi'),('69410', 'Toholampi'),
('69420', 'Toholampi'),('69430', 'Toholampi'),('69440', 'Toholampi'),
('69450', 'Toholampi'),('69460', 'Toholampi'),('69470', 'Toholampi'),
('69480', 'Toholampi'),('69490', 'Toholampi'),('69500', 'Veteli'),
('69510', 'Veteli'),('69520', 'Veteli'),('69530', 'Veteli'),
('69540', 'Veteli'),('69550', 'Veteli'),('69560', 'Veteli'),
('69570', 'Veteli'),('69580', 'Veteli'),('69590', 'Veteli'),
('69600', 'Kaustinen'),('69610', 'Kaustinen'),('69620', 'Kaustinen'),
('69630', 'Kaustinen'),('69640', 'Kaustinen'),('69650', 'Kaustinen'),
('69660', 'Kaustinen'),('69670', 'Kaustinen'),('69680', 'Kaustinen'),
('69690', 'Kaustinen'),('69700', 'Kaustinen'),('69710', 'Kaustinen'),
('69720', 'Kaustinen'),('69730', 'Kaustinen'),('69740', 'Kaustinen'),
('69750', 'Kaustinen'),('69760', 'Kaustinen'),('69770', 'Kaustinen'),
('69780', 'Kaustinen'),('69790', 'Kaustinen'),('69800', 'Haapajärvi'),
('69810', 'Haapajärvi'),('69820', 'Haapajärvi'),('69830', 'Haapajärvi'),
('69840', 'Haapajärvi'),('69850', 'Haapajärvi'),('69860', 'Haapajärvi'),
('69870', 'Haapajärvi'),('69880', 'Haapajärvi'),('69890', 'Haapajärvi'),
('69900', 'Reisjärvi'),('69910', 'Reisjärvi'),('69920', 'Reisjärvi'),
('69930', 'Reisjärvi'),('69940', 'Reisjärvi'),('69950', 'Reisjärvi'),
('69960', 'Reisjärvi'),('69970', 'Reisjärvi'),('69980', 'Reisjärvi'),
('69990', 'Reisjärvi');

-- -----------------------------------------------------
-- Table `vn`.`asiakas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`asiakas` ;

CREATE TABLE IF NOT EXISTS `vn`.`asiakas` (
  `asiakas_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `postinro` CHAR(5) NOT NULL,
  `etunimi` VARCHAR(20) NULL DEFAULT NULL,
  `sukunimi` VARCHAR(40) NULL DEFAULT NULL,
  `lahiosoite` VARCHAR(40) NULL DEFAULT NULL,
  `email` VARCHAR(50) NULL DEFAULT NULL,
  `puhelinnro` VARCHAR(15) NULL DEFAULT NULL,
  PRIMARY KEY (`asiakas_id`),
  INDEX `fk_as_posti1_idx` (`postinro` ASC) VISIBLE,
  INDEX `asiakas_snimi_idx` (`sukunimi` ASC) VISIBLE,
  INDEX `asiakas_enimi_idx` (`etunimi` ASC) VISIBLE,
  CONSTRAINT `fk_asiakas_posti`
    FOREIGN KEY (`postinro`)
    REFERENCES `vn`.`posti` (`postinro`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


-- -----------------------------------------------------
-- Table `vn`.`mokki`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`mokki` ;

CREATE TABLE IF NOT EXISTS `vn`.`mokki` (
  `mokki_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `alue_id` INT UNSIGNED NOT NULL,
  `postinro` CHAR(5) NOT NULL,
  `mokkinimi` VARCHAR(45) NULL DEFAULT NULL,
  `katuosoite` VARCHAR(45) NULL DEFAULT NULL,
  `hinta` DOUBLE(8,2) NOT NULL,
  `kuvaus` VARCHAR(150) NULL DEFAULT NULL,
  `henkilomaara` INT NULL DEFAULT NULL,
  `varustelu` VARCHAR(100) NULL DEFAULT NULL,
  PRIMARY KEY (`mokki_id`),
  INDEX `fk_mokki_alue_idx` (`alue_id` ASC) VISIBLE,
  INDEX `fk_mokki_posti_idx` (`postinro` ASC) VISIBLE,
  CONSTRAINT `fk_mokki_alue`
    FOREIGN KEY (`alue_id`)
    REFERENCES `vn`.`alue` (`alue_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_mokki_posti`
    FOREIGN KEY (`postinro`)
    REFERENCES `vn`.`posti` (`postinro`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


-- -----------------------------------------------------
-- Table `vn`.`varaus`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`varaus` ;

CREATE TABLE IF NOT EXISTS `vn`.`varaus` (
  `varaus_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `asiakas_id` INT UNSIGNED NOT NULL,
  `mokki_id` INT UNSIGNED NOT NULL,
  `varattu_pvm` DATETIME NULL DEFAULT NULL,
  `vahvistus_pvm` DATETIME NULL DEFAULT NULL,
  `varattu_alkupvm` DATETIME NULL DEFAULT NULL,
  `varattu_loppupvm` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`varaus_id`),
  INDEX `varaus_as_id_index` (`asiakas_id` ASC) VISIBLE,
  INDEX `fk_var_mok_idx` (`mokki_id` ASC) VISIBLE,
  CONSTRAINT `fk_varaus_mokki`
    FOREIGN KEY (`mokki_id`)
    REFERENCES `vn`.`mokki` (`mokki_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `varaus_ibfk`
    FOREIGN KEY (`asiakas_id`)
    REFERENCES `vn`.`asiakas` (`asiakas_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


-- -----------------------------------------------------
-- Table `vn`.`lasku`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`lasku` ;

CREATE TABLE IF NOT EXISTS `vn`.`lasku` (
  `lasku_id` INT NOT NULL,
  `varaus_id` INT UNSIGNED NOT NULL,
  `summa` DOUBLE(8,2) NOT NULL,
  `alv` DOUBLE(8,2) NOT NULL,
  `maksettu` TINYINT NOT NULL DEFAULT 0,
  PRIMARY KEY (`lasku_id`),
  INDEX `lasku_ibfk_1` (`varaus_id` ASC) VISIBLE,
  CONSTRAINT `lasku_ibfk_1`
    FOREIGN KEY (`varaus_id`)
    REFERENCES `vn`.`varaus` (`varaus_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


-- -----------------------------------------------------
-- Table `vn`.`palvelu`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`palvelu` ;

CREATE TABLE IF NOT EXISTS `vn`.`palvelu` (
  `palvelu_id` INT UNSIGNED NOT NULL,
  `alue_id` INT UNSIGNED NOT NULL,
  `nimi` VARCHAR(40) NULL DEFAULT NULL,
  `kuvaus` VARCHAR(255) NULL DEFAULT NULL,
  `hinta` DOUBLE(8,2) NOT NULL,
  `alv` DOUBLE(8,2) NOT NULL,
  PRIMARY KEY (`palvelu_id`),
  INDEX `Palvelu_nimi_index` (`nimi` ASC) VISIBLE,
  INDEX `palv_toimip_id_ind` (`alue_id` ASC) VISIBLE,
  CONSTRAINT `palvelu_ibfk_1`
    FOREIGN KEY (`alue_id`)
    REFERENCES `vn`.`alue` (`alue_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


-- -----------------------------------------------------
-- Table `vn`.`varauksen_palvelut`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `vn`.`varauksen_palvelut` ;

CREATE TABLE IF NOT EXISTS `vn`.`varauksen_palvelut` (
  `varaus_id` INT UNSIGNED NOT NULL,
  `palvelu_id` INT UNSIGNED NOT NULL,
  `lkm` INT NOT NULL,
  PRIMARY KEY (`palvelu_id`, `varaus_id`),
  INDEX `vp_varaus_id_index` (`varaus_id` ASC) VISIBLE,
  INDEX `vp_palvelu_id_index` (`palvelu_id` ASC) VISIBLE,
  CONSTRAINT `fk_palvelu`
    FOREIGN KEY (`palvelu_id`)
    REFERENCES `vn`.`palvelu` (`palvelu_id`),
  CONSTRAINT `fk_varaus`
    FOREIGN KEY (`varaus_id`)
    REFERENCES `vn`.`varaus` (`varaus_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_bin;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
