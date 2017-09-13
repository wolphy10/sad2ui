CREATE DATABASE  IF NOT EXISTS `prototype_sad` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `prototype_sad`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: prototype_sad
-- ------------------------------------------------------
-- Server version	5.7.19-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `attendance`
--

DROP TABLE IF EXISTS `attendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attendance` (
  `attendanceID` int(11) NOT NULL AUTO_INCREMENT,
  `eventID` int(11) DEFAULT NULL,
  `caseID` int(11) DEFAULT NULL,
  `attendee` varchar(45) DEFAULT NULL,
  `role` varchar(45) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `eventDays` int(11) DEFAULT NULL,
  PRIMARY KEY (`attendanceID`),
  KEY `eventID_idx` (`eventID`),
  KEY `caseID_idx` (`caseID`),
  CONSTRAINT `caseID` FOREIGN KEY (`caseID`) REFERENCES `casestudyprofile` (`caseID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `eventID` FOREIGN KEY (`eventID`) REFERENCES `event` (`eventID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attendance`
--

LOCK TABLES `attendance` WRITE;
/*!40000 ALTER TABLE `attendance` DISABLE KEYS */;
/*!40000 ALTER TABLE `attendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `casestudyprofile`
--

DROP TABLE IF EXISTS `casestudyprofile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `casestudyprofile` (
  `caseID` int(11) NOT NULL AUTO_INCREMENT,
  `caseName` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`caseID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `casestudyprofile`
--

LOCK TABLES `casestudyprofile` WRITE;
/*!40000 ALTER TABLE `casestudyprofile` DISABLE KEYS */;
INSERT INTO `casestudyprofile` VALUES (1,'ok'),(2,'chick'),(3,'wally');
/*!40000 ALTER TABLE `casestudyprofile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event`
--

DROP TABLE IF EXISTS `event`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `event` (
  `eventID` int(11) NOT NULL AUTO_INCREMENT,
  `evName` varchar(200) DEFAULT NULL,
  `evDesc` varchar(250) DEFAULT NULL,
  `evVenue` varchar(150) DEFAULT NULL,
  `evType` varchar(45) DEFAULT NULL,
  `requestedBy` varchar(50) DEFAULT NULL,
  `status` varchar(45) DEFAULT NULL,
  `evProgress` varchar(45) DEFAULT NULL,
  `attendance` varchar(45) DEFAULT NULL,
  `budget` varchar(45) DEFAULT NULL,
  `reminder` varchar(45) DEFAULT NULL,
  `evDateFrom` varchar(45) DEFAULT NULL,
  `evTimeFrom` varchar(45) DEFAULT NULL,
  `evDateTo` varchar(45) DEFAULT NULL,
  `evTimeTo` varchar(45) DEFAULT NULL,
  `reminderDate` varchar(45) DEFAULT NULL,
  `reminderTime` varchar(45) DEFAULT NULL,
  `remindDateTo` varchar(45) DEFAULT NULL,
  `remindTimeTo` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`eventID`)
) ENGINE=InnoDB AUTO_INCREMENT=115 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event`
--

LOCK TABLES `event` WRITE;
/*!40000 ALTER TABLE `event` DISABLE KEYS */;
INSERT INTO `event` VALUES (110,'dddd','ddd','ddd','Mass','ddd','Canceled','Cancelled','False',NULL,NULL,'2017-09-07','12:00 AM','2017-09-07','12:00 AM',NULL,NULL,NULL,NULL),(111,'asdasd','asdasd','asdasd','Party','asdasd','Canceled','Cancelled','False',NULL,NULL,'2017-09-07','12:00 AM','2017-09-07','12:00 AM',NULL,NULL,NULL,NULL),(112,'asdasdasdasd','asdasd','asdasd','Party','dfsdf','Approved','Finished','False',NULL,NULL,'2017-09-05','12:00 AM','2017-09-05','12:00 AM',NULL,NULL,NULL,NULL),(113,'fhfgh','fghf','fghf','Party','fghfghfgh','Approved','Finished','False',NULL,NULL,'2017-09-06','12:00 AM','2017-09-06','12:00 AM',NULL,NULL,NULL,NULL),(114,'xzc','asdasd','asdasd','Party','asdasdasd','Pending','Finished','False',NULL,'True','2017-09-07','12:00 AM','2017-09-07','12:00 AM','09/07/2017','01:00 AM',NULL,NULL);
/*!40000 ALTER TABLE `event` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_accounts`
--

DROP TABLE IF EXISTS `tbl_accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_accounts` (
  `account_id` int(11) NOT NULL AUTO_INCREMENT,
  `fullname` varchar(45) DEFAULT NULL,
  `username` varchar(45) DEFAULT NULL,
  `password` varchar(45) DEFAULT NULL,
  `account_type` varchar(45) DEFAULT NULL,
  `account_status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`account_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_accounts`
--

LOCK TABLES `tbl_accounts` WRITE;
/*!40000 ALTER TABLE `tbl_accounts` DISABLE KEYS */;
INSERT INTO `tbl_accounts` VALUES (1,'Bahay Pasilungan Administration','admin','admin','Admin','Active'),(2,'Bahay Pasilungan Social Worker','social','social','Social Worker','Active');
/*!40000 ALTER TABLE `tbl_accounts` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-09-12 10:18:10
