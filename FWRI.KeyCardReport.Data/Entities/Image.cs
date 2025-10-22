using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FWRI.KeyCardReport.Data.Entities
{
    /// <summary>
    /// Entity Matching the JSON format for Image.
    /// Define the Keys and/or Foreign Keys to the other JSON files.
    /// </summary>
    public class Image
    {
        // Primary Key of the Table
        [Key]
        public int ImageId { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string ImageExtension { get; set; } = string.Empty;
        public string ImageData { get; set; } = string.Empty;
        public int ImageCategoryId { get; set; }

        // Navigation to the Category who owns the CategoryId. 
        [ForeignKey(nameof(ImageCategoryId))]
        public Category? Category { get; set; }

        // Reverse Navigation to the employee who owns the ImageId. 
        [InverseProperty(nameof(Employee.ProfilePicture))]
        public List<Employee> Employees { get; set; } = new();

        // Reverse Navigation to the KeyCardEntry who owns the ImageId. 
        [InverseProperty(nameof(KeyCardEntry.SecurityImage))]
        public List<KeyCardEntry> KeyCardEntries { get; set; } = new();
    }
}
