-- Clinic management system

create database ClinicManagement
use ClinicManagement

-- Table for front staffs details
create table staffDetails(
Username varchar(10) not null unique check(Username not like '%[^A-Z0-9]%'),
Firstname varchar(20),
Lastname varchar(20),
UserPassword varchar(10) check (UserPassword like '%[0-9]%' and UserPassword like '%[A-Z]%' and UserPassword like '%[@#$%a^&*()-_+=.,;:''"`~]%' 
and len(UserPassword) >= 10)
)



--insert into staffDetails values
--('Dustin20','Dustin','Henderson', 'dustbun@12'),
--('Mike2','Mike','Wilton', 'rosepet10@'),
--('steve25','Steve','Harington', 'nanynan@41'),

select * from staffDetails


-- Table for doctors details

create table doctorDetails(
doctorId bigint not null,
firstname varchar(20) check(firstname not like '%[^A-Z0-9]%'),
lastname varchar(20) check(lastname not like '%[^A-Z0-9]%'),
sex varchar(10),
specialization varchar(20),
specializationID int primary key,
startTime varchar(10),
endTime varchar(10))


insert into doctorDetails values
( FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'Daniel', 'Selvan', 'male', 'Orthopedics', 24, '12:00:00', '14:00:00'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'Joyana', 'Wilson', 'female', 'General',25, '9:00:00', '10:00:00'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'Jenifer', 'Andorson', 'female', 'Pediatrics',27, '7:00:00', '8:00:00'),
( FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'Willian', 'James', 'male', 'Ophthalmology',32, '10:00:00', '12:00:00'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'Richard', 'Mon', 'male', 'Internal Medicine',40, '16:00:00', '18:00:00'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'Joyana', 'Wilson', 'female', 'General',42, '9:00:00', '10:00:00')

-- Patients details
create table patientDetails(
patientId  bigint not null,
firstname varchar(20) check(firstname not like '%[^A-Z0-9]%'),
lastname varchar(20) check(lastname not like '%[^A-Z0-9]%'),
sex varchar(10),
age int check(age<=120 and age>=0),
dateofbirth date)

alter table patientDetails add primary key(patientID)

insert into patientDetails values 
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'viku', 'nara', 'male', 12, '12/05/2001'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'John', 'Wilton', 'male', 30, '08/07/1990'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'kumari', 'murthi', 'female', 20, '08/07/1995'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'geetha', 'yoga', 'female', 23, '12/01/1996'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'sankar', 'dina', 'male', 18, '10/04/2001'),
(FLOOR(RAND() * POWER(CAST(10 as BIGINT), 10)), 'kiran', 'sonalin', 'male', 15, '10/05/2007')

-- appointments table
create table appointments(
appointmentid bigint not null primary key,
patientId bigint foreign key references patientDetails(patientId),
specializationId int foreign key references doctordetails(specializationID),
doctor varchar(20),
visitDate datetime check(visitDate>getdate()) ,
appointmentTimeFrom varchar(10),
appointmentTimeTo varchar(10))

insert into appointments values(9906352627, 4587203929,
24,
'Daniel',
'12/05/2023',
'12:00:00',
'13:00:00'
)



