using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wize.common.tenancy.Interfaces;

namespace wize.content.data.v1.Models
{
    public class ContentFile : ITenantModel
    {
        [Key]
        [Column(Order = 0)]
        public int ContentId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int FileId { get; set; }

        public virtual Content Content { get; set; }
        public virtual File File { get; set; }
    }
}
