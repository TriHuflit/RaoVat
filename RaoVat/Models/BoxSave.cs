namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoxSave")]
    public partial class BoxSave
    {
        [Key]
        [StringLength(10)]
        public string IDBoxSave { get; set; }

        [StringLength(20)]
        public string IDUser { get; set; }

        [StringLength(20)]
        public string IDNews { get; set; }

        public virtual News News { get; set; }

        public virtual Users Users { get; set; }

        [NotMapped]
        public string IDNewsCurrent { get; set; }

        [NotMapped]
        public string Image { get; set; }
    }
}
