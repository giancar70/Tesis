

CREATE  TABLE [Perfil_usuario] (
  [idPerfil_usuario] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(45) NOT NULL ,
  [descripcion] VARCHAR(45) NULL ,
  [token] CHAR(30) NOT NULL ,
  PRIMARY KEY ([idPerfil_usuario]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Departamento[
-- -----------------------------------------------------
CREATE  TABLE [Departamento] (
  [idDepartamento] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(40) NOT NULL ,
  PRIMARY KEY ([idDepartamento]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Provincia[
-- -----------------------------------------------------
CREATE  TABLE [Provincia] (
  [idProvincia] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(40) NOT NULL ,
  [idDepartamento] VARCHAR(8) NOT NULL ,
  PRIMARY KEY ([idProvincia], [idDepartamento]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Distrito[
-- -----------------------------------------------------
CREATE  TABLE [Distrito] (
  [idDistrito] VARCHAR(8) NOT NULL ,
  [idProvincia] VARCHAR(8) NOT NULL ,
  [idDepartamento] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(40) NOT NULL ,
  PRIMARY KEY ([idDistrito], [idProvincia], [idDepartamento]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Usuario[
-- -----------------------------------------------------
CREATE  TABLE [Usuario] (
  [idUsuario] VARCHAR(8) NOT NULL,
  [idDistrito] VARCHAR(8) NOT NULL ,
  [idProvincia] VARCHAR(8) NOT NULL ,
  [idDepartamento] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(45) NOT NULL ,
  [apellido_paterno] VARCHAR(45) NOT NULL ,
  [apellido_materno] VARCHAR(45) NULL ,
  [estado] VARCHAR(10) NOT NULL ,
  [email] VARCHAR(45) NOT NULL ,
  [celular] VARCHAR(10) NOT NULL ,
  [numero_documento] VARCHAR(45) NOT NULL ,
  [direccion] VARCHAR(45) NOT NULL ,
  [user_account] VARCHAR(20) NOT NULL ,
  [pass] VARCHAR(50) NOT NULL ,
  PRIMARY KEY ([idUsuario]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Permiso[
-- -----------------------------------------------------
CREATE  TABLE [Permiso] (
  [idPermiso] VARCHAR(8) NOT NULL ,
  [idPerfil_usuario] VARCHAR(8) NOT NULL ,
  PRIMARY KEY ([idPermiso]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Cafeteria[
-- -----------------------------------------------------
CREATE  TABLE [Cafeteria] (
  [idCafeteria] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(45) NOT NULL ,
  [razonsocial] VARCHAR(45) NOT NULL ,
  [ruc] VARCHAR(45) NOT NULL ,
  [direccion] VARCHAR(45) NOT NULL ,
  [telefono1] VARCHAR(9) NOT NULL ,
  [telefono2] VARCHAR(9) NULL ,
  [estado] VARCHAR(45) NOT NULL ,
  [idDistrito] VARCHAR(8) NOT NULL ,
  [idProvincia] VARCHAR(8) NOT NULL ,
  [idDepartamento] VARCHAR(8) NOT NULL ,
  [nombreAdministrador] VARCHAR(8) NULL ,
  PRIMARY KEY ([idCafeteria]) )
 



-- -----------------------------------------------------
-- Table [Basedatos1[.[Cliente[
-- -----------------------------------------------------
CREATE  TABLE [Cliente] (
  [idcliente] VARCHAR(8) NOT NULL ,
  [idCafeteria] VARCHAR(8) NOT NULL ,
  [estado] VARCHAR(10) NOT NULL ,
  [fecha_registro] DATE NOT NULL ,
  PRIMARY KEY ([idcliente]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Horario[
-- -----------------------------------------------------
CREATE  TABLE [Horario] (
  [idHorario] VARCHAR(8) NOT NULL ,
  [fechaini] DATE NOT NULL ,
  [fechafin] DATE NULL ,
  [idempleado] VARCHAR(8) NOT NULL ,
  PRIMARY KEY ([idHorario]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[HorarioDetalle[
-- -----------------------------------------------------
CREATE  TABLE [HorarioDetalle] (
  [idHorario] VARCHAR(8) NOT NULL ,
  [diasemana] VARCHAR(45) NOT NULL ,
  [horaentrada] VARCHAR(6) NOT NULL ,
  [horasalida] VARCHAR(6) NOT NULL ,
  PRIMARY KEY ([idHorario] ,[horaentrada])  )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Asistencia[
-- -----------------------------------------------------
CREATE  TABLE [Asistencia] (
  [idAsistencia] VARCHAR(8) NOT NULL ,
  [horamarcada] VARCHAR(6) NOT NULL ,
  [estado] VARCHAR(45) NOT NULL ,
  [idHorarioDetalle] VARCHAR(8) NOT NULL ,
  PRIMARY KEY ([idAsistencia]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Proveedor[
-- -----------------------------------------------------
CREATE  TABLE [Proveedor] (
  [idProveedor] VARCHAR(8) NOT NULL ,
  [razonSocial] VARCHAR(45) NOT NULL ,
  [estado] VARCHAR(45) NOT NULL ,
  [contacto] VARCHAR(45) NOT NULL ,
  [email_contacto] VARCHAR(45) NOT NULL ,
  [direccion] VARCHAR(45) NOT NULL ,
  [ruc] VARCHAR(45) NOT NULL ,
  [telefono1] VARCHAR(12) NOT NULL ,
  [cargo_contacto] VARCHAR(45) NULL ,
  [telefono_contacto] VARCHAR(45) NULL ,
  [web] VARCHAR(40) NULL ,
  [observacion] VARCHAR(45) NULL ,
  [telefono2] VARCHAR(45) NULL ,
  PRIMARY KEY ([idProveedor]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Almacen[
-- -----------------------------------------------------
CREATE  TABLE [Almacen] (
  [idAlmacen] VARCHAR(8) NOT NULL ,
  [idCafeteria] VARCHAR(8) NOT NULL ,
  PRIMARY KEY ([idAlmacen], [idCafeteria]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Ingrediente[
-- -----------------------------------------------------
CREATE  TABLE [Ingrediente] (
  [idIngrediente] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(30) NOT NULL ,
  [estado] VARCHAR(10) NOT NULL ,
  [descripcion] VARCHAR(45) NULL ,
  PRIMARY KEY ([idIngrediente]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Ordencompra[
-- -----------------------------------------------------
CREATE  TABLE [Ordencompra] (
  [idOrdencompra] VARCHAR(8) NOT NULL ,
  [idProveedor] VARCHAR(8) NOT NULL ,
  [estado] VARCHAR(10) NOT NULL ,
  [fechaemitida] DATE NOT NULL ,
  [idSucursal] VARCHAR(8) NOT NULL ,
  [preciototal] DECIMAL(10,3) NOT NULL ,
  PRIMARY KEY ([idOrdencompra]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Almacen_x_Producto[
-- -----------------------------------------------------
CREATE  TABLE [Almacen_x_Producto] (
  [idAlmacen] VARCHAR(8) NOT NULL ,
  [idIngrediente] VARCHAR(8) NOT NULL ,
  [stockactual] INT NOT NULL ,
  [stockminimo] INT NOT NULL ,
  [stockmaximo] INT NOT NULL ,
  PRIMARY KEY ([idAlmacen], [idIngrediente]) )
 

-- -----------------------------------------------------
-- Table [Basedatos1[.[Sucursal_x_Usuario[
-- -----------------------------------------------------
CREATE  TABLE [Sucursal_x_Usuario] (
  [idCafeteria] VARCHAR(8) NOT NULL ,
  [idUsuario] VARCHAR(8) NOT NULL ,
  [estado] VARCHAR(10) NOT NULL ,
  PRIMARY KEY ([idCafeteria], [idUsuario]) )


-- -----------------------------------------------------
-- Table [Basedatos1[.[Proveedor_x_Producto[
-- -----------------------------------------------------
CREATE  TABLE [Proveedor_x_Producto] (
  [idProveedor] VARCHAR(8) NOT NULL ,
  [idIngrediente] VARCHAR(8) NOT NULL ,
  [precio] DECIMAL(15,2) NOT NULL ,
  PRIMARY KEY ([idProveedor], [idIngrediente]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Notaentrada[
-- -----------------------------------------------------
CREATE  TABLE [Notaentrada] (
  [idNotaentrada] VARCHAR(8) NOT NULL ,
  [idOrdencompra] VARCHAR(8) NOT NULL ,
  [fechaEntrega] DATE NOT NULL ,
  PRIMARY KEY ([idNotaentrada]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[OrdenCompraDetalle[
-- -----------------------------------------------------
CREATE  TABLE [OrdenCompraDetalle] (
  [idOrdencompra] VARCHAR(8) NOT NULL ,
  [idIngrediente] VARCHAR(8) NOT NULL ,
  [cantidad] INT NOT NULL ,
  [precio] DECIMAL(15,2) NOT NULL ,
  PRIMARY KEY ([idOrdencompra], [idIngrediente]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[notaEntradaDetalle[
-- -----------------------------------------------------
CREATE  TABLE [notaEntradaDetalle] (
  [idNotaentrada] VARCHAR(8) NOT NULL ,
  [idIngrediente] VARCHAR(8) NOT NULL ,
  [cantidadentrante] INT NOT NULL ,
  PRIMARY KEY ([idNotaentrada], [idIngrediente]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Producto[
-- -----------------------------------------------------
CREATE  TABLE [Producto] (
  [idProducto] VARCHAR(8) NOT NULL ,
  [nombre] VARCHAR(45) NOT NULL ,
  [descripcion] VARCHAR(45) NULL ,
  [tipo] VARCHAR(8) NOT NULL ,
  [estado] VARCHAR(10) NOT NULL ,
  PRIMARY KEY ([idProducto]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Producto_x_Ingrediente[
-- -----------------------------------------------------
CREATE  TABLE [Producto_x_Ingrediente] (
  [idProducto] VARCHAR(8) NOT NULL ,
  [idIngrediente] VARCHAR(8) NOT NULL ,
  [cantidad] INT NOT NULL ,
  [unidaddemedida] VARCHAR(45) NOT NULL ,
  PRIMARY KEY ([idProducto], [idIngrediente]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Cafeteria_x_Producto[
-- -----------------------------------------------------
CREATE  TABLE [Cafeteria_x_Producto] (
  [idCafeteria] VARCHAR(8) NOT NULL ,
  [idProducto] VARCHAR(8) NOT NULL ,
  [precioventa] DECIMAL(15,2) NOT NULL ,
  [estado] VARCHAR(10) NULL ,
  [cantidad] INT NOT NULL ,
  PRIMARY KEY ([idCafeteria], [idProducto]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[Venta[
-- -----------------------------------------------------
CREATE  TABLE [Venta] (
  [idVenta] VARCHAR(8) NOT NULL ,
  [idCafeteria] VARCHAR(8) NOT NULL ,
  [fechaventa] VARCHAR(45) NOT NULL ,
  [estado] VARCHAR(10) NOT NULL ,
  [montototal] DECIMAL(15,2) NOT NULL ,
  [nombrecliente] VARCHAR(45) NOT NULL ,
  [dnicliente] VARCHAR(9) NOT NULL ,
  PRIMARY KEY ([idVenta]) )
 


-- -----------------------------------------------------
-- Table [Basedatos1[.[VentaDetalle[
-- -----------------------------------------------------
CREATE  TABLE [VentaDetalle] (
  [idVenta] VARCHAR(8) NOT NULL ,
  [idProducto] VARCHAR(8) NOT NULL ,
  [cantidad] INT NOT NULL ,
  [subtotal] DECIMAL(15,2) NOT NULL ,
  PRIMARY KEY ([idVenta], [idProducto]) )
 
 
 
CREATE TABLE [Tipo](
	[id] VARCHAR(8) NOT NULL,
	[nombre] VARCHAR(20) NOT NULL,
	PRIMARY KEY([id]))
	
	
CREATE TABLE [Perfil_usuario_x_Usuario](
	[idPerfil_usuario] VARCHAR(8) NOT NULL,
	[idUsuario] VARCHAR(8) NOT NULL,
	PRIMARY KEY ([idPerfil_usuario], [idUsuario]) )
	
