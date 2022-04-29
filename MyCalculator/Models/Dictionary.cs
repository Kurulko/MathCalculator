using System.Collections.Generic;
using System.Linq;

namespace MyCalculator.Models
{
    public class Dictionary
    {
        public Language Language { get; set; }
        public CalculatorContext Db { get; set; }

        public Dictionary(Language language, CalculatorContext context)
        {
            Db = context;
            Language = language;
        }
        public string Translate(string str)
        {
            string result = string.Empty;
            Word word = Db.Words.FirstOrDefault(w => w.English == str);
            if (word != null)
            {
                switch (Language)
                {
                    case Language.English:
                        result = word.English;
                        break;
                    case Language.Russian:
                        result = word.Russian;
                        break;
                }
            }
            return result;
        }
    }
}
