CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAllOrders`( in u_id int )
BEGIN
	select * from orders where UserID=u_id order by OrderDate Desc;
END