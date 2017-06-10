using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;

namespace FashionAde.ApplicationServices
{
    public class RegExpTemplatorHelper
    {
        private static object temporalData;
        private static string prefix;

        private static string ReplaceMatchInLoop(Match m)
        {
            if (!string.IsNullOrEmpty(m.Value))
            {
                string loopText = m.Value.Replace("|", "");

                // Find first match of property to get the collection we are looping.
                Regex regMatches = new Regex(@"\$[^\$]*.");
                MatchCollection propMatch = regMatches.Matches(loopText);

                string firstProperty = string.Empty;

                foreach (Match matchValue in propMatch) {
                    string val = matchValue.Value.Replace("$", "");
                    if (val.LastIndexOf("[") >= 0)
                    {
                        firstProperty = val.Substring(0, val.LastIndexOf("["));
                        break;
                    }
                }

                if (string.IsNullOrEmpty(firstProperty))
                    return m.Value;

                // We need to find the collection in the temporalData
                IEnumerable enumerable = FindPropertyValue(temporalData, firstProperty.Split('.'), 1) as IEnumerable;
                if (enumerable == null)
                    return m.Value;

                string output = string.Empty;

                int i = 0;
                prefix = firstProperty.Substring(0, firstProperty.LastIndexOf("."));
                foreach (object o in enumerable)
                {
                    string currentText = loopText.Replace("[N]", string.Format("[{0}]", i));
                    output += regMatches.Replace(currentText, new MatchEvaluator(ReplaceMatch));
                    i++;
                }

                return output;
            }

            return m.Value;
        }

        private static string ReplaceMatch(Match m)
        {
            if (!string.IsNullOrEmpty(m.Value))
            {
                string[] properties = m.Value.Replace("$", "").Split('.');

                // If the prefix is not defined, it means its a direct value.
                if (string.IsNullOrEmpty(prefix))
                    return temporalData.ToString();

                string firstProperty = properties[0];
                int startIndex = 1;

                if (firstProperty.EndsWith("]"))
                {
                    startIndex = 0;
                    firstProperty = firstProperty.Substring(0, firstProperty.LastIndexOf("["));
                }

                if (firstProperty == prefix)
                    return FindPropertyValue(temporalData, properties, startIndex).ToString();
            }

            return m.Value;
        }

        public static object FindPropertyValue(object temporalData, string[] properties, int index)
        {
            if (temporalData == null)
                return string.Empty;

            object val = null;
            
            string currentProperty = properties[index];
            int indexerValue = -1;
            if (currentProperty.EndsWith("]"))
            {
                indexerValue = Convert.ToInt32(currentProperty.Substring(currentProperty.LastIndexOf("[") + 1, currentProperty.LastIndexOf("]") - currentProperty.LastIndexOf("[")-1));
                currentProperty = currentProperty.Substring(0, currentProperty.LastIndexOf("["));
            }

            PropertyInfo pi = temporalData.GetType().GetProperty(currentProperty);

            if (pi != null)
            {
                if (properties.Length <= index + 1)
                    val = pi.GetValue(temporalData, null);
                else
                {
                    object findObject;

                    if (indexerValue >= 0)
                        findObject = ((IList)pi.GetValue(temporalData, null))[indexerValue];
                    else
                        findObject = pi.GetValue(temporalData, null);

                    val = FindPropertyValue(findObject, properties, index + 1);
                }
            }

            return val;
        }

        public static string SetObjectProperties(string text, object data)
        {
            if (data == null)
                return text;

            temporalData = data;

            // If it is not value type, we will try to find the property
            prefix = string.Empty;
            if (!data.GetType().IsValueType && data.GetType() != typeof(String))
                prefix = data.GetType().Name;

            Regex regMatches = new Regex(@"\|[^\|]*.");
            text = regMatches.Replace(text, new MatchEvaluator(ReplaceMatchInLoop));

            regMatches = new Regex(@"\$[^\$]*.");
            return regMatches.Replace(text, new MatchEvaluator(ReplaceMatch));
        }
    }

}
