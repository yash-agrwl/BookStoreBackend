CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_ResetPassword`(
	in email varchar(255),
    in pwd varchar(50)
)
BEGIN
	Update users
    set Password=pwd
    where EmailID=email;
END