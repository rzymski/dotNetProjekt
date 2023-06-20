using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace StyleShareWebsite.Models {
    
    public class User 
    {
        public int Id { get; set; }
        [Required, MinLength(4), MaxLength(10)]
        public string Nickname { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        public Role Role { get; set; }

        public bool? Blocked { get; set; }

        public List<Postable> Postables { get; set; } = new List<Postable>();
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<AchievementUser> AchievementUsers { get; set; }

        public User()
        {
            AchievementUsers = new HashSet<AchievementUser>();
            Likes= new HashSet<Like>();
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Zamieñ tablicê bajtów na string w formacie heksadecymalnym
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
