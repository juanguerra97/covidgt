using System.Collections.Generic;
using System.Threading.Tasks;
using CovidGtAPI.Common;
using CovidGtAPI.Entities;

namespace CovidGtAPI.Services
{
    public interface ICasosCovidService
    {
        
        Task<List<CasoCovid>> GetAll(Filtro filtro);
        Task<long> Insert(CasoCovid nuevoCaso);
        Task Delete(long idCaso);

    }
}