using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserCore.Data;
using UserCore.Models;

namespace UserCore.Repositories
{
    public class UserRepository : IRepository<Oldwhite, int>
    {
        private readonly SchoolContext _context;
        public UserRepository(SchoolContext context)
        {
            _context = context;
        }
        int IRepository<Oldwhite, int>.Create(Oldwhite entity)
        {
            _context.Oldwhite.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        void IRepository<Oldwhite, int>.Delete(int id)
        {
            _context.Oldwhite.Remove(_context.Oldwhite.Find(id));
            _context.SaveChanges();
        }

        IEnumerable<Oldwhite> IRepository<Oldwhite, int>.Find(Expression<Func<Oldwhite, bool>> expression)
        {
            return _context.Oldwhite.Where(expression);
        }

        Oldwhite IRepository<Oldwhite, int>.FindById(int id)
        {
            return _context.Oldwhite.SingleOrDefault(o => o.Id == id);
        }

        void IRepository<Oldwhite, int>.Update(Oldwhite entity)
        {
            var white = _context.Oldwhite.SingleOrDefault(o => o.Id == entity.Id);
            _context.Entry(white).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
    }
}
