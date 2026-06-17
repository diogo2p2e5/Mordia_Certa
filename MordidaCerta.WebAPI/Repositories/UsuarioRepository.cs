using FilmesMoura1.WebAPI.Utils;
using MordidaCerta.WebAPI.BdContextLanches;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Models;

namespace MordidaCerta.WebAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LanchesContext _context;

        public UsuarioRepository(LanchesContext context)
        {
            _context = context;
        }


        public Usuario BuscarPorEmailESenha(string email, string senha)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuarios.FirstOrDefault(u => u.Email == email)!;

                if (usuarioBuscado != null)
                {
                    bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha);

                    if (confere)
                    {
                        return usuarioBuscado;
                    }
                }
                return null!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario BuscarPorId(Guid id)
        {
            try
            {
                Usuario UsuarioBuscado = _context.Usuarios.Find(id.ToString())!;
                return UsuarioBuscado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Cadastrar(Usuario novoUsuario)
        {
            try
            {
                novoUsuario.IdUsuario = Guid.NewGuid().ToString();

                novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha!);

                _context.Usuarios.Add(novoUsuario);

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
