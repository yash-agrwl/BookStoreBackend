CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateCart`(
	in b_id int,
    in u_id int,
    in count int
)
BEGIN
	Update cart
    set BookCount = count
    where BookID=b_id and UserID=u_id;
END