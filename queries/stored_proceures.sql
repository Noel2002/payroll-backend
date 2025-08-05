CREATE OR REPLACE PROCEDURE GetMonthlyEarningsByMonth(
    userId UUID
)
LANGUAGE plpgsql
AS $$
BEGIN
    SELECT EXTRACT(MONTH FROM "EndTime") as "Month", SUM((EXTRACT(EPOCH FROM("EndTime" - "StartTime")/3600))*j."Rate") as "Earnings"
    FROM "Shifts" s JOIN "Jobs" j on j."Id"= s."JobId"
    WHERE s."UserId"=userId
    GROUP BY EXTRACT(MONTH FROM "EndTime");
END;
$$;

SELECT EXTRACT(MONTH FROM "EndTime") as "Month", SUM((EXTRACT(EPOCH FROM("EndTime" - "StartTime")/3600))*j."Rate") as "Earnings"
FROM "Shifts" s JOIN "Jobs" j on j."Id"= s."JobId"
WHERE s."UserId"='091ea355-3618-4c51-bd33-ad61e0213d6e'
GROUP BY EXTRACT(MONTH FROM "EndTime");