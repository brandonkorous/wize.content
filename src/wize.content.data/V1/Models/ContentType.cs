﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wize.common.tenancy.Interfaces;

namespace wize.content.data.v1.Models
{
    public class ContentType : ITenantModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContentTypeId { get; set; }
        public string Title { get; set; }
        public string SafeTitle { get; set; }
        public string Description { get; set; }
        public string CssIcon { get; set; }
        public bool IsDefault { get; set; }
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
        public virtual ICollection<Content> ContentItems { get; set; }
    }
}
