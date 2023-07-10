using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Models
{
    public class Fahrzeug
    {
        [Key]
        [MaxLength(50)]
        public string Fahrgestellnummer { get; set; }
        [MaxLength(50)]
        public string Hersteller { get; set; }
        [MaxLength(50)]
        public string Kennzeichen { get; set; }
        public int ZulGesamtGewicht { get; set; }
    }
}
