create DataBase PontoDeVenda
go

use PontoDeVenda
go


create table Cliente (
clienteID int not null identity primary key,
nome varchar(50) not null,
endereco varchar(50) not null,
cidade varchar(35) not null,
estado char(02) not null,
telefone char(11),
email varchar(80),
sexo char(01) not null check (sexo in (UPPER('M'), UPPER('F')))
)
go

create table Pedido (
pedidoID int not null identity (1,1) primary key,
clienteID int,
dataPedido datetime not null default getdate(),
valorTotal decimal (12,2) not null,
constraint fk_CliPed foreign key (clienteID)
references Cliente (clienteID)
on delete cascade
on update cascade
)
go

create table Itens (
itemID int not null identity,
pedidoID int not null,
precoVenda decimal (12,2) not null,
qtdadeVendida int not null check(qtdadevendida > 0),
produtoID int
)
go


alter table Itens
 add primary key (itemID, pedidoID)

 alter table Itens
 add foreign key (pedidoID)
 references Pedido (pedidoID)
 on delete cascade
 on update cascade


 alter table Itens
 add foreign key (produtoID)
 references Produto (produtoID)
 on delete cascade
 on update cascade


 create table Produto (
 produtoID int not null identity (1,1) primary key,
 descricao varchar(60) not null,
 precoUnitario decimal(12,2) not null,
 qtdadeEstocada int not null,
 discontinuado bit
 )
 go

 alter table Produto
 add check (precoUnitario > 0)

 create table Fornecedor (
fornecedorID int not null primary key,
razaoSocial varchar(80) not null,
fantasia varchar(50) not null,
endereco varchar(60) not null,
telefone char(11),
contato varchar(35) not null,
site varchar(80)
)
go

create table FornProd (
fornecedorID int not null,
produtoID int not null
)
go

alter table FornProd
 add primary key (fornecedorID, produtoID)


 alter table FornProd
 add foreign key (produtoID)
 references Produto (produtoID)
 on delete cascade
 on update cascade

 alter table FornProd
 add foreign key (fornecedorID)
 references Fornecedor (fornecedorID)
 on delete cascade
 on update cascade


 --Procedimentos
create procedure ListarClientes
AS
 Select * from Cliente

create procedure InserirCliente
@nome varchar(50),
@endereco varchar(50),
@cidade varchar(35),
@estado char(02),
@telefone char(11),
@email varchar(80),
@sexo char(01)
AS
 INSERT Cliente (nome,endereco,cidade,estado,telefone,email,sexo)
 VALUES (@nome,@endereco,@cidade,@estado,@telefone,@email,@sexo)

 exec InserirCliente 'Pedro','Rua José Ovídio vale, 1654','Natal','RN','99875643','pedro@gmail.com','M'