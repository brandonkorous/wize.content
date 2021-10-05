using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wize.common.tenancy.Interfaces;

namespace wize.content.data.v1.Models
{
    public class Link : ITenantModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkId { get; set; }
        public int? ParentLinkId { get; set; }
        public int LinkGroupId { get; set; }
        [DefaultValue(true)]
        public bool Published { get; set; }
        [Required]
        public string Title { get; set; }
        public string Alt { get; set; }
        [Required]
        [Display(Description = "This is the destination of the link.  For internal links, use the format: /Home/MyPage.  For external links, use the format: http://www.sample.com and don't forget to include the http://.")]
        public string Path { get; set; }
        public string Roles { get; set; }
        public int Sort { get; set; }
        public string CssClass { get; set; }
        [Display(Description = "This will place an icon on the left of the link.")]
        public string IconClass { get; set; }
        private DateTime _created = DateTime.Now;
        [Required]
        public DateTime Created
        {
            get
            {
                return _created.ToLocalTime();
            }
            set
            {
                if (value.Kind == DateTimeKind.Utc)
                {
                    _created = value;
                }
                else if (value.Kind == DateTimeKind.Local)
                {
                    _created = value.ToUniversalTime();
                }
                else
                {
                    _created = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                }
            }
        }
        [MaxLength(128)]
        public string CreatedBy  { get; set; }

        // Relational Properties
        public virtual LinkGroup LinkGroup { get; set; }
        [ForeignKey("ParentLinkId")]
        public virtual Link Parent { get; set; }
        public virtual ICollection<Link> Children { get; set; }
    }
}
