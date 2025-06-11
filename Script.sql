create database dbCadastroDeProdutos;
use dbCadastroDeProdutos;
-- drop database dbCadastroDeProdutos;

create table Usuarios(
	IdUser int primary key auto_increment,
    Nome varchar(100) not null,
    Email varchar(100) not null,
    Senha varchar(50) not null
);

create table Produtos(
	IdProd int primary key auto_increment,
    Nome varchar(50) not null,
    Descricao varchar(100) not null,
    Preco int not null,
    Quantidade int not null
);

