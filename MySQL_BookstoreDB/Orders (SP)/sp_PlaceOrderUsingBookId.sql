CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PlaceOrderUsingBookId`(
	in b_id int,
    in u_id int,
    in add_id int,
    in qty int,
    out msg text
)
BEGIN
	if ( exists(select * from cart where BookID=b_id and UserID=u_id) ) then
		set @bookqty := (select Quantity from books where BookID=b_id);
		if ( @bookqty >= qty ) then
			set @b_price := (select DiscountPrice from books where BookID=b_id);
			
			set @t_price := qty * @b_price;
			
			insert into orders ( UserID, BookID, AddressID, OrderQty, TotalPrice )
			values ( u_id, b_id, add_id, qty, @t_price );
			
			update books set Quantity = @bookqty - qty where BookID=b_id;
			
            delete from cart where BookID=b_id and UserId=u_id;
		else
			set msg := Concat("Stock Limit exceeded. Max quantity allowed is ", @bookqty);
		end if;
    else
		set msg := "Book Not Available in Cart";
    end if;
END