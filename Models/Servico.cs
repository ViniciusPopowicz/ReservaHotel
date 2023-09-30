using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;

public class Servico
{
    [Key]
    public int IdServico { get; set; }

    public string Descricao { get; set; }

    public float ValorServico { get; set; }

    public virtual ICollection<Pacote> Pacotes { get; set; }


    public Servico()
    {
    }

    public Servico(int idServico, string descricao, float valorServico)
    {
        this.IdServico = idServico;
        this.Descricao = descricao;
        this.ValorServico = valorServico;
    }
}