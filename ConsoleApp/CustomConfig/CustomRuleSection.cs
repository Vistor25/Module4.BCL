using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.CustomConfig
{
    public class CustomRuleSection: ConfigurationSection
    {
        public const string sectionName = "FileRules";

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public RuleCollection FileRules
        {
            get
            {
                return this[""] as RuleCollection;
            }
        }

        public static CustomRuleSection GetSection()
        {
            return (CustomRuleSection)ConfigurationManager.GetSection(sectionName);
        }
    }
}
