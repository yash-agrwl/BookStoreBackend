CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteAddressById`(
	in add_id int,
    in u_id int
)
BEGIN
	Delete from addresses 
    where AddressID=add_id and UserID=u_id;
END