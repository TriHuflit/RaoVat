namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoomChat")]
    public partial class RoomChat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomChat()
        {
            Message_S = new HashSet<Message_S>();
        }

        [Key]
        [StringLength(20)]
        public string IDRoom { get; set; }

        [StringLength(20)]
        public string IDUserA { get; set; }

        [StringLength(20)]
        public string IDUserB { get; set; }

        public byte? status { get; set; }

        public DateTime? Time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message_S> Message_S { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
