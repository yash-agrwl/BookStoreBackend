CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAllFeedbacks`( in b_id int )
BEGIN
	select `FeedbackID`, feedbacks.`UserID`, users.`FullName`, `BookID`, `Rating`, `Comment`
    from feedbacks inner join users on feedbacks.UserID=users.UserID
    where BookID=b_id;
END