CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_AddBook`(
	in name varchar(255),
    in author varchar(255),
    in actual float,
    in discount float,
    in qty int,
    in img varchar(255),
    in detail text
)
BEGIN
	Insert into books ( BookName, Author, ActualPrice, DiscountPrice, Quantity, 
		Rating, RatingCount, BookImage, BookDetail )
    Values ( name, author, actual, discount, qty, 0, 0, img, detail);
END