using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsData.Services;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace CGateMetricsTests
{
    public class Test_GetCurrentLKWSOnLocation
    {

        [Test()]
        public async Task GetCurrentLKWSOnLocation()
        {
            var buchungsListe = new List<Buchung>() {
                    new Buchung() { BuchungsId = 1, StandortId = 1, Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "123"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 2, StandortId = 1, Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "345"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 3, StandortId = 1, Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "678"}, UhrzeitOut = null}
                };

            var standortListe = new List<Standort>() {
                    new Standort() { Id = 1, Standortname = "XYZ"},
                    new Standort() { Id = 2, Standortname = "XYZ2"},
                };

            //Setup
            var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

            cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungsListe);
            cgateMetricsContext.Setup(m => m.Standort).ReturnsDbSet(standortListe);

            var sut = new FahrzeugAbfrageService(cgateMetricsContext.Object);

            var result = await sut.GetCurrentLKWSOnLocation("XYZ");

            result.Should().ContainEquivalentOf(new Fahrzeug() { Fahrgestellnummer = "678" });
        }

        [Test()]
        public async Task GetCurrentLKWSOnLocation_NoLKWFound()
        {
            var buchungsListe = new List<Buchung>() {
                    new Buchung() { BuchungsId = 1, StandortId = 1, Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "123"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 2, StandortId = 1, Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "345"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 3, StandortId = 1, Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "678"}, UhrzeitOut = new DateTime(2001, 5, 12)}
                };
            var standortListe = new List<Standort>() {
                    new Standort() { Id = 1, Standortname = "XYZ"},
                    new Standort() { Id = 2, Standortname = "XYZ2"},
                };

            //Setup
            var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

            cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungsListe);
            cgateMetricsContext.Setup(m => m.Standort).ReturnsDbSet(standortListe);

            var sut = new FahrzeugAbfrageService(cgateMetricsContext.Object);

            var result = await sut.GetCurrentLKWSOnLocation("XYZ");

            result.Should().BeNullOrEmpty();
        }
    }
}