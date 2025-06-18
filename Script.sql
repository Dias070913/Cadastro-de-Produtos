create database dbCadastroDeProdutos;
use dbCadastroDeProdutos;
-- drop database dbCadastroDeProdutos;

create table Usuarios(
	Id int primary key auto_increment,
    Nome varchar(100) not null,
    Email varchar(100) not null,
    Senha varchar(50) not null
);

create table Produtos(
	Id int primary key auto_increment,
    Nome varchar(50) not null,
    Descricao varchar(100) not null,
    Preco int not null,
    Quantidade int not null
);

select * from Usuarios;
select * from Produtos;

insert into Usuarios(Id, Nome, Email, Senha)
			values(1, "João", "joaodasdores@gmail.com", "Password");

insert into Produtos(Id, Nome, Descricao, Preco, Quantidade)
			values(1, "Produto", "Um produto", 10, 5);