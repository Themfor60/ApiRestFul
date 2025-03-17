using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.EntityFrameworkCore;
using PruebaDeApi.Data;
using PruebaDeApi.Models;

namespace PruebaDeApi.Controllers
{
    /*
     * Creado por Uriel Aquino, Api RestFul Basico para CRUD
     */
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public UsuarioController(AppDbContext Context)
        {
            _Context = Context;
        }


        //Obtener Listado
        [HttpGet]
        public async Task<IActionResult> ListaUsuario()
        {
            try
            {
                var usuarios = await _Context.Usuarios.ToListAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del sistema interno: {ex.Message} ");
            }
        }


        //Obtner Id del listado osea detalle
        [HttpGet("{id}")]
        public async Task<IActionResult> DetalleUsuario(int id)
        {
            if (id < 0)
            {
                return BadRequest("400, Id no encontrado");
            }
            try
            {
                var usurio = await _Context.Usuarios.FindAsync(id);
                if (usurio == null)
                {
                    return NotFound("404, Usuario no encontrado");
                }
                return Ok(usurio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del sistmea: {ex.Message}");
            }
        }


        //Crear Nuevo Usuario

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                 _Context.Usuarios.Add(usuario);
                await _Context.SaveChangesAsync();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del sistema: {ex.Message}");
            }
        }

        //Editar Usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest("Id no válido");
            }

            try
            {
                var usuarioExistente = await _Context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    return NotFound("Usuario no encontrado");
                }


                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Apellido = usuario.Apellido;
                usuarioExistente.Telefono = usuario.Telefono;
                usuarioExistente.direccion = usuario.direccion;

                _Context.Usuarios.Update(usuarioExistente);
                await _Context.SaveChangesAsync();

                return Ok(usuarioExistente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del sistema: {ex.Message}");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarUsuario(int id) 
        {
            if (id <= 0) 
            {
                return BadRequest("Id  no encotrado");
            }

            try
            {
                var usuarioExiste = await _Context.Usuarios.FindAsync(id);
                if (usuarioExiste == null)
                {
                    return NotFound("No encontrado");
                }

                _Context.Usuarios.Remove(usuarioExiste);
                await _Context.SaveChangesAsync();

                return Ok($"Usuario con Id {id} eliminado correctamente");
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Error del sistema {ex.Message}");
            }
        }


    }
}
