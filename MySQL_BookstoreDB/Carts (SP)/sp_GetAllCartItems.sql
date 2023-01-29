CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAllCartItems`(
	in u_id int
)
BEGIN
	select * from cart where UserID=u_id;
END