using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]

public class QuartoController : ControllerBase
{
   private readonly ILogger<QuartoController> _logger;
    private readonly BDContext _dbContext;

    public QuartoController(BDContext dbContext, ILogger<QuartoController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(Quarto quarto)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Quartos is null) return NotFound();

        await _dbContext.AddAsync(quarto);
        await _dbContext.SaveChangesAsync();

        return Created("", quarto);
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Quarto>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Quartos is null) return NotFound();

        return await _dbContext.Quartos.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{id}")]
    public async Task<ActionResult<Quarto>> Buscar(int nroQuarto)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Quartos is null) return NotFound();

        var quartoBusca = await _dbContext.Quartos.FindAsync(nroQuarto);
        if (quartoBusca is null) return NotFound();

        return quartoBusca;
    }

    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Quarto quarto)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Quartos is null) return NotFound();

        // Busque o quarto existente no banco de dados pelo numero do quarto
        var quartoExistente = await _dbContext.Quartos.FindAsync(quarto.nroQuarto);

        if (quarto is null) return NotFound();

        // Aplique as alterações no quarto existente
        quartoExistente.nroHospedes = quarto.nroHospedes;
        quartoExistente.valor = quarto.valor;

        // Salve as alterações no banco de dados
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{id}")]
    public async Task<ActionResult> Excluir(int nroQuarto)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        var quartoBusca = await _dbContext.Quartos.FindAsync(nroQuarto);
        if (quartoBusca is null) return NotFound();

        _dbContext.Remove(quartoBusca);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
}