using MordidaCerta.WebAPI.BdContextLanches;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Models;
using static System.Net.WebRequestMethods;

namespace MordidaCerta.WebAPI.Repositories;

public class ComidaRepository : IComidaRepository
{
    private readonly LanchesContext _context;

    public ComidaRepository(LanchesContext context)
    {
        _context = context;
    }


    public void AtualizarIdCorpo(Comida ComidaAtualizado)
    {
        try
        {
            Comida comidaBuscada = _context.Comidas.Find(ComidaAtualizado.IdComida)!;

            if (comidaBuscada != null)
            {
                comidaBuscada.Nome = ComidaAtualizado.Nome;
                comidaBuscada.IdCategoria = ComidaAtualizado.IdCategoria;
            }

            _context.Comidas.Update(comidaBuscada!);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void AtualizarIdUrl(Guid Id, Comida ComidaAtualizado)
    {
        try
        {
            Comida comidaBuscada = _context.Comidas.Find(Id.ToString())!;
            if (comidaBuscada != null)
            {
                comidaBuscada.Nome = ComidaAtualizado.Nome;
                comidaBuscada.IdCategoria = ComidaAtualizado.IdCategoria;
            }

            _context.Comidas.Update(comidaBuscada!);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Comida BuscarPorId(Guid Id)
    {
        try
        {
            Comida comidaBuscada = _context.Comidas.Find(Id.ToString())!;
            return comidaBuscada;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Cadastrar(Comida novaComida)
    {
        try
        {
            novaComida.IdComida = Guid.NewGuid().ToString();

            _context.Comidas.Add(novaComida);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Deletar(Guid Id)
    {
        try
        {
            Comida comidaBuscada = _context.Comidas.Find(Id.ToString())!;

            if (comidaBuscada != null)
            {
                _context.Comidas.Remove(comidaBuscada);
            }
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<Comida> Listar()
    {
        try
        {
            List<Comida> listaComidas = _context.Comidas.ToList();
            return listaComidas;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
