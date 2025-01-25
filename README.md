# PruebaTecnicaImagineApp

## Tecnología Utilizada
- **Framework:** .NET 8.0
- **Base de Datos:** PostgreSQL
- **Backup de Base de Datos:** Carpeta `/basedatos`
- **Razones para actualizar de .NET Framework 4.5:**
  - Mayor rendimiento
  - Soporte de características modernas
  - Mejoras de seguridad
  - Multiplataforma con .NET Core

## Arquitectura: Orientada a Servicios (SOA)
### Ventajas de SOA
1. **Modularidad**
   - Servicios independientes
   - Fácil mantenimiento
   - Escalabilidad

2. **Interoperabilidad**
   - Comunicación entre diferentes sistemas
   - Protocolos estándar (REST, SOAP)

3. **Flexibilidad**
   - Servicios reutilizables
   - Implementación independiente
   - Evolución tecnológica más simple

4. **Reducción de Acoplamiento**
   - Menor dependencia entre componentes
   - Mejora la mantenibilidad del software

## Configuración
- **Servidor Local:** http://localhost:8080
- **Conexión Base de Datos:** Configurada en `appsettings.json`

## Requisitos
- .NET 8.0 SDK
- PostgreSQL
- Visual Studio 2022

## Instalación
1. Clonar repositorio
2. Restaurar base de datos desde `/basedatos`
3. Configurar cadena de conexión
4. Restaurar paquetes NuGet
5. Ejecutar proyecto
