using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleShareWebsite.Models {
    
    public class Postable {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Required, MinLength(4)]
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public virtual ICollection<Like> Likes { get; set; }

        public Postable()
        {
            Likes = new HashSet<Like>();
        }
    }
}
