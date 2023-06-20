using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StyleShareWebsite.Models
{
    public class AchievementUser
    {
        public int Id { get; set; }
        [Required]
        public int AchievementId { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual Achievement Achievement { get; set; }
        public virtual User User { get; set; }
    }
}
