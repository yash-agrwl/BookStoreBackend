create database db_bookstore;
use db_bookstore;

Create table Users (
	UserID int NOT NULL auto_increment,
    FullName varchar(255) NOT NULL,
    EmailID varchar(255) NOT NULL,
    Password varchar(50) NOT NULL,
    MobileNumber BIGINT,
    PRIMARY KEY (UserID)
);

alter table users
modify column MobileNumber varchar(15);

Create table Admins (
	AdminID int NOT NULL auto_increment,
    FullName varchar(255) NOT NULL,
    EmailID varchar(255) NOT NULL,
    Password varchar(255) NOT NULL,
    MobileNumber BIGINT,
    PRIMARY KEY (AdminID)
);

Insert into admins ( FullName, EmailID, Password, MobileNumber)
    Values ( "First Temp", "temp.firstmail@gmail.com", 
    "372402181415114218440251693956011841", 9466457323 );

Create table books (
	BookID int NOT NULL auto_increment,
    BookName varchar(255) NOT NULL,
    Author varchar(255) NOT NULL,
    ActualPrice float Not Null,
    DiscountPrice float Not NUll,
    Quantity int Not NUll,
    Rating float Not Null,
    RatingCount int Not Null,
    BookImage varchar(255) Not Null,
    BookDetail text NOT NULL,
    PRIMARY KEY (BookID)
);
    
Create table Cart (
	CartID int Not Null auto_increment,
    UserID int Not Null,
    BookID int Not Null,
    BookCount int Not Null,
    PRIMARY KEY (CartID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

Create table WishList (
	WishListID int Not Null auto_increment,
    UserID int Not Null,
    BookID int Not Null,
    PRIMARY KEY (WishListID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

create table AddressTypes (
	TypeID int Not Null auto_increment,
	Type varchar(20) Not Null,
    PRIMARY KEY (TypeID)
);

-- adding types --
insert into AddressTypes (Type) values('Home');
insert into AddressTypes (Type) values('Work');
insert into AddressTypes (Type) values('Other');

Create table Addresses(
	AddressID int Not Null auto_increment,
	UserID int not null,
	Address varchar(255) not null,
	City varchar(50) not null,
	State varchar(50) not null,
	TypeID int not null,
    PRIMARY KEY (AddressID),
    foreign key (TypeID) references AddressTypes(TypeID),
    foreign key (UserID) references Users(UserID)
);

create table Orders (
	OrderID int Not NUll auto_increment,
    UserID INT NOT NULL,
	BookID INT NOT NULL,
	AddressID int not null,
	OrderQty int not null,
	TotalPrice float not null,
	OrderDate Date DEFAULT (CURRENT_DATE),
    PRIMARY KEY (OrderID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (AddressID) REFERENCES Addresses(AddressID)
);

create table feedbacks (
	FeedbackID int Not Null auto_increment,
    UserID int Not Null,
    BookID int Not Null,
    Rating int Not Null,
    Comment varchar(255) default null,
    Primary Key (FeedbackID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);