CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_AddAddress`(
	in u_id int,
	in address varchar(255),
    in city varchar(50),
    in state varchar(50),
    in t_id int
)
BEGIN
	insert into addresses (UserID, Address, City, State, TypeID)
    values (u_id, address, city, state, t_id);
END