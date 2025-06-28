# ubnhs-lms




-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 11, 2025 at 09:37 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ubnhs`
--

-- --------------------------------------------------------

--
-- Table structure for table `accessions`
--

CREATE TABLE `accessions` (
  `AccessionID` int(11) NOT NULL,
  `AcquisitionID` int(11) NOT NULL,
  `AccessionNumber` varchar(20) NOT NULL,
  `Status` enum('Available','Issued','Damaged','Lost') DEFAULT 'Available',
  `Condition` enum('As New','Used','Poor') DEFAULT NULL,
  `Remarks` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `accessions`
--

INSERT INTO `accessions` (`AccessionID`, `AcquisitionID`, `AccessionNumber`, `Status`, `Condition`, `Remarks`) VALUES
(1, 1, 'A-0001', 'Issued', 'As New', 'As New'),
(2, 1, 'A-0002', 'Lost', 'As New', 'As New'),
(3, 1, 'A-0007', 'Damaged', 'As New', 'As New'),
(4, 1, 'A-0004', 'Available', 'As New', 'As New'),
(5, 1, 'A-0006', 'Lost', 'As New', 'As New'),
(6, 2, 'A-0009', 'Lost', 'As New', 'used'),
(7, 2, 'A-0008', 'Available', 'As New', 'used');

--
-- Triggers `accessions`
--
DELIMITER $$
CREATE TRIGGER `before_insert_accessions` BEFORE INSERT ON `accessions` FOR EACH ROW BEGIN
    DECLARE nextAccession INT;
    
    SELECT COALESCE(MAX(CAST(SUBSTRING(AccessionNumber, 3) AS UNSIGNED)), 0) + 1
    INTO nextAccession
    FROM accessions;
    
    SET NEW.AccessionNumber = CONCAT('A-', LPAD(nextAccession, 4, '0'));
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `acquisitions`
--

CREATE TABLE `acquisitions` (
  `AcquisitionID` int(11) NOT NULL,
  `MethodName` varchar(255) NOT NULL,
  `BookID` int(11) NOT NULL,
  `DateAcquired` date NOT NULL,
  `Donor` varchar(255) DEFAULT NULL,
  `SupplierID` int(11) DEFAULT NULL,
  `Cost` decimal(10,2) DEFAULT NULL,
  `Quantity` int(11) NOT NULL,
  `PublisherID` int(11) DEFAULT NULL,
  `AuthorID` int(11) DEFAULT NULL,
  `TransactionNumber` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `acquisitions`
--

INSERT INTO `acquisitions` (`AcquisitionID`, `MethodName`, `BookID`, `DateAcquired`, `Donor`, `SupplierID`, `Cost`, `Quantity`, `PublisherID`, `AuthorID`, `TransactionNumber`) VALUES
(1, 'Purchased', 1, '2025-05-04', NULL, 12, 5000.00, 5, NULL, NULL, 'T-0001'),
(2, 'Donated', 3, '2025-05-04', 'Nezuko', NULL, 0.00, 3, NULL, NULL, 'T-0002');

--
-- Triggers `acquisitions`
--
DELIMITER $$
CREATE TRIGGER `before_insert_acquisitions` BEFORE INSERT ON `acquisitions` FOR EACH ROW BEGIN
    DECLARE next_id INT;

    IF NEW.TransactionNumber IS NULL THEN
        SELECT COALESCE(MAX(CAST(SUBSTRING(TransactionNumber, 3, 4) AS UNSIGNED)), 0) + 1
        INTO next_id
        FROM acquisitions;

        SET NEW.TransactionNumber = CONCAT('T-', LPAD(next_id, 4, '0'));
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `archived_accessions`
--

CREATE TABLE `archived_accessions` (
  `AccessionID` int(11) NOT NULL,
  `AcquisitionID` int(11) NOT NULL,
  `AccessionNumber` varchar(20) NOT NULL,
  `Status` enum('Available','Issued','Damaged','Lost') DEFAULT 'Available',
  `Condition` enum('As New','Used','Poor') DEFAULT NULL,
  `Remarks` text DEFAULT NULL,
  `TransactionNumber` varchar(50) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `Donor` varchar(100) DEFAULT NULL,
  `SupplierID` int(11) DEFAULT NULL,
  `SupplierName` varchar(100) DEFAULT NULL,
  `BookID` int(11) DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `ISBN` varchar(50) DEFAULT NULL,
  `Description` text DEFAULT NULL,
  `AuthorName` varchar(100) DEFAULT NULL,
  `GenreName` varchar(100) DEFAULT NULL,
  `PublisherName` varchar(100) DEFAULT NULL,
  `YearPublished` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `archived_circulations`
--

CREATE TABLE `archived_circulations` (
  `CirculationID` int(11) NOT NULL DEFAULT 0,
  `AccessionID` int(11) NOT NULL,
  `MemberID` int(11) NOT NULL,
  `IssueDate` date NOT NULL,
  `DueDate` date NOT NULL,
  `ReturnDate` date DEFAULT NULL,
  `Status` enum('Returned','Issued','Damaged','Lost') NOT NULL,
  `DaysLate` int(11) DEFAULT NULL,
  `PenaltyFee` decimal(10,2) DEFAULT NULL,
  `Remarks` varchar(100) DEFAULT NULL,
  `ISBN` varchar(20) DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `BorrowerID` varchar(50) DEFAULT NULL,
  `FullName` varchar(255) DEFAULT NULL,
  `AccessionNumber` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `authors`
--

CREATE TABLE `authors` (
  `AuthorID` int(11) NOT NULL,
  `AuthorName` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `authors`
--

INSERT INTO `authors` (`AuthorID`, `AuthorName`) VALUES
(1, 'Nicholas Sparkle'),
(2, 'Norms De Leon'),
(3, 'Dr. Jose Rizal');

-- --------------------------------------------------------

--
-- Table structure for table `books`
--

CREATE TABLE `books` (
  `BookID` int(11) NOT NULL,
  `ISBN` varchar(20) NOT NULL,
  `Description` text NOT NULL,
  `Title` varchar(255) NOT NULL,
  `AuthorID` int(11) DEFAULT NULL,
  `GenreID` int(11) DEFAULT NULL,
  `PublisherID` int(11) DEFAULT NULL,
  `YearPublished` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `books`
--

INSERT INTO `books` (`BookID`, `ISBN`, `Description`, `Title`, `AuthorID`, `GenreID`, `PublisherID`, `YearPublished`) VALUES
(1, '979-903-948-41-3', 'A romantic novel about a teenage boy and a girl with a terminal illness, based on the author\'s own experience.', 'A Walk To Remember', 1, 1, 14, 1992),
(2, '979-551-848-55-0', 'No available description.', 'Cost Accounting and Control', 2, 2, 15, 2019),
(3, '979-206-443-57-6', 'No available description.', 'Noli Me Tangerens', 3, 1, 14, 1891);

-- --------------------------------------------------------

--
-- Table structure for table `circulations`
--

CREATE TABLE `circulations` (
  `CirculationID` int(11) NOT NULL,
  `AccessionID` int(11) NOT NULL,
  `MemberID` int(11) NOT NULL,
  `IssueDate` date NOT NULL,
  `DueDate` date NOT NULL,
  `ReturnDate` date DEFAULT NULL,
  `Status` enum('Returned','Issued','Damaged','Lost') NOT NULL,
  `DaysLate` int(11) DEFAULT NULL,
  `PenaltyFee` decimal(10,2) DEFAULT NULL,
  `Remarks` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `circulations`
--

INSERT INTO `circulations` (`CirculationID`, `AccessionID`, `MemberID`, `IssueDate`, `DueDate`, `ReturnDate`, `Status`, `DaysLate`, `PenaltyFee`, `Remarks`) VALUES
(4, 4, 3, '2025-05-04', '2025-05-09', '2025-05-05', 'Returned', 0, 0.00, 'None'),
(7, 1, 4, '2025-05-05', '2025-05-08', NULL, 'Issued', NULL, NULL, NULL),
(8, 2, 5, '2025-05-05', '2025-05-10', '2025-05-05', 'Lost', 0, 1000.00, 'Paid'),
(9, 4, 1, '2025-05-05', '2025-05-10', '2025-05-05', 'Returned', 0, 0.00, 'None');

-- --------------------------------------------------------

--
-- Table structure for table `departments`
--

CREATE TABLE `departments` (
  `departmentID` int(11) NOT NULL,
  `departmentName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `departments`
--

INSERT INTO `departments` (`departmentID`, `departmentName`) VALUES
(2, 'JHS'),
(1, 'SHS');

-- --------------------------------------------------------

--
-- Table structure for table `genres`
--

CREATE TABLE `genres` (
  `GenreID` int(11) NOT NULL,
  `GenreName` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `genres`
--

INSERT INTO `genres` (`GenreID`, `GenreName`) VALUES
(1, 'Science Fiction'),
(2, 'Educational'),
(3, 'Historical Fiction');

-- --------------------------------------------------------

--
-- Table structure for table `gradelevels`
--

CREATE TABLE `gradelevels` (
  `GradeLevelID` int(11) NOT NULL,
  `StrandID` int(11) NOT NULL,
  `DepartmentID` int(11) NOT NULL,
  `GradeLevelName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `gradelevels`
--

INSERT INTO `gradelevels` (`GradeLevelID`, `StrandID`, `DepartmentID`, `GradeLevelName`) VALUES
(8, 3, 2, 'Grade 10'),
(3, 1, 1, 'Grade 11'),
(2, 2, 1, 'Grade 11'),
(9, 4, 1, 'Grade 11'),
(11, 5, 1, 'Grade 11'),
(4, 1, 1, 'Grade 12'),
(1, 2, 1, 'Grade 12'),
(10, 4, 1, 'Grade 12'),
(12, 5, 1, 'Grade 12'),
(5, 3, 2, 'Grade 7'),
(6, 3, 2, 'Grade 8'),
(7, 3, 2, 'Grade 9');

-- --------------------------------------------------------

--
-- Table structure for table `members`
--

CREATE TABLE `members` (
  `MemberID` int(11) NOT NULL,
  `BorrowerType` enum('Student','Faculty') NOT NULL,
  `BorrowerID` varchar(20) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT 'N/A',
  `Email` varchar(100) NOT NULL,
  `DepartmentID` int(11) NOT NULL,
  `StrandID` int(11) DEFAULT NULL,
  `GradeLevelID` int(11) DEFAULT NULL,
  `SectionID` int(11) DEFAULT NULL,
  `Status` enum('Present','Not Present') NOT NULL DEFAULT 'Present'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `members`
--

INSERT INTO `members` (`MemberID`, `BorrowerType`, `BorrowerID`, `LastName`, `FirstName`, `MiddleName`, `Email`, `DepartmentID`, `StrandID`, `GradeLevelID`, `SectionID`, `Status`) VALUES
(1, 'Faculty', '8746-09876-3', 'Bayot', 'Myra', '', 'myra@deped.gov.ph', 1, NULL, NULL, NULL, 'Not Present'),
(3, 'Faculty', '9873-09876-3', 'Bote', 'Ricky', '', 'bote@deped.gov.ph', 2, NULL, NULL, NULL, 'Not Present'),
(4, 'Student', '837362342345', 'Dalinog', 'Jessica', '', 'jess@gmail.com', 1, 2, 2, 1, 'Not Present'),
(5, 'Faculty', '1234-57621-0', 'Padilla', 'Jabes', '', 'jabes@deped.gov.ph', 2, NULL, NULL, NULL, 'Not Present'),
(6, 'Faculty', '4343-09893-4', 'fdsfdsf', 'dsfs', 'fsfs', 'fsf@gmail.com', 2, NULL, NULL, NULL, 'Present');

-- --------------------------------------------------------

--
-- Table structure for table `penaltysettings`
--

CREATE TABLE `penaltysettings` (
  `settingID` int(11) NOT NULL,
  `borrowerType` enum('Faculty','Student') NOT NULL,
  `borrowDuration` int(11) NOT NULL,
  `borrowLimit` int(11) NOT NULL,
  `damageCharge` decimal(10,2) NOT NULL,
  `overdueFee` decimal(10,2) NOT NULL,
  `lostItemFee` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `penaltysettings`
--

INSERT INTO `penaltysettings` (`settingID`, `borrowerType`, `borrowDuration`, `borrowLimit`, `damageCharge`, `overdueFee`, `lostItemFee`) VALUES
(1, 'Faculty', 5, 5, 500.00, 100.00, 1000.00),
(2, 'Student', 3, 3, 250.00, 50.00, 500.00);

-- --------------------------------------------------------

--
-- Table structure for table `publishers`
--

CREATE TABLE `publishers` (
  `PublisherID` int(11) NOT NULL,
  `PublisherName` varchar(100) NOT NULL,
  `Address` varchar(255) NOT NULL,
  `ContactNo` varchar(15) NOT NULL,
  `Email` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `publishers`
--

INSERT INTO `publishers` (`PublisherID`, `PublisherName`, `Address`, `ContactNo`, `Email`) VALUES
(12, 'Harper Perennial Modern Classics', '195 Broadway, New York, NY 10007, USA', '+1 (212) 207-70', 'info@harpercollins.com'),
(13, 'Rex Book Store Inc', 'Sampaloc, Manila, Philippines', '+63 (2) 8737-55', 'info@rex.com.ph'),
(14, 'ABC Publishing Inc.', 'Metro Manila, Philippines', '09562139876', 'info@abcpublishing.com'),
(15, 'GIC ENTERPRISES & CO INC', 'Claro M. Recto, Manila', '(02) 257-14-33', 'gicenterprises1959@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `sections`
--

CREATE TABLE `sections` (
  `SectionID` int(11) NOT NULL,
  `DepartmentID` int(11) NOT NULL,
  `StrandID` int(11) NOT NULL,
  `GradeLevelID` int(11) NOT NULL,
  `SectionName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sections`
--

INSERT INTO `sections` (`SectionID`, `DepartmentID`, `StrandID`, `GradeLevelID`, `SectionName`) VALUES
(1, 1, 2, 2, 'Rizal'),
(2, 1, 1, 3, 'Newton'),
(3, 1, 1, 4, 'Carbon'),
(4, 2, 3, 8, 'Apple'),
(5, 1, 1, 4, 'Tesla');

-- --------------------------------------------------------

--
-- Table structure for table `strands`
--

CREATE TABLE `strands` (
  `StrandID` int(11) NOT NULL,
  `DepartmentID` int(11) NOT NULL,
  `StrandName` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `strands`
--

INSERT INTO `strands` (`StrandID`, `DepartmentID`, `StrandName`) VALUES
(1, 1, 'STEM'),
(2, 1, 'HUMSS'),
(3, 2, 'N/A'),
(4, 1, 'ABM'),
(5, 1, 'GAS'),
(6, 1, 'TVL');

-- --------------------------------------------------------

--
-- Table structure for table `suppliers`
--

CREATE TABLE `suppliers` (
  `supplierID` int(11) NOT NULL,
  `supplierName` varchar(100) NOT NULL,
  `address` varchar(255) NOT NULL,
  `contactNo` varchar(20) NOT NULL,
  `email` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `suppliers`
--

INSERT INTO `suppliers` (`supplierID`, `supplierName`, `address`, `contactNo`, `email`) VALUES
(12, 'BookWorld Inc.', '482 Main Street, Springfield, IL 62704, USA', '000000000', 'contact@bookworld.com'),
(13, 'EduSupplies PH', 'Marikina City, Metro Manila, Philippines', '908987675', 'orders@edusupplies.ph'),
(14, 'ABC Supplies', 'Metro Manila, Philippines', '098765432', 'contact@abcsuppliers.com'),
(15, 'EFG SUPPLIES PH', 'Taguig City, Philippines', '999999999', 'efgsupplies@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `FullName` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `UserType` enum('Librarian','Assistant Librarian') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserID`, `FullName`, `Email`, `Username`, `Password`, `UserType`) VALUES
(3, 'Rachelle Fraga', 'librarian@gmil.com', 'librarian', 'Librarian_123', 'Librarian'),
(18, 'Dalinog Jess', 'jess@gmail.com', 'assistantjess', 'Jess_123', 'Assistant Librarian'),
(20, 'Jade Monge', 'jade@gmail.com', 'assistantjade', 'Jade_123', 'Assistant Librarian');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `accessions`
--
ALTER TABLE `accessions`
  ADD PRIMARY KEY (`AccessionID`),
  ADD UNIQUE KEY `AccessionNumber` (`AccessionNumber`),
  ADD KEY `AcquisitionID` (`AcquisitionID`);

--
-- Indexes for table `acquisitions`
--
ALTER TABLE `acquisitions`
  ADD PRIMARY KEY (`AcquisitionID`),
  ADD KEY `BookID` (`BookID`),
  ADD KEY `SupplierID` (`SupplierID`),
  ADD KEY `acquisitions_ibfk_3` (`PublisherID`),
  ADD KEY `acquisitions_ibfk_4` (`AuthorID`);

--
-- Indexes for table `archived_accessions`
--
ALTER TABLE `archived_accessions`
  ADD PRIMARY KEY (`AccessionID`),
  ADD UNIQUE KEY `AccessionNumber` (`AccessionNumber`),
  ADD KEY `AcquisitionID` (`AcquisitionID`);

--
-- Indexes for table `archived_circulations`
--
ALTER TABLE `archived_circulations`
  ADD PRIMARY KEY (`CirculationID`);

--
-- Indexes for table `authors`
--
ALTER TABLE `authors`
  ADD PRIMARY KEY (`AuthorID`);

--
-- Indexes for table `books`
--
ALTER TABLE `books`
  ADD PRIMARY KEY (`BookID`),
  ADD UNIQUE KEY `ISBN` (`ISBN`),
  ADD UNIQUE KEY `unique_isbn` (`ISBN`),
  ADD KEY `AuthorID` (`AuthorID`),
  ADD KEY `GenreID` (`GenreID`),
  ADD KEY `PublisherID` (`PublisherID`);

--
-- Indexes for table `circulations`
--
ALTER TABLE `circulations`
  ADD PRIMARY KEY (`CirculationID`),
  ADD KEY `AccessionID` (`AccessionID`),
  ADD KEY `MemberID` (`MemberID`);

--
-- Indexes for table `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`departmentID`),
  ADD UNIQUE KEY `departmentName` (`departmentName`);

--
-- Indexes for table `genres`
--
ALTER TABLE `genres`
  ADD PRIMARY KEY (`GenreID`);

--
-- Indexes for table `gradelevels`
--
ALTER TABLE `gradelevels`
  ADD PRIMARY KEY (`GradeLevelID`),
  ADD UNIQUE KEY `UC_GradeLevel` (`GradeLevelName`,`StrandID`,`DepartmentID`),
  ADD KEY `StrandID` (`StrandID`),
  ADD KEY `gradelevels_ibfk_2` (`DepartmentID`);

--
-- Indexes for table `members`
--
ALTER TABLE `members`
  ADD PRIMARY KEY (`MemberID`),
  ADD UNIQUE KEY `BorrowerID` (`BorrowerID`),
  ADD UNIQUE KEY `Email` (`Email`),
  ADD KEY `DepartmentID` (`DepartmentID`),
  ADD KEY `GradeLevelID` (`GradeLevelID`),
  ADD KEY `SectionID` (`SectionID`),
  ADD KEY `fk_members_strand` (`StrandID`);

--
-- Indexes for table `penaltysettings`
--
ALTER TABLE `penaltysettings`
  ADD PRIMARY KEY (`settingID`),
  ADD UNIQUE KEY `borrowerType` (`borrowerType`);

--
-- Indexes for table `publishers`
--
ALTER TABLE `publishers`
  ADD PRIMARY KEY (`PublisherID`);

--
-- Indexes for table `sections`
--
ALTER TABLE `sections`
  ADD PRIMARY KEY (`SectionID`),
  ADD KEY `StrandID` (`StrandID`),
  ADD KEY `GradeLevelID` (`GradeLevelID`),
  ADD KEY `DepartmentID` (`DepartmentID`);

--
-- Indexes for table `strands`
--
ALTER TABLE `strands`
  ADD PRIMARY KEY (`StrandID`),
  ADD KEY `DepartmentID` (`DepartmentID`);

--
-- Indexes for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`supplierID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`),
  ADD UNIQUE KEY `Email` (`Email`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `accessions`
--
ALTER TABLE `accessions`
  MODIFY `AccessionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `acquisitions`
--
ALTER TABLE `acquisitions`
  MODIFY `AcquisitionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `authors`
--
ALTER TABLE `authors`
  MODIFY `AuthorID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `books`
--
ALTER TABLE `books`
  MODIFY `BookID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `circulations`
--
ALTER TABLE `circulations`
  MODIFY `CirculationID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `departments`
--
ALTER TABLE `departments`
  MODIFY `departmentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `genres`
--
ALTER TABLE `genres`
  MODIFY `GenreID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `gradelevels`
--
ALTER TABLE `gradelevels`
  MODIFY `GradeLevelID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `members`
--
ALTER TABLE `members`
  MODIFY `MemberID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `penaltysettings`
--
ALTER TABLE `penaltysettings`
  MODIFY `settingID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `publishers`
--
ALTER TABLE `publishers`
  MODIFY `PublisherID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `sections`
--
ALTER TABLE `sections`
  MODIFY `SectionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `strands`
--
ALTER TABLE `strands`
  MODIFY `StrandID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `supplierID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `accessions`
--
ALTER TABLE `accessions`
  ADD CONSTRAINT `accessions_ibfk_1` FOREIGN KEY (`AcquisitionID`) REFERENCES `acquisitions` (`AcquisitionID`);

--
-- Constraints for table `acquisitions`
--
ALTER TABLE `acquisitions`
  ADD CONSTRAINT `acquisitions_ibfk_1` FOREIGN KEY (`BookID`) REFERENCES `books` (`BookID`),
  ADD CONSTRAINT `acquisitions_ibfk_2` FOREIGN KEY (`SupplierID`) REFERENCES `suppliers` (`supplierID`),
  ADD CONSTRAINT `acquisitions_ibfk_3` FOREIGN KEY (`PublisherID`) REFERENCES `publishers` (`PublisherID`),
  ADD CONSTRAINT `acquisitions_ibfk_4` FOREIGN KEY (`AuthorID`) REFERENCES `authors` (`AuthorID`);

--
-- Constraints for table `archived_accessions`
--
ALTER TABLE `archived_accessions`
  ADD CONSTRAINT `archived_accessions_ibfk_1` FOREIGN KEY (`AcquisitionID`) REFERENCES `acquisitions` (`AcquisitionID`);

--
-- Constraints for table `books`
--
ALTER TABLE `books`
  ADD CONSTRAINT `books_ibfk_1` FOREIGN KEY (`AuthorID`) REFERENCES `authors` (`AuthorID`),
  ADD CONSTRAINT `books_ibfk_2` FOREIGN KEY (`GenreID`) REFERENCES `genres` (`GenreID`),
  ADD CONSTRAINT `books_ibfk_3` FOREIGN KEY (`PublisherID`) REFERENCES `publishers` (`PublisherID`);

--
-- Constraints for table `circulations`
--
ALTER TABLE `circulations`
  ADD CONSTRAINT `circulations_ibfk_1` FOREIGN KEY (`AccessionID`) REFERENCES `accessions` (`AccessionID`),
  ADD CONSTRAINT `circulations_ibfk_2` FOREIGN KEY (`MemberID`) REFERENCES `members` (`MemberID`);

--
-- Constraints for table `gradelevels`
--
ALTER TABLE `gradelevels`
  ADD CONSTRAINT `gradelevels_ibfk_1` FOREIGN KEY (`StrandID`) REFERENCES `strands` (`StrandID`),
  ADD CONSTRAINT `gradelevels_ibfk_2` FOREIGN KEY (`DepartmentID`) REFERENCES `departments` (`departmentID`);

--
-- Constraints for table `members`
--
ALTER TABLE `members`
  ADD CONSTRAINT `fk_members_strand` FOREIGN KEY (`StrandID`) REFERENCES `strands` (`StrandID`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `members_ibfk_1` FOREIGN KEY (`DepartmentID`) REFERENCES `departments` (`departmentID`),
  ADD CONSTRAINT `members_ibfk_2` FOREIGN KEY (`GradeLevelID`) REFERENCES `gradelevels` (`GradeLevelID`),
  ADD CONSTRAINT `members_ibfk_3` FOREIGN KEY (`SectionID`) REFERENCES `sections` (`SectionID`);

--
-- Constraints for table `sections`
--
ALTER TABLE `sections`
  ADD CONSTRAINT `sections_ibfk_1` FOREIGN KEY (`StrandID`) REFERENCES `strands` (`StrandID`),
  ADD CONSTRAINT `sections_ibfk_2` FOREIGN KEY (`GradeLevelID`) REFERENCES `gradelevels` (`GradeLevelID`),
  ADD CONSTRAINT `sections_ibfk_3` FOREIGN KEY (`DepartmentID`) REFERENCES `departments` (`departmentID`);

--
-- Constraints for table `strands`
--
ALTER TABLE `strands`
  ADD CONSTRAINT `strands_ibfk_1` FOREIGN KEY (`DepartmentID`) REFERENCES `departments` (`departmentID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
