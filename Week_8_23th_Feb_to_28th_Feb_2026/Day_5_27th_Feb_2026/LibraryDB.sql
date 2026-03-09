CREATE DATABASE LibraryDB;
GO

USE LibraryDB;
GO

CREATE TABLE Authors (
    AuthorId INT PRIMARY KEY IDENTITY(1,1),
    AuthorName NVARCHAR(100) NOT NULL
);

CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200),
    AuthorId INT FOREIGN KEY REFERENCES Authors(AuthorId),
    PublishedYear INT
);

CREATE TABLE Members (
    MemberId INT PRIMARY KEY IDENTITY(1,1),
    MemberName NVARCHAR(100),
    Email NVARCHAR(100)
);

CREATE TABLE BorrowRecords (
    RecordId INT PRIMARY KEY IDENTITY(1,1),
    MemberId INT FOREIGN KEY REFERENCES Members(MemberId),
    BookId INT FOREIGN KEY REFERENCES Books(BookId),
    BorrowDate DATE,
    ReturnDate DATE
);

INSERT INTO Authors (AuthorName)
VALUES ('J.K. Rowling'), ('George Orwell'), ('Jane Austen');

INSERT INTO Books (Title, AuthorId, PublishedYear)
VALUES
('Harry Potter', 1, 1997),
('1984', 2, 1949),
('Pride and Prejudice', 3, 1813);

INSERT INTO Members (MemberName, Email)
VALUES
('Alice Johnson', 'alice@library.com'),
('Bob Smith', 'bob@library.com');

INSERT INTO BorrowRecords (MemberId, BookId, BorrowDate, ReturnDate)
VALUES
(1,1,'2026-01-01','2026-01-15'),
(2,2,'2026-02-01',NULL);


CREATE PROCEDURE sp_InsertBook
    @Title NVARCHAR(200),
    @AuthorId INT,
    @PublishedYear INT
AS
BEGIN
    INSERT INTO Books (Title, AuthorId, PublishedYear)
    VALUES (@Title, @AuthorId, @PublishedYear);
END
GO

CREATE PROCEDURE sp_GetAllBooks
AS
BEGIN
    SELECT b.BookId, b.Title, a.AuthorName, b.PublishedYear
    FROM Books b
    INNER JOIN Authors a ON b.AuthorId = a.AuthorId;
END
GO

CREATE PROCEDURE sp_GetBookById
    @BookId INT
AS
BEGIN
    SELECT * FROM Books WHERE BookId = @BookId;
END
GO

CREATE PROCEDURE sp_UpdateBook
    @BookId INT,
    @Title NVARCHAR(200),
    @AuthorId INT,
    @PublishedYear INT
AS
BEGIN
    UPDATE Books
    SET Title=@Title,
        AuthorId=@AuthorId,
        PublishedYear=@PublishedYear
    WHERE BookId=@BookId;
END
GO

CREATE PROCEDURE sp_DeleteBook
    @BookId INT
AS
BEGIN
    DELETE FROM Books WHERE BookId=@BookId;
END
GO


