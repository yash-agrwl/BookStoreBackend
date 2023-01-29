CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateBook`(
	in bid int,
	in name varchar(255),
    in author varchar(255),
    in actual float,
    in discount float,
    in qty int,
    in rating float,
    in count int,
    in img varchar(255),
    in detail text
)
BEGIN
	Update books
    set BookName=name, Author=author, ActualPrice=actual, 
		DiscountPrice=discount, Quantity=qty, Rating=rating, 
		RatingCount=count, BookImage=img, BookDetail=detail
    where BookID=bid;
END