using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnicaImagineApp.Data;
using pruebaTecnicaImagineApp.Models;
using pruebaTecnicaImagineApp.Request;

namespace pruebaTecnicaImagineApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo electrónico ya existe
                var existingUser = await _context.Users
                                                 .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (existingUser != null)
                {
                    // Si el usuario con ese correo ya existe, retornamos un mensaje adecuado
                    return Conflict(new
                    {
                        status = "error",
                        message = "El correo electrónico ya está registrado.",
                        data = new
                        {
                            email = request.Email
                        }
                    });
                }

                // Crear el nuevo usuario
                var user = new User { Name = request.Name, Email = request.Email };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();  // Usar SaveChangesAsync para operaciones asincrónicas

                // Respuesta con éxito
                var response = new
                {
                    status = "success",
                    message = "El usuario ha sido creado exitosamente",
                    data = new
                    {
                        id = user.Id,
                        name = user.Name,
                        email = user.Email
                    }
                };

                return Ok(response);
            }

            // Si los datos de entrada no son válidos, retorna el mensaje de error
            return BadRequest(new
            {
                status = "error",
                message = "Error en la solicitud",
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Error en la solicitud",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            // Buscar el usuario por ID
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "El usuario no fue encontrado."
                });
            }

            // Verificar si el correo electrónico ya está registrado por otro usuario
            var existingUserWithEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Id != id);

            if (existingUserWithEmail != null)
            {
                return Conflict(new
                {
                    status = "error",
                    message = "El correo electrónico ya está registrado por otro usuario.",
                    data = new
                    {
                        email = request.Email
                    }
                });
            }

            // Actualizar datos del usuario
            user.Name = request.Name;
            user.Email = request.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success",
                message = "El usuario ha sido actualizado exitosamente",
                data = new
                {
                    id = user.Id,
                    name = user.Name,
                    email = user.Email
                }
            });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Buscar el usuario por ID
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "El usuario no fue encontrado."
                });
            }

            // Eliminar el usuario
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "success",
                message = "El usuario ha sido eliminado exitosamente",
                data = new
                {
                    id = user.Id,
                    name = user.Name,
                    email = user.Email
                }
            });
        }

    }




}
