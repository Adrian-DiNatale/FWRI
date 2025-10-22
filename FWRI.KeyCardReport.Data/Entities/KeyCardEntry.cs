
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FWRI.KeyCardReport.Data.Entities
{
    /// <summary>
    /// Entity Matching the JSON format for KeyCardEntry.
    /// Define the Keys and/or Foreign Keys to the other JSON files.
    /// </summary>
    public class KeyCardEntry
    {
        // Primary Key of the Table
        [Key]
        public Guid EntryId { get; set; }
        public DateTime EntryDateTime { get; set; }

        // Foreign Key to Employee based on KeyCardId
        [ForeignKey(nameof(Employee.KeyCardId))]
        public Guid KeyCardId { get; set; }
        public Guid ScannerId { get; set; }
        public int EntryCategoryId { get; set; }

        // Foreign Key to Image by ImageId
        [ForeignKey(nameof(Image.ImageId))]
        public int SecurityImageId { get; set; }

        // Navigation to the Image who owns the ImageId. 
        [ForeignKey(nameof(SecurityImageId))]
        public Image? SecurityImage { get; set; }

        // Navigation to the Category who owns the CategoryId. 
        [ForeignKey(nameof(EntryCategoryId))]
        public Category? EntryCategory { get; set; }

        // Navigation to the employee who owns the KeyCardId.        
        [ForeignKey(nameof(KeyCardId))]
        public Employee? Employee { get; set; }

        // Custom property to access CategoryName
        [NotMapped]
        public string? CategoryName => EntryCategory?.categoryName;

        // Custom property to access the Employee's Full Name
        [NotMapped]
        public string EmployeeFullName => string.Format("{0} {1}", Employee?.FirstName, Employee?.LastName);
    }
}
