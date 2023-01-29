CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_AddToCart`(
	in b_id int,
    in u_id int
)
BEGIN
	Insert into cart ( UserID, BookID, BookCount )
    values ( u_id, b_id, 1 );
END