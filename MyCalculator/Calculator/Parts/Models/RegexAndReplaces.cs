namespace MyCalculator.Calculator.Parts.Models
{
    public class RegexAndReplaces//Для обычных выражений, в которых нет скобок
    {
        public string RegexStr { get; set; }//Регулярное выражение
        public bool IsTwoExpressions { get; set; }//Имеется два выражения? 
        public string ReplaceStr { get; set; }//Выражение для замены
        public string SecondReplaceStr { get; set; }//Второе выражение для замены
    }
}
