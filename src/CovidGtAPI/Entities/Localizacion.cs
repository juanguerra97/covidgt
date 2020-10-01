namespace CovidGtAPI.Entities
{
    public class Localizacion
    {

        public static readonly int MAX_PAIS_LENGTH = 128;
        public static readonly int MAX_DEPARTAMENTO_LENGTH = 32;
        public static readonly int MAX_MUNICIPIO_LENGTH = 128;

        public string Pais { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
    }
}