
-- 1. CREACIÓN DE LA BASE DE DATOS
USE master;
GO

-- Si la base de datos ya existe, cierra conexiones y la borra para empezar limpio
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'RepairShopDB')
BEGIN
    ALTER DATABASE RepairShopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE RepairShopDB;
END
GO

CREATE DATABASE RepairShopDB;
GO

USE RepairShopDB;
GO

-- 2. CREACIÓN DE TABLAS CATÁLOGO

CREATE TABLE TipoEquipo (
    IdTipoEquipo INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Marca (
    IdMarca INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE TipoFalla (
    IdTipoFalla INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Estado (
    IdEstado INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(150) NOT NULL,
    Telefono VARCHAR(50),
    Email VARCHAR(150)
);

CREATE TABLE Tecnico (
    IdTecnico INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(50),
    Especialidad VARCHAR(100),
    Email VARCHAR(150)
);

-- Tabla Usuario ajustada exactamente a tu DbContext y al frontend
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Usuario VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Rol VARCHAR(20) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    IdTecnico INT NULL,
    CONSTRAINT FK_Usuario_Tecnico FOREIGN KEY (IdTecnico) REFERENCES Tecnico(IdTecnico)
);

-- 3. CREACIÓN DE TABLAS TRANSACCIONALES

CREATE TABLE Equipo (
    IdEquipo INT PRIMARY KEY IDENTITY(1,1),
    IdTipoEquipo INT NOT NULL,
    IdMarca INT NOT NULL,
    Modelo VARCHAR(150) NOT NULL,
    CONSTRAINT FK_Equipo_TipoEquipo FOREIGN KEY (IdTipoEquipo) REFERENCES TipoEquipo(IdTipoEquipo),
    CONSTRAINT FK_Equipo_Marca FOREIGN KEY (IdMarca) REFERENCES Marca(IdMarca)
);

CREATE TABLE OrdenReparacion (
    IdOrden INT PRIMARY KEY IDENTITY(1,1),
    NumeroServicio VARCHAR(50) NOT NULL UNIQUE,
    FechaIngreso DATETIME NOT NULL DEFAULT GETDATE(),
    IdCliente INT NOT NULL,
    IdEquipo INT NOT NULL,
    IdTecnico INT NOT NULL,
    IdEstadoActual INT NOT NULL,
    IdTipoFalla INT NULL,
    DescripcionFalla VARCHAR(500),
    CONSTRAINT FK_Orden_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    CONSTRAINT FK_Orden_Equipo FOREIGN KEY (IdEquipo) REFERENCES Equipo(IdEquipo),
    CONSTRAINT FK_Orden_Tecnico FOREIGN KEY (IdTecnico) REFERENCES Tecnico(IdTecnico),
    CONSTRAINT FK_Orden_Estado FOREIGN KEY (IdEstadoActual) REFERENCES Estado(IdEstado),
    CONSTRAINT FK_Orden_TipoFalla FOREIGN KEY (IdTipoFalla) REFERENCES TipoFalla(IdTipoFalla)
);

CREATE TABLE HistorialEstado (
    IdHistorial INT PRIMARY KEY IDENTITY(1,1),
    IdOrden INT NOT NULL,
    IdEstado INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Observaciones VARCHAR(500),
    CONSTRAINT FK_Historial_Orden FOREIGN KEY (IdOrden) REFERENCES OrdenReparacion(IdOrden),
    CONSTRAINT FK_Historial_Estado FOREIGN KEY (IdEstado) REFERENCES Estado(IdEstado)
);

-- 4. ÍNDICES (Extraídos exactamente del DbContext)
CREATE INDEX IX_Orden_Cliente ON OrdenReparacion(IdCliente);
CREATE INDEX IX_Orden_Tecnico ON OrdenReparacion(IdTecnico);
CREATE INDEX IX_Orden_Estado ON OrdenReparacion(IdEstadoActual);

-- 5. INSERCIÓN DE DATOS DE PRUEBA (SEED DATA)

INSERT INTO Estado (Nombre) VALUES 
('Ingresado'), ('En Diagnóstico'), ('En Reparación'), ('Esperando Repuesto'), ('Reparado'), ('Entregado'), ('Cancelado');

INSERT INTO TipoEquipo (Nombre) VALUES 
('Notebook'), ('PC Escritorio'), ('Impresora'), ('Monitor'), ('Servidor');

INSERT INTO Marca (Nombre) VALUES 
('HP'), ('Dell'), ('Lenovo'), ('Asus'), ('Apple');

INSERT INTO TipoFalla (Nombre) VALUES 
('No enciende'), ('Pantalla dañada'), ('Problema de software'), ('Sobrecalentamiento'), ('Fuente dañada');

INSERT INTO Cliente (Nombre, Telefono, Email) VALUES 
('Juan Perez', '1122334455', 'juan.perez@email.com'),
('Maria Gomez', '1155443322', 'maria.gomez@email.com');

INSERT INTO Tecnico (Nombre, Telefono, Especialidad, Email) VALUES 
('Jose Suarez', '1199887766', 'Hardware y Redes', 'admin@reparaciones.com');

-- Usuario admin vinculado al técnico. 
-- Acá ya guardamos el hash válido de BCrypt para la contraseña '123456'
INSERT INTO Usuario (Usuario, PasswordHash, Nombre, Rol, Activo, IdTecnico) VALUES 
('admin', '$2a$10$gTYwCN66/tBRoCr3.TXa1.v1iyvwIF7GRBqxzv7G.AHLMt/owXrp.', 'admin', 'Admin', 1, 1);

INSERT INTO Equipo (IdTipoEquipo, IdMarca, Modelo) VALUES 
(1, 3, 'ThinkPad T14'),
(2, 2, 'OptiPlex 7090');

INSERT INTO OrdenReparacion (NumeroServicio, IdCliente, IdEquipo, IdTecnico, IdEstadoActual, IdTipoFalla, DescripcionFalla) VALUES 
('ORD-2026-0001', 1, 1, 1, 1, 1, 'El equipo no da video al presionar el botón de encendido.');
GO