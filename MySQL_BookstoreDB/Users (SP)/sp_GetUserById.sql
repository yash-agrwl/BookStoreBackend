CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetUserById`(
	in uid int
)
BEGIN
	select * from users where UserID=uid;
END