using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleShareWebsite.Models {

    [Table("Posts")]
    public class Post : Postable
    {
        [Required]
        public string Content { get; set; }
    }
}
