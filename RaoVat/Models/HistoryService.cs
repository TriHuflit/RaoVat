namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistoryService")]
    public partial class HistoryService
    {
        [Key]
        [StringLength(10)]
        public string IDHistoryService { get; set; }

        [StringLength(20)]
        public string IDUser { get; set; }

        [StringLength(5)]
        public string IDService { get; set; }

        public DateTime? Time { get; set; }

        public virtual Servicess Servicess { get; set; }

        public virtual Users Users { get; set; }
    }
}
