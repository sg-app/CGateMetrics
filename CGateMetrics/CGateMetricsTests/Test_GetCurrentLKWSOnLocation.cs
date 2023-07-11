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
                    new Buchung() { BuchungsId = 1, Standort = "XYZ", Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "123"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 2, Standort = "XYZ", Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "345"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 3, Standort = "XYZ", Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "678"}, UhrzeitOut = null}
                };

            //Setup
            var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

            cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungsListe);

            var sut  = new FahrzeugAbfrageService(cgateMetricsContext.Object);

            var result = await sut.GetCurrentLKWSOnLocation("XYZ");

            result.Should().ContainEquivalentOf(new Fahrzeug() { Fahrgestellnummer = "678" });
        }

        [Test()]
        public async Task GetCurrentLKWSOnLocation_NoLKWFound()
        {
            var buchungsListe = new List<Buchung>() {
                    new Buchung() { BuchungsId = 1, Standort = "XYZ", Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "123"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 2, Standort = "XYZ", Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "345"}, UhrzeitOut = new DateTime(2001, 5, 12)},
                    new Buchung() { BuchungsId = 3, Standort = "XYZ", Fahrzeug = new Fahrzeug() { Fahrgestellnummer = "678"}, UhrzeitOut = new DateTime(2001, 5, 12)}
                };

            //Setup
            var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

            cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungsListe);

            var sut = new FahrzeugAbfrageService(cgateMetricsContext.Object);

            var result = await sut.GetCurrentLKWSOnLocation("XYZ");

            result.Should().BeNullOrEmpty();
        }
    }
}