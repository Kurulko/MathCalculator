using System;
using System.ComponentModel.DataAnnotations;

namespace MyCalculator.Models
{
    public class ExpressionModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a mathematical expression")]
        [Display(Name = "Mathematical expression")]
        public string Expression { get; set; }
    }
}
