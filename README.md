# DCXAir
Flight management application, using to backend .net and frontend Angular,

## En español

### Introducción

El proyecto DCXAir es una aplicación que ofrece a los usuarios una interfaz fácil de interpretar para realizar búsquedas de rutas y vuelos de manera eficiente. Los usuarios pueden ingresar a los detalles de su viaje, validar los campos necesarios y enviar el formulario para obtener información sobre los vuelos disponibles.

### Backend

El backend del proyecto está implementado en C# utilizando arquitectura limpia en capas. Se emplearon patrones como Mediator y CQRS para una gestión eficiente de las solicitudes y respuestas. Además, se implementó un algoritmo de búsqueda en anchura (BFS) para generar rutas óptimas entre los puntos de origen y destino. El algoritmo utiliza un servicio y su interfaz, así como un diccionario para el manejo de llaves y rutas, asegurando así una búsqueda rápida y eficiente.

### Frontend

El frontend está desarrollado con Angular, proporcionando una experiencia de usuario intuitiva y responsiva. Se utilizan formularios reactivos para asegurar que los campos del formulario se completen correctamente antes de enviarlo. A partir de servicio logramos enviar los datos que nos retornaba el backend de una vista a otra.

## GitHub

El código del proyecto se encuentra en GitHub: [DCXAir](https://github.com/mateo022/DCXAir/).
La rama donde está el proyecto es `development`.

## Tecnologias y versiones
C# .NET 8
Angular typescript 17

## In English

### Introduction

The DCXAir project is an application that offers users an easy-to-understand interface for conducting route and flight searches efficiently. Users can enter travel details, validate the required fields, and submit the form to obtain information about available flights.

### Backend

The project's backend is implemented in C# using clean architecture in layers. Patterns such as Mediator and CQRS were employed for efficient handling of requests and responses. Additionally, a breadth-first search (BFS) algorithm was implemented to generate optimal routes between origin and destination points. The algorithm uses a service and its interface, as well as a dictionary for managing keys and routes, ensuring a quick and efficient search.

### Frontend

The frontend is developed with Angular, providing an intuitive and responsive user experience. Reactive forms are used to ensure that the form fields are properly filled before submitting. From the service we were able to send the data returned by the backend from one view to another.

## GitHub

The project's code can be found on GitHub: [DCXAir](https://github.com/mateo022/DCXAir/).
The branch where the project is located is `development`.

## Technologies and versions
C# .NET 8
Angular typescript 17