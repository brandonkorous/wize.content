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
    [Table("FrontPageContentItems")]
    public class FrontPage : ITenantModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FrontPageId { get; set; }
        [Required]
        public int ContentId { get; set; }
        public int? FileId { get; set; }
        public int Sort { get; set; }
        [DefaultValue(false)]
        public bool Spotlight { get; set; }
        [DefaultValue(false)]
        public bool Section { get; set; }
        [DefaultValue(false)]
        public bool Featured { get; set; }
        private DateTime? _startDate;
        [Display(Name = "Start Date", Description = "Format: MM/DD/YYYY, this will become active on or after this date.")]
        public DateTime? StartDate {
            get {
                return _startDate?.ToLocalTime();
            }
            set {
                if(!value.HasValue)
                {
                    _startDate = value;
                    return;
                }
                if (value.Value.Kind == DateTimeKind.Utc)
                {
                    _startDate = value;
                }
                else if(value.Value.Kind == DateTimeKind.Local)
                {
                    _startDate = value.Value.ToUniversalTime();
                }
                else
                {
                    _startDate = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                }
            }
        }
        private DateTime? _endDate;
        [Display(Name = "End Date", Description = "Format: MM/DD/YYYY, this will be active before or upto this date.")]
        public DateTime? EndDate
        {
            get
            {
                return _endDate?.ToLocalTime();
            }
            set
            {
                if (!value.HasValue)
                {
                    _endDate = value;
                    return;
                }
                if (value.Value.Kind == DateTimeKind.Utc)
                {
                    _endDate = value;
                }
                else if (value.Value.Kind == DateTimeKind.Local)
                {
                    _endDate = value.Value.ToUniversalTime();
                }
                else
                {
                    _endDate = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                }
            }
        }
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
        public virtual Content Content { get; set; }
        public virtual File Image { get; set; }

    }
}
