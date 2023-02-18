using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.Utils
{
    public class Validation
    {
        Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
        public string RgexString(string str) {
            Match result = re.Match(str);
            string alphaPart = result.Groups[1].Value;
            string numberPart = result.Groups[2].Value +1;
            str = alphaPart + numberPart;
            return str;
        }
        public static bool ValidateCapitalName(string str) {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(str).Equals(str);
        }
    }
}
