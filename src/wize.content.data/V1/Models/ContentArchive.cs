using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wize.common.tenancy.Interfaces;

namespace wize.content.data.v1.Models
{
    public class ContentArchive : ITenantModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContentArchiveId { get; set; }
        public int OriginalContentId { get; set; }
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
        public string SafeTitle { get; set; }
//        [AllowHtml]
        public string Intro { get; set; }
//       [AllowHtml]
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

        // Relational Properties
        [ForeignKey("OriginalContentId")]
        public Content Content { get; set; }
    }
}