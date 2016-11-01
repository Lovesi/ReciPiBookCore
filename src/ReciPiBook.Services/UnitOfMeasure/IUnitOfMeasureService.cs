using System;

namespace ReciPiBook.Services.UnitOfMeasure
{
    public interface IUnitOfMeasureService : IDisposable
    {
        int Create(Dtos.UnitOfMeasure uom);
        Dtos.UnitOfMeasure Get(int id);
        void Update(Dtos.UnitOfMeasure uom);
        void Delete(int id);
    }
}
