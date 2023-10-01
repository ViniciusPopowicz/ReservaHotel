using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]

public class PremioController : ControllerBase
{
    private readonly BDContext _dbContext;

    public PremioController(BDContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(Premio premio)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Premios is null) return NotFound();

        await _dbContext.AddAsync(premio);
        await _dbContext.SaveChangesAsync();

        return Created("", premio);
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Premio>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Premios is null) return NotFound();

        return await _dbContext.Premios.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{idPremio}")]
    public async Task<ActionResult<Premio>> Buscar(int idPremio)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Premios is null) return NotFound();

        var premioBusca = await _dbContext.Premios.FindAsync(idPremio);
        if (premioBusca is null) return NotFound();

        return premioBusca;
    }





    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Premio premio)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Premios is null) return NotFound();

        // Busque o Voucher existente no banco de dados pelo numero do Voucher
        var premioExistente = await _dbContext.Premios.FindAsync(premio.IdPremio);

        if (premioExistente is null) return NotFound();

        // Aplique as alterações no Voucher existente
        premioExistente.Descricao = premio.Descricao;

        // Salve as alterações no banco de dados
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{idPremio}")]
    public async Task<ActionResult> Excluir(int idPremio)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Premios is null) return NotFound();

        var premioBusca = await _dbContext.Premios.FindAsync(idPremio);
        if (premioBusca is null) return NotFound();

        _dbContext.Premios.Remove(premioBusca);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

}