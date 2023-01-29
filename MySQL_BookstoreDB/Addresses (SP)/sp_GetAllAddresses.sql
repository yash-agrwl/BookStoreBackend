CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAllAddresses`(
	in u_id int 
 )
BEGIN
	select addresses.AddressID, addresses.UserID,
		addresses.Address, addresses.City, addresses.State, 
        addresstypes.Type
    from addresses inner join addresstypes
    on addresses.TypeID=addresstypes.TypeID
    where UserID=u_id;
END