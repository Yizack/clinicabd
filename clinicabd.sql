USE MASTER
DROP DATABASE clinica

CREATE DATABASE clinica;
USE clinica;

CREATE TABLE empleados(
	ced VARCHAR(14) NOT NULL,
	nombres VARCHAR(20) NOT NULL,
    apellidos VARCHAR(20) NOT NULL,
    cargo VARCHAR(20) NOT NULL,
    direccion VARCHAR(50) NOT NULL,
    telefono VARCHAR(9) NOT NULL,
    salario DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (ced)
);

CREATE TABLE vacantes(
	cod INT NOT NULL,
	empleados INT NOT NULL,
	medicos INT NOT NULL
	PRIMARY KEY (cod)
);

CREATE TABLE pacientes(
	ced VARCHAR(14) NOT NULL,
	nombres VARCHAR(20) NOT NULL,
    apellidos VARCHAR(20) NOT NULL,
    fecha_nac DATE NOT NULL,
    direccion VARCHAR(50) NOT NULL,
    telefono VARCHAR(9) NOT NULL,
	empleado VARCHAR(14) NOT NULL,
    PRIMARY KEY (ced),
	FOREIGN KEY (empleado) REFERENCES empleados(ced)
);

CREATE TABLE especialidades(
	cod INT NOT NULL, -- Auto incrementado
    especialidad VARCHAR(30) NOT NULL,
    PRIMARY KEY (cod)
    
);

CREATE TABLE cod_medicos(
	cod INT NOT NULL, -- Auto incrementado
    nombre VARCHAR(50) NOT NULL,
	cod_especialidad INT NOT NULL,
    PRIMARY KEY (cod),
    FOREIGN KEY (cod_especialidad) REFERENCES especialidades(cod)
);

CREATE TABLE servicio_laboratorio(
	paciente VARCHAR(14) NOT NULL,
	medico INT NOT NULL,
    metodo VARCHAR(30) NOT NULL,
    resultado VARCHAR(30) NOT NULL,
    precio DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (paciente) REFERENCES pacientes(ced),
    FOREIGN KEY (medico) REFERENCES especialidades(cod)
);

CREATE TABLE citas(
	cod INT NOT NULL, -- Auto incrementado
	paciente VARCHAR(14) NOT NULL,
	empleado VARCHAR(14) NOT NULL,
    medico INT NOT NULL,
    especialidad VARCHAR(30) NOT NULL,
    fecha DATETIME NOT NULL,
    sala INT,
    PRIMARY KEY (cod),
    FOREIGN KEY (paciente) REFERENCES pacientes(ced),
    FOREIGN KEY (empleado) REFERENCES empleados(ced)
);

CREATE TABLE tratamientos(
	cita INT,
    medico int,
    descripcion VARCHAR(50), 
    PRIMARY KEY(cita),
    FOREIGN KEY (medico) REFERENCES especialidades(cod),
    FOREIGN KEY (cita) REFERENCES citas(cod)
);
GO

-- Procedimiento para insertar empleados
CREATE PROCEDURE spI_empleados (
	@ced VARCHAR(14),
	@nombres VARCHAR(20),
	@apellidos VARCHAR(20),
	@cargo VARCHAR(20),
	@direccion VARCHAR(50),
	@telefono VARCHAR(9),
	@salario DECIMAL(18,2)
) AS
BEGIN
	INSERT INTO empleados VALUES (@ced, @nombres, @apellidos, @cargo, @direccion, @telefono, @salario);
END
GO

-- Procedimiento para insertar pacientes
CREATE PROCEDURE spI_pacientes (
	@ced VARCHAR(14),
	@nombres VARCHAR(20),
    @apellidos VARCHAR(20),
    @fecha_nac DATE,
    @direccion VARCHAR(50),
    @telefono VARCHAR(9),
	@empleado VARCHAR(14)
) AS
BEGIN
	INSERT INTO pacientes VALUES (@ced, @nombres, @apellidos, @fecha_nac, @direccion, @telefono, @empleado);
END
GO

-- Procedimiento para insertar especialidades de los medicos
CREATE PROCEDURE spI_especialidades (
	@especialidad VARCHAR(30)
) AS
BEGIN
	IF (SELECT MAX(cod) FROM especialidades) IS NOT NULL
		INSERT INTO especialidades VALUES ((SELECT MAX(cod)+1 FROM especialidades), @especialidad);
	ELSE
		BEGIN
			INSERT INTO especialidades VALUES (0, 'Sin especialidad');
			INSERT INTO especialidades VALUES (1, @especialidad);
		END
END
GO

-- Procedimiento para insertar medicos
CREATE PROCEDURE spI_cod_medicos (
	@nombres VARCHAR(50),
	@especialidad INT
) AS
BEGIN
	IF (SELECT MAX(cod) FROM cod_medicos) IS NOT NULL
		INSERT INTO cod_medicos VALUES ((SELECT MAX(cod)+1 FROM cod_medicos), @nombres, @especialidad);
	ELSE
		BEGIN
			INSERT INTO cod_medicos VALUES (0, 'Sin Medico', 0);
			INSERT INTO cod_medicos VALUES (1, @nombres, @especialidad);
		END
END
GO

-- Procedimiento para insertar citas
CREATE PROCEDURE spI_citas(
	@paciente VARCHAR(14),
	@empleado VARCHAR(14),
    @medico INT,
    @fecha DATETIME,
    @sala INT
) AS
BEGIN
	DECLARE @especialidad VARCHAR(30);
	IF @medico = 0
		BEGIN
			SELECT @especialidad = ''
			IF (SELECT MAX(cod) FROM citas) IS NOT NULL
				INSERT INTO citas VALUES ((SELECT MAX(cod)+1 FROM citas), @paciente, @empleado, @medico, @especialidad, @fecha, @sala);
			ELSE
				INSERT INTO citas VALUES (1, @paciente, @empleado, @medico, @especialidad, @fecha, @sala);
		END
	ELSE
		BEGIN
			SELECT @especialidad = (SELECT especialidades.especialidad FROM especialidades INNER JOIN cod_medicos ON especialidades.cod = cod_medicos.cod_especialidad WHERE cod_medicos.cod = @medico);
			IF (SELECT MAX(cod) FROM citas) IS NOT NULL
				INSERT INTO citas VALUES ((SELECT MAX(cod)+1 FROM citas), @paciente, @empleado, @medico, @especialidad, @fecha, @sala);
			ELSE
				INSERT INTO citas VALUES (1, @paciente, @empleado, @medico, @especialidad, @fecha, @sala);
		END
END
GO

-- Procedimiento para insertar tratamientos
CREATE PROCEDURE spI_tratamientos(
	@cita INT,
    @medico INT,
    @descripcion VARCHAR(50)
) AS
BEGIN
	INSERT INTO tratamientos VALUES (@cita, @medico, @descripcion);
END
GO

-- Procedimiento para insertar servicio de laboratorio
CREATE PROCEDURE spI_servicio_laboratorio(
	@paciente VARCHAR(14),
	@medico INT,
    @metodo VARCHAR(30),
    @resultado VARCHAR(30),
    @precio DECIMAL(18,2)
) AS
BEGIN
	INSERT INTO servicio_laboratorio VALUES (@paciente, @medico, @metodo, @resultado, @precio);
END
GO

-- Genera una cita inicial para el paciente que se acaba de registrar
CREATE TRIGGER RegistraPaciente ON pacientes FOR INSERT AS
BEGIN
	DECLARE @paciente VARCHAR(14)
	SELECT @paciente = (SELECT INSERTED.ced FROM INSERTED);

	DECLARE @fecha DATETIME
	SELECT @fecha = (SELECT GETDATE())

	DECLARE @empleado VARCHAR(14)
	SELECT @empleado = (SELECT INSERTED.empleado FROM INSERTED);

	EXEC spI_citas @paciente, @empleado, 0, @fecha, 0;
END
GO

-- Vacantes para empleados
CREATE TRIGGER VacanteEmpleado ON empleados FOR INSERT AS
BEGIN
	DECLARE @Vacantes int
	SELECT @Vacantes = (SELECT empleados FROM vacantes)
	IF @Vacantes = 0
		BEGIN
		RAISERROR('No hay vacantes para mas empleados', 10, 1)
		ROLLBACK TRANSACTION
		END
	ELSE
		BEGIN
			UPDATE vacantes
			SET empleados = empleados - 1
			FROM vacantes
		END
END
GO

-- Vacantes para empleados
CREATE TRIGGER VacanteMedico ON cod_medicos FOR INSERT AS
BEGIN
	DECLARE @Vacantes int
	SELECT @Vacantes = (SELECT medicos FROM vacantes)
	IF @Vacantes = 0
		BEGIN
		RAISERROR('No hay vacantes para mas empleados', 10, 1)
		ROLLBACK TRANSACTION
		END
	ELSE
		BEGIN
			IF (SELECT INSERTED.cod FROM INSERTED) > 0
				BEGIN
					UPDATE vacantes
					SET medicos = medicos - 1
					FROM vacantes
				END
		END
END
GO

-- Procedimiento para insertar cantidad de vacantes
CREATE PROCEDURE spI_vacantes (
	@empleados INT,
	@medicos INT
) AS
BEGIN
	INSERT INTO vacantes VALUES (1, @empleados, @medicos);
END
GO

-- UPDATE EMPLEADO
CREATE PROCEDURE spU_empleados (
	@ced VARCHAR(14),
	@cargo VARCHAR(20),
	@direccion VARCHAR(50),
	@telefono VARCHAR(9),
	@salario DECIMAL(18,2)
)AS
BEGIN
	UPDATE empleados SET cargo = @cargo, direccion = @direccion, telefono = @telefono, salario = @salario WHERE ced = @ced
END
GO

CREATE PROCEDURE spD_empleados (
	@ced VARCHAR(14)
)AS
BEGIN
	DELETE FROM empleados WHERE ced = @ced
END
GO

CREATE PROCEDURE spU_pacientes (
	@ced VARCHAR(14),
	@direccion VARCHAR(50),
	@telefono VARCHAR(9)
)AS
BEGIN
	UPDATE pacientes SET direccion = @direccion, telefono = @telefono WHERE ced = @ced
END
GO

CREATE PROCEDURE spD_medicos (
	@cod INT
)AS
BEGIN
	DELETE FROM cod_medicos WHERE cod = @cod
END
GO

EXEC spI_vacantes 6, 10;

EXEC spI_empleados 'DNI1', 'Ana Luisa', 'Castillo', 'Aseo', 'DIRECCION', 'TELEFONO1', 444.26;
EXEC spI_empleados 'DNI2', 'Aracelys Angélica', 'Baysa López', 'Recepcionista', 'DIRECCION', 'TELEFONO2', 481.28;
EXEC spI_empleados 'DNI3', 'Irma', 'Villareal', 'Administradora', 'DIRECCION', 'TELEFONO3', 400.00;

EXEC spI_especialidades 'Ginecología';
EXEC spI_especialidades 'Pediatría';
EXEC spI_especialidades 'Medicina general';
EXEC spI_especialidades 'Laboratorio Clínico'	;

EXEC spI_cod_medicos 'Niurka Quintero', 1;
EXEC spI_cod_medicos 'Cecimar Berguida', 2;
EXEC spI_cod_medicos 'Enmanuel Tejeira', 3;
EXEC spI_cod_medicos 'Marañón M', 4;

EXEC spI_pacientes 'DNI4', 'Julia', 'Rosas', '1960-05-18', 'DIRECCION', 'TELEFONO1', 'DNI2';
EXEC spI_pacientes 'DNI5', 'Isaias', 'Barba', '2015-12-07', 'DIRECCION', 'TELEFONO2', 'DNI1';
EXEC spI_pacientes 'DNI6', 'Yi Ping', 'Hung', '2007-11-11', 'DIRECCION', 'TELEFONO3', 'DNI1';
EXEC spI_pacientes 'DNI7', 'Josue', 'Pimentel', '2016-05-21', 'DIRECCION', 'TELEFONO4', 'DNI3';

-- EXEC spI_citas 'DNI4', 'DNI2', 1, '2018-07-18 16:30:00', 6;
-- EXEC spI_citas 'DNI5', 'DNI2', 2, '2018-04-30 14:25:00', 8;
-- EXEC spI_citas 'DNI6', 'DNI2', 3, '2018-05-15 10:00:00', 1;

-- EXEC spI_tratamientos 1, 1, 'Mamografía y efectuar un ultrasonido vaginal';
-- EXEC spI_tratamientos 2, 2, 'Nutrición, ejercicios y preparaciones saludables';
-- EXEC spI_tratamientos 3, 3, 'Náuseas, malestar estomacal';

EXEC spI_servicio_laboratorio 'DNI7', 4, 'Solubilidad en buffer fosfato', 'HGB Insoluble', 50;


-- Vistas:
-- 1. Procedimiento para Mostrar Cantidad de citas realizadas hace un mes
	GO
	CREATE PROCEDURE citas_realizadas(
		@formato VARCHAR(5),
		@numero INT
	) AS
	BEGIN
		DECLARE @fecha_ant AS DATETIME
		SELECT @fecha_ant = (
			CASE @formato
				WHEN 'Meses' THEN DATEADD(MM, -@numero, GETDATE())
				WHEN 'Días' THEN DATEADD(dd, -@numero, GETDATE())
				WHEN 'Años' THEN DATEADD(yy, -@numero, GETDATE())
				WHEN 'Semanas' THEN DATEADD(ww, -@numero, GETDATE())		
			END
		)
		SELECT COUNT(cod) AS 'Cantidad' FROM citas WHERE fecha >= @fecha_ant AND medico > 0
	END
	GO

	-- 2. Mostrar Monto total obtenido de todos los servicios de laboratorio
	CREATE PROCEDURE monto_total_laboratorio  AS
	BEGIN
		SELECT
			SUM(precio)
		AS 
			'Total'
		FROM
			servicio_laboratorio;
	END
	GO

	-- 3. Mostrar Nombre, Apellido de todos los pacientes que se ha realizado un laboratorio con su método y resultado
	CREATE PROCEDURE pacietes_laboratorio  AS
	BEGIN
		SELECT
			pacientes.nombres, pacientes.apellidos, servicio_laboratorio.metodo, servicio_laboratorio.resultado
		FROM
			pacientes
		INNER JOIN
			servicio_laboratorio
		ON
			pacientes.ced = servicio_laboratorio.paciente;
	END
	GO

	-- 4. Mostrar Nombre, Apellido de todos los pacientes que tienen una cita de Ginecología y Pediatría con su código de cita
	CREATE PROCEDURE pacientes_cita_ginecologia_pediatria  AS
	BEGIN
		SELECT
			pacientes.nombres, pacientes.apellidos, citas.cod
		FROM
			pacientes
		INNER JOIN
			citas
		ON
			pacientes.ced = citas.paciente
		WHERE
			citas.especialidad LIKE 'Ginecología' OR citas.especialidad LIKE 'Pediatría';
	END
	GO

	-- 5. Mostrar cantidad de pacientes que han sido atendidos para sacar citas por el empleado con cedula
	CREATE PROCEDURE pacientes_citas_empleado (
		@empleado VARCHAR(14)
	)
	AS
	BEGIN
		SELECT
			COUNT(citas.paciente) AS 'Cantidad',
			(SELECT nombres FROM empleados WHERE ced like @empleado) AS 'Nombre',
			(SELECT telefono FROM empleados WHERE ced like @empleado) AS'Teléfono'
		FROM
			empleados
		INNER JOIN
			citas
		ON
			empleados.ced = citas.empleado
		WHERE
			citas.empleado LIKE @empleado
	END
	GO

	-- 6. Mostrar nombre, apellido, cargo y salario de todos los empleados que tengan un salario menor a 450
	CREATE PROCEDURE salario_menor_450  AS
	BEGIN
		SELECT
			nombres, apellidos, cargo, salario
		FROM
			empleados
		WHERE
			salario <= 450;
	END
	GO

	-- 7. Mostrar codigo, nombre, especialidad de los medicos que hayan realizado tratamietos.
	CREATE PROCEDURE medicos_tratamientos  AS
	BEGIN
		SELECT
			especialidades.cod, cod_medicos.nombre, especialidades.especialidad
		FROM
			especialidades inner join cod_medicos
		ON
			especialidades.cod = cod_medicos.cod 
		WHERE
			especialidades.cod IN (SELECT medico FROM tratamientos);
	END
	GO

	-- 8. Mostrar nombres, apellidos, cargo, dirección, telefono de empleados que realicen citas
	CREATE PROCEDURE empleado_citas  AS
	BEGIN
		SELECT
			nombres, apellidos, cargo, direccion, telefono
		FROM
			empleados
		WHERE ced IN (SELECT empleado from citas);
	END
	GO