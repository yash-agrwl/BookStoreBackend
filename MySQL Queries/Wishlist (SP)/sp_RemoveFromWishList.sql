CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_RemoveFromWishList`(
	in b_id int,
    in u_id int
)
BEGIN
	Delete from wishlist where UserID=u_id and BookID=b_id;
END