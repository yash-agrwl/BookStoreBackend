CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAdminByEmail`(
	in email varchar(255)
)
BEGIN
	select * from admins where EmailID=email;
END