CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAllWishListItems`(
	in u_id int
)
BEGIN
	select * from wishlist where UserID=u_id;
END