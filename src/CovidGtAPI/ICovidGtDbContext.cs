using CovidGtAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CovidGtAPI
{
    public interface ICovidGtDbContext
    {
         
        DbSet<CasoCovid> CasosCovid { get; set; }

    }
}