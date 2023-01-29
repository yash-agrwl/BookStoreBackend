CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetBookFromWishList`(
	in b_id int,
    in u_id int
)
BEGIN
	select * from wishlist where UserID=u_id and BookID=b_id;
END