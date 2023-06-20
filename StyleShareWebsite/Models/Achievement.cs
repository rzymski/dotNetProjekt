using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace StyleShareWebsite.Models {
    
    public class Achievement {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        //public List<User> AchievmentOwners { get; set; } = new List<User>();

        public virtual ICollection<AchievementUser> AchievementUsers { get; set; }

        public Achievement()
        {
            AchievementUsers = new HashSet<AchievementUser>();
        }
    }
}
