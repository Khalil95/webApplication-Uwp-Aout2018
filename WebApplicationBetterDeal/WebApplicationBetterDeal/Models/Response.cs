using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBetterDeal.Data;

namespace WebApplicationBetterDeal.Models
{
    public class Response
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string response { get; set; }

        public int PublicationId { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual Publication Publication { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
