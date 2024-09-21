

CREATE PROCEDURE sp_GetWhaleActivitiesByToken
    @TokenId INT
AS
BEGIN
    SELECT *
    FROM WhaleActivities
    WHERE TokenId = @TokenId
    ORDER BY Timestamp DESC
END
