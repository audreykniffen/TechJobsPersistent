--Part 1
Id INT(64)
Name VARCHAR(255)
EmployerId VARCHAR(255)
--Part 2
SELECT Name
FROM employers
WHERE LOCATION = "St. Louis City"
--Part 3
SELECT * FROM Skills
LEFT JOIN JobSkills ON Skills.Id = JobSkills.SkillId
WHERE JobSkills.JobId IS NOT NULL
ORDER BY Name DESC;
