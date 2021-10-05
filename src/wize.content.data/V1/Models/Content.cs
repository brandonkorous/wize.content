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
    public class Content : ITenantModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContentId { get; set; }
        public int? ParentContentId { get; set; }
        public int ContentTypeId { get; set; }
        [DefaultValue(true)]
        public bool Published { get; set; }
        public int Sort { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [Display(Description = "This is the name of the page that will be used to create linking and direct access.  This field needs to be standard characters with no special characters.  Replace all [spaces] with - [hyphens].")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Spaces are not allowed.")]
        public string SafeTitle { get; set; }
        //[AllowHtml]
        public string Intro { get; set; }
        //[AllowHtml]
        public string Body { get; set; }
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


        // Relation Properties
        [ForeignKey("ParentContentId")]
        public virtual Content Parent { get; set; }
        public virtual ICollection<Content> Children { get; set; }
        public virtual ContentType ContentType { get; set; }
        public virtual ICollection<ContentArchive> Archive { get; set; }
        public virtual ICollection<ContentFile> ContentFiles { get; set; }
    }
}
