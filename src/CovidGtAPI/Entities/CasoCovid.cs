using System;

namespace CovidGtAPI.Entities
{
    public class CasoCovid
    {
        public Localizacion Localizacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
    }
}