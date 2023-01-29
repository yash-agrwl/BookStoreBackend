CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteBook`(
	in b_id int
)
BEGIN
	Delete from books where BookID=b_id;
END