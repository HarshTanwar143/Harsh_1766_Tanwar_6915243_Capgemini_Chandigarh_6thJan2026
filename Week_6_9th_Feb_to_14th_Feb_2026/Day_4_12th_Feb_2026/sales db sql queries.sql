
create database sales;
use sales;

create table salespeople(
snum int primary key,
sname varchar(50),
city varchar(50),
comm int 
);

create table cust(
cnum int primary key,
cname varchar(50) ,
city varchar(50),
rating int ,
snum int,
foreign key (snum) references salespeople(snum)
);

create table orders(
onum int primary key,
amt decimal,
odate date,
cnum int,
snum int,
foreign key (cnum) references cust(cnum),
foreign key (snum) references salespeople(snum) 
);

insert into salespeople values (1001,'Peel', 'London',12);
insert into salespeople values (1002,'Serres', 'San Jose',13);
insert into salespeople values (1004,'Motika', 'London',11);
insert into salespeople values (1007,'Rafkin', 'Barcelona',15);
insert into salespeople values (1003,'Axelrod', 'New York',1);

insert into cust values (2001,'Hoffman ', 'London',100,1001);
insert into cust values (2002,'Giovanne', 'Rome',200,1003);
insert into cust values (2003,'Liu', 'San Jose',300,1002);
insert into cust values (2004,'Grass', 'Brelin',100,1002);
insert into cust values (2006,'Clemens', 'London',300,1007);
insert into cust values (2007,'Pereira', 'Rome',100,1004);

--insert into orders values (3001,18.69,'1994-10-03',2008,1007);
insert into orders values (3003,767.19,'1994-10-03',2001,1001);
insert into orders values (3002,1900.10,'1994-10-03',2007,1004);
insert into orders values (3005,5160.45,'1994-10-03',2003,1002);
--insert into orders values (3006,1098.16,'1994-10-04',2008,1007);
insert into orders values (3009,1713.23,'1994-10-04',2002,1003);
insert into orders values (3007,75.75,'1994-10-05',2004,1002);
insert into orders values (3008,4723.00,'1994-10-05',2006,1001);
insert into orders values (3010,1309.95,'1994-10-06',2004,1002);
insert into orders values (3011,9891.88,'1994-10-06',2006,1001);

--1
select * from salespeople;
--2
select distinct snum from orders;
--3
select sname,comm from salespeople where city='London';
--4
select * from cust where rating=100;
--5
select onum,amt,odate from orders;
--6
select * from cust where city='san jose' and rating >200;
--7
select * from cust where city='san jose' or rating >200;
--8
select * from orders where amt>1000;
--9
select sname,city from salespeople where city='london' and comm>0.10;
--10
select * from cust where rating>100 or city ='rome';
--11
select * from salespeople where city in ('Barcelona','london');
--12
select * from salespeople where comm > 10 and comm < 12;
--13
select * from cust where city is Null;
--14
select * from orders where odate between '1994-10-03'and '1994-10-04';
--15
select * from cust where snum in (1001,1004);
--16
select * from cust where cname like 'a%' or cname like 'b%';
--17
select * from orders where amt is not null and amt>0;
--18
select count(distinct snum) as total_salesperson from orders;
--19
select snum, odate, MAX(amt) AS Largest_Order
from orders
group by snum, odate
order by snum, odate;

--20
select snum, max(amt) as largest_order
from orders
where amt>3000
group by snum ;