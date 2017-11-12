using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace ConsoleApp.CustomConfig
{
    public class CustomFolderSection : ConfigurationSection
    {
        public const string sectionName = "WatchedFolders";

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public FolderCollection WatchedFolders
        {
            get
            {
                return this[""] as FolderCollection;
            }
        }

        public static CustomFolderSection GetSection()
        {
            return (CustomFolderSection)ConfigurationManager.GetSection(sectionName);
        }
    }
}
