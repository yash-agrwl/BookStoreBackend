CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_AddToWishList`(
	in b_id int,
    in u_id int
)
BEGIN
	Insert into wishlist ( UserID, BookID )
    values ( u_id, b_id);
END