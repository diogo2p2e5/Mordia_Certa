using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MordidaCerta.WebAPI.DTO;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MordidaCerta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDto)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDto.Email!, loginDto.Senha!);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou Senha inválidos");
                }

                //Caso encontre o usuário, prossegue para a criação do token

                // 1° - Definir as informações(Claims) que serão fornecidos no token

                var claims = new[]
                {
                //formato da claim
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!)

                //existe a possibilidade de criar uma claim personalizada
                //new Claim("Claim Personalizada", "Valor da Claim")
            };

                //2° - Definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Pratos-chave-autenticacao-webapi-dev"));

                //3° - Definir as credenciais do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4° - Gerar Token
                var token = new JwtSecurityToken
                    (
                        //emissor do token
                        issuer: "api_Pratos",

                        //destinatário do token
                        audience: "api_Pratos",

                        //dados definidos nas claims(informações)
                        claims: claims,

                        //tempo de expiração do token
                        expires: DateTime.Now.AddMinutes(5),

                        //credenciais do token
                        signingCredentials: creds
                    );

                //5° - retornar o token criado
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
