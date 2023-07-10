using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData
{
    public class CGateMetricsDbContext : DbContext
    {
        public CGateMetricsDbContext(DbContextOptions<CGateMetricsDbContext> options) : base (options)
        {
            
        }

    }
}
