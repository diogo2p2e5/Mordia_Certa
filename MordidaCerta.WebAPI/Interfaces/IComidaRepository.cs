using MordidaCerta.WebAPI.Models;

namespace MordidaCerta.WebAPI.Interfaces;

public interface IComidaRepository
{
    Comida BuscarPorId(Guid Id);
    List<Comida> Listar();
    void Cadastrar(Comida novaComida);
    void Deletar(Guid Id);
    void AtualizarIdCorpo(Comida ComidaAtualizado);
    void AtualizarIdUrl(Guid Id, Comida ComidaAtualizado);
}
