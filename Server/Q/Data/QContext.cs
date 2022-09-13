using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Q.Model;

namespace Q.Data
{
    public class QContext : DbContext
    {
        public QContext (DbContextOptions<QContext> options)
            : base(options)
        {
        }

        public DbSet<Q.Model.TblProducts> TblProducts { get; set; }
        public DbSet<Q.Model.User> Users { get; set; }
        public DbSet<Q.Model.Game> Games { get; set; }


    }
}
