using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]

public class PagamentoController : ControllerBase
{
    private readonly BDContext _dbContext;

    public PagamentoController(BDContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(int IdReserva, MetodoPagamento metodoPagamento)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Hotels is null) return NotFound(); 

        var pagamento = new Pagamento();
        var reservaPagamento = await _dbContext.Reservas.FindAsync(IdReserva);
        pagamento.Reserva = reservaPagamento;
        pagamento.Valor = reservaPagamento.ValorReserva;
        pagamento.MetodoPagamento = metodoPagamento;

        await _dbContext.AddAsync(pagamento);
        await _dbContext.SaveChangesAsync();

        return Created("", pagamento);
    }





    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Pagamento>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Pagamentos is null) return NotFound();

        return await _dbContext.Pagamentos.ToListAsync();
    }




    [HttpGet]
    [Route("buscar/{idPagamento}")]
    public async Task<ActionResult<Pagamento>> Buscar(int idPagamento)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Pagamentos is null) return NotFound();

        var pagamentoBusca = await _dbContext.Pagamentos.FindAsync(idPagamento);
        if (pagamentoBusca is null) return NotFound();

        return pagamentoBusca;
    }






    [HttpPut()]
    [Route("alterar/{idPagamento}")]
    public async Task<ActionResult> Alterar(int idPagamento, MetodoPagamento metodoPagamento)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Pagamentos is null) return NotFound();

        if (idPagamento <= 0) return NotFound();

        var pagamentoExistente = await _dbContext.Pagamentos.FindAsync(idPagamento);

        if (pagamentoExistente is null) return NotFound();

        pagamentoExistente.MetodoPagamento = metodoPagamento;

        await _dbContext.SaveChangesAsync();

        return Ok();
    }






    [HttpDelete]
    [Route("excluir/{idPagamento}")]
    public async Task<ActionResult> Excluir(int idPagamento)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Pagamentos is null) return NotFound();

        var pagamentoBusca = await _dbContext.Pagamentos.FindAsync(idPagamento);
        if (pagamentoBusca is null) return NotFound();


        _dbContext.Remove(pagamentoBusca);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
}