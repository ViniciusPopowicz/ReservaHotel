//Criado por Leonardo
using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;
public class Voucher{

[Key]
public int IdVoucher { get; set; }

[Required]
public float Desconto {get; set;}

public Voucher(){

}

public Voucher(int IdVoucher, float Desconto){
    this.Desconto=Desconto;
}

}