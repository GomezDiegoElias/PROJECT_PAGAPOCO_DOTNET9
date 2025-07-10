<p align="center">
  <img src="./pagapoco-banner.jpg" alt="PAGAPOCO Banner" width="600"/>
</p>

# Gestor de Presupuestos - Fullstack (API + SPA)

## Descripción General
Este proyecto es un sistema completo para la gestión de presupuestos de servicios automotrices, desarrollado bajo el patrón MVC para el backend (API) y una SPA moderna en React para el frontend. Permite registrar usuarios, iniciar sesión, crear, editar, listar y eliminar presupuestos, así como gestionar sesiones y recuperación de contraseña.

---

## Estructura del Proyecto

```
/ (root)
├── api/                # (Backend, fuera de este repo, ver sección API)
├── src/                # Frontend SPA React
│   ├── api/            # Lógica de conexión a la API (axios, react-query)
│   ├── components/     # Componentes reutilizables (Modal, Badge, Button, etc.)
│   ├── context/        # Contexto global de presupuestos
│   ├── pages/          # Vistas principales (Login, Registro, Perfil, Listado, Formulario, etc.)
│   └── assets/         # Imágenes y recursos
├── public/             # Archivos estáticos
├── package.json        # Dependencias y scripts
├── vite.config.js      # Configuración de Vite
└── README.md           # (Este archivo)
```

---

## Tecnologías Utilizadas

### Frontend (SPA)
- **React 19**
- **Vite**
- **React Router DOM** (ruteo)
- **React Query** (manejo de datos remotos)
- **Axios** (peticiones HTTP)
- **React Icons** (íconos visuales)
- **qrcode.react** (QR para login)
- **CSS moderno y componentes reutilizables**

### Backend (API - Ejemplo típico MVC)
- **Node.js + Express**
- **Controladores, modelos y rutas**
- **JWT para autenticación**
- **Persistencia en base de datos (ej: MongoDB, MySQL, etc.)**

---

## Funcionalidades Principales

### SPA (Frontend)
- Registro y login de usuarios
- Recuperación de contraseña
- Listado paginado de presupuestos
- Alta, edición y eliminación de presupuestos
- Modal de confirmación y éxito para todas las acciones importantes
- Badges para estados y trabajos
- Botones modernos con íconos
- QR para login rápido en otros dispositivos
- Responsive y experiencia de usuario moderna

### API (Backend)
- Endpoints RESTful para usuarios y presupuestos
- Autenticación y autorización con JWT
- Validación de datos y manejo de errores
- Rutas protegidas para operaciones sensibles

---

## Estructura MVC (API)
- **Modelos:** Definen la estructura de los datos (Usuario, Presupuesto)
- **Controladores:** Lógica de negocio (crear, editar, eliminar, autenticar)
- **Rutas:** Definen los endpoints y conectan con los controladores

---

## Instalación y Ejecución

### 1. Clonar el repositorio
```
git clone <repo-url>
```

### 2. Instalar dependencias del frontend
```
cd SPA-progIII
npm install
```

### 3. Configurar y ejecutar el backend (API)
- Ver README específico en la carpeta `/api` (no incluido aquí)
- Configurar variables de entorno (puerto, DB, JWT, etc.)
- Ejecutar: `npm run dev` o similar

### 4. Ejecutar el frontend
```
npm run dev
```

La SPA estará disponible en `http://localhost:5173` (o el puerto que indique Vite).

---

## Notas y Mejores Prácticas
- El frontend está desacoplado del backend: puedes cambiar la URL de la API en `src/api/axios.js`.
- El sistema de modales y feedback visual está centralizado y es reutilizable.
- El código es fácilmente extensible para agregar nuevas entidades o funcionalidades.

---

## Créditos y Licencia
Desarrollado por [Tu Nombre].
Licencia MIT.

---

## Contacto
Para dudas o mejoras, contacta a [geymomati@gmail.com o gomezdiegoelias1@gmail.com].