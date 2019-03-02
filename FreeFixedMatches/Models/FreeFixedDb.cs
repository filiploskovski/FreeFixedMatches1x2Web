namespace FreeFixedMatches.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FreeFixedDb : DbContext
    {
        public DbSet<FreeTip> FreeTips { get; set; }
        public DbSet<MonthlySubscration> MonthlySubscrations { get; set; }

        public DbSet<VipTicket> VipTickets { get; set; }

        public FreeFixedDb()
            : base("name=FreeFixedDb")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
