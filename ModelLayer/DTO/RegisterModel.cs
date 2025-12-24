using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTO
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required,EmailAddress]

        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
