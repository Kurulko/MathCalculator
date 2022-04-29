using Microsoft.AspNetCore.Identity;
using MyCalculator.Models;
using System.Collections.Generic;

namespace MyCalculator.Models
{
    public class User : IdentityUser
    {
        public Color ThemeColor { get; set; }
        public Language Language { get; set; }
        public List<ExpressionAndSolution> Expressions { get; set; } = new();
    }
}
