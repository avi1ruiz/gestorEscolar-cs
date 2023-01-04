CREATE DATABASE Escuela;
GO
USE Escuela;
GO
CREATE TABLE materias(
    materia_id INT PRIMARY KEY IDENTITY (1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE,
    estado BIT NOT NULL  
);
GO
CREATE TABLE maestros(
    maestro_id INT PRIMARY KEY IDENTITY (1,1),
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50)  NOT NULL,
    telefono VARCHAR(10) NOT NULL,
    correo VARCHAR(50) NOT NULL,
    materia_id INT FOREIGN KEY REFERENCES materias(materia_id)
);
GO
CREATE TABLE alumnos(
    alumno_id INT PRIMARY KEY IDENTITY(1,1),
    no_control INT NOT NULL UNIQUE,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    correo VARCHAR(50) NOT NULL,
    genero VARCHAR(9) NOT NULL,
    fecha_nacimiento DATETIME NOT NULL 
);
GO
SET DATEFORMAT dmy;
GO
CREATE TABLE calificaciones(
    calificacion_id INT PRIMARY KEY IDENTITY(1,1),
    parcial_uno DECIMAL,
    parcial_dos DECIMAL,
    parcial_tres DECIMAL,
	final DECIMAL
);
GO
CREATE TABLE kardexs(
	id INT PRIMARY KEY IDENTITY(1,1),
    materia_id INT FOREIGN KEY REFERENCES materias(materia_id),
    no_control INT FOREIGN KEY REFERENCES alumnos(no_control),
    calificacion_id INT FOREIGN KEY REFERENCES calificaciones(calificacion_id)
);



