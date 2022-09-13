using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Q.Model
{
    public class User
    {
        [Range(1,1000 , ErrorMessage = "*Invalid Id, Id Range (1-1000)")]
        [Required]
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "The  Name must be with a minimum length of 3 and a maximum length of 200.")]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[0-9]{0,10}$", ErrorMessage ="Phone Number Digits (0-9)")]
        [StringLength(10, MinimumLength = 10)]
        [Required]
        public String PhoneNumber { get; set; }
        
        public int? GamesCount { get; set; }





    }
}
