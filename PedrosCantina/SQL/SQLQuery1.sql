USE [altay_dk_db_DBMS]
DROP TABLE IF EXISTS VagtMedarbejder;
DROP TABLE IF EXISTS Vagt;
DROP TABLE IF EXISTS Månedsplan;
DROP TABLE IF EXISTS Medarbejder;

-- Oprettelse af tabeller 

CREATE TABLE Medarbejder 
(
MedarbejderID INT PRIMARY KEY IDENTITY(1,1),
Navn NVARCHAR(100) NOT NULL, 
Email NVARCHAR(100) NOT NULL, 
Telefon NVARCHAR(20), 
Rolle NVARCHAR(50) NOT NULL
); 

CREATE TABLE Månedsplan 
(
MånedsplanID INT PRIMARY KEY IDENTITY(1,1), 
År INT NOT NULL, 
Måned INT NOT NULL
); 

CREATE TABLE Vagt 
(
VagtID INT PRIMARY KEY IDENTITY(1,1), 
MånedsplanID INT NOT NULL, 
Dato DATE NOT NULL, 
StartTid TIME NOT NULL, 
SlutTid TIME NOT NULL, 

FOREIGN KEY (MånedsplanId)
REFERENCES Månedsplan(MånedsplanId)
);

CREATE TABLE VagtMedarbejder 
(
VagtId INT NOT NULL,
MedarbejderId INT NOT NULL, 

PRIMARY KEY (VagtId, MedarbejderId), 
FOREIGN KEY (VagtId)
REFERENCES Vagt(VagtId),

FOREIGN KEY (MedarbejderId)
REFERENCES Medarbejder(MedarbejderId)
);

-- Testdata til medarbejdere

INSERT INTO Medarbejder (Navn, Email, Telefon, Rolle)
VALUES
('Pedro', 'Pedro@Cantina.dk', '11223344', 'Leder'), 
('Jacoby', 'Jacoby@Cantina.dk', '20223344', 'Leder'),
('Anna', 'Anna@Cantina.dk', '20334455', 'Medarbejder'),
('Mikkel', 'Mikkel@Cantina.dk', '20445566', 'Medarbejder'),
('Sofie', 'Sofie@Cantina.dk', '20556677', 'Medarbejder'),
('Lucas', 'Lucas@Cantina.dk', '20667788', 'Medarbejder');

-- Oprettelse af månedsplan

INSERT INTO Månedsplan (År, Måned) 
VALUES (2026, 10); 

-- Oprettelse af vagter 

INSERT INTO Vagt (MånedsplanID, Dato, StartTid, SlutTid)
VALUES
(1, '2026-10-01', '09:00', '14:00'),
(1, '2026-10-01', '14:00', '19:00'),
(1, '2026-10-02', '09:00', '14:00'),
(1, '2026-10-02', '14:00', '19:00'),
(1, '2026-10-03', '09:00', '14:00'),
(1, '2026-10-03', '14:00', '19:00'),
(1, '2026-10-04', '09:00', '14:00'),
(1, '2026-10-04', '14:00', '19:00'),
(1, '2026-10-05', '09:00', '14:00'),
(1, '2026-10-05', '14:00', '19:00'),
(1, '2026-10-06', '09:00', '14:00'),
(1, '2026-10-06', '14:00', '19:00'),
(1, '2026-10-07', '09:00', '14:00'),
(1, '2026-10-07', '14:00', '19:00'),
(1, '2026-10-08', '09:00', '14:00'),
(1, '2026-10-08', '14:00', '19:00'),
(1, '2026-10-09', '09:00', '14:00'),
(1, '2026-10-09', '14:00', '19:00'),
(1, '2026-10-10', '09:00', '14:00'),
(1, '2026-10-10', '14:00', '19:00'),
(1, '2026-10-11', '09:00', '14:00'),
(1, '2026-10-11', '14:00', '19:00'),
(1, '2026-10-12', '09:00', '14:00'),
(1, '2026-10-12', '14:00', '19:00'),
(1, '2026-10-13', '09:00', '14:00'),
(1, '2026-10-13', '14:00', '19:00'),
(1, '2026-10-14', '09:00', '14:00'),
(1, '2026-10-14', '14:00', '19:00'),
(1, '2026-10-15', '09:00', '14:00'),
(1, '2026-10-15', '14:00', '19:00'),
(1, '2026-10-16', '09:00', '14:00'),
(1, '2026-10-16', '14:00', '19:00'),
(1, '2026-10-17', '09:00', '14:00'),
(1, '2026-10-17', '14:00', '19:00'),
(1, '2026-10-18', '09:00', '14:00'),
(1, '2026-10-18', '14:00', '19:00'),
(1, '2026-10-19', '09:00', '14:00'),
(1, '2026-10-19', '14:00', '19:00'),
(1, '2026-10-20', '09:00', '14:00'),
(1, '2026-10-20', '14:00', '19:00'),
(1, '2026-10-21', '09:00', '14:00'),
(1, '2026-10-21', '14:00', '19:00'),
(1, '2026-10-22', '09:00', '14:00'),
(1, '2026-10-22', '14:00', '19:00'),
(1, '2026-10-23', '09:00', '14:00'),
(1, '2026-10-23', '14:00', '19:00'),
(1, '2026-10-24', '09:00', '14:00'),
(1, '2026-10-24', '14:00', '19:00'),
(1, '2026-10-25', '09:00', '14:00'),
(1, '2026-10-25', '14:00', '19:00'),
(1, '2026-10-26', '09:00', '14:00'),
(1, '2026-10-26', '14:00', '19:00'),
(1, '2026-10-27', '09:00', '14:00'),
(1, '2026-10-27', '14:00', '19:00'),
(1, '2026-10-28', '09:00', '14:00'),
(1, '2026-10-28', '14:00', '19:00'),
(1, '2026-10-29', '09:00', '14:00'),
(1, '2026-10-29', '14:00', '19:00'),
(1, '2026-10-30', '09:00', '14:00'),
(1, '2026-10-30', '14:00', '19:00'),
(1, '2026-10-31', '09:00', '14:00'),
(1, '2026-10-31', '14:00', '19:00');

-- Tilmedling af medarbejdere til vagter

INSERT INTO VagtMedarbejder (VagtId, MedarbejderId)
VALUES
(1, 1),
(1, 3),
(1, 4),
(2, 2),
(2, 5),
(2, 6),

(3, 1),
(3, 3),
(3, 5),
(4, 2),
(4, 4),
(4, 6),

(5, 1),
(5, 3),
(5, 6),
(6, 2),
(6, 4),
(6, 5),

(7, 1),
(7, 4),
(7, 5),
(8, 2),
(8, 3),
(8, 6),

(9, 1),
(9, 4),
(9, 6),
(10, 2),
(10, 3),
(10, 5),

(11, 1),
(11, 5),
(11, 6),
(12, 2),
(12, 3),
(12, 4),

(13, 1),
(13, 3),
(13, 5),
(14, 2),
(14, 4),
(14, 6),

(15, 1),
(15, 3),
(15, 6),
(16, 2),
(16, 4),
(16, 5),

(17, 1),
(17, 4),
(17, 5),
(18, 2),
(18, 3),
(18, 6),

(19, 1),
(19, 4),
(19, 6),
(20, 2),
(20, 3),
(20, 5),

(21, 1),
(21, 5),
(21, 6),
(22, 2),
(22, 3),
(22, 4),

(23, 1),
(23, 3),
(23, 5),
(24, 2),
(24, 4),
(24, 6),

(25, 1),
(25, 3),
(25, 6),
(26, 2),
(26, 4),
(26, 5),

(27, 1),
(27, 4),
(27, 5),
(28, 2),
(28, 3),
(28, 6),

(29, 1),
(29, 4),
(29, 6),
(30, 2),
(30, 3),
(30, 5),

(31, 1),
(31, 5),
(31, 6),
(32, 2),
(32, 3),
(32, 4),

(33, 1),
(33, 3),
(33, 5),
(34, 2),
(34, 4),
(34, 6),

(35, 1),
(35, 3),
(35, 6),
(36, 2),
(36, 4),
(36, 5),

(37, 1),
(37, 4),
(37, 5),
(38, 2),
(38, 3),
(38, 6),

(39, 1),
(39, 4),
(39, 6),
(40, 2),
(40, 3),
(40, 5),

(41, 1),
(41, 5),
(41, 6),
(42, 2),
(42, 3),
(42, 4),

(43, 1),
(43, 3),
(43, 5),
(44, 2),
(44, 4),
(44, 6),

(45, 1),
(45, 3),
(45, 6),
(46, 2),
(46, 4),
(46, 5),

(47, 1),
(47, 4),
(47, 5),
(48, 2),
(48, 3),
(48, 6),

(49, 1),
(49, 4),
(49, 6),
(50, 2),
(50, 3),
(50, 5),

(51, 1),
(51, 5),
(51, 6),
(52, 2),
(52, 3),
(52, 4),

(53, 1),
(53, 3),
(53, 5),
(54, 2),
(54, 4),
(54, 6),

(55, 1),
(55, 3),
(55, 6),
(56, 2),
(56, 4),
(56, 5),

(57, 1),
(57, 4),
(57, 5),
(58, 2),
(58, 3),
(58, 6),

(59, 1),
(59, 4),
(59, 6),
(60, 2),
(60, 3),
(60, 5),

(61, 1),
(61, 5),
(61, 6),
(62, 2),
(62, 3),
(62, 4);

-- Vise månedsplan 

SELECT
Vagt.Dato,
Vagt.StartTid,
Vagt.SlutTid,
Medarbejder.Navn,
Medarbejder.Rolle
FROM Vagt
INNER JOIN VagtMedarbejder
ON Vagt.VagtID = VagtMedarbejder.VagtID
INNER JOIN Medarbejder 
ON VagtMedarbejder.MedarbejderId = Medarbejder.MedarbejderID
ORDER BY Vagt.Dato, Vagt.StartTid;

-- Vise belastning for en måned

SELECT 
Medarbejder.Navn, 
COUNT(Vagt.VagtID) AS AntalVagter
FROM Medarbejder
INNER JOIN VagtMedarbejder
ON Medarbejder.MedarbejderID = VagtMedarbejder.MedarbejderID
INNER JOIN Vagt
ON VagtMedarbejder.VagtID = Vagt.VagtID
INNER JOIN Månedsplan 
ON Vagt.MånedsplanID = Månedsplan.MånedsplanID
WHERE Månedsplan.År = 2026
AND Månedsplan.Måned = 10 
GROUP BY Medarbejder.Navn
ORDER BY AntalVagter DESC; 

-- Vise kontaktoplysninger

SELECT
Navn,
Email,
Telefon
FROM Medarbejder 
WHERE Navn <> 'Anna'; 

-- Vise belastning over året 

SELECT 
Medarbejder.Navn, 
COUNT(Vagt.VagtID) AS AntalVagter
FROM Medarbejder
INNER JOIN VagtMedarbejder
ON Medarbejder.MedarbejderID = VagtMedarbejder.MedarbejderID
INNER JOIN Vagt
ON VagtMedarbejder.VagtID = Vagt.VagtID
INNER JOIN Månedsplan 
ON Vagt.MånedsplanID = Månedsplan.MånedsplanID
WHERE Månedsplan.År = 2026
GROUP BY Medarbejder.Navn
ORDER BY AntalVagter DESC; 

-- Lave en ny månedsplan 

INSERT INTO Månedsplan (År, Måned)
VALUES (2026, 11); 

-- Ændre en vagt

UPDATE Vagt
SET StartTid = '10:00',
SlutTid = '15:00'
WHERE VagtID = 3;  

-- Vise den ændret vagt 

SELECT * FROM Vagt WHERE VagtID = 3; 

-- Ændre vagt tilbage 

UPDATE Vagt
SET StartTid = '09:00',
SlutTid = '14:00'
WHERE VagtID = 3; 
