-- create database bd_pagapoco_dotnet;

use bd_pagapoco_dotnet;

select * from tbl_user;

-- TRUNCATE TABLE tbl_user;

/*create table tbl_user(
	id int identity(1,1),
	dni bigint not null,
	first_name varchar(50) not null,
	last_name varchar(50),
	email varchar(50),
	[password] varchar(150) not null,
	primary key(id)
);*/


INSERT INTO dbo.tbl_user (dni, first_name, last_name, email, [password])
    VALUES 
        (12345678, 'Juan', 'Pérez', 'juan.perez@email.com', '123456'),
        (23456789, 'María', 'González', 'maria.gonzalez@email.com', 'abc123'),
        (34567890, 'Carlos', 'López', 'carlos.lopez@email.com', 'pass01'),
        (45678901, 'Ana', 'Martínez', 'ana.martinez@email.com', 'ana456'),
        (56789012, 'Pedro', 'Rodríguez', 'pedro.rodriguez@email.com', 'pedro1'),
        (67890123, 'Sofía', 'Fernández', 'sofia.fernandez@email.com', 'sofia2'),
        (78901234, 'Diego', 'Silva', 'diego.silva@email.com', 'diego3'),
        (89012345, 'Valentina', 'Torres', 'valentina.torres@email.com', 'val789'),
        (90123456, 'Mateo', 'Morales', 'mateo.morales@email.com', 'mateo4'),
        (10234567, 'Camila', 'Castro', 'camila.castro@email.com', 'cam123'),
        (11345678, 'Santiago', 'Herrera', 'santiago.herrera@email.com', 'santi5'),
        (12456789, 'Isabella', 'Vargas', 'isabella.vargas@email.com', 'isa456'),
        (13567890, 'Nicolás', 'Ramírez', 'nicolas.ramirez@email.com', 'nico67'),
        (14678901, 'Martina', 'Jiménez', 'martina.jimenez@email.com', 'marti8'),
        (15789012, 'Sebastián', 'Mendoza', 'sebastian.mendoza@email.com', 'seba90'),
        (16890123, 'Emilia', 'Ortega', 'emilia.ortega@email.com', 'emi123'),
        (17901234, 'Alejandro', 'Ruiz', 'alejandro.ruiz@email.com', 'ale456'),
        (18012345, 'Lucía', 'Medina', 'lucia.medina@email.com', 'lucy78'),
        (19123456, 'Tomás', 'Rojas', 'tomas.rojas@email.com', 'tom901'),
        (20234567, 'Julieta', 'Aguirre', 'julieta.aguirre@email.com', 'juli23'),
        (21345678, 'Facundo', 'Vega', 'facundo.vega@email.com', 'facu45'),
        (22456789, 'Antonella', 'Sosa', 'antonella.sosa@email.com', 'anto67'),
        (23567890, 'Ignacio', 'Peña', 'ignacio.pena@email.com', 'nacho8'),
        (24678901, 'Agustina', 'Luna', 'agustina.luna@email.com', 'agus90'),
        (25789012, 'Benjamín', 'Cabrera', 'benjamin.cabrera@email.com', 'benja1'),
        (26890123, 'Delfina', 'Carrasco', 'delfina.carrasco@email.com', 'delfi2'),
        (27901234, 'Thiago', 'Moreno', 'thiago.moreno@email.com', 'thia34'),
        (28012345, 'Amparo', 'Blanco', 'amparo.blanco@email.com', 'ampa56'),
        (29123456, 'Lautaro', 'Guerrero', 'lautaro.guerrero@email.com', 'lauta7'),
        (30234567, 'Milagros', 'Campos', 'milagros.campos@email.com', 'mila89');


DECLARE @PageIndex INT = 1,
  @PageSize INT = 10,
  @SortColumn VARCHAR(50) = 'u.id DESC',
  @TextToSearch VARCHAR(200) = ''


DECLARE @Offset INT = (@PageSize * (@PageIndex - 1)) 

SELECT
 u.id,
 u.dni,
 u.first_name,
 u.last_name,
 u.email,
 Fila = ROW_NUMBER() OVER(ORDER BY u.id DESC),
 TotalFilas = COUNT(*) OVER()
FROM dbo.tbl_user u
WHERE (
	@TextToSearch IS NULL
	OR @TextToSearch = ''
	OR u.first_name LIKE @TextToSearch + '%'
)
ORDER BY Fila DESC
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY;
 
 /*CREATE PROCEDURE getUserPagination 
	@PageIndex INT = 1,
	@PageSize INT = 10
 AS
 BEGIN

	DECLARE @Offset INT = (@PageSize * (@PageIndex - 1));

	SELECT
		u.id,
		u.dni,
		u.first_name,
		u.last_name,
		u.email,
		u.[password],
		Fila = ROW_NUMBER() OVER(ORDER BY u.id ASC),
		TotalFilas = COUNT(*) OVER()
	FROM tbl_user u
	ORDER BY Fila ASC
	OFFSET @Offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

 END*/

 EXEC getUserPagination @PageIndex = 1, @PageSize = 15;


