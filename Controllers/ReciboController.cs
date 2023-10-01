using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]

public class ReciboController : ControllerBase
{
    private readonly BDContext _dbContext;

    public ReciboController(BDContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(int idRecibo, int idPagamento)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Recibos is null) return NotFound(); 

        var recibo = new Recibo();
        var pagamentoRecibo = await _dbContext.Pagamentos.Include(p => p.Reserva).FirstOrDefaultAsync(p => p.IdPagamento == idPagamento);


        recibo.Pagamento = pagamentoRecibo;



        if(recibo.Pagamento.Valor < 200){
            recibo.Premio = await _dbContext.Premios.FindAsync(1);
        }
        if(recibo.Pagamento.Valor >= 200 && recibo.Pagamento.Valor < 500){
            recibo.Premio = await _dbContext.Premios.FindAsync(2);
        }
        if(recibo.Pagamento.Valor >= 500){
            recibo.Premio = await _dbContext.Premios.FindAsync(3);
        }


        await _dbContext.Recibos.AddAsync(recibo);
        await _dbContext.SaveChangesAsync();

        return Created("", recibo);
    }





    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Recibo>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Recibos is null) return NotFound();

        return await _dbContext.Recibos.Include(r => r.Pagamento).Include(r => r.Premio).ToListAsync();
    }




    [HttpGet]
    [Route("buscar/{idRecibo}")]
    public async Task<ActionResult<Recibo>> Buscar(int idRecibo)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Recibos is null) return NotFound();

        var reciboBusca = _dbContext.Recibos.Include(r => r.Pagamento).Include(r => r.Premio).FirstOrDefaultAsync(r => r.IdRecibo == idRecibo);
        if (reciboBusca is null) return NotFound();

        return Ok(reciboBusca);
    }






    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Recibo recibo)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Recibos is null) return NotFound();

        var reciboExistente = await _dbContext.Recibos.FindAsync(recibo.IdRecibo);

        if (reciboExistente is null) return NotFound();

        reciboExistente.Pagamento = recibo.Pagamento;
        reciboExistente.Premio = recibo.Premio;

        await _dbContext.SaveChangesAsync();

        return Ok();
    }






    [HttpDelete]
    [Route("excluir/{idRecibo}")]
    public async Task<ActionResult> Excluir(int idRecibo)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Recibos is null) return NotFound();

        var reciboBusca = await _dbContext.Recibos.FindAsync(idRecibo);
        if (reciboBusca is null) return NotFound();

        _dbContext.Recibos.Remove(reciboBusca);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
}