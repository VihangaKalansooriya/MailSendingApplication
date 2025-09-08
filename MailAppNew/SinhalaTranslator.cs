using System;
using System.Collections.Generic;

namespace MailAppNew
{
    internal class SinhalaTranslator
    {
        static Dictionary<string, string> independentVowels = new Dictionary<string, string>()
        {
            {"a", "අ"}, {"aa", "ආ"},
            {"i", "ඉ"}, {"ii", "ඊ"},
            {"u", "උ"}, {"uu", "ඌ"},
            {"e", "එ"}, {"ee", "ඒ"},
            {"ai", "ඓ"},
            {"o", "ඔ"}, {"oo", "ඕ"},
            {"au", "ඖ"}
        };

        static Dictionary<string, string> consonants = new Dictionary<string, string>()
        {
            {"kh", "ඛ"}, {"gh", "ඝ"}, {"ch", "ච"}, {"jh", "ඣ"},
            {"ṭh", "ඨ"}, {"ḍh", "ඪ"}, {"dh", "ධ"},
            {"ph", "ඵ"}, {"bh", "භ"}, {"sh", "ශ"},

            {"k", "ක්"}, {"g", "ග්"}, {"j", "ජ්"},
            {"t", "ට්"}, {"ḍ", "ඩ්"}, {"th", "ත්"}, {"d", "ද්"},
            {"n", "න්"}, {"p", "ප්"}, {"b", "බ්"}, {"m", "ම්"},
            {"y", "ය්"}, {"r", "ර්"}, {"l", "ල්"}, {"v", "ව්"},
            {"s", "ස්"}, {"h", "හ්"}, {"L", "ළ්"}, {"f", "ෆ්"}
        };

        static Dictionary<string, string> vowelSigns = new Dictionary<string, string>()
        {
            {"aa", "ා"}, {"i", "ි"}, {"ii", "ී"}, {"u", "ු"}, {"uu", "ූ"},
            {"e", "ෙ"}, {"ee", "ේ"}, {"ai", "ෛ"}, {"o", "ො"}, {"oo", "ෝ"}, {"au", "ෞ"}
        };
        public static string ConvertToSinhala(string input)
        {
            string output = input.ToLower();

            // Step 1: Handle consonant + vowel
            foreach (var con in consonants.OrderByDescending(c => c.Key.Length))
            {
                foreach (var vow in vowelSigns.OrderByDescending(v => v.Key.Length))
                {
                    string pattern = con.Key + vow.Key;

                    // remove the halant "්" when vowel is present
                    string replace = consonants[con.Key].Replace("්", "") + vowelSigns[vow.Key];

                    output = output.Replace(pattern, replace);
                }

                // Special case: consonant + "a" → remove "්"
                string patternA = con.Key + "a";
                string replaceA = consonants[con.Key].Replace("්", ""); // just consonant letter
                output = output.Replace(patternA, replaceA);
            }

            // Step 2: Independent vowels
            foreach (var vow in independentVowels.OrderByDescending(v => v.Key.Length))
            {
                output = output.Replace(vow.Key, vow.Value);
            }

            // Step 3: Remaining consonants (with hal)
            foreach (var con in consonants.OrderByDescending(c => c.Key.Length))
            {
                output = output.Replace(con.Key, con.Value);
            }

            return output;
        }

    }
}
