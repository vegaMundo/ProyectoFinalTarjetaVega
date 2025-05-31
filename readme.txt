PROYECTO FINAL: SISTEMA DE GESTIÓN DE TARJETAS DE CRÉDITO

Desarrollado en C# 

CÓMO EJECUTAR EL PROYECTO:
1. Abrir Visual Studio.
2. Seleccionar "Abrir un proyecto o solución".
3. Cargar la solución llamada "ProyectoFinalTarjeta.sln".
4. Presionar F5 para ejecutar el proyecto, usar http para la ajecución.
5. Se abrirá el navegador con Swagger, donde se pueden probar las APIS REST

ENDPOINTS DISPONIBLES (PRINCIPALES FUNCIONALIDADES):
- `GET /api/clientes` → Obtener lista de clientes
- `POST /api/clientes` → Agregar un nuevo cliente
- `PUT /api/clientes/{nit}` → Actualizar datos de un cliente
- `DELETE /api/clientes/{nit}` → Eliminar un cliente
- `POST /api/carga` → Carga de datos desde archivo JSON
- `GET /api/tarjeta/saldo/{numero}` → Consultar el saldo actual de una tarjeta
- `POST /api/tarjeta/pago` → Realizar un pago (`{numero}`, `{monto}`)
- `POST /api/tarjeta/consumo` → Registrar un consumo (`{numero}`, `{monto}`, `{detalle}`)
- `GET /api/tarjeta/movimientos/{numero}` → Lista de últimos movimientos de la tarjeta
- `PUT /api/tarjeta/renovar/{numero}` → Renovación automática de tarjeta vencida
- `PUT /api/tarjeta/cambiarpin` → Cambiar el PIN actual (`{numero}`, `{nuevoPin}`)
- `PUT /api/tarjeta/bloquear/{numero}` → Bloqueo temporal por robo/pérdida
- `POST /api/tarjeta/aumentolimite` → Solicitud de aumento de crédito (`{numero}`, `{nuevoLimite}`)

INFORMACIÓN MUY IMPORTANTE:
- Toda la información se almacena en memoria usando estructuras de datos como listas enlazadas, pilas, colas, árboles binarios de búsqueda, árboles AVL y tablas hash.
- Al reiniciar la aplicación, se debe volver a cargar el archivo JSON si se quiere restaurar el estado.


VIDEO DEL FUNCIONAMIENTO SUBIDO EN DRIVE:

https://drive.google.com/file/d/1IfvZ59ZSJfP9aKuicqtRLMjjNpmapNhO/view?usp=sharing

LINK DEL PROYECTO EN DRIVE POR SI DA PROBLEMAS EN GITHUB

LINK DEL PROYECTO EN GITHUB

GRACIAS POR TODO SU APOYO INGENIERO Y NUEVAMENTE DISCULPE POR LA BULLA.
