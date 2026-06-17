using Microsoft.AspNetCore.Mvc;
using MordidaCerta.WebAPI.DTO;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Models;
using static System.Net.WebRequestMethods;


namespace MordidaCerta.WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ComidaController : ControllerBase
{
    private readonly IComidaRepository _comidaRepository;

    public ComidaController(IComidaRepository comidaRepository)
    {
        _comidaRepository = comidaRepository;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_comidaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_comidaRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromForm] ComidaDTO novaComida)
    {
        if (String.IsNullOrWhiteSpace(novaComida.Nome) && novaComida.IdCategoria != null)
            return BadRequest("É obrigatório que o Prato tenha Nome e Categoria");

        Comida comida = new Comida();

        if (novaComida.Imagem != null && novaComida.Imagem.Length > 0)
        {
            var extensao = Path.GetExtension(novaComida.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            //Garante que a pasta exista
            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await novaComida.Imagem.CopyToAsync(stream);
            }

            comida.Imagem = nomeArquivo;
        }

        comida.IdCategoria = novaComida.IdCategoria.ToString();
        comida.Nome = novaComida.Nome!;

        try
        {
            _comidaRepository.Cadastrar(comida);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, ComidaDTO comida)
    {
        var comidaBuscada = _comidaRepository.BuscarPorId(id);

        if (comidaBuscada == null)
            return NotFound("Prato não encontrada!");

        if (!String.IsNullOrWhiteSpace(comida.Nome))
            comidaBuscada.Nome = comida.Nome;

        if (comida.IdCategoria != null && comida.IdCategoria.ToString() != comidaBuscada.IdCategoria)
            comidaBuscada.IdCategoria = comida.IdCategoria.ToString();

        if (comida.Imagem != null && comida.Imagem.Length != 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            // Deleta arquivo antigo
            if (!String.IsNullOrEmpty(comidaBuscada.Imagem))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, comidaBuscada.Imagem);

                if (System.IO.File.Exists(caminhoAntigo))
                    System.IO.File.Delete(caminhoAntigo);
            }

            // Salva a nova imagem
            var extensao = Path.GetExtension(comida.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await comida.Imagem.CopyToAsync(stream);
            }

            comidaBuscada.Imagem = nomeArquivo;
        }

        try
        {
            _comidaRepository.AtualizarIdUrl(id, comidaBuscada);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpPut]
    public IActionResult PutBody(Comida comida)
    {
        try
        {
            _comidaRepository.AtualizarIdCorpo(comida);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var comidaBuscada = _comidaRepository.BuscarPorId(id);
        if (comidaBuscada == null)
            return NotFound("Filme não encontrado!");

        var pastaRelativa = "wwwroot/imagens";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

        // Deleta Arquivo
        if (!String.IsNullOrEmpty(comidaBuscada.Imagem))
        {
            var caminho = Path.Combine(caminhoPasta, comidaBuscada.Imagem);

            if (System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);
        }

        try
        {
            _comidaRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }



}
