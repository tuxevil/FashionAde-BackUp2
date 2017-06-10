/*
Script created by Quest Data Compare at 30/06/2010 13:07:20
This script must be executed on fashionade_qa
This script applies changes to fashionade_qa to make it the same as fashionade_dev
*/

INSERT INTO contentcategories (ContentCategoryId, Name, Description, Type)
VALUES (13, Recommendations, Recommendations, 2);

INSERT INTO contents (ContentId, ApprovedBy, AssignedTo, Body, CategoryId, CreatedOn, EditedBy, Keywords, LastContentPublishedId, PromotionalText, ScheduleFrom, ScheduleTo, Status, Title, Type)
VALUES (127, NULL, 85, '<p>Testing</p>', 13, '2010-06-28 11:55:10', 73, 'recommendation', null, 'Testing', NULL, NULL, 4, 'Recommendations', 2);

INSERT INTO `contentpublisheds` (ContentPublishedId, ApprovedBy, AssignedTo, Body, CategoryId, ContentId, CreatedOn, EditedBy, Keywords, PromotionalText, ScheduleFrom, ScheduleTo, Status, Title, Type)
VALUES (22, NULL, NULL, '<p>Testing</p>', 13, 127, '2010-06-28 12:49:33', 0, 'recommendation', 'Testing', NULL, NULL, 1, 'Recommendations', 2);

UPDATE fashionade_qa.`contents`
SET LastContentPublishedId = 22
WHERE ContentId = 127

