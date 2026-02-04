using System;
using System.Collections.Generic;

namespace SabroRH.Schemas
{
    public class Recibo
    {
        public string cnpjEmpresa { get; set; }
        public string cpf { get; set; }
        public string anoMes { get; set; }
        public string rotina { get; set; }
        public DateTime dataPagamento { get; set; }

        public List<Evento> eventos { get; set; } = new List<Evento>();
    }

    public class Evento
    {
        public string descricao { get; set; }
        public string tipo { get; set; }
        public decimal referencia { get; set; }
        public decimal valor { get; set; }
        public string eventoId { get; set; }
    }
}
