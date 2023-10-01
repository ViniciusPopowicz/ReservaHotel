//criado por Vinicius
using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;

public class Reserva
{
    [Key]
    public int IdReserva {get; set;}

    [Required]
    public string DataReserva {get; set;}

    [Required]
    public string DataCheckIn {get; set;}

    [Required]
    public string DataCheckOut {get; set;}

    [Required]
    public Quarto Quarto {get; set;}

    [Required]
    public Hotel Hotel {get; set;}

    [Required]
    public Pacote Pacote {get; set;}

    [Required]
    public Cliente Cliente {get; set;}

    [Required]
    public Voucher Voucher {get; set;}

    [Required]
    public float ValorReserva {get; set;}

    public Reserva()
    {
    }

    public Reserva(string DataReserva, string DataCheckIn, string DataCheckOut, Quarto Quarto, Hotel Hotel, Pacote Pacote, Cliente Cliente, Voucher Voucher, float ValorReserva){
        this.DataReserva = DataReserva;
        this.DataCheckIn = DataCheckIn;
        this.DataCheckOut = DataCheckOut;
        this.Quarto = Quarto;
        this.Hotel = Hotel;
        this.Pacote = Pacote;
        this.Cliente = Cliente;
        this.Voucher = Voucher;
        this.ValorReserva = ValorReserva;
    }
}

