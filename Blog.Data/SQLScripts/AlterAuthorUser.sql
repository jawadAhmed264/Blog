use blog


alter table author add Gender varchar(10)
alter table author add [Address] varchar(max)
alter table author add Country varchar(100)
alter table author add City varchar(100)

alter table [User] add Gender varchar(10)
alter table [User] add [Address] varchar(max)
alter table [User] add Country varchar(100)
alter table [User] add City varchar(100)
alter table [User] add ImageUrl varchar(max)
