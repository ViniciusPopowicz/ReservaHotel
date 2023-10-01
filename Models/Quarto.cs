//criado por Vinicius
using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;

public class Quarto
{
    [Key]
    public int NroQuarto {get; set;}

    [Required]
    public int NroHospedes {get; set;}

    [Required]
    public float Valor {get; set;}

    public Quarto()
    {
    }

    public Quarto(int nroQuarto, int nroHospedes, float valor)
    {
        this.NroQuarto = nroQuarto;
        this.NroHospedes = nroHospedes;
        this.Valor = valor;
    }
}

