using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni_Sphere.Utility
{
    // Static Data
    public static class SD
    {
        // Roles
        public const string Role_Admin = "Admin";
        public const string Role_Teacher = "Teacher";
        public const string Role_Student = "Student";

        /// Adds spaces before each capital letter in a string except the first one.
        public static string AddSpacesToPascalCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);

            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && char.IsLower(text[i - 1]))
                    newText.Append(' ');
                newText.Append(text[i]);
            }

            return newText.ToString();
        }
    }

}
