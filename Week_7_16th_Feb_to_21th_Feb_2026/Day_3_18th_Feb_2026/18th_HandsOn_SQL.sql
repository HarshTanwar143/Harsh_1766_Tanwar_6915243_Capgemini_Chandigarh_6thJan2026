CREATE DATABASE insurancedb;

USE insurancedb;

CREATE TABLE address_details(
    address_id INT PRIMARY KEY,
    h_no VARCHAR(6),
    city VARCHAR(50),
    addressline1 VARCHAR(50),
    state VARCHAR(50),
    pin VARCHAR(10)
);

CREATE TABLE user_details(
    user_id INT PRIMARY KEY,
    firstname VARCHAR(50),
    lastname VARCHAR(50),
    email VARCHAR(50),
    mobileno VARCHAR(15),
    address_id INT FOREIGN KEY REFERENCES address_details(address_id),
    dob DATE
);

CREATE TABLE ref_policy_types(
    policy_type_code VARCHAR(10) PRIMARY KEY,
    policy_type_name VARCHAR(50)
);

CREATE TABLE policy_sub_types(
    policy_type_id VARCHAR(10) PRIMARY KEY,
    policy_type_code VARCHAR(10) FOREIGN KEY REFERENCES ref_policy_types(policy_type_code),
    description VARCHAR(50),
    yearsofpayements INT,
    amount FLOAT,
    maturityperiod INT,
    maturityamount FLOAT,
    validity INT
);

CREATE TABLE user_policies(
    policy_no VARCHAR(20) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES user_details(user_id),
    date_registered DATE,
    policy_type_id VARCHAR(10) FOREIGN KEY REFERENCES policy_sub_types(policy_type_id)
);

CREATE TABLE policy_payments(
    receipno INT PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES user_details(user_id),
    policy_no VARCHAR(20) FOREIGN KEY REFERENCES user_policies(policy_no),
    dateofpayment DATE,
    amount FLOAT,
    fine FLOAT
);

INSERT INTO address_details VALUES
(1,'6-21','hyderabad','kphb','andhrapradesh','1254'),
(2,'7-81','chennai','seruseri','tamilnadu','16354'),
(3,'3-71','lucknow','street','uttarpradesh','86451'),
(4,'4-81','mumbai','iroli','maharashtra','51246'),
(5,'5-81','bangalore','mgroad','karnataka','125465'),
(6,'6-81','ahamadabad','street2','gujarat','125423'),
(7,'9-21','chennai','sholinganur','tamilnadu','654286');

INSERT INTO user_details VALUES
(1111,'raju','reddy','raju@gmail.com','9854261456',4,'1986-04-11'),
(2222,'vamsi','krishna','vamsi@gmail.com','9854261463',1,'1990-04-11'),
(3333,'naveen','reddy','naveen@gmail.com','9854261496',4,'1985-03-14'),
(4444,'raghava','rao','raghava@gmail.com','9854261412',4,'1985-09-21'),
(5555,'harsha','vardhan','harsha@gmail.com','9854261445',4,'1992-10-11');

INSERT INTO ref_policy_types VALUES
('58934','car'),
('58539','home'),
('58683','life');

INSERT INTO policy_sub_types VALUES
('6893','58934','theft',1,5000,NULL,200000,1),
('6894','58934','accident',1,20000,NULL,200000,3),
('6895','58539','fire',1,50000,NULL,500000,3),
('6896','58683','anandhlife',7,50000,15,1500000,NULL),
('6897','58683','sukhlife',10,5000,13,300000,NULL);

INSERT INTO user_policies VALUES
('689314',1111,'1994-04-18','6896'),
('689316',1111,'2012-05-18','6895'),
('689317',1111,'2012-06-20','6894'),
('689318',2222,'2012-06-21','6894'),
('689320',3333,'2012-06-18','6894'),
('689420',4444,'2012-04-09','6896');

INSERT INTO policy_payments VALUES
(121,4444,'689420','2012-04-09',50000,NULL),
(345,4444,'689420','2013-04-09',50000,NULL),
(300,1111,'689317','2012-06-20',20000,NULL),
(225,1111,'689316','2012-05-18',20000,NULL),
(227,1111,'689314','1994-04-18',50000,NULL),
(100,1111,'689314','1995-04-10',50000,NULL),
(128,1111,'689314','1996-04-11',50000,NULL),
(96,1111,'689314','1997-04-18',50000,200),
(101,1111,'689314','1998-04-09',50000,NULL),
(105,1111,'689314','1999-04-08',50000,NULL),
(120,1111,'689314','2000-04-05',50000,NULL),
(367,2222,'689318','2012-06-21',20000,NULL),
(298,3333,'689320','2012-06-18',20000,NULL);


-- Shows car policy subtype id, type name and description
SELECT pst.policy_type_id, rpt.policy_type_name, pst.description
FROM policy_sub_types pst
JOIN ref_policy_types rpt ON pst.policy_type_code = rpt.policy_type_code
WHERE rpt.policy_type_name = 'car';


-- Counts number of policies under each policy type code
SELECT policy_type_code, COUNT(*) AS NO_OF_POLICIES
FROM policy_sub_types
GROUP BY policy_type_code;


-- Shows users living in Chennai
SELECT u.user_id, firstname, lastname, email, mobileno
FROM user_details u
JOIN address_details a ON u.address_id = a.address_id
WHERE city = 'chennai'; 


-- Shows users who have taken car policies
SELECT DISTINCT u.user_id,
firstname + ' ' + lastname AS USER_NAME,
email, mobileno
FROM user_details u
JOIN user_policies up ON u.user_id = up.user_id
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
JOIN ref_policy_types rpt ON pst.policy_type_code = rpt.policy_type_code
WHERE rpt.policy_type_name = 'car';


-- Shows users who have car policy but not home policy
SELECT DISTINCT u.user_id, firstname, lastname
FROM user_details u
JOIN user_policies up ON u.user_id = up.user_id
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
WHERE pst.policy_type_code = '58934'
AND u.user_id NOT IN (
SELECT u.user_id
FROM user_details u
JOIN user_policies up ON u.user_id = up.user_id
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
WHERE pst.policy_type_code = '58539'
);


-- Shows policy type having maximum policies
SELECT TOP 1 rpt.policy_type_code, policy_type_name
FROM ref_policy_types rpt
JOIN policy_sub_types pst ON rpt.policy_type_code = pst.policy_type_code
GROUP BY rpt.policy_type_code, policy_type_name
ORDER BY COUNT(*) DESC;


-- Shows users whose city ends with 'bad'
SELECT u.user_id, firstname, lastname, city, state
FROM user_details u
JOIN address_details a ON u.address_id = a.address_id
WHERE city LIKE '%bad';


-- Shows users who registered before May 2012
SELECT u.user_id, firstname, lastname, policy_no, date_registered
FROM user_details u
JOIN user_policies up ON u.user_id = up.user_id
WHERE date_registered < '2012-05-01';


-- Shows users who have more than one policy
SELECT u.user_id, firstname, lastname
FROM user_details u
JOIN user_policies up ON u.user_id = up.user_id
GROUP BY u.user_id, firstname, lastname
HAVING COUNT(policy_no) > 1;


-- Shows policies maturing in August 2013
SELECT rpt.policy_type_code, policy_type_name, pst.policy_type_id, up.user_id, policy_no
FROM user_policies up
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
JOIN ref_policy_types rpt ON pst.policy_type_code = rpt.policy_type_code
WHERE DATEADD(year, maturityperiod, date_registered) 
BETWEEN '2013-08-01' AND '2013-08-31';


-- Shows policy where maturity amount is double of total paid amount
SELECT pst.policy_type_code, policy_type_name, pst.policy_type_id
FROM policy_sub_types pst
JOIN ref_policy_types rpt ON pst.policy_type_code = rpt.policy_type_code
JOIN user_policies up ON pst.policy_type_id = up.policy_type_id
JOIN policy_payments pp ON up.policy_no = pp.policy_no
GROUP BY pst.policy_type_code, policy_type_name, pst.policy_type_id, maturityamount
HAVING maturityamount = 2 * SUM(pp.amount);


-- Shows total amount paid by each user
SELECT user_id, SUM(amount) AS total_amount
FROM policy_payments
GROUP BY user_id;


-- Shows total paid amount per user per policy
SELECT user_id, policy_no, SUM(amount) AS total_amount
FROM policy_payments
GROUP BY user_id, policy_no;


-- Shows remaining balance amount per policy
SELECT up.user_id, up.policy_no,
pst.maturityamount - SUM(pp.amount) AS balance_amount
FROM user_policies up
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
JOIN policy_payments pp ON up.policy_no = pp.policy_no
GROUP BY up.user_id, up.policy_no, pst.maturityamount;


-- Shows remaining years for payment
SELECT up.user_id, up.policy_no,
pst.yearsofpayements - COUNT(pp.receipno) AS BALANCE_YEARS
FROM user_policies up
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
LEFT JOIN policy_payments pp ON up.policy_no = pp.policy_no
GROUP BY up.user_id, up.policy_no, pst.yearsofpayements;


-- Shows users having car, home and life policies
SELECT user_id, firstname, lastname
FROM user_details
WHERE user_id IN (
SELECT user_id
FROM user_policies up
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
GROUP BY user_id
HAVING COUNT(DISTINCT pst.policy_type_code) = 3
);

-- Shows total amount paid per policy department
SELECT pst.policy_type_code, SUM(pp.amount) AS total_amount
FROM policy_payments pp
JOIN user_policies up ON pp.policy_no = up.policy_no
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
GROUP BY pst.policy_type_code;


-- Shows users having multiple policy types under same code
SELECT user_id,
firstname + ' ' + lastname AS user_name,
pst.policy_type_code, pst.policy_type_id
FROM user_details u
JOIN user_policies up ON u.user_id = up.user_id
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
GROUP BY user_id, firstname, lastname, pst.policy_type_code, pst.policy_type_id
HAVING COUNT(*) > 1;


-- Shows policy department with minimum policies
SELECT TOP 1 pst.policy_type_code, policy_type_name
FROM ref_policy_types rpt
JOIN policy_sub_types pst ON rpt.policy_type_code = pst.policy_type_code
GROUP BY pst.policy_type_code, policy_type_name
ORDER BY COUNT(*) ASC;


-- Shows users who completed all payments
SELECT up.user_id,
firstname + ' ' + lastname AS user_name,
city, mobileno,
pst.policy_type_code, pst.policy_type_id, policy_type_name
FROM user_details u
JOIN address_details a ON u.address_id = a.address_id
JOIN user_policies up ON u.user_id = up.user_id
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
JOIN ref_policy_types rpt ON pst.policy_type_code = rpt.policy_type_code
WHERE pst.yearsofpayements = (
SELECT COUNT(*) FROM policy_payments pp
WHERE pp.policy_no = up.policy_no
);


-- Shows latest 2 registered policies
SELECT TOP 2
u.user_id,
firstname + ' ' + lastname AS user_name,
city, mobileno,
pst.policy_type_code, pst.policy_type_id, policy_type_name,
date_registered
FROM user_details u
JOIN address_details a ON u.address_id = a.address_id
JOIN user_policies up ON u.user_id = up.user_id
JOIN policy_sub_types pst ON up.policy_type_id = pst.policy_type_id
JOIN ref_policy_types rpt ON pst.policy_type_code = rpt.policy_type_code
ORDER BY date_registered DESC;
