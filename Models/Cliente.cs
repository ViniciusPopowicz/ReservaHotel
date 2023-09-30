using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;  
public class Cliente
{
    [Key]
    public string Cpf { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Telefone { get; set; }

    public Cliente()
    {
    }

    public Cliente(string cpf, string nome, string telefone)
    {
        this.Cpf = cpf;
        this.Nome = nome;
        this.Telefone = telefone;
    }
}
