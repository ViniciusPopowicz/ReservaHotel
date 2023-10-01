using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;

public class Premio
{
    [Key]
    public int IdPremio{get;set;}
    [Required]
    public string Descricao{get;set;}


    public Premio()
    {
    }

}

