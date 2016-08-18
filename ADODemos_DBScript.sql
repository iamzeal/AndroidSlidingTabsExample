create table tblEmployeeDetails(
EmpNo int primary key,
Name varchar(50) not null,
Salary money check(salary between 1000 and 100000),
Designation varchar(50))


--Inserting values to Employee table
insert tblEmployeeDetails values
(101,'Smith',15000,'Clerk'),
(102,'Jones',25000,'Assistant'),
(103,'Clark',30000,'ASE'),
(104,'James',40000,'SE'),
(105,'Sonu',31000,'ASE')


--Procedure to retrieve unique designations from EmployeeDetails table
create procedure usp_GetJobs
as
begin
 select distinct Designation from tblEmployeeDetails
end


exec usp_GetJobs

--Procedure to retrive particular Employee Details
alter procedure usp_GetEmpInfo(@id int)
as
begin
   select * from tblEmployeeDetails where Empno=@id   
end
 
--execute the procedure
exec usp_GetEmpInfo 101



--Procedures to add Employee Details
create procedure usp_AddEmployee(@name varchar(50),@sal money,@job varchar(50),@empNo int out)
as
begin
  select @empNo=max(empno)+1 from tblEmployeeDetails
  insert tblEmployeeDetails values(@empNo, @name, @sal,@job)  
  if(@@error<>0)
   set @empNo=0
end


--execute
declare @no int
exec usp_AddEmployee 'Sneha',50000,'ITA',@no out
if(@no<>0)
 print 'Details stored successfully with ID:'
 print @no
go


--Procedures to Modify Employee Details
create procedure usp_ModifyEmployee(@id int,@name varchar(50),@sal money,@job varchar(50))
as
begin
  if exists(select 1 from tblEmployeeDetails where EmpNo=@id)
  begin
   update tblEmployeeDetails set Name=@name, Salary=@sal, Designation=@job where EmpNo=@id
   return 1
  end
  else
   return 0
end

--execute
declare @result int
exec @result=usp_ModifyEmployee 106,'Sneha',55000,'ITA'
if(@result<>0)
 print 'Details modified successfully' 
go

select * from tblEmployeeDetails


--Procedures to Remove Employee Details
create procedure usp_RemoveEmployee(@id int)
as
begin
if exists(select 1 from tblEmployeeDetails where EmpNo=@id)
  begin
   delete tblEmployeeDetails where EmpNo=@id
   return 1
  end
else
  return 0
end


--execute
declare @result int
exec @result=usp_RemoveEmployee 106
if(@result<>0)
 print 'Details removed successfully' 
go

select * from tblEmployeeDetails