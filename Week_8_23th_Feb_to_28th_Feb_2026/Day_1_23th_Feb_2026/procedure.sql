

create database harshdb
use harshdb

<-- add two number and print-->
create function sum()
returns int as
begin
declare @a int,@b int,@s int
set @A=10
set @B=20
set @s=@a+@B
return @s

end

select dbo.sum()


<--creating a function-->
Create Function NewDno() returns int as
		Begin
			Declare @NDno int 
			Set @NDno=(Select isnull(max(deptno),0)+10 from dept)

			Return @NDno
		End





Create Function Total(@A int,@B int, @C int) 
returns int as 
		Begin
			Return @A+@B+@C
		End

select dbo.Total(3,5,4)


Create procedure MySum(@A int,@B int, @C int=0,
@D int=0,@E int=0) As 
	Begin
		Print @A+@B+@C+@D+@E
	End

Execute MySum 10,20
	Execute MySum 10,20,30
	Execute MySUm 10,20,30,40
	Execute mySum 10,20,30,40,50

create function multiplys(@num1 int,@num2 int)
returns int
as
begin
declare @result int
set @result=@num1*@num2
return @result
end

select dbo.multiplys(3,4)

create procedure callingfunct(@firstnum1 int,
@secnum2 int)
as 
begin
declare @setval int
set @setval= dbo.multiplys(@firstnum1,@secnum2)
end

exec callingfunct
@firstnum1=3,@secnum2=4


use EmployeeDB

CREATE TABLE Employee
(
 EmpID int PRIMARY KEY,
 FirstName varchar(50) NULL,
 LastName varchar(50) NULL,
 Salary int NULL,
 Address varchar(100) NULL,
)
--Insert Data
Insert into Employee(EmpID,FirstName,LastName,Salary,Address) Values(1,'Mohan','Chauahn',22000,'Delhi');
Insert into Employee(EmpID,FirstName,LastName,Salary,Address) Values(2,'Asif','Khan',15000,'Delhi');
Insert into Employee(EmpID,FirstName,LastName,Salary,Address) Values(3,'Bhuvnesh','Shakya',19000,'Noida');
Insert into Employee(EmpID,FirstName,LastName,Salary,Address) Values(4,'Deepak','Kumar',19000,'Noida');
Insert into Employee(EmpID,FirstName,LastName,Salary,Address) Values(5,'Deepika','Kumari',25000,'Noida');

--See created table
Select * from Employee 

 --Create function to get emp full name 
Create function fnGetEmpFullName
(
 @FirstName varchar(50),
 @LastName varchar(50)
)
returns varchar(101)
As
Begin return (Select @FirstName + ' '+ @LastName);
end 

 --Calling the above created function
Select dbo.fnGetEmpFullName(FirstName,LastName) as Name, Salary from Employee 

<--Inline Table-Valued Function
<--User defined inline table-valued function returns a table variable
<-- as a result of actions perform by function. The value of table
<-- variable should be derived from a single SELECT statement.
-->
--Create function to get employees
Create function fnGetEmployee()
returns Table
As
 return (Select * from Employee) 

--Now call the above created function
Select * from fnGetEmployee() 
<--Multi-Statement Table-Valued Function
<--User defined multi-statement table-valued function returns a table
<-- variable as a result of actions perform by function. In this a table
<-- variable must be explicitly declared and defined whose value can be
<-- derived from a multiple sql statements.

--Create function for EmpID,FirstName and Salary of Employee
Create function fnGetMulEmployee2()
returns @Emp Table
(
EmpID int, 
FirstName varchar(50),
Salary int
)
As
begin

Insert into @Emp Select e.EmpID,e.FirstName,e.Salary from Employee e 
return
end
<---------------------------------
Create function fnGetMulEmployeeIU()
returns @Emp Table
(
EmpID int, 
FirstName varchar(50),
Salary int
)
As
begin

Insert into @Emp Select e.EmpID,e.FirstName,e.Salary from Employee e 
update @Emp set Salary=25000 where EmpID=1;
return
end
select * from employee
<---------------------------------------
Create function fnGetMulEmployeeU()
returns @Emp Table
(
EmpID int, 
FirstName varchar(50),
Salary int
)
As
begin

 update @Emp set Salary=25000 where EmpID=1;
return
end

<--Insert into @Emp Select e.EmpID,e.FirstName,e.Salary from Employee e -->

<--update @Emp set Salary=25000 where EmpID=1; 
--Now update salary of first employee
 update @Emp set Salary=25000 where EmpID=1;
--It will update only in @Emp table not in Original Employee table
return
end 
select * from employee
--Now call the above created function
select * from fnGetMulEmployeeIU()
Select * from fnGetMulEmployeeu() 
select * from fnGetMulEmployee2()
 --Now see the original table. This is not affected by above function update command
Select * from Employee 

create function multply(@num1 int,@num2 int)
returns int
Begin
Declare @result int
select @result=@num1*@num2
return @result
end

create proc callingfun(@FirstNum int,@secNum int)
as begin
declare @SetVal int
select dbo.multply(@FirstNum,@secNum)
end

declare @return_val int
exec @return_val=dbo.callingfun @FirstNum=3,@secNum=4



8888888888888888888888
Create procedure Swap(@X int output, @Y int output)
 As 
	Begin
		Declare @T int
		Set @T=@X
		Set @X=@Y
		Set @Y=@T
	End

drop procedure swap


Create procedure Swap(@X int output, @Y int output) As 
Begin
		Declare @A int,@B int
		Set @A=10
		Set @B=20
		Print 'Before Swap A = ' + cast(@A as varchar(3)) + ' B = ' +
		 cast(@B as varchar(3))
		Swap @A output,@B output
		Print 'After Swap A = ' + cast(@A as varchar(3)) + ' B = ' + 
		cast(@B as varchar(3))
	End

execute swap(20,10)
create function totalemp
return int as
Begin
select count(*) into total
from emptb
return total
end;





<--cursor-->
CREATE TABLE EmployeeCursor
	(
	 EmpID int PRIMARY KEY,
	 EmpName varchar (50) NOT NULL,
	 Salary int NOT NULL,
	 Address varchar (200) NOT NULL,
	)

	INSERT INTO EmployeeCursor(EmpID,EmpName,Salary,Address) VALUES(1,'Mohan',12000,'Noida')
	INSERT INTO EmployeeCursor(EmpID,EmpName,Salary,Address) VALUES(2,'Pavan',25000,'Delhi')
	INSERT INTO EmployeeCursor(EmpID,EmpName,Salary,Address) VALUES(3,'Amit',22000,'Dehradun')
	INSERT INTO EmployeeCursor(EmpID,EmpName,Salary,Address) VALUES(4,'Sonu',22000,'Noida')
	INSERT INTO EmployeeCursor(EmpID,EmpName,Salary,Address) VALUES(5,'Deepak',28000,'Gurgaon')

	SELECT * FROM Employee 

<--Static Cursor - Example
	 SET NOCOUNT ON
	DECLARE @Id int
	DECLARE @name varchar(50)
	DECLARE @salary int
	 DECLARE cur_emp CURSOR
	STATIC FOR 
	SELECT EmpID,EmpName,Salary from EmployeeCursor
	OPEN cur_emp
	IF @@CURSOR_ROWS > 0
	 BEGIN 
	 FETCH NEXT FROM cur_emp INTO @Id,@name,@salary
	 WHILE @@Fetch_status = 0
	 BEGIN
	 PRINT 'ID : '+ convert(varchar(20),@Id)+
', Name : '+@name+ ', Salary : '+convert(varchar(20),@salary)
	 FETCH NEXT FROM cur_emp INTO @Id,@name,@salary
	 END
	END
<--execute one by one-->>

	CLOSE cur_emp

	DEALLOCATE cur_emp
	SET NOCOUNT OFF 

--Dynamic Cursor - Example
 --Dynamic Cursor for Update
 SET NOCOUNT ON
	DECLARE @Id int
	DECLARE @name varchar(50)
	 DECLARE Dynamic_cur_empupdate CURSOR
	DYNAMIC 
	FOR 
	SELECT EmpID,EmpName from EmployeeCursor ORDER BY EmpName
	OPEN Dynamic_cur_empupdate
	IF @@CURSOR_ROWS > 0
	 BEGIN 
	 FETCH NEXT FROM Dynamic_cur_empupdate INTO @Id,@name
	 WHILE @@Fetch_status = 0
	 BEGIN
	 IF @name='Pavan'
	 Update EmployeeCursor SET Salary=15000 WHERE CURRENT OF
 Dynamic_cur_empupdate
	 FETCH NEXT FROM Dynamic_cur_empupdate INTO @Id,@name
	 END
	END
	CLOSE Dynamic_cur_empupdate
	DEALLOCATE Dynamic_cur_empupdate
	SET NOCOUNT OFF

	Select * from EmployeeCursor

drop proc Increment
Create procedure Increment as 
	Begin
		Declare @Eno int,@J varchar(30),@S decimal(7,2)
		Declare MyCur Cursor for select empno,job,sal from emp
		Open MyCur
		Fetch next from mycur into @Eno,@J,@S
		While @@Fetch_Status=0 
		Begin
			If @J='President'
				Set @S=@S+5000
			Else if @J='Manager'
				Set @S=@S+3000
			Else if @J='Analyst'
				Set @S=@S+2000
			Else if @J='Salesman'
				Set @S=@S+1500
			Else
				Set @S=@S+700
			Update Emp set Sal=@S where Empno=@Eno
			Fetch Next From MyCUr into @Eno,@J,@S
		End
		Close Mycur
		Deallocate Mycur
	End


--- Trigger---
use kk
CREATE TABLE Employee_Demo
(
 Emp_ID int identity,
 Emp_Name varchar(55),
 Emp_Sal decimal (10,2)
)

create table Employee_Demo_Audit
(
 Emp_ID int,
 Emp_Name varchar(55),
 Emp_Sal decimal(10,2),
 Audit_Action varchar(100),
 Audit_Timestamp datetime
) 
drop trigger trgInsteadOfInsert
--inserting data--
Insert into Employee_Demo values ('Amit',1000);
Insert into Employee_Demo values ('Mohan',1200);
Insert into Employee_Demo values ('Avin',1100);
Insert into Employee_Demo values ('Manoj',1300);
Insert into Employee_Demo values ('Riyaz',1400);
Insert into Employee_Demo values ('Miyaz',1100);


select * from Employee_Demo
select * from Employee_Demo_Audit
 -- Create trigger on table Employee_Demo for Insert statement
drop trigger trgAfterInsert

CREATE TRIGGER trgAfterInsert on Employee_Demo
FOR INSERT
AS declare @empid int, @empname varchar(55), @empsal decimal(10,2), @audit_action varchar(100);
select @empid=i.Emp_ID from inserted i;
select @empname=i.Emp_Name from inserted i;
select @empsal=i.Emp_Sal from inserted i;
set @audit_action='Inserted Record -- After Insert Trigger.'; insert into Employee_Demo_Audit(Emp_ID,Emp_Name,Emp_Sal,Audit_Action,Audit_Timestamp)
values (@empid,@empname,@empsal,@audit_action,getdate());
PRINT 'AFTER INSERT trigger fired.'

---insert one record tigger will fire
insert into Employee_Demo(Emp_Name,Emp_Sal)
values ('krishna',1000);

select * from Employee_Demo
select * from Employee_Demo_Audit

--After Update Trigger

 -- Create trigger on table Employee_Demo for Update statement
drop trigger trgAfterUpdate
CREATE TRIGGER trgAfterUpdates ON dbo.Employee_Demo
FOR UPDATE
AS
declare @empid int, @empname varchar(55), @empsal decimal(10,2), @audit_action varchar(100);
select @empid=i.Emp_ID from inserted i; 
select @empname=i.Emp_Name from inserted i;
select @empsal=i.Emp_Sal from inserted i; if update(Emp_Name)
 set @audit_action='Update Record --- After Update Trigger.';
if update (Emp_Sal)
 set @audit_action='Update Record --- After Update Trigger.';
insert into Employee_Demo_Audit(Emp_ID,Emp_Name,Emp_Sal,Audit_Action,Audit_Timestamp)
values (@empid,@empname,@empsal,@audit_action,getdate());
PRINT 'AFTER UPDATE trigger fired.'

--Now try to upadte data in Employee_Demo table
update Employee_Demo set Emp_Name='krishna' Where Emp_ID =2;

select * from Employee_Demo
select * from Employee_Demo_Audit

--After Delete Trigger

 -- Create trigger on table Employee_Demo for Delete statement
CREATE TRIGGER trgAfterDelete ON dbo.Employee_Demo
FOR DELETE
AS
declare @empid int, @empname varchar(55), @empsal decimal(10,2), @audit_action varchar(100); select @empid=d.Emp_ID FROM deleted d;
select @empname=d.Emp_Name from deleted d;
select @empsal=d.Emp_Sal from deleted d;
select @audit_action='Deleted -- After Delete Trigger.';
insert into Employee_Demo_Audit (Emp_ID,Emp_Name,Emp_Sal,Audit_Action,Audit_Timestamp)
values (@empid,@empname,@empsal,@audit_action,getdate());
PRINT 'AFTER DELETE TRIGGER fired.'

 --Now try to delete data in Employee_Demo table
DELETE FROM Employee_Demo where emp_id = 3

select * from Employee_Demo
select * from Employee_Demo_Audit

--Instead of Insert Trigger

 -- Create trigger on table Employee_Demo for Insert statement
CREATE TRIGGER trgInsteadOfInsert ON dbo.Employee_Demo
INSTEAD OF Insert
AS
declare @emp_id int, @emp_name varchar(55), @emp_sal decimal(10,2), @audit_action varchar(100);
select @emp_id=i.Emp_ID from inserted i;
select @emp_name=i.Emp_Name from inserted i;
select @emp_sal=i.Emp_Sal from inserted i;
SET @audit_action='Inserted Record -- Instead Of Insert Trigger.';
BEGIN 
 BEGIN TRAN
 SET NOCOUNT ON
 if(@emp_sal>=1000)
 begin
 RAISERROR('Cannot Insert where salary < 1000',16,1); ROLLBACK; end
 else begin Insert into Employee_Demo (Emp_Name,Emp_Sal) values (@emp_name,@emp_sal); Insert into Employee_Demo_Audit(Emp_ID,Emp_Name,Emp_Sal,Audit_Action,Audit_Timestamp) values(@@identity,@emp_name,@emp_sal,@audit_action,getdate());
 COMMIT;
 PRINT 'Record Inserted -- Instead Of Insert Trigger.'
END
END

--Now try to insert data in Employee_Demo table
insert into Employee_Demo values ('Shailu',1300)
insert into Employee_Demo values ('Shailu',900) 
-- It will raise error since we are checking salary >=1000

 --now select data from both the tables to see trigger action
select * from Employee_Demo
select * from Employee_Demo_Audit

--Instead of Update Trigger

 -- Create trigger on table Employee_Demo for Update statement
CREATE TRIGGER trgInsteadOfUpdate ON dbo.Employee_Demo
INSTEAD OF Update
AS
declare @emp_id int, @emp_name varchar(55), @emp_sal decimal(10,2), @audit_action varchar(100);
select @emp_id=i.Emp_ID from inserted i;
select @emp_name=i.Emp_Name from inserted i;
select @emp_sal=i.Emp_Sal from inserted i;
BEGIN
 BEGIN TRAN
if(@emp_sal>=1000)
 begin
 RAISERROR('Cannot Insert where salary < 1000',16,1); ROLLBACK; end
 else begin 
 insert into Employee_Demo_Audit(Emp_ID,Emp_Name,Emp_Sal,Audit_Action,Audit_Timestamp) values(@emp_id,@emp_name,@emp_sal,@audit_action,getdate());
 COMMIT;
 PRINT 'Record Updated -- Instead Of Update Trigger.'; END
END

 --Now try to upadte data in Employee_Demo table
update Employee_Demo set Emp_Sal = '1400' where emp_id = 6
update Employee_Demo set Emp_Sal = '900' where emp_id = 6

 --now select data from both the tables to see trigger action
select * from Employee_Demo
select * from Employee_Demo_Audit

--Instead of Delete Trigger

 -- Create trigger on table Employee_Demo for Delete statement
drop trigger trgAfterDelete
CREATE TRIGGER trgAfterDelete ON dbo.Employee_Demo
INSTEAD OF DELETE
AS
declare @empid int, @empname varchar(55), @empsal decimal(10,2), @audit_action varchar(100); select @empid=d.Emp_ID FROM deleted d;
select @empname=d.Emp_Name from deleted d;
select @empsal=d.Emp_Sal from deleted d;
BEGIN TRAN if(@empsal>1200) begin
 RAISERROR('Cannot delete where salary > 1200',16,1);
 ROLLBACK;
 end
 else begin
 delete from Employee_Demo where Emp_ID=@empid;
 COMMIT;
 insert into Employee_Demo_Audit(Emp_ID,Emp_Name,Emp_Sal,Audit_Action,Audit_Timestamp)
 values(@empid,@empname,@empsal,'Deleted -- Instead Of Delete Trigger.',getdate());
 PRINT 'Record Deleted -- Instead Of Delete Trigger.'  
END

 --Now try to delete data in Employee_Demo table
DELETE FROM Employee_Demo where emp_id = 1
DELETE FROM Employee_Demo where emp_id = 3

 --now select data from both the tables to see trigger action
select * from Employee_Demo
select * from Employee_Demo_Audit




