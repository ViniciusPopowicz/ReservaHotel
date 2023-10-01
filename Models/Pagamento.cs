using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;  

public enum  MetodoPagamento
{
    Pix,
    Boleto,
    Cartao
}

public class Pagamento
{
    [Key]
    public int IdPagamento {get;set;}
    public Reserva Reserva{get;set;}
    public float Valor {get;set;}
    public MetodoPagamento MetodoPagamento{get;set;}

    public Pagamento(){}
}
