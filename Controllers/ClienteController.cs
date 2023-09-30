using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace ReservaHotel.Controllers;


[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ILogger<ClienteController> _logger;
    // Cria o atributo que ira realizar as operações de banco
    private BDContext _dbContext;


    public ClienteController(BDContext dbContext,ILogger<ClienteController> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }


    // metodo post para cadastrar cliente Task é um objeto que representa uma operação assincrona
    //opercao assincrona é uma operação pode ser executada em "segundo plano". o codigo nao precisa parar e esperar q a operacao seja concluida
    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar(Cliente cliente)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Clientes is null) return NotFound();
        await _dbContext.AddAsync(cliente);
        await _dbContext.SaveChangesAsync();
        return Created("",cliente);
    }



    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Cliente>>> Listar()
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Clientes is null) return NotFound();
        return await _dbContext.Clientes.ToListAsync();
    }


    [HttpGet]
    [Route("buscar/{cpf}")]
    public async Task<ActionResult<Cliente>> Buscar(string cpf)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Clientes is null) return NotFound();
        var clienteBusca = await _dbContext.Clientes.FindAsync(cpf);
        if(clienteBusca is null) return NotFound();
        return clienteBusca;
    }



    [HttpPut()]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Cliente cliente)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Clientes is null) return NotFound();

        // pegando o cliente que vai ser atualizado
        var clienteExistente = await _dbContext.Clientes.FindAsync(cliente.Cpf);

        if (clienteExistente is null) return NotFound();

        // mudando os valores dos atributos
        clienteExistente.Nome = cliente.Nome;
        clienteExistente.Telefone = cliente.Telefone;

        // salvando
        await _dbContext.SaveChangesAsync();

        return Ok();
    }





    [HttpDelete()]
    [Route("excluir/{cpf}")]
    public async Task<ActionResult> Excluir(string cpf)
    {
        if(_dbContext is null) return NotFound();
        if(_dbContext.Clientes is null) return NotFound();
        var clienteBusca = await _dbContext.Clientes.FindAsync(cpf);
        if(clienteBusca is null) return NotFound();
        _dbContext.Remove(clienteBusca);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}