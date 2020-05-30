using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace E9U.Tangello.Private
{
    public class FileBasedRepository : IRepository
    {
        public Task<IEnumerable<string>> GetAllProjectNamesForCategoryAsync(string category)
        {
            var availableProjectNamesFilePath = @"C:\Temp\Tangello Available Project Names.txt";

            var categoryDict = ReadProjectNamesFileToDict(availableProjectNamesFilePath);

            var categoryList = categoryDict[category] as IEnumerable<string>;

            return Task.FromResult(categoryList);
        }

        /// <summary>
        /// Converts a project names file to a dictionary.
        /// </summary>
        public static IDictionary<string, IList<string>> ReadProjectNamesFileToDict(string projectNamesFilePath)
        {
            var projectNamesDictionary = new Dictionary<string, IList<string>>();
            IList<string> projectNamesList;
            string projectNameCategory = null;

            using (var textFile = new StreamReader(projectNamesFilePath))
            {
                while (true)
                {
                    var line = textFile.ReadLine();

                    if (String.IsNullOrEmpty(line))
                    {
                        return projectNamesDictionary;
                    }

                    if (line[0] == '>')
                    {
                        projectNameCategory = line.Substring(1);
                        continue;
                    }

                    projectNamesList = FileBasedRepository.LineToList(line);
                    projectNamesDictionary.Add(projectNameCategory, projectNamesList);
                }
            }
        }

        /// <summary>
        /// Converts a line of names to a list of strings
        /// </summary>
        public static IList<string> LineToList(string line)
        {
            var projectNamesList = new List<string>();

            var tokens = line.Split(';');
            foreach (var token in tokens)
            {
                projectNamesList.Add(token);
            }

            return projectNamesList;
        }

        public Task PostProjectNamesForCategoryAsync(string category, string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAvailableProjectNamesForCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }
    }
}