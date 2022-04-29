namespace MyCalculator.Calculator.Parts.Models
{
    public class RegexAndReplacesWithBrackets//Для выражений, в которых есть скобкu
    {
        public string RegexStr { get; set; }//Регулярное выражение
        public bool IsTwoExpressions { get; set; }//Имеется два выражения?
        public string MainReplaceStr { get; set; }//Основное выражение для замены
        public string ReplaceStr { get; set; }//Выражение для замены
        public string SecondReplaceStr { get; set; }//Второе выражение для замены
    }
}
