using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]
public class ServicoController : ControllerBase
{

    private readonly ILogger<ServicoController> _logger;
    private readonly BDContext _dbContext;

    public ServicoController(BDContext dbContext, ILogger<ServicoController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(Servico servico)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        await _dbContext.AddAsync(servico);
        await _dbContext.SaveChangesAsync();

        return Created("", servico);
    }





    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Servico>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        return await _dbContext.Servicos.ToListAsync();
    }




    [HttpGet]
    [Route("buscar/{id}")]
    public async Task<ActionResult<Servico>> Buscar(int id)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        var servicoBusca = await _dbContext.Servicos.FindAsync(id);
        if (servicoBusca is null) return NotFound();

        return servicoBusca;
    }




    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Servico servico)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        // Busque o serviço existente no banco de dados pelo IdServico
        var servicoExistente = await _dbContext.Servicos.FindAsync(servico.IdServico);

        if (servicoExistente is null) return NotFound();

        // Aplique as alterações no serviço existente
        servicoExistente.Descricao = servico.Descricao;
        servicoExistente.ValorServico = servico.ValorServico;

        // Salve as alterações no banco de dados
        await _dbContext.SaveChangesAsync();

        return Ok();
    }




    [HttpDelete]
    [Route("excluir/{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        var servicoBusca = await _dbContext.Servicos.FindAsync(id);
        if (servicoBusca is null) return NotFound();

        _dbContext.Remove(servicoBusca);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
