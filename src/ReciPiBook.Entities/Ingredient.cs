using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReciPiBook.Entities
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        public int Rank { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public int? UomId { get; set; }

        public bool Optional { get; set; }

        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }

        [ForeignKey("UomId")]
        public UnitOfMeasure Uom { get; set; }
    }
}
