using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DDAC_Assignment.Models;

namespace DDAC_Assignment.Data
{
    public class DDAC_Context : DbContext
    {
        public DDAC_Context (DbContextOptions<DDAC_Context> options)
            : base(options)
        {
        }

        public DbSet<DDAC_Assignment.Models.WasteServices> WasteServices { get; set; }
    }
}
