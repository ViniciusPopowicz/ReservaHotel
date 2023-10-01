using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;
public class Hotel
{
    [Key]
    public int IdHotel {get; set;}
    [Required]
    public string Nome { get; set; }
    [Required]
    public string Cidade { get; set; }
    [Required]
    public string Pais { get; set; }
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
    [Required]
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
