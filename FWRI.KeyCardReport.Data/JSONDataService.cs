using FWRI.KeyCardReport.Data.Entities;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace FWRI.KeyCardReport.Data
{
    public static class JSONDataService
    {
        /// <summary>
        /// Reads an Embedded Resource file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static string ReadResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = name;
            if (!name.StartsWith(nameof(assembly.FullName)))
            {
                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith(name));
            }

            using (Stream? stream = assembly.GetManifestResourceStream(resourcePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// JSON files set as embedded resource.
        /// </summary>
        private enum ResourceFiles
        {
            [DescriptionAttribute("employees.json")]
            employees,
            [DescriptionAttribute("categories.json")]
            categories,
            [DescriptionAttribute("images.json")]
            images,
            [DescriptionAttribute("keycardentries.json")]
            keycardentries
        }




        private static List<Category> GetCategories()
        {
            string jsonContent = ReadResource(ResourceFiles.categories.StringValueOf());
            return JsonSerializer.Deserialize<List<Category>>(jsonContent) ?? new List<Category>();
        }

        private static List<Employee> GetEmployees()
        {
            string jsonContent = ReadResource(ResourceFiles.employees.StringValueOf());
            return JsonSerializer.Deserialize<List<Employee>>(jsonContent) ?? new List<Employee>();
        }

        private static List<Image> GetImages()
        {
            string jsonContent = ReadResource(ResourceFiles.images.StringValueOf());
            return JsonSerializer.Deserialize<List<Image>>(jsonContent) ?? new List<Image>();
        }

        private static List<KeyCardEntry> GetKeyCardEntries()
        {
            string jsonContent = ReadResource(ResourceFiles.keycardentries.StringValueOf());
            return JsonSerializer.Deserialize<List<KeyCardEntry>>(jsonContent) ?? new List<KeyCardEntry>();
        }

        /// <summary>
        /// Loads the json data to an in memory database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static void LoadJsonDataToInMemoryDatabase(AppDbContext context)
        {
            if (context.Employees.Any() || context.Categories.Any() || context.Images.Any() || context.KeyCardEntries.Any())
                return;

            // Add to context                
            context.Employees.AddRange(GetEmployees());
            context.Categories.AddRange(GetCategories());
            context.Images.AddRange(GetImages());
            context.KeyCardEntries.AddRange(GetKeyCardEntries());

            // Save to memory
            context.SaveChanges();

        }
    }
}
