using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ReciPiBook.Api.Extensions
{
    public static class ModelStateExtensions
    {
        public static ModelStateDictionary AddErrors(this ModelStateDictionary modelStateDictionary, IdentityResult result)
        {
            if (result == null) return null;

            foreach (var error in result.Errors)
            {
                modelStateDictionary.AddModelError(string.Empty, error.Description);
            }

            return modelStateDictionary;
        }
    }
}
