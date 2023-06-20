using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleShareWebsite.Models
{
    [Table("ColorSets")]
    public class ColorSet : Postable
    {
        [Required]
        public string Colors { get; set; }
    }
}
