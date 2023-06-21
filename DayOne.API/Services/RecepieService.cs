using DayOne.API.Context;
using DayOne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DayOne.API.Services
{
    public class RecepieService : IRecepieService
    {
        private readonly CateringContext _context;

        public RecepieService(CateringContext context)
        {
            _context = context;
        }
        public Recepie Add(Recepie recepie)
        {
            _context.Recepie.Add(recepie);
             _context.SaveChanges();
            return recepie;

        }

        public async Task<IList<Recepie>> GetAll()
        =>await _context.Recepie.ToListAsync();

        public async Task<Recepie> GetOne(int id)
        => await _context.Recepie.FindAsync(id);

        public bool Remove(Recepie recepie)
        {
            _context.Recepie.Remove(recepie);
            return _context.SaveChanges() > 0;
        }
    }
}
