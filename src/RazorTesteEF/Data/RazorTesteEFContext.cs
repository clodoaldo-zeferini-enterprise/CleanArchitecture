using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace RazorTesteEF.Data
{
    public class RazorTesteEFContext : DbContext
    {
        public RazorTesteEFContext (DbContextOptions<RazorTesteEFContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Member> Member { get; set; } = default!;
    }
}
