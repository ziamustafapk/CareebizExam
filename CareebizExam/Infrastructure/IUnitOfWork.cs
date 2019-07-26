using System;
using CareebizExam.Repositories;

namespace CareebizExam.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IShapesRepository Shapes { get; }
        void SaveChanges();
    }

}
