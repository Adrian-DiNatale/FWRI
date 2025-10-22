using System.ComponentModel.DataAnnotations;

namespace FWRI.KeyCardReport.Data.Entities
{
    /// <summary>
    /// Entity Matching the JSON format for Category.
    /// Define the Keys and/or Foreign Keys to the other JSON files.
    /// </summary>
    public class Category
    {
        // Primary Key of the Table
        [Key] 
        public int categoryId { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = new();
    }
}
