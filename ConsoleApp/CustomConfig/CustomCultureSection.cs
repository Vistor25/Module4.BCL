using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CustomConfig
{
    public class CustomCultureSection: ConfigurationSection
    {
        public const string sectionName = "Culture";

        [ConfigurationProperty("")]
        public Culture Culture
        {
            get
            {
                return Culture;
            }
        }

        public static CustomCultureSection GetSection()
        {
            return (CustomCultureSection)ConfigurationManager.GetSection(sectionName);
        }
    }
}
