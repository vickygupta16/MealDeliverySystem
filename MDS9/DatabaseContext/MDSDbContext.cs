using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MDS9.Models;

namespace MDS9.DatabaseContext
{
    public class MDSDbContext:System.Data.Entity.DbContext
    {
        public MDSDbContext()
            : base("DefaultConnection")
        {
        }
        public System.Data.Entity.DbSet<Student> s1 { get; set; }
        public System.Data.Entity.DbSet<Meal> m1 { get; set; }
        public System.Data.Entity.DbSet<OrderDetails> o1 { get; set; }
        public System.Data.Entity.DbSet<Bill> b1 { get; set; }

        public System.Data.Entity.DbSet<UserProfile> UserProfiles { get; set; }

        
    }
}