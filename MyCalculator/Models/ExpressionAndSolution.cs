using MyCalculator.Models;
using MyCalculator.Calculator.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System;

namespace MyCalculator.Models
{
    public class ExpressionAndSolution
    {
        public int Id { get; set; }
        public string ExpressionStr { get; set; }
        public string ExpressionHtml { get; set; }
        public decimal Answer { get; set; }
        public DateTime Time { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public List<Solution> Solutions { get; set; } = new();
        public List<Warning> Warnings { get; set; } = new();
        public List<Error> Errors { get; set; } = new();
    }
}
