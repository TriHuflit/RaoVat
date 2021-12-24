namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reports
    {
        [Key]
        [StringLength(10)]
        public string IDReports { get; set; }

        [StringLength(20)]
        public string IDNews { get; set; }

        [StringLength(100)]
        public string Content { get; set; }

        [StringLength(20)]
        public string TypeOfReport { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public bool? Status { get; set; }

        public virtual News News { get; set; }
    }
}
