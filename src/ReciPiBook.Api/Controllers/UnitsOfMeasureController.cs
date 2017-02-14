using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReciPiBook.Services.UnitOfMeasure;

namespace ReciPiBook.Api.Controllers
{
    [Route("api/[controller]")]
    public class UnitsOfMeasureController : Controller
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;

        public UnitsOfMeasureController(IUnitOfMeasureService unitOfMeasureService)
        {
            _unitOfMeasureService = unitOfMeasureService;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(ActiveAuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        public Dtos.UnitOfMeasure Get(int id) => _unitOfMeasureService.Get(id);

        [HttpPost]
        public int Create([FromBody] Dtos.UnitOfMeasure uom) => _unitOfMeasureService.Create(uom);

        [HttpPut]
        public void Update([FromBody] Dtos.UnitOfMeasure uom) => _unitOfMeasureService.Update(uom);

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id) => _unitOfMeasureService.Delete(id);
    }
}
