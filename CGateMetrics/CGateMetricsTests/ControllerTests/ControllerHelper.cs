using CGateMetricsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsTests.ControllerTests
{
    public static class ControllerHelper
    {
        public static List<Buchung> Buchungen()
        {
            var list = new List<Buchung>();
            Buchung buchung;
            int inDiff;
            int outDiff;
            int inWeight;
            int outWeight;
            for (int i = 0; i < 10; i++)
            {
                inDiff = new Random().Next((int)TimeSpan.FromDays(500).TotalMinutes);
                outDiff = new Random().Next(inDiff);
                inWeight = new Random().Next(5, 42);
                outWeight = new Random().Next(5, inWeight);

                buchung = new Buchung
                {
                    BuchungsId = i,
                    UhrzeitIn = DateTime.Now.AddMinutes(-inDiff),
                    UhrzeitOut = DateTime.Now.AddMinutes(-outDiff),
                    AusweisId = $"AID{i}",
                    Fahrgestellnummer = $"WIOALPOG{i}S2390SF",
                    Standort = new() { Id = 1, Standortname = "Regensburg" },
                    GewichtIn = inWeight,
                    GewichtOut = outWeight,
                };
                list.Add(buchung);
            }
            return list;
        }

        public static List<Fahrzeug> Fahrzeuge()
        {
            var list = new List<Fahrzeug>();
            Fahrzeug fahrzeug;
            int zulGew;
            for (int i = 0; i < 10; i++)
            {
                zulGew = new Random().Next(5, 42);

                fahrzeug = new Fahrzeug
                {
                    Fahrgestellnummer = $"WIOALPOG{i}S2390SF",
                    Hersteller = $"LKW{i}",
                    Kennzeichen = $"R-CO-{i}",
                    ZulGesamtGewicht = zulGew,
                };
                list.Add(fahrzeug);
            }
            return list;
        }

        public static List<Fahrer> Fahrer()
        {
            var list = new List<Fahrer>();
            Fahrer fahrer;
            int zulGew;
            for (int i = 0; i < 10; i++)
            {
                zulGew = new Random().Next(5, 42);

                fahrer = new Fahrer
                {
                    AusweisId = $"AID{i}",
                    Vorname = $"Vorname{i}",
                    Nachname = $"Nachname{i}",

                };
                list.Add(fahrer);
            }
            return list;
        }

    }
}
