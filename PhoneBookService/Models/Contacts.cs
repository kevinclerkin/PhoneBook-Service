using System.ComponentModel.DataAnnotations;

namespace PhoneBookService.Models
{
    public class Contacts

    {
        [Required]
        public string ?Name { get; set; }
        
        [Required]
        public string ?PhoneNumber { get; set; }
        
        [Required]
        public string ?Address { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} | Address: {Address} | Phone-Number: {PhoneNumber}";
        }
    }
}
