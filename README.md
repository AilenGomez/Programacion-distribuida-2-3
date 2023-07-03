# Programación distribuida 2 Segunda entrega

## :computer: Tecnologías

- .NET v6.0
- MediatR v10.0.1
- FluentValidation v10.3.6
- Swashbuckle v6.2.3
- Serilog v2.12.0
- SendGrid

## :clipboard: Requisitos

### 1 - Docker

Abrir la consola y ejecutar el siguiente comando:

``` 
docker -v
```

La salida debe ser algo similar a "Docker versión 20.10.14", donde indica la versión de docker instalada, en caso contrario se debe instalar docker. En los siguientes links pueden encontrar un tutorial de la documentación oficial como guía.
- [Linux](https://docs.docker.com/desktop/install/linux-install/)
- [Windows](https://docs.docker.com/desktop/install/windows-install/)

### 2 - GitHub

Abrir la consola y ejecutar el siguiente comando:

``` 
git --version
```

La salida debe ser similar a "git version 2.35.1.windows.2", indicando la versión de git instalada, en caso de no tener instalado git, seguir el tutorial que corresponda según tu Sistema operativo.

- [Linux](https://git-scm.com/download/linux)
- [Windows](https://git-scm.com/download/win)

## :rocket: Ejecución del proyecto

Clonar proyecto ejecutando el siguiente comando en la consola:

``` 
git clone https://github.com/AilenGomez/Programacion-distribuida-2-
```


### :zap: Ejecución


- Existe la posibilidad de configurar las variables:

    - M=Tiempo de vencimiento del numero de transaccion 
    - N= Medida de las dimensiones del lugar
    - P= Cantidad de personas permitidas en simultaneo atendidas por el sistema de venta de entrada
    - SERVER= Nombre del servicio de base de datos
    - PORT= Puerto del servicio de base de datos
    - DATABASE=Nombre de la base de datos
    - USERID=Nombre de usuario del servicio de base de datos
    - PASSWORDDB=Contraseña del servicio de base de datos
    - VENTAENTRADAAPIURL= Url del servicio venta de entrada
    - NOTIFICACIONAPIURL=Url del servicio notificacion
    
- Una vez instalado docker, abrir terminal en la carpeta donde se clonó el repositorio /Programacion-distribuida-2 y ejecutar el siguiente comando:

``` 
docker compose up -d 
```

- Abrir en un browser la siguiente URL: 

Servicio Venta de entrada 

http://localhost:8070/swagger/index.html

Servicio Puerta de entrada

http://localhost:8070/swagger/index.html


## :heavy_check_mark:HealthChecks

Para cada servicio podemos ver los healthchecks en /health

Servicio Venta de entrada 

http://localhost:8070/health

Servicio Puerta de entrada

http://localhost:8070/health

## :alembic:Test 

### Swagger

Donde se abrirá un swagger UI, con los endpoints de las api rest.
![image](https://github.com/AilenGomez/Programacion-distribuida-2-/assets/32937466/bfb72c6f-9e73-45d5-a116-5a599a3f6b7d)

Haciendo click en "Try it out"

Luego en Execute:

Lo mismo para el otro endpoint

![image](https://github.com/AilenGomez/Programacion-distribuida-2-/assets/32937466/d12ca9cd-a761-427b-a9db-6ba3f1a277e2)





