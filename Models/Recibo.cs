using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;  


public class Recibo
{
    [Key]
    public int IdRecibo {get;set;}
    public Pagamento Pagamento{get;set;}

    public Premio Premio{get;set;}

    public Recibo(){}
}
