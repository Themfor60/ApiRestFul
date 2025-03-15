using Microsoft.AspNetCore.Mvc;
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


        //EndPoint de generar una lista de usuarios
        [HttpGet]
        public async Task <IActionResult> ListaUsuarioGet()
        {
            try
            {
                var usuario = await _Context.Usuarios.ToArrayAsync();
                return Ok(usuario);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Error interno del sistema: { ex.Message}");
            }

        }

        //EndPoint de obtener detalle del usuario 
        [HttpGet("{id}")]
        public async Task<IActionResult> DetalleUsuarioGet(int id) 
        {
            if (id < 0) 
            {
                return BadRequest("Id no valido");
            }
            try
            {
                var DetalleUsuario = _Context.Usuarios.Find(id);
                return Ok(DetalleUsuario);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Error Intero del sistema {ex.Message}");
            }
        }

        //EndPoint de Modificar el usuario
        [HttpPost("{id}")]
        public async Task<IActionResult> ModificarUsuario(int id, Usuario usuario) 
        {
            if (id < 0) 
            {
                return BadRequest("El id no coincide");
            }
            try
            {
                var EditarUsuario = _Context.Usuarios.Find(id);
                if (EditarUsuario == null)
                {
                    return BadRequest("Usuario no encontrado");
                }

                _Context.Usuarios.Update(usuario);
                await _Context.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Problema en el sistema: {ex.Message}");
            }
        }


        //EndPoint de Borrar Usuario 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id) 
        {
            try
            {
                var Borrar = _Context.Usuarios.Find(id);
                if (Borrar == null)
                {
                    return BadRequest("Id no coindice");
                }
                _Context.Usuarios.Remove(Borrar);
                await _Context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Error del sistema: {ex.Message}");
            }
        }

    }
}
