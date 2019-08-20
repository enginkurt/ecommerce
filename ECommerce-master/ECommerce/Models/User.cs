
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        [MaxLength(150)]
        public string EMail { get; set; }

        public List<Address> Addresses { get; set; }
    }
}