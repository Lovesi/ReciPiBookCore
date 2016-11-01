using System.Collections.Generic;
using System.Linq;

namespace ReciPiBook.Translators
{
    public static class UnitOfMeasureTranslators
    {
        public static IEnumerable<Dtos.UnitOfMeasure> AsUnitOfMeasures(this IEnumerable<Entities.UnitOfMeasure> values)
        {
            return values.Select(x => x.AsUnitOfMeasure());
        }

        public static Dtos.UnitOfMeasure AsUnitOfMeasure(this Entities.UnitOfMeasure value)
        {
            return value == null ? null : new Dtos.UnitOfMeasure
            {
                Id = value.Id,
                Abbreviation = value.Abbreviation,
                Description = value.Description
            };
        }

        public static IEnumerable<Entities.UnitOfMeasure> AsUnitOfMeasures(this IEnumerable<Dtos.UnitOfMeasure> values)
        {
            return values.Select(x => x.AsUnitOfMeasure());
        }

        public static Entities.UnitOfMeasure AsUnitOfMeasure(this Dtos.UnitOfMeasure value)
        {
            return value == null ? null : new Entities.UnitOfMeasure
            {
                Id = value.Id,
                Abbreviation = value.Abbreviation,
                Description = value.Description
            };
        }
    }
}
