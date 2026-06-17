using Microsoft.AspNetCore.Mvc;
using MordidaCerta.WebAPI.DTO;
using MordidaCerta.WebAPI.Interfaces;
using MordidaCerta.WebAPI.Models;

namespace MordidaCerta.WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{      
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

     [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_categoriaRepository.BuscarPorId(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_categoriaRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post(CategoriaDTO novaCategoria)
    {
        try
        {
            var categoria = new Categoria
            {
                Titulo = novaCategoria.Titulo
            };

            _categoriaRepository.Cadastrar(categoria);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, CategoriaDTO categoriaAtualizada)
    {
        try
        {
            var categoria = new Categoria
            {
                Titulo = categoriaAtualizada.Titulo
            };

            _categoriaRepository.AtualizarIdUrl(id, categoria);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public IActionResult PutBody(Categoria categoriaAtualizada)
    {
        try
        {
            _categoriaRepository.AtualizarIdCorpo(categoriaAtualizada);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
    