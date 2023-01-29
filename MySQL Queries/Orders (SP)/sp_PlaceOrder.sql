CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_PlaceOrder`(
	in c_id int,
    in u_id int,
    in add_id int,
    out msg text
)
BEGIN
	if ( exists(select * from cart where CartID=c_id and UserID=u_id) ) then
		set @b_id := (select BookID from cart where CartID=c_id);
        set @qty := (select BookCount from cart where CartID=c_id);
		set @stock := (select Quantity from books where BookID=@b_id);
        
		if ( @stock >= @qty ) then
			set @b_price := (select DiscountPrice from books where BookID=@b_id);
			set @t_price := @qty * @b_price;
			
			insert into orders ( UserID, BookID, AddressID, OrderQty, TotalPrice )
			values ( u_id, @b_id, add_id, @qty, @t_price );
			
			update books set Quantity = @stock - @qty where BookID=@b_id;
			
            delete from cart where CartID=c_id;
		else
			set msg := Concat("Stock Limit exceeded. Max quantity allowed is ", @stock);
		end if;
        
    else
		set msg := "Invalid Cart Item";
    end if;
END