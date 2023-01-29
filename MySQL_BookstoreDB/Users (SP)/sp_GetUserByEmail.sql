CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetUserByEmail`(
	in email varchar(255)
)
BEGIN
	select * from users where EmailID=email;
END