//criado por Vinicius
using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;

public class Quarto
{
    [Key]
    public int nroQuarto {get; set;}

    [Required]
    public int nroHospedes {get; set;}

    [Required]
    public float valor {get; set;}

    public Quarto()
    {
    }

    public Quarto(int nroQuarto, int nroHospedes, float valor)
    {
        this.nroQuarto = nroQuarto;
        this.nroHospedes = nroHospedes;
        this.valor = valor;
    }
}

