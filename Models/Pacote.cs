using System;
using System.ComponentModel.DataAnnotations;
using ReservaHotel.Models;

namespace ReservaHotel.Models;

    public class Pacote
    {
        [Key]
        public int IdPacote { get; set; }

        public List<Servico> Servicos { get; set; }

        public float ValorPacote { get; set; }

        public Pacote()
        {
        }

        public Pacote(int idPacote, List<Servico> servicos, float valorPacote)
        {
            this.IdPacote = idPacote;
            this.Servicos = servicos;
            this.ValorPacote = valorPacote;
        }
    }
