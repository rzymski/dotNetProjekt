using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleShareWebsite.Models {

    [Table("Styles")]
    public class Style : Postable
    {
        [Required]
        public string Css { get; set; }
        [Required]
        public string Html { get; set; }
        //public List<StylePack> StylePacks { get; set; } = new List<StylePack>();
        public virtual ICollection<StyleStylePack> StyleStylePacks { get; set; }
        public Style() 
        {
            StyleStylePacks = new HashSet<StyleStylePack>();
        }
    }
}
