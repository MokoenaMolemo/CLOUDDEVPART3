USE master;
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'EventEaseDB') 
DROP DATABASE EventEaseDB;
CREATE DATABASE EventEaseDB;



USE EventEaseDB;


CREATE TABLE Venue (

 VenueID INT IDENTITY(1,1) PRIMARY KEY,
 VenueName NVARCHAR(255) NOT NULL, 
 Location NVARCHAR(255) NOT NULL,
 Capacity INT NOT NULL CHECK (Capacity > 0),
 ImageUrl NVARCHAR(500) NULL
 );

 CREATE TABLE EventType(
 EventTypeID INT IDENTITY(1,1) PRIMARY KEY,
 Name NVARCHAR(100) NOT NULL
 );

 CREATE TABLE Event (
 EventID INT IDENTITY (1,1) PRIMARY KEY,
 EventName NVARCHAR(255) NOT NULL,
 EventDate DATETIME NOT NULL,
 Description NVARCHAR(1000) NULL,
 VenueID INT NULL,
 EventTypeID INT NULL,
 FOREIGN KEY (VenueID) REFERENCES Venue(VenueID) ON DELETE SET NULL,
 FOREIGN KEY(EventTypeID) REFERENCES EventType(EventTypeID) ON DELETE SET NULL

 );

 CREATE TABLE Booking (
BookingID INT IDENTITY (1,1) PRIMARY KEY,
EventID INT NOT NULL,
VenueID INT NOT NULL,
BookingDate DATETIME DEFAULT GETDATE(),
FOREIGN KEY(EventID) REFERENCES Event(EventID) ON DELETE CASCADE,
FOREIGN KEY(VenueID) REFERENCES Venue(VenueID)  ON DELETE CASCADE,
CONSTRAINT UQ_Venue_Event UNIQUE (VenueID, EventID)

 );


 CREATE UNIQUE INDEX UQ_Venue_Booking ON Booking (VenueID, BookingDate);


 INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)
 VALUES
 ('Beko Conference Hall','123 Main Street', 350, 'https://www.jasso.go.jp/en/ryugaku/kyoten/tiec/plazaheisei/facility/__icsFiles/afieldfile/2021/03/24/ich_01_000_1.jpg'),
 ('Venue Nouveau', 'Zwavelpoort St Pretoria 0084', 120, 'https://costschedulerstorage.blob.core.windows.net/ovimages/ov-site/29/4I7A2942.jpg'),
 ('The Green Conference center', '545 Gordon Road', 500, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS2q4YCxGf6MGCEu5atyhdmsbryBMwTtLHriA&s'),
 (' Garden Venue', '308 Boundary Road, North Riding', 400, 'https://thegardenvenue.co.za/wp-content/uploads/2023/10/4-e1697006814142.jpg'),
 ('The O2 Arena', 'Peninsula Square, London', 2000, 'https://www.theo2.co.uk/assets/img/The-O2-Wintery-Entrance-29th-November-2021-by-Luke-Dyson-LD1_1685-819d862fe4.jpg');

 INSERT INTO EventType(Name)
 VALUES
 ('Conference'),
 ('Wedding'),
 ('Naming'),
 ('Birthday'),
 ('Concert');
 

 INSERT INTO Event(EventName, EventDate, Description, VenueID, EventTypeID)
 VALUES
 ('Tech Conference 2025','2025-05-15 09:00:00',' Annual conference on technology and inovations.',1,1),
 ('Wedding reception - Johnson', '2025-06-20 18:00:00', 'Wedding Celebration between Sarah and John Johnson',2,2),
 ('Business Seminar','2025-07-10 14:00:00', 'Business management and strategy',4,4),
 ('Music Concert','2025-08-23 19:00:00','Live music concert',4,4),
 ('Birthday Party','2025-09-12 15:00:00','Birthday party with entertainment',5,5);


 INSERT INTO Booking (EventID, VenueID, BookingDate)
 VALUES
 (1,1,'2025-05-01 10:00:00'),
 (2,2, '2025-06-01 12:00:00'),
 (3,3, '2025-07-01 13:00:00'),
 (4,4, '2025-08-01 14:00:00'),
 (5,5, '2025-09-01 11:00:00');



 SELECT * FROM EventType;