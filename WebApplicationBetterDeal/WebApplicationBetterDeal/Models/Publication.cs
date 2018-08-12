using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBetterDeal.Data;

namespace WebApplicationBetterDeal.Models
{
    public class Publication
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public String ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
