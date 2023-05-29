USE Usuario

DROP TABLE Usuarios

CREATE TABLE rol(
idRol int PRIMARY KEY IDENTITY,
nombre varchar(80)
)

CREATE TABLE division(
idDivision int PRIMARY KEY IDENTITY,
nombre varchar(80)
)

CREATE TABLE Usuarios(
 boleta bigint PRIMARY KEY,
 nombre varchar(80) not null,
 indicativo varchar(80),
 aPaterno varchar(80),
 escuela varchar(80),
 carrera varchar(80),
 especialidades varchar(80),
 estatus varchar(10),
 userName varchar(80),
 idRol int REFERENCES rol(idRol),
 idDivision int REFERENCES division(idDivision),
 clave varbinary(8000)
)

--PROCEDIMMIENTO PARA AGREGAR USUARIOS
CREATE PROCEDURE Agregar_Usuarios
@boleta bigint,
@nombre varchar(80),
@indicativo varchar(80),
@aPaterno varchar(80),
@escuela varchar(80),
@carrera varchar(80),
@especialidades varchar(80),
@estatus varchar(10),
@userName varchar(80),
@idRol int,
@idDivision int,
@clave varchar(80),
@patron varchar(80)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO Usuarios(boleta, nombre, indicativo, aPaterno, escuela, carrera, especialidades, estatus, userName, idRol, idDivision, clave)
VALUES(@boleta, @nombre, @indicativo, @aPaterno, @escuela, @carrera, @especialidades, @estatus, @userName, @idRol, @idDivision, ENCRYPTBYPASSPHRASE(@patron, @clave))

END

--PROCEDIMIENTO DE VALIDACIÓN
CREATE PROCEDURE Validar_Usuarios
@nombre varchar(80),
@clave varchar(80),
@patron varchar(80)
AS
BEGIN
SELECT *FROM Usuarios WHERE nombre = @nombre and CONVERT(VARCHAR(80), DECRYPTBYPASSPHRASE(@patron, clave)) = @clave
END

--ELIMINAR PROCEDIMIENTOS ALMACENADOS
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Validar_Usuarios]')
 AND type IN (N'P', N'PC'))
DROP PROCEDURE [ViewUsers];

DROP PROCEDURE UpdateUsers

--PROCEDIMIENTO PARA VER USUARIOS
CREATE PROCEDURE Ver_Usuarios
AS
BEGIN
SELECT *FROM Usuarios
END

--PROCEDIMIENTO PARA VER USUARIOS CON INNER JOIN
CREATE PROCEDURE ViewUsers
@condicion varchar(5)
AS
BEGIN
SELECT u.*, d.nombre AS division, r.nombre AS rol
FROM Usuarios u
INNER JOIN division d ON u.idDivision = d.idDivision
INNER JOIN rol r ON u.idRol = r.idRol
WHERE u.nombre LIKE @condicion + '%'
END
--PROCEDIMIENTO PARA EDITAR USUARIOS

CREATE PROCEDURE UpdateUsers
@boleta bigint,
@nombre varchar(80),
@indicativo varchar(80),
@aPaterno varchar(80),
@escuela varchar(80),
@carrera varchar(80),
@especialidades varchar(80),
@estatus varchar(10),
@userName varchar(80),
@idRol int,
@idDivision int,
@clave varchar(80),
@patron varchar(80)
AS
BEGIN
SET NOCOUNT ON;
UPDATE Usuarios SET boleta = @boleta, nombre=@nombre,indicativo=@indicativo,
aPaterno=@aPaterno,escuela=@escuela,carrera=@carrera,especialidades=@especialidades,
estatus=@estatus,userName=@userName,idRol=@idRol,idDivision=@idDivision, clave=
ENCRYPTBYPASSPHRASE(@patron, @clave)
WHERE boleta = @boleta
END