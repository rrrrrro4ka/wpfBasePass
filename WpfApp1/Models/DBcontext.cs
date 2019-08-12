using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WpfApp1.Models
{
    public class DBcontext : DbContext
    {
        public DBcontext() : base("DBConnect")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInformation { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.UserInfoes)
                .WithRequired(c => c.User);

            base.OnModelCreating(modelBuilder);
        }
    }
}
