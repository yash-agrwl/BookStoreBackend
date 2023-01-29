CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateAddress`(
	in add_id int,
    in u_id int,
	in address varchar(255),
    in city varchar(50),
    in state varchar(50)
)
BEGIN
	Update addresses
    set Address=address, City=city, State=state
    where AddressID=add_id and UserID=u_id;
END