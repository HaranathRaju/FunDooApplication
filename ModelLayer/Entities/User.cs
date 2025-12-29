using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ModelLayer.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }


        [Required(ErrorMessage = "First Name cannot be null")]
        [MaxLength(100, ErrorMessage = "length of the characters cannot exceed 100")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name cannot be null")]
        [MaxLength(100, ErrorMessage = "length of the characters cannot exceed 100")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "email cannot be null")]
        [EmailAddress(ErrorMessage = "Email should be in valid form")]
        [MaxLength(255, ErrorMessage = "length of the characters cannot exceed 255")]
        public string Email { get; set; }
        [Required(ErrorMessage = "email cannot be null")]
        [MaxLength(255, ErrorMessage = "length of the characters cannot exceed 255")]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Notes> Notes { get; set; }

        public ICollection<Collaborator> Collaborators {get; set;}

    }
}
