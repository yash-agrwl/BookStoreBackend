CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_EditFeedback`(
	in u_id int,
	in f_id int,
    in rtg int,
    in cmt varchar(255),
    out msg varchar(50)
)
BEGIN
	if( exists(select * from feedbacks where FeedbackID=f_id and UserID=u_id) ) then
    
		update feedbacks set `Rating`=rtg, `Comment`=cmt where FeedbackID=f_id;
        
        set @b_id := (select BookID from feedbacks where FeedbackID=f_id);
        set @avg_rating := (select avg(Rating) from feedbacks where BookID=@b_id);
		set @rtg_count := (select count(Rating) from feedbacks where BookID=@b_id);
		
		update books set Rating=@avg_rating, RatingCount=@rtg_count where BookID=@b_id;
        
    else
    
		set msg := "Invalid Action";
        
    end if;
END