using ReciPiBook.Entities;
using ReciPiBook.Repository;

namespace ReciPiBook.Services
{
    public class TestService : ITestService
    {
        private readonly IRepository<UnitOfMeasure> _repository;

        public TestService(IRepository<UnitOfMeasure> repository)
        {
            _repository = repository;
        }

        public string SayHello()
        {
            return "Hello world service!";
        }
    }
}
