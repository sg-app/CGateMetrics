using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Models
{
    public class Standort
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Standortname { get; set; }
    }
}
