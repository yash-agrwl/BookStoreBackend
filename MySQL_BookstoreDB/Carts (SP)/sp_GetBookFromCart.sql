CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetBookFromCart`(
	in b_id int,
    in u_id int
)
BEGIN
	select * from cart where UserID=u_id and BookID=b_id;
END