use MoodleSSOTest;
go

insert into Enterprise (nameEnterprise,  idOrganizationDefault, domain, moodleUrl, moodleApiKey) values 
('Google', 1234, 'gmail', 'http://www.moodleUrl.com/webservice/rest/server.php', 'a24061e4b2a02c1ad96e534d87867275'), 
('Microsoft', 1235, 'hotmail', 'http://www.moodleUrl.com/webservice/rest/server.php', 'a24061e4b2a02c1ad96e534d87867276');
go