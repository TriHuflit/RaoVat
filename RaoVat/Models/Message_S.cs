namespace RaoVat.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message_S
    {
        [Key]
        [StringLength(20)]
        public string IDMessage { get; set; }

        [StringLength(20)]
        public string IDRoom { get; set; }

        public bool? Type { get; set; }

        public string Content { get; set; }

        [Column(TypeName = "image")]
        public byte[] Img { get; set; }

        public DateTime? Time { get; set; }

        public virtual RoomChat RoomChat { get; set; }
    }
}
