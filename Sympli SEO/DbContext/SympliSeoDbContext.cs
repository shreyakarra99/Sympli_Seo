using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SympliSeo.DbModels;

namespace SimpliSeo.DbContext
{
    //DbContext to retrieve/store data.
    public class SympliSeoDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SympliSeoDbContext(DbContextOptions<SympliSeoDbContext> options) : base(options)
        {

        }
        public DbSet<DbSearchData> dbsetSearchData { get; set; }

    }
}
