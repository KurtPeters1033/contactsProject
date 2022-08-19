using System.ComponentModel.DataAnnotations;

namespace Contacts.Client.Model
{
    public class Contact
    {
        [Required] 
        public string name { get; set; }

        [Required]
        public string position { get; set; }

        [Required]
        public string salary { get; set; }

        [Required]
        public string startDate { get; set; }

        [Required]
        public string office { get; set; }

        [Required]
        public string extn { get; set; }
    }
}
