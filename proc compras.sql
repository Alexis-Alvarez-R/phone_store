create type [dbo].[Det_Compra] as table(
	[Prod_Id] int null,
	[Purchase_Price] Decimal(18,2) null,
	[Sale_Price] Decimal(18,2) null,
	[Stock] int null,
	[Total] Decimal(18,2) null

);

create procedure sp_RegistrarCompra(
@Id_User int,
@Prov_ID int,
@Doc_Type nvarchar(50),
@Doc_Num nvarchar(50),
@total decimal(18,2),
@DetalleCompra [Det_Compra] readonly,
@Resultado bit output,
@Mensaje nvarchar(500)
)
as
begin

	begin try
			declare @Purchase_Id int = 0
			set @Resultado = 1
			set @Mensaje = ''

			begin transaction registro
			--registro compra
			insert into Compra(Id_User, Prov_Id, Doc_Type, Doc_Num, Total)
			values(@Id_User, @Prov_Id, @Doc_Type, @Doc_Num, @Total)

			set @Purchase_Id = SCOPE_IDENTITY()
			
			--registro Det_compra
			insert into Det_Compra(Purchase_Id, Prod_Id, Purchase_Price, Sale_Price, Stock, Total)
			select @Purchase_Id,Prod_Id, Purchase_Price, Sale_Price, Stock, Total from @DetalleCompra

			--Actualizar Stock

			update p set p.Stock = p.Stock + dc.Cantidad,
			p.Purchase_Price = dc.Purchase_Price,
			p.Sale_Price = dc.Sale_Price 
			from Producto p inner join @DetalleCompra dc on dc.Prod_Id = p.Prod_Id

			commit transaction registro
	end try
	begin catch
		
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()

		rollback transaction registro

	end catch
end