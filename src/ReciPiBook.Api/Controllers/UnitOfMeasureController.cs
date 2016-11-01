using Microsoft.AspNetCore.Mvc;
using ReciPiBook.Services.UnitOfMeasure;

namespace ReciPiBook.Api.Controllers
{
    [Route("api/[controller]")]
    public class UnitOfMeasureController : Controller
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;

        public UnitOfMeasureController(IUnitOfMeasureService unitOfMeasureService)
        {
            _unitOfMeasureService = unitOfMeasureService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public string Get(int id)
        {
            return _unitOfMeasureService.Get(id).Description;
        }
    }
}
