using Microsoft.EntityFrameworkCore;
using NHibernate.Criterion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleShareWebsite.Models
{

    public class Like
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PostableId { get; set; }
        public virtual Postable Postable { get; set; }
        public virtual User User { get; set; }
    }
}
