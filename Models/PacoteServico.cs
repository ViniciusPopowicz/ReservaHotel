using System;
using System.ComponentModel.DataAnnotations;
using RESERVA_HOTEL.Models;

namespace RESERVA_HOTEL.Models;

    public class PacoteServico{

        public int idPacoteServico{get;set;}
        public int PacotesIdPacte{get;set;}
        public int ServicosIdServico{get;set;}

        public PacoteServico(){}
        
    }