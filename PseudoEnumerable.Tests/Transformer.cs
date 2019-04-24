using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    public interface ITransformer<in TSource, out TResult>
    {
        TResult Transform(TSource item);
    }

    public interface IDictionaryCreater
    {
        Dictionary<char, string> GetWords();

        Dictionary<double, string> GetSpecialDoubles();
    }

    public abstract class Transformer : ITransformer<double, string>
    {
        private IDictionaryCreater dictionaryCreater;

        protected IDictionaryCreater DictionaryCreater { get => dictionaryCreater; set => dictionaryCreater = value; }

        public virtual string Transform(double number)
        {
            Dictionary<double, string> specialNumbers = DictionaryCreater?.GetSpecialDoubles();
            if (specialNumbers.TryGetValue(number, out string result))
            {
                return result;
            }

            Dictionary<char, string> words = DictionaryCreater?.GetWords();
            return GetWordFormat(number, words);
        }

        private string GetWordFormat(double number, Dictionary<char, string> words)
        {
            var numbersString = number.ToString(CultureInfo.InvariantCulture);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < numbersString.Length; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(" ");
                }

                stringBuilder.Append(words[numbersString[i]]);
            }

            return stringBuilder.ToString();
        }
    }

    public class RusssianTransformer : Transformer, IDictionaryCreater
    {
        public RusssianTransformer() => DictionaryCreater = this;

        public Dictionary<double, string> GetSpecialDoubles() => new Dictionary<double, string>
        {
            [double.PositiveInfinity] = "положительная бесконечность",
            [double.NegativeInfinity] = "отрицательная бесконечность",
            [double.NaN] = "не является числом"
        };

        public Dictionary<char, string> GetWords() => new Dictionary<char, string>
        {
            ['0'] = "нуль",
            ['1'] = "один",
            ['2'] = "два",
            ['3'] = "три",
            ['4'] = "четыре",
            ['5'] = "пять",
            ['6'] = "шесть",
            ['7'] = "семь",
            ['8'] = "восемь",
            ['9'] = "девять",
            ['-'] = "минус",
            [','] = "точка",
            ['.'] = "точка",
            ['e'] = "экспонента",
            ['+'] = "плюс"
        };
    }
}
