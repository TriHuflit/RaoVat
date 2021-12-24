namespace RaoVat.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RaoVatModel : DbContext
    {
        public RaoVatModel()
            : base("name=RaoVat_Model")
        {
        }

        public virtual DbSet<BoxSave> BoxSave { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<HistoryService> HistoryService { get; set; }
        public virtual DbSet<ImgNews> ImgNews { get; set; }
        public virtual DbSet<Message_S> Message_S { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<RoomChat> RoomChat { get; set; }
        public virtual DbSet<Servicess> Servicess { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.RoomChat)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.IDUserA);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.RoomChat1)
                .WithOptional(e => e.Users1)
                .HasForeignKey(e => e.IDUserB);
        }

        public System.Data.Entity.DbSet<RaoVat.Models.Register> Registers { get; set; }
    }
}
