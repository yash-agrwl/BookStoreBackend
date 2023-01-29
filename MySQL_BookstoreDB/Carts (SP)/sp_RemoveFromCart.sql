CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_RemoveFromCart`(
	in b_id int,
    in u_id int
)
BEGIN
	Delete from cart where UserID=u_id and BookID=b_id;
END