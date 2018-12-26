using System;
using System.Collections.Generic;
using System.Text;

namespace FirstKata_StringCalculator
{
    public class StringCalculator
    {
        List<Char> separatorAllowedList = new List<char>();

        public int Add(string numbers)
        {
            int result = 0;
            if(string.IsNullOrEmpty(numbers))
                return result;

            separatorAllowedList.Add(',');
            separatorAllowedList.Add('\n');

            if (numbers.StartsWith("//"))
            {
                List<Char> delimeters = getDelimiters(numbers);
                if(delimeters.Count > 0)
                {
                    separatorAllowedList.Clear();
                    separatorAllowedList.AddRange(delimeters);
                    numbers = numbers.Split('\n')[1];
                }
            }
            var arrayNumbers = numbers.Split(separatorAllowedList.ToArray());
            List<string> negatives = new List<string>();
            try
            {
                foreach (var n in arrayNumbers)
                {
                    var number = int.Parse(n);
                    if (number >= 0 && number <= 1000)
                    {
                        result += number;
                    }
                    else
                    {
                        if(number < 0)
                            negatives.Add(n);
                    }
                }

                if (negatives.Count > 0) {
                    var negativesFound = String.Join(", ", negatives.ToArray());
                    throw new Exception("Negatives found: " + negativesFound);
                }
            }catch(Exception e)
            {
                throw e;
            }

            return result;
        }

        private List<char> getDelimiters(string numbers)
        {
            // //;\n1;2
            List<Char> delimeters = new List<char>();

            int posA = numbers.IndexOf("//");
            int posB = numbers.LastIndexOf("\n");
            if (posA == -1)  return delimeters;
            if (posB == -1)  return delimeters;

            int adjustedPosA = posA + "//".Length;
            if (adjustedPosA >= posB)
                return delimeters;

            var delimitersString = numbers.Substring(adjustedPosA, posB - adjustedPosA);
            
            foreach(Char c in delimitersString)
            {
                if (c != '[' && c != ']')
                    delimeters.Add(c);
            }

            return delimeters;
        }
    }
}
