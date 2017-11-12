using ConsoleApp.CustomConfig;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class FileWatcher
    {
        private List<string> FoldersToWatch;
        private List<FileSystemWatcher> watchers;
        private List<FileRule> rules;
        private int counter;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string defaultFolder = Resource.defaultFolder;

       public FileWatcher()
        {
            FoldersToWatch = new List<string>();
            watchers = new List<FileSystemWatcher>();
            rules = new List<FileRule>();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ConfigurationManager.AppSettings["Culture"]);
            GetValuesFromFileConfiguration();
        }

        public void Watch()
        {
            foreach(var folder in FoldersToWatch)
            {
                FileSystemWatcher watcher = new FileSystemWatcher(folder);
                watcher.Filter = "*.*";
                watcher.EnableRaisingEvents = true;
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watchers.Add(watcher);

            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            logger.Info($"Added new file {e.Name} in {Path.GetDirectoryName(e.FullPath)}");
            FilterFile(e.FullPath);
        }

        private void MoveFile(string pathToFile, string newPath)
        {
            try
            {
                File.Move(pathToFile, newPath);
                logger.Info($"{Path.GetFileName(newPath)} was moved to {Path.GetDirectoryName(newPath)}");
            }
            catch (IOException)
            {
                logger.Info($"Something went wrong. {Path.GetFileName(newPath)} was not moved to {Path.GetDirectoryName(newPath)}");
            }
        }

        private void FilterFile(string pathToFile)
        {
            FileInfo file = new FileInfo(pathToFile);
            bool IsMatched = false;
            foreach (var rule in rules)
            {
                Regex regex = new Regex(rule.Pattern);
                if (Regex.IsMatch(file.Name, rule.Pattern, RegexOptions.IgnoreCase))
                {
                    
                    logger.Info($"File {file.Name} matches regular expression {rule.Pattern}");
                    IsMatched = true;
                    MoveFile(pathToFile, ChangeFileName(pathToFile, rule));
                }
                              
            }
            if (!IsMatched) MoveFile(pathToFile, defaultFolder +"/"+ file.Name);
        }

        private string ChangeFileName(string FileName, FileRule rule)
        {
            string newPath = String.Empty;
            if (rule.ShouldAddNumber)
            {
                counter++;
                newPath = $"{rule.FolderDestination}/{Path.GetFileNameWithoutExtension(FileName)}({counter}){Path.GetExtension(FileName)}";
            }
            else if(rule.ShouldAddMovingDate)
            {

                newPath = $"{rule.FolderDestination}/{Path.GetFileNameWithoutExtension(newPath)}.{DateTime.Now}{Path.GetExtension(FileName)}";
            }
            else newPath = $"{rule.FolderDestination}/{Path.GetFileName(FileName)}";
            return newPath;
        }

        private void GetValuesFromFileConfiguration()
        {
            CustomFolderSection section = (CustomFolderSection)ConfigurationManager.GetSection("WatchedFolders");
            if (section != null && section?.WatchedFolders.Count != 0)
            {
                for (int i = 0; i < section.WatchedFolders.Count; i++)
                {
                    if (Directory.Exists((string)Resource.ResourceManager.GetObject(section.WatchedFolders[i].Name)))
                    {
                        FoldersToWatch.Add((string)Resource.ResourceManager.GetObject(section.WatchedFolders[i].Name));
                    }
                    else
                    {
                        logger.Info($"Directory not exist {Resource.ResourceManager.GetObject(section.WatchedFolders[i].Name)}");
                    }
                }
            }

            var patternSection = (CustomRuleSection)ConfigurationManager.GetSection("FileRules");
            if (patternSection != null && patternSection?.FileRules.Count != 0)
            {
                for (int i = 0; i < patternSection.FileRules.Count; i++)
                {
                    if (Directory.Exists(Resource.ResourceManager.GetObject(patternSection.FileRules[i].Destination).ToString()))
                    {
                        rules.Add(new FileRule
                        {
                            Pattern = Resource.ResourceManager.GetObject(patternSection.FileRules[i].Pattern).ToString(),
                            FolderDestination = Resource.ResourceManager.GetObject(patternSection.FileRules[i].Destination).ToString(),
                            ShouldAddNumber = patternSection.FileRules[i].ShouldAddNumber.Equals("true"),
                            ShouldAddMovingDate = patternSection.FileRules[i].ShouldAddMovingDate.Equals("true")
                        });
                    }
                    else
                    {
                        logger.Info($"Nonexistent folder in the list of rules: {Directory.Exists(Resource.ResourceManager.GetObject(patternSection.FileRules[i].Destination).ToString())}");
                    }
                }
            }
        }
    }

}
