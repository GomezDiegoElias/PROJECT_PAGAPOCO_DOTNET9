-- create database bd_pagapoco_dotnet;

use bd_pagapoco_dotnet;

/*create table tbl_user(
	id int identity(1,1),
	first_name varchar(50) not null,
	last_name varchar(50),
	email varchar(50),
	password varchar(150) not null,
	primary key(id)
);*/

/*insert into tbl_user(first_name, last_name, email, password)
values ('Diego', 'Gomez', 'micorreo123@gmail.com', '1234');*/

/*INSERT INTO tbl_user (first_name, last_name, email, password)
VALUES
('Juan', 'P�rez', 'juan.perez@example.com', 'password123'),
('Mar�a', 'Gonz�lez', 'maria.gonzalez@example.com', 'maria123'),
('Carlos', 'Rodr�guez', 'carlos.rodriguez@example.com', 'carlos456'),
('Ana', 'Mart�nez', 'ana.martinez@example.com', 'ana789'),
('Luis', 'L�pez', 'luis.lopez@example.com', 'luispass'),
('Laura', 'Hern�ndez', 'laura.hernandez@example.com', 'laura2023'),
('Pedro', 'Garc�a', 'pedro.garcia@example.com', 'pedro123'),
('Sof�a', 'Fern�ndez', 'sofia.fernandez@example.com', 'sofia456'),
('Jorge', 'D�az', 'jorge.diaz@example.com', 'jorge789'),
('M�nica', 'S�nchez', 'monica.sanchez@example.com', 'monica123'),
('Ricardo', 'Ram�rez', 'ricardo.ramirez@example.com', 'ricardo456'),
('Patricia', 'Torres', 'patricia.torres@example.com', 'patricia789'),
('Fernando', 'Flores', 'fernando.flores@example.com', 'fernando123'),
('Gabriela', 'Vargas', 'gabriela.vargas@example.com', 'gabriela456'),
('Roberto', 'Mendoza', 'roberto.mendoza@example.com', 'roberto789'),
('Isabel', 'Castillo', 'isabel.castillo@example.com', 'isabel123'),
('Alejandro', 'Ortega', 'alejandro.ortega@example.com', 'alejandro456'),
('Adriana', 'Cruz', 'adriana.cruz@example.com', 'adriana789'),
('Miguel', 'Reyes', 'miguel.reyes@example.com', 'miguel123'),
('Carmen', 'Morales', 'carmen.morales@example.com', 'carmen456'),
('Ra�l', 'Ortiz', 'raul.ortiz@example.com', 'raul789'),
('Luc�a', 'G�mez', 'lucia.gomez@example.com', 'lucia123'),
('Arturo', 'Jim�nez', 'arturo.jimenez@example.com', 'arturo456'),
('Ver�nica', 'Ruiz', 'veronica.ruiz@example.com', 'veronica789'),
('Daniel', 'Alvarez', 'daniel.alvarez@example.com', 'daniel123'),
('Teresa', 'Moreno', 'teresa.moreno@example.com', 'teresa456'),
('Francisco', 'Romero', 'francisco.romero@example.com', 'francisco789'),
('Beatriz', 'Navarro', 'beatriz.navarro@example.com', 'beatriz123'),
('Jos�', 'Medina', 'jose.medina@example.com', 'jose456'),
('Diana', 'Aguilar', 'diana.aguilar@example.com', 'diana789'),
('Manuel', 'Rojas', 'manuel.rojas@example.com', 'manuel123'),
('Claudia', 'Miranda', 'claudia.miranda@example.com', 'claudia456'),
('Eduardo', 'Cort�s', 'eduardo.cortes@example.com', 'eduardo789'),
('Silvia', 'Guerrero', 'silvia.guerrero@example.com', 'silvia123'),
('Antonio', 'Pacheco', 'antonio.pacheco@example.com', 'antonio456'),
('Mariana', 'Delgado', 'mariana.delgado@example.com', 'mariana789'),
('�scar', 'Vega', 'oscar.vega@example.com', 'oscar123'),
('Rosa', 'Campos', 'rosa.campos@example.com', 'rosa456'),
('Javier', 'Valdez', 'javier.valdez@example.com', 'javier789'),
('Alicia', 'Fuentes', 'alicia.fuentes@example.com', 'alicia123'),
('Rub�n', 'C�rdenas', 'ruben.cardenas@example.com', 'ruben456'),
('Lorena', 'Santos', 'lorena.santos@example.com', 'lorena789'),
('Gerardo', 'Salazar', 'gerardo.salazar@example.com', 'gerardo123'),
('Natalia', 'Mej�a', 'natalia.mejia@example.com', 'natalia456'),
('H�ctor', 'Castro', 'hector.castro@example.com', 'hector789'),
('Paulina', 'N��ez', 'paulina.nunez@example.com', 'paulina123'),
('Guillermo', 'Rosales', 'guillermo.rosales@example.com', 'guillermo456'),
('Elena', 'Acosta', 'elena.acosta@example.com', 'elena789'),
('Alberto', 'M�rquez', 'alberto.marquez@example.com', 'alberto123'),
('Martha', 'Le�n', 'martha.leon@example.com', 'martha456');*/

select * from tbl_user;

-- PROCEDIMIENTO ALMACENADO PARA PAGINACION

CREATE PROCEDURE sp_GetUsersPaginated
    @PageIndex INT = 1,
    @PageSize INT = 10
AS
BEGIN
    DECLARE @Offset INT = (@PageSize * (@PageIndex - 1));

    SELECT
        u.id,
        u.first_name,
        u.last_name,
        u.email,
        fila = ROW_NUMBER() OVER(ORDER BY u.id ASC),
        totalFilas = COUNT(*) OVER()
    FROM tbl_user u
    ORDER BY u.id
    OFFSET @Offset ROWS 
    FETCH NEXT @PageSize ROWS ONLY;
END
GO

-- Ejecutar el procedimiento
EXEC sp_GetUsersPaginated @PageIndex = 3, @PageSize = 10;

select * from tbl_user;

DECLARE @PageIndex INT = 1,
  @PageSize INT = 10,
  @SortColumn VARCHAR(50) = 'u.id DESC',
  @TextToSearch VARCHAR(200) = ''


DECLARE @Offset INT = (@PageSize * (@PageIndex - 1)) 

SELECT
 u.id,
 u.first_name,
 u.last_name,
 u.email,
 Fila = ROW_NUMBER() OVER(ORDER BY u.id DESC),
 TotalFilas = COUNT(*) OVER()
FROM tbl_user u
WHERE (
	@TextToSearch IS NULL
	OR @TextToSearch = ''
	OR u.first_name LIKE @TextToSearch + '%'
)
ORDER BY Fila DESC
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY;
 
 CREATE PROCEDURE getUserPagination 
	@PageIndex INT = 1,
	@PageSize INT = 10
 AS
 BEGIN

	DECLARE @Offset INT = (@PageSize * (@PageIndex - 1));

	SELECT
		u.id,
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

 END

 EXEC getUserPagination @PageIndex = 4, @PageSize = 15;
