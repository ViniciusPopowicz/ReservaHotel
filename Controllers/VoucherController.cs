using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Constraints;

namespace ReservaHotel.Controllers;

[ApiController]
[Route("[controller]")]

public class VoucherController : ControllerBase
{
    private readonly BDContext _dbContext;

    public VoucherController(BDContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(Voucher voucher)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Vouchers is null) return NotFound();

        await _dbContext.AddAsync(voucher);
        await _dbContext.SaveChangesAsync();

        return Created("", voucher);
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Voucher>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Vouchers is null) return NotFound();

        return await _dbContext.Vouchers.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{idVoucher}")]
    public async Task<ActionResult<Voucher>> Buscar(int IdVoucher)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Vouchers is null) return NotFound();

        var VoucherBusca = await _dbContext.Vouchers.FindAsync(IdVoucher);
        if (VoucherBusca is null) return NotFound();

        return VoucherBusca;
    }

    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Voucher Voucher)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Vouchers is null) return NotFound();

        // Busque o Voucher existente no banco de dados pelo numero do Voucher
        var VoucherExistente = await _dbContext.Vouchers.FindAsync(Voucher.IdVoucher);

        if (Voucher is null) return NotFound();

        // Aplique as alterações no Voucher existente
        VoucherExistente.Desconto = Voucher.Desconto;

        // Salve as alterações no banco de dados
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{idVoucher}")]
    public async Task<ActionResult> Excluir(int idVoucher)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Servicos is null) return NotFound();

        var VoucherBusca = await _dbContext.Vouchers.FindAsync(idVoucher);
        if (VoucherBusca is null) return NotFound();

        _dbContext.Remove(VoucherBusca);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

}