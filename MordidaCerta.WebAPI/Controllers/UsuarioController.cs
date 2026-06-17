using Microsoft.AspNetCore.Mvc;
using MordidaCerta.WebAPI.DTO;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Models;

namespace MordidaCerta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Post(UsuarioDTO novoUsuario)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = novoUsuario.Nome,
                    Email = novoUsuario.Email,
                    Senha = novoUsuario.Senha
                };

                _usuarioRepository.Cadastrar(usuario);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
