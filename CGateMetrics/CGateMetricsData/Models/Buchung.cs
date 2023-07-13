using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Models
{
    public class Buchung
    {
        [Key]
        public int BuchungsId { get; set; }
        public DateTime UhrzeitIn { get; set; }
        public DateTime? UhrzeitOut { get; set; }
        [ForeignKey(nameof(Fahrer))]
        public string AusweisId { get; set; }
        [ForeignKey(nameof(Fahrzeug))]
        public string Fahrgestellnummer { get; set; }
        //public string Standort { get; set; }
        public int GewichtIn { get; set; }
        public int? GewichtOut { get; set; }
        public string? Gefahrgut { get; set; }

        public int? StandortId { get; set; }

        public Fahrer Fahrer { get; set; }
        public Fahrzeug Fahrzeug { get; set; }
        public Standort Standort { get; set; }
    }
}
