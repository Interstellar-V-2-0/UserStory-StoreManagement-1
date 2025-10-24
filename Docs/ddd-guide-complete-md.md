# Taller Célula – Sistema de Gestión de Clientes y Pedidos

---

## 1. Introducción y Objetivo

Este documento detalla la arquitectura basada en **Domain Driven Design (DDD)** aplicada al desarrollo de un sistema de gestión de clientes y pedidos en **.NET 8**, con **Entity Framework Core**.

El objetivo es proporcionar una guía clara para el equipo de desarrollo, fomentando un flujo de trabajo ordenado, mantenible y escalable.

### Contexto del Negocio

La empresa necesita un sistema para administrar clientes y los pedidos que realizan.

- Cada pedido puede contener varios productos, fecha de creación y estado (Pendiente, Enviado, Cancelado).

---

## 2. Arquitectura DDD y Capas

### Concepto

**DDD (Domain Driven Design)** busca que el software refleje la realidad del negocio, separando responsabilidades en capas bien definidas.

### Capas Principales

**Domain**: Entidades, Value Objects, eventos de dominio, reglas de negocio puras.

**Application**: Servicios de aplicación que coordinan la lógica de negocio, usando repositorios e interfaces.

**Infrastructure**: Implementación de acceso a datos, repositorios, servicios externos, logs.

**Api**: Controladores que exponen los endpoints REST, validan datos y delegan a la capa Application.

### Analogía

- **Domain** → Reglamento interno de la empresa
- **Application** → Gerentes que aplican las reglas
- **Infrastructure** → Herramientas y recursos
- **Api** → Recepcionistas que interactúan con los clientes

---

## 3. Estructura de Carpetas del Proyecto

```
StoreManagement/
│
├─ StoreManagement.Api/                    # Controladores y configuración de API
│   ├─ Controllers/
│   │   ├─ CustomerController.cs
│   │   ├─ OrderController.cs
│   │   └─ OrderDetailController.cs
│   └─ Program.cs
│
├─ StoreManagement.Application/            # Servicios de aplicación (lógica de negocio)
│   ├─ Interfaces/
│   │   ├─ ICustomerService.cs
│   │   ├─ IOrderService.cs
│   │   └─ IOrderDetailService.cs
│   └─ Services/
│       ├─ CustomerService.cs
│       ├─ OrderService.cs
│       └─ OrderDetailService.cs
│
├─ StoreManagement.Domain/                 # Entidades del dominio y reglas de negocio
│   ├─ Entities/
│   │   ├─ Customer.cs
│   │   ├─ Order.cs
│   │   └─ OrderDetail.cs
│   ├─ ValueObjects/                       # Objetos de valor (opcional)
│   ├─ Enums/                              # Enumeraciones de estado, etc.
│   └─ Interfaces/                         # Contratos de repositorios (DDD puro)
│       ├─ ICustomerRepository.cs
│       ├─ IOrderRepository.cs
│       └─ IOrderDetailRepository.cs
│
├─ StoreManagement.Infrastructure/         # Implementaciones de repositorios y DbContext
│   ├─ Data/
│   │   └─ StoreManagementDbContext.cs
│   └─ Repositories/
│       ├─ CustomerRepository.cs          # Implementa ICustomerRepository
│       ├─ OrderRepository.cs             # Implementa IOrderRepository
│       └─ OrderDetailRepository.cs       # Implementa IOrderDetailRepository
│
└─ StoreManagement.sln
```

### Detalles por Carpeta

**Api:**
- Contiene los controladores, que exponen los endpoints REST.
- No deben contener lógica de negocio; solo delegan a los servicios.
- Ejemplo: `CustomerController` con métodos GET, POST, PUT, DELETE.

**Application:**
- Contiene los servicios que implementan la lógica de negocio.
- Usa interfaces de repositorios para comunicarse con la infraestructura.
- Ejemplo: `OrderService` valida que un pedido no se pueda enviar si está cancelado.

**Domain:**
- Contiene entidades, value objects, enumeraciones y reglas de negocio puras.
- **Contiene las interfaces de repositorios** (contratos del dominio).
- Ejemplo: `OrderStatus` enum → Pendiente, Enviado, Cancelado.
- Ejemplo: `ICustomerRepository` define QUÉ operaciones necesita el dominio.

**Infrastructure:**
- **Implementa las interfaces de repositorios** definidas en Domain.
- Contiene DbContext para EF Core, conexión a la base de datos y migraciones.
- Ejemplo: `CustomerRepository` implementa `ICustomerRepository` usando LINQ y EF.

---

## 4. Entidades y Value Objects (Domain)

### Entidades

Representan los objetos principales del negocio con identidad propia.

- **Customer**: Id, Name, Email
- **Order**: Id, CustomerId, OrderDate, Status (Pendiente, Enviado, Cancelado)
- **OrderDetail**: Id, OrderId, ProductName, Quantity, UnitPrice

### Value Objects

Objetos que representan atributos sin identidad propia, como una dirección o un monto.

### Analogía Restaurante

- **Customer** → cliente
- **Order** → pedido
- **OrderDetail** → plato específico
- **Value Object** → ingredientes de un plato

---

## 5. Interfaces y Repositorios

### Interfaces (Domain)

Definen contratos que los repositorios deben cumplir, permitiendo desacoplar la lógica de negocio de la implementación de datos.

**Ubicación:** Las interfaces van en la capa **Domain** siguiendo el principio de **Inversión de Dependencias**. El dominio define QUÉ necesita, Infrastructure define CÓMO se implementa.

- **ICustomerRepository**: GetAll, GetById, Add, Update, Delete
- **IOrderRepository**: métodos para manejar pedidos
- **IOrderDetailRepository**: métodos para detalles de pedidos

### Repositorios (Infrastructure)

Implementan estas interfaces usando Entity Framework Core, ocultando la complejidad del acceso a datos.

**Ubicación:** Las implementaciones van en la capa **Infrastructure** porque contienen detalles técnicos de persistencia.

### Analogía

- **Interfaz (Domain)** → instrucciones de qué hacer
- **Repositorio (Infrastructure)** → empleado que sabe cómo hacerlo

### Principio de Inversión de Dependencias

```
Domain (define interfaces)
   ↑
   | Infrastructure depende de Domain
   |
Infrastructure (implementa interfaces)
```

Esto permite que el dominio no dependa de detalles técnicos, y que Infrastructure pueda cambiar sin afectar el dominio.

---

## 6. Servicios de Aplicación (Application)

### Concepto

Coordinan la lógica de negocio sin manejar directamente la base de datos.

**Ejemplo:**

`OrderService` usa `OrderRepository` y `OrderDetailRepository` para crear un pedido, validar reglas y calcular totales.

### Patrones Aplicados

- **Dependency Injection (DI)**: Los servicios reciben los repositorios como dependencias, evitando acoplamiento.
- **Single Responsibility (SRP) / SoC**: Cada servicio tiene una única responsabilidad.
- **Repository Pattern**: Acceso a datos encapsulado y desacoplado de la lógica de negocio.

### Analogía

Gerentes que toman decisiones aplicando las políticas del negocio, sin realizar el trabajo físico (eso lo hacen los repositorios).

---

## 7. Controladores (API)

### Concepto

Punto de entrada a la aplicación. Reciben las solicitudes HTTP, validan datos y llaman a los servicios.

### Ejemplo: CustomerController

- **GET** `/api/customers` → lista clientes
- **POST** `/api/customers` → crea cliente
- **PUT** `/api/customers/{id}` → actualiza cliente

### Analogía

Recepcionistas que reciben solicitudes y las envían al gerente correspondiente (servicios).

---

## 8. Patrones de Diseño y Principios

### Patrones Utilizados

**Dependency Injection (DI)**: Inyecta dependencias en lugar de crearlas dentro de la clase, mejorando testabilidad.

**Repository Pattern**: Separa la lógica de acceso a datos de la lógica de negocio.

**Unit of Work**: Coordina la escritura de cambios en la base de datos como una sola transacción.

### Principios SOLID Aplicados

**Single Responsibility Principle (SRP)**: Cada clase tiene una única responsabilidad.

**Separation of Concerns (SoC)**: Separación clara de lógica de negocio, presentación y acceso a datos.

**Inversión de Dependencias**: Las capas superiores dependen de abstracciones (interfaces), no de implementaciones concretas.

---

## 9. Flujo de Datos

### Proceso Completo

1. **Api** recibe la solicitud HTTP.
2. Valida los datos y llama al servicio de **Application** correspondiente.
3. El servicio aplica reglas de negocio y llama al repositorio si necesita persistencia.
4. El repositorio interactúa con **DbContext** para guardar o consultar la base de datos.
5. Los resultados se devuelven al servicio, luego al controlador, y finalmente al cliente HTTP.

### Analogía del Flujo

- **Api** → Recepcionista
- **Application** → Gerente
- **Domain** → Reglamento
- **Infrastructure** → Empleado que maneja el almacén

---

## 10. Entity Framework Core y Base de Datos

### DbContext

Representa la sesión con la base de datos.

### Migraciones

Permiten crear y actualizar el esquema de la base de datos.

### Comandos Clave

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Analogía

EF Core actúa como traductor entre el lenguaje de C# y SQL/base de datos.

---

## 11. Buenas Prácticas

- Controladores sin lógica de negocio
- Servicios usan repositorios e interfaces
- Entidades representan el dominio, no la base de datos
- Mantener independencia de capas
- Usar DI para inyección de dependencias
- Documentar y probar endpoints con Postman

---

## 12. Analogía Final: Equipo de Fútbol

- **Domain** → reglamento
- **Application** → estrategias del entrenador
- **Infrastructure** → estadio y herramientas
- **Api** → partidos donde el público ve los resultados

---

**Fin del Documento**