using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWRI.KeyCardReport.Data.Entities
{
    /// <summary>
    /// Entity Matching the JSON format for Employee.
    /// Define the Keys and/or Foreign Keys to the other JSON files.
    /// </summary>
    public class Employee
    {
        // Primary Key of the Table
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string WorkEmail { get; set; } = string.Empty;
        public string WorkTitle { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public int ProfilePictureId { get; set; }
        public byte IsActive { get; set; }

        [InverseProperty(nameof(Employee.KeyCardId))]
        public Guid KeyCardId { get; set; }

        // Navigation to the Image who owns the ImageId. 
        [ForeignKey(nameof(ProfilePictureId))]
        public Image? ProfilePicture { get; set; }

        //[InverseProperty(nameof(Employee.KeyCardId))]
        public List<KeyCardEntry> KeyCardEntries { get; set; } = new();

        // Custom property for Active Status
        [NotMapped]
        public bool ActiveStatus => IsActive != 0;

        // Custom Property for Employee's full name
        [NotMapped]
        public string FullName => string.Format("{0} {1}", FirstName, LastName);
    }
}
