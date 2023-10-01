using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]

public class HotelController : ControllerBase
{
    private readonly BDContext _dbContext;

    public HotelController(BDContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(Hotel hotel)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Hotels is null) return NotFound();

        await _dbContext.AddAsync(hotel);
        await _dbContext.SaveChangesAsync();

        return Created("", hotel);
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Hotel>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Hotels is null) return NotFound();

        return await _dbContext.Hotels.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{idHotel}")]
    public async Task<ActionResult<Hotel>> Buscar(int idHotel)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Hotels is null) return NotFound();

        var hotelBusca = await _dbContext.Hotels.FindAsync(idHotel);
        if (hotelBusca is null) return NotFound();

        return hotelBusca;
    }

    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Hotel hotel)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Hotels is null) return NotFound();

        if (hotel is null) return NotFound();

        var hotelExistente = await _dbContext.Hotels.FindAsync(hotel.IdHotel);

        if (hotel is null) return NotFound();

        hotelExistente.Nome = hotel.Nome;
        hotelExistente.Cidade = hotel.Cidade;
        hotelExistente.Pais = hotel.Pais;
        hotelExistente.Rating = hotel.Rating;
        hotelExistente.NumQuartos = hotel.NumQuartos;

        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{idHotel}")]
    public async Task<ActionResult> Excluir(int idHotel)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Hotels is null) return NotFound();

        var hotelBusca = await _dbContext.Hotels.FindAsync(idHotel);
        if (hotelBusca is null) return NotFound();

        _dbContext.Remove(hotelBusca);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
    
}