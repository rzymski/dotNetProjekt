using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleShareWebsite.Models {

    [Table("Comments")]    
    public class Comment : Postable
    {
        
        public int PostableId { get; set; }
        public virtual Postable CommentedContent { get; set; }
        public string Content { get; set; }
    }
}
