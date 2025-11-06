USE MASTER;
GO

-- Crear la base de datos
CREATE DATABASE TrabajadoresPrueba;
GO

USE TrabajadoresPrueba;
GO

-- Crear tabla principal
CREATE TABLE Trabajadores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    TipoDocumento NVARCHAR(50) NOT NULL,
    NumeroDocumento NVARCHAR(20) NOT NULL,
    Sexo NVARCHAR(10) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    FotoRuta NVARCHAR(200),
    Direccion NVARCHAR(200),
	Estado bit
);
GO

-- Agregar restricción de unicidad en el número de documento
ALTER TABLE Trabajadores
ADD CONSTRAINT UQ_NumeroDocumento UNIQUE (NumeroDocumento);
GO

-- Crear procedimiento almacenado para listar trabajadores
ALTER PROCEDURE [dbo].[sp_ListarTrabajadores]
AS
BEGIN
    SELECT 
        Id,
        Nombres,
        Apellidos,
        TipoDocumento,
        NumeroDocumento,
        Sexo,
        FechaNacimiento,
        FotoRuta,
        Direccion,
        Estado
    FROM Trabajadores
    WHERE Estado = 1
    ORDER BY Apellidos, Nombres;
END;
GO


CREATE PROCEDURE sp_RegistrarTrabajador
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @TipoDocumento NVARCHAR(50),
    @NumeroDocumento NVARCHAR(20),
    @Sexo NVARCHAR(10),
    @FechaNacimiento DATE,
    @FotoRuta NVARCHAR(200),
    @Direccion NVARCHAR(200),
    @estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Trabajadores (
        Nombres, Apellidos, TipoDocumento, NumeroDocumento,
        Sexo, FechaNacimiento, FotoRuta, Direccion, Estado
    )
    VALUES (
        @Nombres, @Apellidos, @TipoDocumento, @NumeroDocumento,
        @Sexo, @FechaNacimiento, @FotoRuta, @Direccion, @estado
    );

    DECLARE @Id INT = SCOPE_IDENTITY();

    SELECT *
    FROM Trabajadores
    WHERE Id = @Id;
END;
GO


CREATE PROCEDURE sp_EditarTrabajador
    @Id INT,
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @TipoDocumento NVARCHAR(50),
    @NumeroDocumento NVARCHAR(20),
    @Sexo NVARCHAR(10),
    @FechaNacimiento DATE,
    @FotoRuta NVARCHAR(200),
    @Direccion NVARCHAR(200)
AS
BEGIN
    UPDATE Trabajadores
    SET
        Nombres = @Nombres,
        Apellidos = @Apellidos,
        TipoDocumento = @TipoDocumento,
        NumeroDocumento = @NumeroDocumento,
        Sexo = @Sexo,
        FechaNacimiento = @FechaNacimiento,
        FotoRuta = @FotoRuta,
        Direccion = @Direccion
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE sp_DesactivarTrabajador
    @Id INT
AS
BEGIN
    UPDATE Trabajadores
    SET Estado = 0
    WHERE Id = @Id;
END;
GO
