using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace StyleShareWebsite.Models {
    
    public class Tag {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Postable> Postables { get; set; } = new List<Postable>();
    }
}
