using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservaController : ControllerBase
{
        private readonly BDContext _dbContext;

        public ReservaController(BDContext dbContext)
        {
        _dbContext = dbContext;
        }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(string dataReserva, string dataCheckIn, string dataCheckOut, int idQuarto, int idHotel, int idPacote, string idCliente, int idVoucher)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Reservas is null) return NotFound();

        var quarto = await _dbContext.Quartos.FindAsync(idQuarto);
        var hotel = await _dbContext.Hotels.FindAsync(idHotel);
        var pacote = await _dbContext.Pacotes.FindAsync(idPacote);
        var voucher = await _dbContext.Vouchers.FindAsync(idVoucher);
        var cliente = await _dbContext.Clientes.FindAsync(idCliente);

        var valorReserva = 100.0f;

        valorReserva += quarto.Valor;
        valorReserva += pacote.ValorPacote;
        valorReserva -= voucher.Desconto;

        var reserva = new Reserva(dataReserva, dataCheckIn, dataCheckOut, quarto, hotel, pacote, cliente, voucher, valorReserva);

        await _dbContext.AddAsync(reserva);
        await _dbContext.SaveChangesAsync();

        return Created("", reserva);
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Reserva>>> Listar()
    {
        
        return await _dbContext.Reservas
        .Include(_dbContext => _dbContext.Quarto)
        .Include(_dbContext => _dbContext.Hotel)
        .Include(_dbContext => _dbContext.Pacote)
        .Include(_dbContext => _dbContext.Cliente)
        .Include(_dbContext => _dbContext.Voucher).ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{idReserva}")]
    public async Task<ActionResult<Reserva>> Buscar(int idReserva)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Reservas is null) return NotFound();

        //var reserva = await _dbContext.Reservas.FindAsync(idReserva);
        var reservaBusca = await _dbContext.Reservas
        .Include(r => r.Quarto)
        .Include(r => r.Hotel)
        .Include(r => r.Pacote)
        .Include(r => r.Cliente)
        .Include(r => r.Voucher)
        .FirstOrDefaultAsync(r => r.IdReserva == idReserva);
        if (reservaBusca is null)
        {
            return NotFound();
        }

        return Ok(reservaBusca);
    }


    [HttpPut()]
    [Route("alterar/{idReserva}")]
    public async Task<IActionResult> Alterar(int idReserva, string dataReserva, string dataCheckIn, 
    string dataCheckOut, int idQuarto, int idHotel, int idPacote, string idCliente, int idVoucher)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Reservas is null) return NotFound();

        var reserva = await _dbContext.Reservas.FindAsync(idReserva);
        reserva.DataReserva = dataReserva;
        reserva.DataCheckIn = dataCheckIn;
        reserva.DataCheckOut = dataCheckOut;
        reserva.Quarto = await _dbContext.Quartos.FindAsync(idQuarto);
        reserva.Hotel = await _dbContext.Hotels.FindAsync(idHotel);
        reserva.Pacote = await _dbContext.Pacotes.FindAsync(idPacote);
        reserva.Voucher = await _dbContext.Vouchers.FindAsync(idVoucher);
        reserva.Cliente = await _dbContext.Clientes.FindAsync(idCliente);

         reserva.ValorReserva = 100f;
         reserva.ValorReserva += reserva.Quarto.Valor;
         reserva.ValorReserva += reserva.Pacote.ValorPacote;
         reserva.ValorReserva -= reserva.Voucher.Desconto;

        await _dbContext.SaveChangesAsync();

        return Ok();
    }


        // DELETE: api/Reserva/5
        [HttpDelete]
        [Route("excluir/{idReserva}")]
        public async Task<IActionResult> DeleteReserva(int idReserva)
        {
            var reserva = await _dbContext.Reservas.FindAsync(idReserva);
            if (reserva == null)
            {
                return NotFound();
            }

            _dbContext.Reservas.Remove(reserva);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // private bool ReservaExists(int id)
        // {
        //     return _dbContext.Reservas.Any(e => e.IdReserva == id);
        // }
}