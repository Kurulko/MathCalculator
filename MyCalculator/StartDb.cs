using MyCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCalculator
{
    public static class StartDb
    {
        public static void Initialize(CalculatorContext db)
        {
            if (!db.Words.Any())
            {
                List<Word> words = new List<Word>
                {
                    new Word { English = "General:", Russian = "Общие:"},
                    new Word { English = "Input rules", Russian = "Правила ввода"},
                    new Word { English = "Calculate!", Russian = "Посчитать!"},
                    new Word { English = "Calculator", Russian = "Калькулятор"},
                    new Word { English = "Solution", Russian = "Решение"},
                    new Word { English = "History", Russian = "История"},
                    new Word { English = "Expression", Russian = "Выражение"},
                    new Word { English = "Answer", Russian = "Ответ"},
                    new Word { English = "Time", Russian = "Время"},
                    new Word { English = "Details", Russian = "Детали"},
                    new Word { English = "Delete", Russian = "Удалить"},
                    new Word { English = "* Only correctly entered data is saved", Russian = "* Только корректно введенные данные сохраняються"},
                    new Word { English = "Rules", Russian = "Правила"},
                    new Word { English = "Back", Russian = "Назад"},
                    new Word { English = "Figure:", Russian = "Число"},
                    new Word { English = "Trigonometry:", Russian = "Трегонометрия:"},
                    new Word { English = "Sinus", Russian = "Синус"},
                    new Word { English = "Cosine", Russian = "Косинус"},
                    new Word { English = "Tangent", Russian = "Тангенс"},
                    new Word { English = "Cotangent", Russian = "Котангенс"},
                    new Word { English = "Arcsinus", Russian = "Арксинус"},
                    new Word { English = "Arccosine", Russian = "Арккосинус"},
                    new Word { English = "Arctangent", Russian = "Арктангенс"},
                    new Word { English = "Arccotangent", Russian = "Аркатангенс"},
                    new Word { English = "Hyperbolic sinus", Russian = "Гиперболический синус"},
                    new Word { English = "Hyperbolic cosine", Russian = "Гиперболический косинус"},
                    new Word { English = "Hyperbolic tangent", Russian = "Гиперболический тангенс"},
                    new Word { English = "Hyperbolic cotangent", Russian = "Гиперболический катангенс"},
                    new Word { English = "Hyperbolic arcsinus", Russian = "Гиперболический арксинус"},
                    new Word { English = "Hyperbolic arccosine", Russian = "Гиперболический арккосинус"},
                    new Word { English = "Hyperbolic arctangent", Russian = "Гиперболический арктангенс"},
                    new Word { English = "Hyperbolic arccotangent", Russian = "Гиперболический аркатангенс"},
                    new Word { English = "Factorial:", Russian = "Факториал:"},
                    new Word { English = "Exponent:", Russian = "Экспонента:"},
                    new Word { English = "Extent:", Russian = "Степень:"},
                    new Word { English = "Square root:", Russian = "Квадратный корень:"},
                    new Word { English = "Module:", Russian = "Модуль:"},
                    new Word { English = "Logarithm:", Russian = "Логарифм:"},
                    new Word { English = "Natural logarithm:", Russian = "Натуральный логарифм:"},
                    new Word { English = "Brackets:", Russian = "Скобки:"},
                    new Word { English = "Plus:", Russian = "Плюс:"},
                    new Word { English = "Minus:", Russian = "Минус:"},
                    new Word { English = "Multiply:", Russian = "Умножить:"},
                    new Word { English = "Divide:", Russian = "Поделить:"},
                    new Word { English = "Remainder of the division:", Russian = "Остаток от деления:"},
                    new Word { English = "Name", Russian = "Имя"},
                    new Word { English = "Email", Russian = "Почта"},
                    new Word { English = "Password", Russian = "Пароль"},
                    new Word { English = "Enter your name!", Russian = "Введите ваше имя!"},
                    new Word { English = "Enter password", Russian = "Введите пароль"},
                    new Word { English = "Remember me?", Russian = "Запомнить меня?"},
                    new Word { English = "Enter your email address!", Russian = "Введите свою почту!"},
                    new Word { English = "Passwords do not match", Russian = "Пароли не совпадают"},
                    new Word { English = "Confirm password", Russian = "Повторный пароль"},
                    new Word { English = "Mathematical expression", Russian = "Математическое выражение"},
                    new Word { English = "Register", Russian = "Регистрация"},
                    new Word { English = "Login", Russian = "Вход"},
                    new Word { English = "in expression", Russian = "в выражении"},
                    new Word { English = "Count of left brackets != count of right brackets", Russian = "Кол-во левых скобок не совпадает с кол-вом правых"},
                    new Word { English = "It is not a mathematical expression", Russian = "Это не математическое выражение"},
                    new Word { English = "Cannot have two characters in a row", Russian = "Не может быть двух символов подряд"},
                    new Word { English = "This value is greater than 8E+28", Russian = "Это число больше чем 8E+28"},
                    new Word { English = "This value is less than -8E+28", Russian = "Это число меньше чем -8E+28"},
                    new Word { English = "The value is not between -1 and 1", Russian = "Значение больше 1 или меньше -1"},
                    new Word { English = "The value is not integer or less than zero", Russian = "Значение меньше нуля или не целое"},
                    new Word { English = "The value is less than zero", Russian = "Значение меньше нуля"},
                    new Word { English = "Case and spaces do not matter", Russian = "Регистр и пробелы не имеют значения"},
                    new Word { English = "Maximum allowable result:", Russian = "Максимальный допустимый результат:"},
                    new Word { English = "Minimum allowable result:", Russian = "Минимальный допустимый результат:"},
                    new Word { English = "", Russian = ""},
                };
                db.Words.AddRange(words);
                db.SaveChanges();
            }
        }
    }
}
