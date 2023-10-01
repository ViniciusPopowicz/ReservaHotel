using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;

public class Servico
{
    [Key]
    public int IdServico { get; set; }

    public string Descricao { get; set; }
    public List<Pacote> Pacotes { get; set; }

    public float ValorServico { get; set; }


    public Servico()
    {
    }

    public Servico(int idServico, string descricao, List<Pacote> pacotes,float valorServico)
    {
        this.IdServico = idServico;
        this.Pacotes = pacotes;
        this.Descricao = descricao;
        this.ValorServico = valorServico;
    }


    public String toString(){

        

        return "" + this.IdServico + " " + this.Descricao + " " + this.Pacotes.ToString() + " " +this.ValorServico;
    }
}