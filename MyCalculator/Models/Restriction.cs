using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCalculator.Models
{
    public class Restriction
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Expression { get; set; }
    }
}
