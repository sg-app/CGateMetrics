using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsData.Models
{
    public class Fahrer
    {
        [Key]
        [MaxLength(50)]
        [Required(ErrorMessage = "Bitte eindeutige AusweisId angeben.")]
        public string AusweisId { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Bitte Vorname angeben.")]
        public string Vorname { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Bitte Nachname angeben.")]
        public string Nachname { get; set; }
        [MaxLength(50)] 
        public string? Geburtsort { get; set; }
        public DateTime? Geburtstag { get; set; }
        [MaxLength(50)] 
        public string? Anrede { get; set; }
        [MaxLength(50)] 
        public string? Telefon { get; set; }
        [Required(ErrorMessage = "Bitte Firma angeben.")]
        [MaxLength(50)] 
        public string Firma { get; set; }
    }
}
