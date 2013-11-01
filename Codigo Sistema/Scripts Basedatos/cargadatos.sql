---------------------------------------------------------------------------
--Inserts de las Cafeterias
---------------------------------------------------------------------------

INSERT INTO Cafeteria (idCafeteria, nombre, razonsocial, ruc, direccion, telefono1, telefono2, estado, idDistrito, idProvincia, idDepartamento) values ('SUCU0001','San Miguel','Cafeteria S.A','45678912591','Av. Marina 2504','4534488','','ACTIVO','DIST0036','PROV0001','DEPA0015')
INSERT INTO Cafeteria (idCafeteria, nombre, razonsocial, ruc, direccion, telefono1, telefono2, estado, idDistrito, idProvincia, idDepartamento) values ('SUCU0002','Olivos','Cafeteria S.A','45678912591','Av. Trebol','4561244','4586454','ACTIVO','DIST0017','PROV0001','DEPA0015')
INSERT INTO Cafeteria (idCafeteria, nombre, razonsocial, ruc, direccion, telefono1, telefono2, estado, idDistrito, idProvincia, idDepartamento) values ('SUCU0003','Javier Prado','Cafeteria S.A','45678912591','Av. Javier Prado 2504','4532588','','ACTIVO','DIST0021','PROV0001','DEPA0015')
INSERT INTO Cafeteria (idCafeteria, nombre, razonsocial, ruc, direccion, telefono1, telefono2, estado, idDistrito, idProvincia, idDepartamento) values ('SUCU0004','Callao','Cafeteria S.A','45678912591','Av. Alfredo Palacios 2508','4534411','4524877','ACTIVO','DIST0001','PROV0001','DEPA0007')
INSERT INTO Cafeteria (idCafeteria, nombre, razonsocial, ruc, direccion, telefono1, telefono2, estado, idDistrito, idProvincia, idDepartamento) values ('SUCU0005','Lima','Cafeteria S.A','45678912591','Av. Abancay 1520','4587624','4582564','ACTIVO','DIST0001','PROV0001','DEPA0015')


---------------------------------------------------------------------------
--Inserts de las Almacen
---------------------------------------------------------------------------

INSERT INTO Almacen(idAlmacen, idCafeteria) values ('ALMA0001','SUCU0001')
INSERT INTO Almacen(idAlmacen, idCafeteria) values ('ALMA0002','SUCU0002')
INSERT INTO Almacen(idAlmacen, idCafeteria) values ('ALMA0003','SUCU0003')
INSERT INTO Almacen(idAlmacen, idCafeteria) values ('ALMA0004','SUCU0004')
INSERT INTO Almacen(idAlmacen, idCafeteria) values ('ALMA0005','SUCU0005')

---------------------------------------------------------------------------
--Inserts de las perfiles
---------------------------------------------------------------------------
INSERT INTO Perfil_usuario(idPerfil_usuario, nombre, token) values ('PERF0001','Administrador', '1111111111')
INSERT INTO Perfil_usuario(idPerfil_usuario, nombre, token) values ('PERF0002','Recepcionista', '0000000002')
INSERT INTO Perfil_usuario(idPerfil_usuario, nombre, token) values ('PERF0003','Supervisor de Almacen', '0000000003')
INSERT INTO Perfil_usuario(idPerfil_usuario, nombre, token) values ('PERF0004','Supervisor de Logistica', '0000000004')
INSERT INTO Perfil_usuario(idPerfil_usuario, nombre, token) values ('PERF0005','Administrador Master', '0000000005')

---------------------------------------------------------------------------
--Inserts de las tipo
---------------------------------------------------------------------------
INSERT INTO Tipo(id, nombre) values ('TIPO0001','Desayuno')
INSERT INTO Tipo(id, nombre) values ('TIPO0002','Sandwich')
INSERT INTO Tipo(id, nombre) values ('TIPO0003','Piqueos')
INSERT INTO Tipo(id, nombre) values ('TIPO0004','Salados')
INSERT INTO Tipo(id, nombre) values ('TIPO0005','Bebidas')
INSERT INTO Tipo(id, nombre) values ('TIPO0006','Postres')
INSERT INTO Tipo(id, nombre) values ('TIPO0007','Helados')

---------------------------------------------------------------------------
--Inserts de las Proveedor
---------------------------------------------------------------------------

INSERT INTO Proveedor(idProveedor, razonSocial, estado, contacto, email_contacto, direccion, ruc, telefono1, cargo_contacto,telefono_contacto, web) values ('PROV0001','Metro','ACTIVO','Jose', 'jose@gmail.com', 'Av marina','12345876157','4538544','supervisor', '5687599','www.metro.com' )
INSERT INTO Proveedor(idProveedor, razonSocial, estado, contacto, email_contacto, direccion, ruc, telefono1, cargo_contacto,telefono_contacto, web) values ('PROV0002','Plaza vea','ACTIVO','Rosa', 'rosa@gmail.com', 'Av javier Prado','67525876157','4546874','Recepcionista', '5632479','www.plazavea.com' )
INSERT INTO Proveedor(idProveedor, razonSocial, estado, contacto, email_contacto, direccion, ruc, telefono1, cargo_contacto,telefono_contacto, web) values ('PROV0003','Wong','ACTIVO','Jose', 'jose@gmail.com', 'Av marina','98345876157','5367874','supervisor', '8762544','www.wong.com' )

