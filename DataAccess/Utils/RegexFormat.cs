using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.Utils
{
    public class RegexFormat
    {
        public static string FormatId(string input)
        {
            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match result = re.Match(input);

            string alphaPart = result.Groups[1].Value;
            string numberPart = (int.Parse(result.Groups[2].Value) + 1).ToString("d4");
            return String.Concat(alphaPart, numberPart);
        }
    }
}
