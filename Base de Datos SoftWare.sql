create database Phone_Store;
GO

use Phone_Store;
GO


create table Rol(
Role_Id int identity(1,1) primary key not null,
Rol_Desc nvarchar(50) not null,
Creation_Date date default getdate()
);
GO


create table Permiso(
Perm_Id int identity(1,1) primary key not null,
Rol_Id int foreign key references Rol(Role_Id) not null,
Menu_Name nvarchar(50),
Creation_Date date default getdate()
);
GO


create table Proveedores(
Prov_Id int identity(1,1) primary key not null,
Documen nvarchar(50) not null,
Prov_Name nvarchar(50) not null,
Gmail nvarchar(60) not null,
Telephone char(8) check(Telephone like '[2|5|7|8][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
Prov_Address nvarchar(120),
Prov_State bit default 1 not null,
Creation_Date date default getdate()
);
GO


create table Cliente(
Client_Id int identity(1,1) primary key not null,
Document nvarchar(50) not null,
Client_FullName nvarchar(50) not null,
Gmail nvarchar(60) not null,
Telephone char(8) check(Telephone like '[2|5|7|8][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
Client_State bit default 1 not null,
Creation_Date date default getdate()
);
GO


create table Usuario(
Id_User int identity(1,1) primary key not null,
Document nvarchar(50) not null,
User_FullName nvarchar(50) not null,
Gmail nvarchar(60) not null,
Pssword nvarchar(50) not null,
Role_Id int foreign key references Rol(Role_Id) not null,
User_State bit default 1 not null,
Creation_State date default getdate()
);
GO


create table Marca(
Id_Marca int identity(1,1) primary key not null,
Marca_Description nvarchar(50) not null,
Marca_State bit default 1 not null,
Creation_Date date default getdate()
);
GO



create table Producto(
Prod_Id int identity(1,1) primary key not null,
Prod_Cod nvarchar(50) not null,
Prod_Name nvarchar(25) not null,
Prod_Description nvarchar(50) not null,
Id_Marca int foreign key references Marca(Id_Marca) on update cascade not null,
Stock int not null default 0,
Purchase_Price decimal(10,2) default 0,
Sale_Price decimal(10,2) default 0,
Prod_State bit default 1,
Reg_Date date default getdate()
);
GO


create table Doc_Type(
Doc_Type_Id int identity(1,1) primary key not null,
Doc_Type_Description nvarchar(50) not null
);
GO


create table Compra(
Purchase_Id int identity(1,1) primary key not null,
Id_User int foreign key references Usuario(Id_User) not null,
Prov_ID int foreign key references Proveedores(Prov_Id) not null,
Doc_Num int not null,
Doc_Type int foreign key references Doc_Type(Doc_Type_Id) not null,
Total money not null,
Reg_Date date default getdate()
);
GO


create table Det_Compra(
Purc_Det_Id int identity(1,1) primary key not null,
Purchase_Id int foreign key references Compra(Purchase_Id) not null,
Prod_Id int foreign key references Producto(Prod_Id) not null,
Purchase_Price money not null,
Sale_Price money not null,
Stock int not null,
Total money not null,
Reg_Date date
);
GO


create table Venta(
Sale_Id int identity(1,1) primary key not null,
Id_User int foreign key references Usuario(Id_User) not null,
Doc_Num int not null,
Doc_Type int foreign key references Doc_Type(Doc_Type_Id) not null,
Client_Doc nvarchar(50) not null,
Client_Name nvarchar(50) not null,
Pay_Amount money not null,
Change_Amount money not null,
Total_Amount money not null,
Reg_Date date default getdate()
);
GO


create table Det_Venta(
Det_Sale_Id int identity(1,1) primary key not null,
Sale_Id int foreign key references Venta(Sale_Id) not null,
Prod_Id int foreign key references Producto(Prod_Id) not null,
Sale_Price decimal(10,2) not null,
Quantity int not null,
SubTotal decimal(10,2) not null,
Creation_Date date default getdate()
);
GO


----NUEVA TABLA------

Create table Negocio(
Id_Negocio int primary key not null,
Nombre Nvarchar (60) not null,
RUC nvarchar(60) not null,
Direccion nvarchar(60) not null,
Logo varbinary(max) NULL
)

select Id_Negocio, Nombre, RUC, Direccion from Negocio where Id_Negocio = 1


insert into Rol(Rol_Desc)
values ('ADMIN'),('Empleado');
GO



 ---DBCC CHECKIDENT ('Marca', RESEED, 0);


insert into Usuario(Document, User_FullName, Gmail, Pssword, Role_Id, User_State)
values ('101010', 'ADMIN-nombre', 'CR7@gmail.com', '123', 1,1),
('202020', 'Empleado-nombre', 'MESSI@gmail.com', '456', 2,1);
GO


insert into Permiso(Rol_Id, Menu_Name) values 
(1, 'menuUsuarios'),
(1, 'menuEditar'),
(1, 'menuVentas'),
(1, 'menuCompras'),
(1, 'menuClientes'),
(1, 'menuProveedores');
GO


insert into Permiso(Rol_Id, Menu_Name) values 
(2, 'menuVentas'),
(2, 'menuCompras'),
(2, 'menuClientes'),
(2, 'menuProveedores');
GO

--select * from Rol
--select * from Permiso
--select * from Usuario



-- Desde aqui los procedimientos relacionados a Usuario del ModuloPresentacion ------

-- --
create procedure SP_REGISTRARUSUARIO(
@Document nvarchar(50),
@User_Fullname nvarchar(50),
@Gmail nvarchar(60),
@Pssword nvarchar(50),
@Role_Id int,
@User_State bit,
@IdUsuarioResultado int output,
@Mensaje nvarchar(500) output
)
as
begin
   set @IdUsuarioResultado=0
   set @Mensaje=''  --   <-- Originalmente esto no lo puso engel, voy a ver si con esto igual corre, si no lo quiot
   if  not exists(select * from Usuario where Document=@Document)
   begin
    insert into Usuario(Document,User_FullName,Gmail,Pssword,Role_Id,User_State) values
	(@Document,@User_Fullname,@Gmail,@Pssword,@Role_Id,@User_State)

	set @IdUsuarioResultado=SCOPE_IDENTITY()
   end
   else
    set @Mensaje='No se Puede Repetir el documento para mas de un usuario'
end;
GO


-- --
create procedure SP_EDITARUSUARIO(
@Id_User int,
@Document nvarchar(50),
@User_Fullname nvarchar(50),
@Gmail nvarchar(60),
@Pssword nvarchar(50),
@Role_Id int,
@User_State bit,
@IdUsuarioResultado bit output,
@Mensaje nvarchar(500) output
)
as
begin
  set @IdUsuarioResultado=0
  set @Mensaje=''

  if not exists(select * from Usuario where Document=@Document and Id_User!=@Id_USer)
  begin
   update Usuario set
   Document=@Document,
   User_FullName=@User_Fullname,
   Gmail=@Gmail,
   Pssword=@Pssword,
   Role_Id=@Role_Id,
   User_State=@User_State
   where Id_User=@Id_User 

   set @IdUsuarioResultado=1
  end
  else
    set @Mensaje='No se Puede Repetir El Documento para mas de un Usuario'
end;
GO


-- --
create procedure SP_ELIMINARUSUARIO(
@Id_User int,
@IdUsuarioResultado bit output,
@Mensaje nvarchar(500) output
)
as
begin
  set @IdUsuarioResultado=0
  set @Mensaje=''
  declare @rulepass bit=1

  if exists(select * from Compra C inner join
  Usuario U on U.Id_User=C.Id_User
  where U.Id_User=@Id_User
  )
  begin
    set @rulepass=0
    set @IdUsuarioResultado=0
	set @Mensaje = @Mensaje + 'El Usuario se Encuentra Relacionado a la Compra\n'
  end

  if exists(select * from Venta V inner join
  Usuario U on U.Id_User=V.Id_User
  where U.Id_User=@Id_User
  )
  begin
    set @rulepass=0
    set @IdUsuarioResultado=0
	set @Mensaje = @Mensaje + 'El Usuario se Encuentra Relacionado a la Venta\n'
  end

  if(@rulepass=1)
  begin
    delete from Usuario WHERE Id_User = @Id_User  ---  <--Le agrege un where no se
	set @IdUsuarioResultado=1
  end
end;
GO






-- Desde aqui los procedimientos relacionados a Marca ------

insert into Marca(Marca_Description, Marca_State) values('Samsung', 1)
,('Xiaomi', 1)
,('OnePLus', 1);
GO



create procedure SP_RegistrarMarca(
@Marca_Description nvarchar(500),
@Marca_State bit,
@Resultado int output,
@Mensaje nvarchar(500) output
)
as
begin
   set @Resultado=0
   if not exists(Select * from Marca where Marca_Description=@Marca_Description)
   begin
     insert into Marca(Marca_Description, Marca_State) values (@Marca_Description, @Marca_State)
	 set @Resultado=SCOPE_IDENTITY()
   end
   else
     set @Mensaje='No Se Puede Repetir la descripcion de una Marca'
end;
GO



create procedure SP_EditarMarca(
@Id_Marca int,
@Marca_State bit,
@Marca_Description nvarchar(50),
@Resultado bit output,
@Mensaje nvarchar(500) output
)
as
begin
  set @Resultado=1
  if not exists (select * from Marca where @Marca_Description=Marca_Description and Id_Marca!=@Id_Marca)

     update Marca set
     Marca_Description=@Marca_Description,
	 Marca_State=@Marca_State
     where Id_Marca=@Id_Marca
  else
     set @Resultado=0
	 set @Mensaje='No se Puede Repetir la Descripcion de una Marca'
end;
GO


create procedure sp_EliminarMarca(
@Id_Marca int,
@Resultado bit output,
@Mensaje nvarchar(500) output
)
as
begin
  set @Resultado=1
  if not exists (select * from Marca m inner join Producto p on p.Id_Marca = m.Id_Marca
  where m.Id_Marca=@Id_Marca)
  begin
     delete top (1) from Marca where Id_Marca=@Id_Marca
  end
  else
  begin
    set @Resultado=0
	set @Mensaje='La Marca se encuentra relacionada a un producto'
  end
end;
GO





-- Desde aqui los procedimientos relacionados a Producto ------

insert into Producto(Prod_Cod, Prod_Name, Prod_Description, Id_Marca)
values ('989', 'S24 Ultra', 'SP-885gen2/12-ram/512gb', 1);
GO



create procedure sp_RegistrarProducto(
@Prod_Cod nvarchar(50),
@Prod_name nvarchar(25),
@Prod_description nvarchar(50),
@Id_Marca int,
@Prod_State bit,
@Resultado bit output,
@Mensaje nvarchar(50) output
)
as
begin
   set @Resultado=0
   if not exists(select * from Producto where Prod_Cod=@Prod_Cod)
   begin
       insert into Producto(Prod_Cod,Prod_Name,Prod_Description,Id_Marca, Prod_State) values (@Prod_Cod,@Prod_name,@Prod_description,@Id_Marca, @Prod_State)
	   set @Resultado=SCOPE_IDENTITY()
	end
	else
	  set @Mensaje='Ya Existe un producto con el mismo Codigo'
end;
GO





create procedure sp_ModificarProducto(
@Prod_Id int,
@Prod_Cod nvarchar(50),
@Prod_Name nvarchar(25),
@Prod_Description nvarchar(50),
@Id_Marca int,
@Prod_State bit,
@Resultado bit output,
@Mensaje nvarchar(50) output
)
as
begin
    set @Resultado=1
	if not exists(select * from Producto where Prod_Cod=@Prod_Cod and Prod_Id!=@Prod_Id)

          update Producto set
		  Prod_Cod=@Prod_Cod,
		  Prod_Name=@Prod_Name,
		  Prod_Description=@Prod_Description,
		  Id_Marca=@Id_Marca,
		  Prod_State=@Prod_State
		  where Prod_Id=@Prod_Id
	else
	begin
	  set @Resultado=0
	  set @Mensaje='Ya Existe un producto con el mismo codigo'
	end
end;
GO




create procedure sp_EliminarProducto(
@Prod_Id int,
@Respuesta bit output,
@Mensaje nvarchar(50) output
)
as
begin
   set @Respuesta=0
   set @Mensaje=''
   declare @rulepass bit=1

   if exists (select * from Det_Compra dc inner join Producto p on p.Prod_Id=dc.Prod_Id
   where p.Prod_Id=@Prod_Id)
   begin
     set @rulepass=0
	 set @Respuesta=0
	 set @Mensaje=@Mensaje + 'No Se Puede eliminar porque se encuentra relacionado a una Compra\n'
   end

   if exists (select * from Det_Venta dv inner join Producto p on p.Prod_Id=dv.Prod_Id 
   where p.Prod_Id=@Prod_Id)
   begin
      set @rulepass=0
	  set @Respuesta=0
	  set @Mensaje=@Mensaje + 'No se puede eliminar por que se encuentra relacionado a una Venta\n'
   end

   if(@rulepass=1)
   begin
     delete from Producto where Prod_Id=@Prod_Id
	 set @Respuesta=1
   end
end;
GO


--select * from Producto
--select * from Marca





-- Desde aqui los procedimientos relacionados a Cliente ------



create procedure sp_RegistrarCliente(
@Document nvarchar(50),
@Client_Fullname nvarchar(50),
@Gmail nvarchar(60),
@Telephone char(8),
@Client_State bit,
@Resultado bit output,
@Mensaje nvarchar(50) output
)
as
begin
   set @Resultado=0
   declare @IdPerson int
   if not exists (select * from Cliente 
   where Document=@Document)
   begin
      insert into Cliente(Document,Client_FullName,Gmail,Telephone,Client_State) values (@Document,@Client_Fullname,@Gmail,@Telephone,@Client_State)

	  set @Resultado=SCOPE_IDENTITY()
	end
	else
	  set @Mensaje='El Numero de Documento ya Existe'
end;
GO



create procedure sp_ModificarCliente(
@Client_Id int,
@Document nvarchar(50),
@Client_Fullname nvarchar(50),
@gmail nvarchar(60),
@telephone char(8),
@Client_State bit,
@Resultado bit output,
@Mensaje nvarchar(50) output
)
as
begin
  set @Resultado=1
  declare @IdPerson int
  if not exists(select * from Cliente where Document=@Document and Client_Id!=@Client_Id)     
  begin
    update Cliente set
	Document=@Document,
	Client_FullName=@Client_Fullname,
	Gmail=@gmail,
	Telephone=@telephone,
	Client_State=@Client_State
	where Client_Id=@Client_Id
  end
  else
  begin
    set @Resultado=0
	set @Mensaje='El Numero de Documento Ya Existe'
  end
end;
GO







-- Desde aqui los procedimientos relacionados a Proveedores ------

create procedure sp_RegistrarProveedor(
@Documen nvarchar(50),
@Prov_name nvarchar(50),
@Gmail nvarchar(60),
@Telephone char(8),
@Prov_State bit,
@Resultado bit output,
@Mensaje nvarchar(50) output
)
as
begin
   set @Resultado=0
   declare @IdPerson int
   if not exists (select * from Proveedores 
   where Documen=@Documen)
   begin
      insert into Proveedores(Documen,Prov_Name,Gmail,Telephone,Prov_State) values (@Documen,@Prov_name,@Gmail,@Telephone,@Prov_State)

	  set @Resultado=SCOPE_IDENTITY()
	end
	else
	  set @Mensaje='El Numero de Documento ya Existe'
end;
GO



create procedure sp_ModificarProveedor(
@Prov_Id int,
@Documen nvarchar(50),
@Prov_name nvarchar(50),
@Gmail nvarchar(60),
@Telephone char(8),
@Prov_State bit,
@Resultado bit output,
@Mensaje nvarchar(500) output
)
as
begin
  set @Resultado=1
  declare @IdPerson int
  if not exists(select * from Proveedores where Documen=@Documen and Prov_Id!=@Prov_Id)     
  begin
    update Proveedores set
	Documen=@Documen,
	Prov_Name=@Prov_name,
	Gmail=@Gmail,
	Telephone=@Telephone,
	Prov_State=@Prov_State
	where Prov_Id=@Prov_Id
  end
  else
  begin
    set @Resultado=0
	set @Mensaje='El Numero de Documento Ya Existe'
  end
end;
GO

create procedure sp_EliminarProveedor(
@Prov_Id int,
@Resultado bit output,
@Mensaje nvarchar(500) output
)
as
begin
  set @Resultado=1
  if not exists (select * from Proveedores p inner join Compra c on p.Prov_Id = c.Prov_ID
  where p.Prov_Id=@Prov_Id)
  begin
     delete top (1) from Proveedores where Prov_Id = @Prov_Id
  end
  else
  begin
    set @Resultado=0
	set @Mensaje='El Proveedor se encuentra relacionado a una Compra'
  end
end;
GO

