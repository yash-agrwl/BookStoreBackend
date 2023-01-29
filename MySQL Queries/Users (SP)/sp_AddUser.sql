CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_AddUser`(
	in fName varchar(255),
    in email varchar(255),
    in pwd varchar(50),
    in mobileNo varchar(15)
)
BEGIN
	Insert into users ( FullName, EmailID, Password, MobileNumber)
    Values ( fName, email, pwd, mobileNo );
END