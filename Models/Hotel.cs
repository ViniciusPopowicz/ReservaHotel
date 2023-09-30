using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;
public class Hotel
{
    [Key]
    public string Nome { get; set; }

    public string Cidade { get; set; }

    public string Pais { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public int NumQuartos { get; set; }

    public Hotel()
    {
    }

    public Hotel(string nome, string cidade, string pais, int rating, int numQuartos)
    {
        this.Nome = nome;
        this.Cidade = cidade;
        this.Pais = pais;
        this.Rating = rating;
        this.NumQuartos = numQuartos;
    }
}
