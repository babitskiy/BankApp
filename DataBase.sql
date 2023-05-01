create database MobileBank

use MobileBank

create table client (
	id_client int identity(1,1) not null primary key,
	client_last_name nvarchar(50) not null,
	client_first_name nvarchar(50) not null,
	client_middle_name nvarchar(50) not null,
	client_gender nvarchar(3) not null,
	client_password nvarchar(256) not null,
	client_email nvarchar(50) not null,
	client_phone_number nchar(13) not null,
)

create table bank_card(
	id_bank_card int identity(1,1) not null primary key,
	bank_card_type nvarchar(50) not null,
	bank_card_number nvarchar(16) not null,
	bank_card_cvv_code nvarchar(3) not null,
	bank_card_balance money default 0,
	bank_card_currnecy nvarchar(10) not null,
	bank_card_paymentSystem nvarchar(50) not null,
	bank_card_date date not null,
	bank_card_pin int not null
)

alter table bank_card add id_client int
alter table bank_card add foreign key (id_client) references dbo.Client(id_client) on delete no action on update cascade

create table transactions(
	id_transaction int identity(1,1) not null primary key,
	transaction_type varchar(50) not null,
	transaction_destination varchar(200) not null,
	transaction_date date not null,
	transaction_number nchar(50),
	transaction_value money
)

alter table transactions add id_bank_card int
alter table transactions add foreign key (id_bank_card) references dbo.bank_card(id_bank_card) on delete no action on update cascade

create table credits(
	id_credit int identity(1,1) not null primary key,
	credit_total_sum money not null,
	credit_sum money not null,
	credit_date date not null,
	credit_status bit not null default 0,
	repayment_date date,
	repayment_sum money
)

alter table credits add id_bank_card int
alter table credits add foreign key (id_bank_card) references dbo.bank_card(id_bank_card) on delete no action on update cascade

create table clientServices (
	id_service int identity(1,1) not null primary key,
	serviceName varchar(100) not null,
	serviceBalance money default 0,
	serviceType varchar(100) not null
)

create table clientPersonalAccount (
	id_personal_account int identity(1,1) not null primary key,
	personal_account nvarchar(10) not null
)

alter table clientPersonalAccount add id_service int
alter table clientPersonalAccount add foreign key (id_service) references dbo.clientServices(id_service) on delete no action on update cascade

alter table clientPersonalAccount add id_bank_card int
alter table clientPersonalAccount add foreign key (id_client) references dbo.Client(id_client) on delete no action on update cascade

insert into clientServices values 
	("MTS", 0, "Mobile"),
	("Megafon", 0, "Mobile"),
	("Beeline", 0, "Mobile"),
	("Tele2", 0, "Mobile"),	
	("CityLink", 0, "Internet"),	
	("NevaLink", 0, "Internet"),	
	("Sampo", 0, "Internet"),	
	("Газ", 0, "communal"),	
	("Вода", 0, "communal"),	
	("Свет", 0, "communal"),	
	("Благотворительность", 0, "charity")