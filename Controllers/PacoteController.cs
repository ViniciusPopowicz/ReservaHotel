using ReservaHotel.Data;
using ReservaHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReservaHotel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacoteController : ControllerBase
    {
        private readonly ILogger<PacoteController> _logger;
        private readonly BDContext _dbContext;

        public PacoteController(BDContext dbContext, ILogger<PacoteController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult> Cadastrar(List<int> idsServicos)
        {


            if (_dbContext is null) return NotFound();
            if (idsServicos == null || idsServicos.Count == 0) return BadRequest("A lista de serviços está vazia.");

            // criando a lista de serviços que o Pacote ira incluir.
            var servicosDoPacote = new List<Servico>();
            //criando a classe Pacote que sera inserida no banco.
            var pacote = new Pacote();

            //percorrendo a lista passada por json no swagger
            foreach (var idServico in idsServicos)
            {
                // pegando os objetos Servico baseado XD na lista passada por json
                var servicoExistente = await _dbContext.Servicos.FindAsync(idServico);

                if (servicoExistente == null)
                {
                    return NotFound($"O serviço com ID {idServico} não foi encontrado.");
                }

                //adicionando o servico passado no json ao objeto pacote criado no começo do metodo
                servicosDoPacote.Add(servicoExistente);

                //acessa o valor do pacote que sera o valor somado de todos os servicos adicionados
                pacote.ValorPacote += servicoExistente.ValorServico;
            }        
                //passando a lista de serviso para o objeto pacote
                pacote.Servicos = servicosDoPacote;
            

            //adicionando e salavando as mudançda nos banco
            await _dbContext.AddAsync(pacote);
            await _dbContext.SaveChangesAsync();

            return Created("", pacote);
        }






        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Pacote>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            
            // o include é necessario pq o EF nao iria incluir na consulta os obejtos Servico
            //duvida, se o obejto servico tivesse mais um obejto como atributo, como seria o include do include??
            // algo assim?
            //  _dbContext.lista1.Include(_dbContext => _dbContext.lista2.Include(_dbContext => _dbContext.lista3))ToListAsync();
            return await _dbContext.Pacotes.Include(_dbContext => _dbContext.Servicos).ToListAsync();
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ActionResult<Pacote>> Buscar(int id)
        {
            if (_dbContext is null) return NotFound();
            
            // o metodo FirstOrDefaultAsync faz com que o obejto a ser incluidfo sera o primeiro obejto encontrado com o id forncedio
            //como usamos a annotation key acho q n fara diferença tira-lo, mas o bard fez assim
            // tirei e deu erro com o findasync
            //nao sei pq
            var pacoteBusca = await _dbContext.Pacotes.Include(p => p.Servicos).FirstOrDefaultAsync(p => p.IdPacote == id);
            
            if (pacoteBusca is null) return NotFound();

            return pacoteBusca;
        }



        //aqui o usuario passa apenas o id do pacote a ser alterado e uma lista com o id dos novos serviços
        [HttpPut]
        [Route("alterar")]
        public async Task<ActionResult> Alterar(int id, List<int> servicosIds)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Pacotes is null) return NotFound();

            // busca o objto pacote no banco de dados
            var pacoteBusca = await _dbContext.Pacotes.Include(p => p.Servicos).FirstOrDefaultAsync(p => p.IdPacote == id);

            if (pacoteBusca is null) return NotFound();

            // Cria uma nova lista de serviços com base na lista de IDs dos novos serviços
            //o where busca o servico da tabela Servicos aonde o id é igual ao id da lista
            var servicos = await _dbContext.Servicos.Where(s => servicosIds.Contains(s.IdServico)).ToListAsync();

            //reseta o valor do pacote para 0, para recalcular no foreach
            pacoteBusca.ValorPacote = 0;

            foreach(var servico in servicos)
            {
                pacoteBusca.ValorPacote += servico.ValorServico;
            }

            // passa a lista com os novos servicos
            pacoteBusca.Servicos = servicos;

            // Salva as alterações no banco de dados
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("excluir/{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            if (_dbContext is null) return NotFound();

            var pacoteBusca = await _dbContext.Pacotes.Include(p => p.Servicos).FirstOrDefaultAsync(p => p.IdPacote == id);

            if (pacoteBusca is null) return NotFound();

            // Exclua os serviços associados manualmente
            //_dbContext.Servicos.RemoveRange(pacoteBusca.Servicos);

            _dbContext.Remove(pacoteBusca);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}