using System;
using ReciPiBook.Repository;
using ReciPiBook.Translators;

namespace ReciPiBook.Services.UnitOfMeasure
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly IRepository<Entities.UnitOfMeasure> _repository;
        private bool _disposed = false;

        public UnitOfMeasureService(IRepository<Entities.UnitOfMeasure> repository)
        {
            _repository = repository;
        }

        public int Create(Dtos.UnitOfMeasure uom)
        {
            return _repository.Add(uom.AsUnitOfMeasure()).Id;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Dtos.UnitOfMeasure Get(int id)
        {
            return _repository.Get(id).AsUnitOfMeasure();
        }

        public void Update(Dtos.UnitOfMeasure uom)
        {
            var toUpdate = _repository.Get(uom.Id);
            toUpdate.Abbreviation = uom.Abbreviation;
            toUpdate.Description = uom.Description;
            _repository.Update(toUpdate);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
                _repository.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
