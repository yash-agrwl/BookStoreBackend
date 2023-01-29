CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_AddFeedback`(
	in u_id int,
    in b_id int,
    in rtg int,
    in cmt varchar(255),
    out msg varchar(50)
)
BEGIN
	if(not exists(select * from feedbacks where BookID=b_id and UserID=u_id)) then
    
		insert into feedbacks ( UserID, BookID, Rating, Comment)
		values ( u_id, b_id, rtg, cmt);
		
		set @avg_rating := (select avg(Rating) from feedbacks where BookID=b_id);
		set @rtg_count := (select count(Rating) from feedbacks where BookID=b_id);
		
		update books set Rating=@avg_rating, RatingCount=@rtg_count where BookID=b_id;
        
	else
    
		set msg := "Feedback Already Exists";
        
	end if;
END