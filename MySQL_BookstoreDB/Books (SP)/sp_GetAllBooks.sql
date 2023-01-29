CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAllBooks`()
BEGIN
	Select * from books;
END