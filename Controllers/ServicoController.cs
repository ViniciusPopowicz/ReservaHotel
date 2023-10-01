using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]
public class ServicoController : ControllerBase
{
    private readonly BDContext _dbContext;

    public ServicoController(BDContext dbContext)
    {
        _dbContext = dbContext;
    }



    

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(string descricao,float valorServico)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();


        var servico = new Servico{
            Descricao = descricao,
            ValorServico = valorServico
        };

        await _dbContext.AddAsync(servico);
        await _dbContext.SaveChangesAsync();

        return Created("", servico);
    }





    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<ServicoViewModel>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        var servicos = await _dbContext.Servicos.Include(s => s.Pacotes).ToListAsync();

        // Mapeia os Servicos para a ViewModel
        var servicosViewModel = servicos.Select(servico => new ServicoViewModel
        {
            IdServico = servico.IdServico,
            Descricao = servico.Descricao,
            Pacotes = servico.Pacotes.Select(p => p.IdPacote).ToList(),
            ValorServico = servico.ValorServico
        });

        return Ok(servicosViewModel);
    }





    [HttpGet]
    [Route("buscar/{id}")]
    public async Task<ActionResult<ServicoViewModel>> Buscar(int id)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        var servico = await _dbContext.Servicos
            .Include(s => s.Pacotes)
            .FirstOrDefaultAsync(s => s.IdServico == id);

        if (servico is null) return NotFound();

        // pega os valores do servico buscado e passa para a viewmodel, uma classe formatada para a exibição do json;
        var servicoViewModel = new ServicoViewModel
        {
            IdServico = servico.IdServico,
            Descricao = servico.Descricao,
            Pacotes = servico.Pacotes.Select(p => p.IdPacote).ToList(),
            ValorServico = servico.ValorServico
        };

        return Ok(servicoViewModel);
    }





    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult<Servico>> Alterar(int id, string descricao, float valorServico)
    {
        if (_dbContext is null) return NotFound("O _dbContext não foi inicializado.");

        var servicoExistente = await _dbContext.Servicos
            .Include(s => s.Pacotes)
            .FirstOrDefaultAsync(s => s.IdServico == id);

        if (servicoExistente is null) return NotFound("Serviço não encontrado.");

        servicoExistente.Descricao = descricao;
        servicoExistente.ValorServico = valorServico;


        // Recalcula o valor dos pacotes associados ao serviço
        var pacotes = servicoExistente.Pacotes.ToArray();


        // Itera sobre a coleção de pacotes
        foreach (var pacote in pacotes)
        {
            // Obtém a lista de serviços do pacote
            var servicos = await _dbContext.Servicos
                .Include(s => s.Pacotes)
                .Where(s => s.Pacotes.Any(p => p.IdPacote == pacote.IdPacote))
                .ToListAsync();

            // Calcula o valor total dos serviços 
            var valorTotal = servicos.Sum(s => s.ValorServico);

            // Atualiza o valor do pacote
            pacote.ValorPacote = valorTotal;
        }

        // Salva as alterações no banco de dados
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
