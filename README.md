# 🧠 Módulo de Mantenimiento de Trabajadores (.NET Core)

Este proyecto fue desarrollado como parte de la prueba técnica para el puesto de **Analista Programador .NET** en **MYPER Software**. El objetivo fue construir un módulo web funcional para el mantenimiento de trabajadores, cumpliendo con los lineamientos técnicos y funcionales establecidos.

---

## 🛠️ Tecnologías utilizadas

- ASP.NET Core 8
- Entity Framework Core (Database First con procedimientos almacenados)
- SQL Server
- Bootstrap 5
- jQuery / AJAX
- HTML5 / Razor Views

---

## 🧩 Arquitectura del proyecto

- **Modelo:** `Trabajador` representa la entidad principal.
- **DbContext:** `TrabajadoresDbContext` gestiona la conexión y ejecuta procedimientos como `sp_ListarTrabajadores`.
- **Controladores:** `TrabajadorController` maneja las acciones de CRUD.
- **Vistas:** Razor Views con modales para registro y edición.
- **Procedimientos almacenados:** usados para listar, registrar, editar y eliminar trabajadores.

---

## 📷 Funcionalidades implementadas

- Listado de trabajadores con filtro por sexo
- Registro de nuevo trabajador (modal + imagen)
- Edición de trabajador (modal + imagen)
- Eliminación con confirmación
- Carga y previsualización de imagen
- Validaciones en frontend y backend
- Colores por sexo (azul: masculino, naranja: femenino)

---

## 📦 Entregables

- `TrabajadoresPrueba.sql`: script de base de datos
- `QA_Validacion_MYPER.pdf`: documento con casos de prueba y evidencias
- [Video Loom](https://loom.com/tu-enlace): explicación técnica y demostración
- [Repositorio GitHub](https://github.com/tu-repo): código fuente con control de versiones

---

## 🧪 Validación y QA

Se realizaron pruebas funcionales para:

- Registro, edición y eliminación
- Filtro por sexo
- Carga de imagen
- Validaciones de campos

Se documentaron los resultados en el archivo `QA_Validacion_MYPER.pdf` con capturas y observaciones.

---

## 🎓 Lecciones aprendidas

- Uso eficiente de procedimientos almacenados con EF Core
- Integración de formularios modales con AJAX
- Validación de imágenes y rutas en entorno web
- Separación clara de responsabilidades en capas

---

## 🚀 Cómo ejecutar

1. Restaurar paquetes NuGet
2. Configurar cadena de conexión en `appsettings.json`
3. Ejecutar el script `TrabajadoresPrueba.sql` en SQL Server
4. Ejecutar el proyecto en Visual Studio o VS Code

---

## 📬 Autor

Desarrollado por **Jose Luis Guzman Arias**  
[LinkedIn](https://www.linkedin.com/in/joseluisguzmanarias/) | [Correo](mailto:jl_guzman_arias@hotmail.com)

---

## 📄 Licencia

Este proyecto fue desarrollado exclusivamente para fines de evaluación técnica.
