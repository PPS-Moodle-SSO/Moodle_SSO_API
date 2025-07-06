create database MoodleSSOTest;
go

use MoodleSSOTest;
go

CREATE TABLE Enterprise (
  idEnterprise int PRIMARY KEY IDENTITY(1, 1),
  nameEnterprise nvarchar(255) NOT NULL,
  idOrganizationDefault nvarchar(450) NULL,
  domain nvarchar(255) NOT NULL,
  moodleUrl nvarchar(255) NOT NULL,
  moodleApiKey nvarchar(255) NOT NULL
);
GO