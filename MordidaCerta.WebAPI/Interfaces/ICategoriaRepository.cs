using MordidaCerta.WebAPI.Models;

namespace MordidaCerta.WebAPI.Interfaces;

public interface ICategoriaRepository
    {
    Categoria BuscarPorId(Guid id);
    List<Categoria> Listar();
    void Cadastrar(Categoria novaCategoria);
    void Deletar(Guid id);
    void AtualizarIdCorpo(Categoria CategoriaAtualizada);
    void AtualizarIdUrl(Guid id, Categoria CategoriaAtualizada);


}

