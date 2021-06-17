using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
  {
      public class Dish
      {
        [Key] // the below prop is the primary key, [Key] is not needed if named with pattern: ModelNameId
        public int DishId { get; set; }
 
        [Required(ErrorMessage = "is required")]
        [Display(Name = "Chef's Name:")]
        public string ChefName { get; set; }

        [Required(ErrorMessage = "is required")]
        [Display(Name = "Name of Dish:")]
        public string DishName { get; set; }

        [Required(ErrorMessage = "must be greater than zero")]
        [Display(Name = "# of Calories:")]
        [Range(1,5000)]
        public int Calories { get; set; }

        [Required(ErrorMessage = "must be between 1 and 5")]
        [Display(Name = "Tastiness:")]
        [Range(1,5)]
        public int Tastiness { get; set; }

        [Required(ErrorMessage = "is required")]
        [Display(Name = "Description:")]
        public string Description { get; set; }
 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
      }
  }