-- create database bd_pagapoco_dotnet;

use bd_pagapoco_dotnet;

select * from tbl_user;
select * from tbl_publication;

-- TRUNCATE TABLE tbl_user;
-- TRUNCATE TABLE tbl_publication;

/*create table tbl_user(
	id int identity(1,1),
	dni bigint not null,
	first_name varchar(50) not null,
	last_name varchar(50),
	email varchar(50),
	[password] varchar(150) not null,
	primary key(id)
);*/


/*INSERT INTO dbo.tbl_user (dni, first_name, last_name, email, [password])
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
        (30234567, 'Milagros', 'Campos', 'milagros.campos@email.com', 'mila89');*/

/*INSERT INTO tbl_publication (
    code_publication, title, description, price, image_url,
    brand, model, year, create_at, updated_at, user_id
)
VALUES
(100001, 'Toyota Corolla 2015', 'Auto en excelente estado, único dueño.', 14500, 'https://example.com/images/car1.jpg', 'Toyota', 'Corolla', 2015, GETDATE(), GETDATE(), 1),
(100002, 'Ford Focus 2018', 'Con servicio recién hecho, listo para transferir.', 17500, 'https://example.com/images/car2.jpg', 'Ford', 'Focus', 2018, GETDATE(), GETDATE(), 1),
(100003, 'Volkswagen Golf 2016', 'Versión Comfortline con muy buen consumo.', 15800, 'https://example.com/images/car3.jpg', 'Volkswagen', 'Golf', 2016, GETDATE(), GETDATE(), 1),
(100004, 'Chevrolet Cruze 2017', 'Con pocos kilómetros y papeles al día.', 16200, 'https://example.com/images/car4.jpg', 'Chevrolet', 'Cruze', 2017, GETDATE(), GETDATE(), 1),
(100005, 'Honda Civic 2019', 'Impecable estado, sin detalles.', 21500, 'https://example.com/images/car5.jpg', 'Honda', 'Civic', 2019, GETDATE(), GETDATE(), 1),
(100006, 'Nissan Sentra 2015', 'Gran oportunidad, motor en perfectas condiciones.', 13900, 'https://example.com/images/car6.jpg', 'Nissan', 'Sentra', 2015, GETDATE(), GETDATE(), 1),
(100007, 'Toyota Corolla 2020', 'Nuevo, con garantía vigente.', 23000, 'https://example.com/images/car7.jpg', 'Toyota', 'Corolla', 2020, GETDATE(), GETDATE(), 1),
(100008, 'Ford Focus 2014', 'Con detalles estéticos menores, buen motor.', 12500, 'https://example.com/images/car8.jpg', 'Ford', 'Focus', 2014, GETDATE(), GETDATE(), 1),
(100009, 'Volkswagen Golf 2021', 'Versión Highline automática.', 27800, 'https://example.com/images/car9.jpg', 'Volkswagen', 'Golf', 2021, GETDATE(), GETDATE(), 1),
(100010, 'Chevrolet Cruze 2013', 'Ideal primer auto, económico.', 9800, 'https://example.com/images/car10.jpg', 'Chevrolet', 'Cruze', 2013, GETDATE(), GETDATE(), 1),

(100011, 'Honda Civic 2017', 'Full equipo, mantenimiento al día.', 18500, 'https://example.com/images/car11.jpg', 'Honda', 'Civic', 2017, GETDATE(), GETDATE(), 1),
(100012, 'Nissan Sentra 2018', 'Muy cuidado, sin accidentes.', 16800, 'https://example.com/images/car12.jpg', 'Nissan', 'Sentra', 2018, GETDATE(), GETDATE(), 1),
(100013, 'Toyota Corolla 2012', 'Versión base, mecánico.', 11200, 'https://example.com/images/car13.jpg', 'Toyota', 'Corolla', 2012, GETDATE(), GETDATE(), 1),
(100014, 'Ford Focus 2022', 'Último modelo, sin uso.', 29500, 'https://example.com/images/car14.jpg', 'Ford', 'Focus', 2022, GETDATE(), GETDATE(), 1),
(100015, 'Volkswagen Golf 2015', 'Buen estado general, ITV vigente.', 14800, 'https://example.com/images/car15.jpg', 'Volkswagen', 'Golf', 2015, GETDATE(), GETDATE(), 1),
(100016, 'Chevrolet Cruze 2020', 'Un solo dueño, full equipo.', 23500, 'https://example.com/images/car16.jpg', 'Chevrolet', 'Cruze', 2020, GETDATE(), GETDATE(), 1),
(100017, 'Honda Civic 2014', 'Motor 1.8, excelente andar.', 13500, 'https://example.com/images/car17.jpg', 'Honda', 'Civic', 2014, GETDATE(), GETDATE(), 1),
(100018, 'Nissan Sentra 2016', 'GNC recién instalado, ahorrativo.', 14200, 'https://example.com/images/car18.jpg', 'Nissan', 'Sentra', 2016, GETDATE(), GETDATE(), 1),
(100019, 'Toyota Corolla 2013', 'Pintura original, sin choques.', 11900, 'https://example.com/images/car19.jpg', 'Toyota', 'Corolla', 2013, GETDATE(), GETDATE(), 1),
(100020, 'Ford Focus 2019', 'Interior como nuevo, muy cuidado.', 19900, 'https://example.com/images/car20.jpg', 'Ford', 'Focus', 2019, GETDATE(), GETDATE(), 1),

-- 30 más para completar
(100021, 'Volkswagen Golf 2012', 'Buen mantenimiento, listo para transferir.', 11000, 'https://example.com/images/car21.jpg', 'Volkswagen', 'Golf', 2012, GETDATE(), GETDATE(), 1),
(100022, 'Chevrolet Cruze 2018', 'Sin detalles, recién alineado.', 16800, 'https://example.com/images/car22.jpg', 'Chevrolet', 'Cruze', 2018, GETDATE(), GETDATE(), 1),
(100023, 'Honda Civic 2016', 'Con techo solar, excelente manejo.', 17500, 'https://example.com/images/car23.jpg', 'Honda', 'Civic', 2016, GETDATE(), GETDATE(), 1),
(100024, 'Nissan Sentra 2017', 'Tapizados nuevos, buen motor.', 15900, 'https://example.com/images/car24.jpg', 'Nissan', 'Sentra', 2017, GETDATE(), GETDATE(), 2),
(100025, 'Toyota Corolla 2010', 'Económico y confiable.', 9500, 'https://example.com/images/car25.jpg', 'Toyota', 'Corolla', 2010, GETDATE(), GETDATE(), 2),
(100026, 'Ford Focus 2011', 'Con detalles de uso, funciona bien.', 8700, 'https://example.com/images/car26.jpg', 'Ford', 'Focus', 2011, GETDATE(), GETDATE(), 1),
(100027, 'Volkswagen Golf 2018', 'Versión GTI, muy potente.', 25800, 'https://example.com/images/car27.jpg', 'Volkswagen', 'Golf', 2018, GETDATE(), GETDATE(), 2),
(100028, 'Chevrolet Cruze 2019', 'Con cámara trasera y sensores.', 22500, 'https://example.com/images/car28.jpg', 'Chevrolet', 'Cruze', 2019, GETDATE(), GETDATE(), 1),
(100029, 'Honda Civic 2020', 'Nuevo, con todos los papeles.', 27500, 'https://example.com/images/car29.jpg', 'Honda', 'Civic', 2020, GETDATE(), GETDATE(), 1),
(100030, 'Nissan Sentra 2013', 'Buen estado general.', 12500, 'https://example.com/images/car30.jpg', 'Nissan', 'Sentra', 2013, GETDATE(), GETDATE(), 2),

(100031, 'Toyota Corolla 2016', 'Versión SEG automática.', 16500, 'https://example.com/images/car31.jpg', 'Toyota', 'Corolla', 2016, GETDATE(), GETDATE(), 1),
(100032, 'Ford Focus 2017', 'Interior cuidado, aire y dirección.', 15800, 'https://example.com/images/car32.jpg', 'Ford', 'Focus', 2017, GETDATE(), GETDATE(), 1),
(100033, 'Volkswagen Golf 2013', 'Motor 1.6 TDI, muy económico.', 13200, 'https://example.com/images/car33.jpg', 'Volkswagen', 'Golf', 2013, GETDATE(), GETDATE(), 2),
(100034, 'Chevrolet Cruze 2015', 'Con gnc y cubiertas nuevas.', 14900, 'https://example.com/images/car34.jpg', 'Chevrolet', 'Cruze', 2015, GETDATE(), GETDATE(), 2),
(100035, 'Honda Civic 2011', 'Con detalles estéticos, buen motor.', 10200, 'https://example.com/images/car35.jpg', 'Honda', 'Civic', 2011, GETDATE(), GETDATE(), 2),
(100036, 'Nissan Sentra 2019', 'Versión Advance.', 20500, 'https://example.com/images/car36.jpg', 'Nissan', 'Sentra', 2019, GETDATE(), GETDATE(), 1),
(100037, 'Toyota Corolla 2018', 'Pocos kilómetros.', 18500, 'https://example.com/images/car37.jpg', 'Toyota', 'Corolla', 2018, GETDATE(), GETDATE(), 1),
(100038, 'Ford Focus 2013', 'Buen auto para ciudad.', 12100, 'https://example.com/images/car38.jpg', 'Ford', 'Focus', 2013, GETDATE(), GETDATE(), 1),
(100039, 'Volkswagen Golf 2010', 'Modelo básico.', 8900, 'https://example.com/images/car39.jpg', 'Volkswagen', 'Golf', 2010, GETDATE(), GETDATE(), 1),
(100040, 'Chevrolet Cruze 2016', 'Con gnc de 5ta generación.', 14300, 'https://example.com/images/car40.jpg', 'Chevrolet', 'Cruze', 2016, GETDATE(), GETDATE(), 1),

-- Últimos 10
(100041, 'Honda Civic 2013', 'Tapizado nuevo, dirección hidráulica.', 11700, 'https://example.com/images/car41.jpg', 'Honda', 'Civic', 2013, GETDATE(), GETDATE(), 1),
(100042, 'Nissan Sentra 2011', 'Muy económico.', 9800, 'https://example.com/images/car42.jpg', 'Nissan', 'Sentra', 2011, GETDATE(), GETDATE(), 1),
(100043, 'Toyota Corolla 2014', 'Automático, aire y dirección.', 13500, 'https://example.com/images/car43.jpg', 'Toyota', 'Corolla', 2014, GETDATE(), GETDATE(), 1),
(100044, 'Ford Focus 2010', 'Funciona bien.', 8200, 'https://example.com/images/car44.jpg', 'Ford', 'Focus', 2010, GETDATE(), GETDATE(), 1),
(100045, 'Volkswagen Golf 2017', 'Version Trendline.', 17200, 'https://example.com/images/car45.jpg', 'Volkswagen', 'Golf', 2017, GETDATE(), GETDATE(), 2),
(100046, 'Chevrolet Cruze 2021', 'Auto nuevo.', 26500, 'https://example.com/images/car46.jpg', 'Chevrolet', 'Cruze', 2021, GETDATE(), GETDATE(), 2),
(100047, 'Honda Civic 2015', 'Versión EX.', 15500, 'https://example.com/images/car47.jpg', 'Honda', 'Civic', 2015, GETDATE(), GETDATE(), 2),
(100048, 'Nissan Sentra 2012', 'Con cubiertas nuevas.', 10500, 'https://example.com/images/car48.jpg', 'Nissan', 'Sentra', 2012, GETDATE(), GETDATE(), 2),
(100049, 'Toyota Corolla 2021', 'Tope de gama.', 27500, 'https://example.com/images/car49.jpg', 'Toyota', 'Corolla', 2021, GETDATE(), GETDATE(), 2),
(100050, 'Ford Focus 2016', 'Buen estado.', 14200, 'https://example.com/images/car50.jpg', 'Ford', 'Focus', 2016, GETDATE(), GETDATE(), 2);*/



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
 
 -- Ejemplo de Joselo

 -- Paginacion para usuarios
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

 -- Modificacion
/*ALTER PROCEDURE getUserPagination 
    @PageIndex INT = 1,
    @PageSize INT = 10
AS
BEGIN
    DECLARE @Offset INT = (@PageSize * (@PageIndex - 1));

    SELECT
        u.id AS Id,
        u.dni AS Dni,
        u.first_name AS FirstName,
        u.last_name AS LastName,
        u.email AS Email,
        u.[password] AS Password,
        TotalFilas = COUNT(*) OVER()
    FROM tbl_user u
    ORDER BY u.id ASC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY
END*/

-- Ejecucion de prueba
 --EXEC getUserPagination @PageIndex = 1, @PageSize = 15;

 -- Paginacion para publicaciones
 /* CREATE PROCEDURE getPublicationPagination 
    @PageIndex INT = 1,
    @PageSize INT = 10
AS
BEGIN
    DECLARE @Offset INT = (@PageSize * (@PageIndex - 1));

    SELECT
        p.id AS Id,
        p.code_publication AS Code,
        p.Title AS Title,
        p.[Description] AS Description,
        p.Price AS Price,
        p.image_url AS ImageUrl,
		p.Brand AS Brand,
        p.Model AS Model,
        p.Year AS Year,
		p.create_at AS createAt,
        p.updated_at AS updateAt,
        p.user_id AS userId,
        TotalFilas = COUNT(*) OVER()
    FROM tbl_publication p
    ORDER BY p.id ASC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY
END*/

-- EXEC getPublicationPagination @PageIndex = 3, @PageSize = 15;