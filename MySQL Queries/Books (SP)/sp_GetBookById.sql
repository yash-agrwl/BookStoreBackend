CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetBookById`(
	in b_id int
)
BEGIN
	Select * from books where BookID=b_id;
END