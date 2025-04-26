-- Monaco
WITH inserted_country AS (INSERT INTO Countries (Name, CountryCode, PhoneRegex, PhoneMask) VALUES ('--', '--', null, null) RETURNING Id AS CountryId )
INSERT INTO Cities (Name, CountryId)
VALUES
('--', (SELECT CountryId FROM inserted_country));
