using System.Text;
using System.Text.RegularExpressions;

namespace App
{
    public class Helper
    {
        static char[] specialChars = { '!', '?', '.', ',' };  // символы, на которые строка должна заканчиваться

        // проверяет строку HTML на присутствие атрибутов в тегах
        public bool ContainsAttributes(String html)
        {
            string pattern = @"<(\w+\s+[^=>])*(\w+=[^>]+)+>";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(html);
        }

        // заменяет активные HTML символы на сущность
        public string EscapeHtml(string html, Dictionary<string, string>? map = null)
        {
            if (html is null) { throw new ArgumentException("Argument 'html' is null"); }
            if (html.Length == 0) { return html; }

            Dictionary<string, string> htmlSpecSymbols = map ?? new()  // если map null, то создаётся новый словарь
            {
                { "&", "&amp;" },
                { "<", "&lt;" },
                { ">", "&gt;" }
            };
            foreach (var pair in htmlSpecSymbols)
            {
                html = html.Replace(pair.Key, pair.Value);
            }
            return html;
        }

        // обрезает строку добавляя 3 точки в конце, учитывая макс. длину, который текст должен иметь
        public string Ellipsis(string input, int len)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));  // с помощью nameof строка в пул не будет занесена
            }

            // проверка диапазона ввода
            if (len < 3)
            {
                throw new ArgumentException("Argument 'len' could not be less then 3");
            }
            if (len > input.Length)
            {
                throw new ArgumentOutOfRangeException("Argument 'len' could not be greater than input length");
            }

            //return "He...";
            //return (len > 5)?"Hel...":"He...";
            //return "Hel"[..(len-3)]+"...";
            return input[..(len - 3)] + "...";
        }

        // добавляет точку если в строке её нет
        public string Finalize(string input)
        {
            int len = input.Length;

            // дополнительная проверка, если уже какие-то символы есть в конце строки
            return (len > 0 && !specialChars.Contains(input[len - 1])) ? input += "." : input;
        }

        // объединяет элементы массива в адресную строку
        public string CombineUrl(params string[] parts)
        {
            if (parts is null) { throw new NullReferenceException("Parts is null"); }
            if (parts.Length == 0) { throw new ArgumentException("Parts is empty"); }

            StringBuilder result = new();  // будет много работы с добавлением строк, поэтому используем StringBuilder
            string temp;
            bool wasNull = false;  // для проверки, что если после null идёт строка, то это ошибка
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] is null)
                {
                    wasNull = true;  // устанавливаем флаг
                    continue; 
                }
                if (wasNull)
                {
                    throw new ArgumentException("Non-Null argument after Null one");
                }

                if (parts[i] == "..") { continue; }  // игнорируем строку
                temp = "/" + parts[i].TrimStart('/').TrimEnd('/');  // удаляем все '/' и добавляем один в начало

                if ((i != parts.Length - 1) && parts[i + 1] == "..") { continue; }  // если строка не последняя в массиве и следующая строка это '..'
                result.Append(temp);
            }
            if (result.Length == 0)
            {
                throw new ArgumentException("All arguments are null");
            }
            return result.ToString();


            // ------------- Третья версия -------------
            //StringBuilder result = new();
            //string temp;
            //foreach (string part in parts)
            //{
            //    // !!! СВОИ ЦИКЛЫ СДЕЛАЛ, НО ПОТОМ НАШЁЛ ГОТОВЫЕ МЕТОДЫ TrimStart() и TrimEnd() !!!
            //    // while (temp[0] == '/') { temp = temp[1..^0]; temp = "/" + temp }
            //    // while (temp[temp.Length - 1] == '/') { temp = temp[..^1]; }

            //    temp = "/" + part.TrimStart('/').TrimEnd('/');
            //    result.Append(temp);
            //}
            //return result.ToString();


            // ------------- Вторая версия -------------
            //StringBuilder result = new();
            //string temp;
            //foreach (string part in parts)
            //{
            //    temp = part;
            //    if (!part.StartsWith('/'))
            //    {
            //        temp = "/" + temp;
            //    }
            //    if (part.EndsWith('/'))
            //    {
            //        temp = temp[..^1];  // от 0 до предпоследнего
            //    }
            //    result.Append(temp);
            //}
            //return result.ToString();


            // ------------- Первая версия -------------
            //if (!part1.StartsWith('/'))
            //{
            //    part1 = "/" + part1;
            //}
            //if (part1.EndsWith('/'))
            //{
            //    part1 = part1[0..^1];  // от 0 до предпоследнего
            //}

            //if (!part2.StartsWith('/'))
            //{
            //    part2 = "/" + part2;
            //}
            //if (part2.EndsWith('/'))
            //{
            //    part2 = part2[..^1];
            //}

            //return $"{part1}{part2}";
        }
    }
}
