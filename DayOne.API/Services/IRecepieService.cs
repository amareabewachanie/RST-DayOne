using DayOne.API.Entities;

namespace DayOne.API.Services
{
    public interface IRecepieService
    {
        public Task<IList<Recepie>> GetAll();
        public Task<Recepie> GetOne(int id);
        public Recepie Add(Recepie recepie);
        public bool Remove(Recepie recepie);

    }
}
