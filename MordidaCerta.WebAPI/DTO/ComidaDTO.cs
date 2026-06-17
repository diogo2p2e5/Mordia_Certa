namespace MordidaCerta.WebAPI.DTO;

public class ComidaDTO
{
    public string? Nome { get; set; }
    public IFormFile? Imagem { get; set; }
    public Guid? IdCategoria { get; set; }
}
