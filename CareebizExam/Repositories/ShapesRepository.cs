using System.Collections.Generic;
using System.Linq;
using CareebizExam.Infrastructure;
using CareebizExam.Model;

namespace CareebizExam.Repositories
{
    public class ShapesRepository : Repository<Shapes>, IShapesRepository
    {
        public ShapesRepository(CareebizExamDbContext dbContext) : base(dbContext)
        {
        }


        public IEnumerable<Shapes> GetShapesByIds(int[] ids)
        {
           return CareebizExamDbContext.Shapes.Where(s => ids.Contains(s.ShapeId));
        }
        public CareebizExamDbContext CareebizExamDbContext
        {
            get { return _dbContext as CareebizExamDbContext; }
        }
    }

    public interface IShapesRepository : IRepository<Shapes>
    {
        IEnumerable<Shapes> GetShapesByIds(int[] ids);
    }
}
