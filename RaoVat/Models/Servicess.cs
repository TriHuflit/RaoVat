namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servicess")]
    public partial class Servicess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servicess()
        {
            HistoryService = new HashSet<HistoryService>();
        }

        [Key]
        [StringLength(5)]
        public string IDService { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public double? Price { get; set; }

        public TimeSpan? TotalTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoryService> HistoryService { get; set; }
    }
}
