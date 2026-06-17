using MordidaCerta.WebAPI.BdContextLanches;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Models;

namespace MordidaCerta.WebAPI.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly LanchesContext _context;

    public CategoriaRepository(LanchesContext context)
    {
        _context = context;
    }



    public void AtualizarIdCorpo(Categoria CategoriaAtualizada)
    {
        try
        {
            Categoria categoriaBuscada = _context.Categorias.Find(CategoriaAtualizada.IdCategoria)!;

            if (categoriaBuscada != null)
            {
                categoriaBuscada.Titulo = CategoriaAtualizada.Titulo;
            }

            _context.Categorias.Update(categoriaBuscada!);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void AtualizarIdUrl(Guid id, Categoria CategoriaAtualizada)
    {
        try
        {
            Categoria categoriaBuscada = _context.Categorias.Find(id.ToString())!;

            if (categoriaBuscada != null)
            {
                categoriaBuscada.Titulo = CategoriaAtualizada.Titulo;
            }

            _context.Categorias.Update(categoriaBuscada!);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Categoria BuscarPorId(Guid id)
    {
        try
        {
            Categoria categoriaBuscada = _context.Categorias.Find(id.ToString())!;
            return categoriaBuscada;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Cadastrar(Categoria novaCategoria)
    {
        try
        {
            novaCategoria.IdCategoria = Guid.NewGuid().ToString();
            _context.Categorias.Add(novaCategoria);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Deletar(Guid id)
    {
        try
        {
            Categoria categoriaBuscada = _context.Categorias.Find(id.ToString())!;

            if (categoriaBuscada != null)
            {
                _context.Categorias.Remove(categoriaBuscada);
            }
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public List<Categoria> Listar()
    {
        try
        {
            List<Categoria> listaCategorias = _context.Categorias.ToList();

            return listaCategorias;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
