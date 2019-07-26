using CareebizExam.Model;
using CareebizExam.Repositories;

namespace CareebizExam.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CareebizExamDbContext _context;
        public UnitOfWork(CareebizExamDbContext context)
        {
            _context = context;
        }


        private IShapesRepository _shapesRepository;
        public IShapesRepository Shapes => _shapesRepository ?? (_shapesRepository = new ShapesRepository(_context));

        public void Dispose()
        {
            _context?.Dispose();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
