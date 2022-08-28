select * from staffDetails

-- getting the staff details
create proc spcheckuserpass 
(@username varchar(10))
as
select * from staffDetails where Username=@username  

-- getting all the doctor details
create proc spdocdetails
as
select * from doctorDetails

-- checking if patientid exists
create proc sqcheckpatient
(@patientid bigint)
as
select * from patientDetails where patientId=@patientid;

-- get spetializations
create proc spgetspecializations
as
select specialization, specializationid from doctorDetails

-- check appointment dates
create proc spcheckAppoint
(@appointDate datetime,
@specializationId int)
as
select * from appointments where visitDate=@appointDate and specializationId=@specializationId;


-- get doctor details for appointments
create proc spGetDocdetails
(@specializationId int)
as
select firstname, specialization, startTime, endTime from doctorDetails where specializationID=@specializationId

-- scheduling an appointment
create proc spScheduleAppointment
(
@appointmentid bigint,
@patientId bigint,
@specializationId int,
@doctor varchar(20),
@visitDate datetime,
@appointmentStartTime varchar(10),
@appointmentEndTime varchar(10))

as
insert into appointments values(@appointmentid, @patientId, @specializationId, @doctor, @visitDate, @appointmentStartTime, @appointmentEndTime);

exec spScheduleAppointment 2345676787, 3749024169, 24, 'daniel', '12/10/22', '12:00', '1:00';


-- getting patientname 
create proc spPatientname
(@patientid bigint)
as
select firstname +' '+lastname as name  from patientDetails where patientId=@patientid;


-- check if appointment detail is present
create proc spcheckAppointPresent
(@id bigint)
as
select * from appointments where appointmentid=@id;


-- delete appointments
create proc spDeleteAppointment
(@id bigint)
as
delete appointments where appointmentid=@id;

-- add new patient
create proc spAddPatient
(@patientId bigint, @firstname varchar(20), @lastname varchar(20), @sex varchar(10), @age int, @dateofbirth date)
as
insert into patientDetails values(@patientId, @firstname, @lastname, @sex, @age, @dateofbirth);


