# Documentación de Interfaces y Repositorios - StoreManagement

Este documento describe detalladamente las interfaces y repositorios implementados en la capa **Infrastructure** del proyecto **StoreManagement**. Cada repositorio define las operaciones **CRUD (Create, Read, Update, Delete)** para las entidades del dominio, siguiendo los principios de la arquitectura **DDD (Domain-Driven Design)**.

El propósito de esta documentación es proporcionar una guía clara a los desarrolladores que trabajarán en las capas superiores (Application, API o Presentation), para entender la lógica y el flujo de acceso a datos.

---

## ICustomerRepository y CustomerRepository

### Descripción
La interfaz `ICustomerRepository<T>` define las operaciones relacionadas con la entidad **Customer**. Contiene métodos asincrónicos para obtener, crear, actualizar y eliminar clientes.

### Métodos Principales

- **GetAllAsync()**: Retorna todos los clientes registrados.
- **GetByIDAsync(int? customerId)**: Busca un cliente por su identificador.
- **CreateAsync(Customer customer)**: Crea un nuevo registro de cliente.
- **UpdateAsync(Customer customer)**: Actualiza un cliente existente.
- **DeleteAsync(Customer customer)**: Elimina un cliente existente.

### Implementación
La clase `CustomerRepository` implementa esta interfaz usando **Entity Framework Core**. Cada operación ejecuta una acción sobre el contexto de base de datos `AppDbContext`.

---

## IOrderDetailsRepository y OrderDetailsRepository

### Descripción
La interfaz `IOrderDetailsRepository<T>` gestiona las operaciones de la entidad **OrderDetails**. Incluye métodos para obtener detalles individuales o asociados a un pedido.

### Métodos Principales

- **GetAllAsync()**: Obtiene todos los detalles de pedido, incluyendo la información del pedido y producto asociado.
- **GetByIdAsync(int id)**: Obtiene un detalle específico por ID.
- **GetByOrderIdAsync(int orderId)**: Obtiene todos los detalles de un pedido específico.
- **AddAsync(T orderDetails)**: Agrega un nuevo detalle de pedido.
- **UpdateAsync(T orderDetails)**: Actualiza un detalle de pedido existente.
- **DeleteAsync(T orderDetails)**: Elimina un detalle de pedido.

### Implementación
La clase `OrderDetailsRepository` usa `Include()` para traer relaciones con **Order** y **Product**, garantizando la carga de datos relacionados.

---

## IOrderRepository y OrderRepository

### Descripción
La interfaz `IOrderRepository<T>` define la gestión de las órdenes de compra (**Orders**).

### Métodos Principales

- **GetAllAsync()**: Retorna todas las órdenes.
- **GetByIdAsync(int? id)**: Retorna una orden con su cliente y estado asociados.
- **CreateAsync(T order)**: Crea una nueva orden.
- **UpdateAsync(T order)**: Actualiza una orden existente.
- **DeleteAsync(T order)**: Elimina una orden.
- **CustomerExistAsync(int? id)**: Valida si un cliente existe antes de asociarlo a una orden.
- **GetByCustomerIdAsync(int? customerId)**: Obtiene todas las órdenes de un cliente.

### Implementación
La clase `OrderRepository` realiza operaciones con carga relacional (`Include` en **Customer** y **OrderStatus**) y métodos de validación previos a la creación o actualización.

---

## IOrderStatusRepository y OrderStatusRepository

### Descripción
La interfaz `IOrderStatusRepository<T>` gestiona los estados de las órdenes.

### Métodos Principales

- **GetAllAsync()**: Obtiene todos los estados disponibles (Pendiente, Enviado, Cancelado, etc.).
- **GetByIDAsync(int? orderStatusId)**: Busca un estado específico por su identificador.

### Implementación
El repositorio `OrderStatusRepository` ofrece consultas simples sobre la tabla **OrderStatuses**, sin operaciones de creación o modificación, ya que los estados suelen ser predefinidos.

---

## IProductRepository y ProductRepository

### Descripción
La interfaz `IProductRepository<T>` define las operaciones para la entidad **Product**.

### Métodos Principales

- **GetAllAsync()**: Lista todos los productos.
- **GetByIDAsync(int? productId)**: Obtiene un producto específico.
- **CreateAsync(T product)**: Crea un nuevo producto.
- **UpdateAsync(T product)**: Actualiza un producto existente.
- **DeleteAsync(T product)**: Elimina un producto.

### Implementación
El repositorio `ProductRepository` utiliza `AppDbContext` y **Entity Framework Core** para manipular los productos en la base de datos, siguiendo una lógica estándar de persistencia.

---

## Conclusión

La implementación de estos repositorios permite **desacoplar la lógica de acceso a datos** del resto del sistema, facilitando la mantenibilidad, pruebas unitarias y escalabilidad del proyecto.

Cada interfaz define un **contrato claro** que las capas superiores pueden consumir sin preocuparse por los detalles internos de persistencia.

Esta documentación debe servir de base para que los desarrolladores que trabajen en la capa **Application** puedan construir servicios y casos de uso basados en estos repositorios.