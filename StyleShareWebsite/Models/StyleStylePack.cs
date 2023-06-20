using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleShareWebsite.Models {
    public class StyleStylePack
    {
        public int Id { get; set; }

        [Required]
        public int StyleId { get; set; }

        [Required]
        public int StylePackId { get; set; }

        public virtual Style Style { get; set; }
        public virtual StylePack StylePack { get; set; }
    }
}
