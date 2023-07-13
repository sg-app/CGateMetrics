using NUnit;
using FluentAssertions;
using Moq;
using System.Net.Mail;
using CGateMetricsData.Models;
using CGateMetricsData;
using Microsoft.EntityFrameworkCore;
using CGateMetricsData.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Moq.EntityFrameworkCore;

namespace CGateMetricsTests
{
    public partial class Tests
    {



        [TestCase("Wolfsburg", ("01/21/2004"), ("01/23/2004"), 2)]
        [TestCase("Salzgitter", null, null, 2)]
        [TestCase("Zwickau", ("01/20/2012"), null, 1)]
        public async Task GetDriverCountByLocationWithTimeFilterShouldBeGood(string location, DateTime? startTimeFilter, DateTime? endTimeFilter, int result)
        {
            //Arrange

            var buchungContextMock = new Mock<CGateMetricsDbContext>();

            IList<Buchung> buchungen = new List<Buchung>
            {
            new Buchung
            {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2004,01,22,9,15,0),
                UhrzeitOut = new DateTime(2004,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=1, Standortname="Wolfsburg" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },
            new Buchung
            {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2004,01,22,9,15,0),
                UhrzeitOut = new DateTime(2004,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=1, Standortname="Wolfsburg" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

                new Buchung
                {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2028,01,22),
                UhrzeitOut = new DateTime(2028,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=2, Standortname="Salzgitter" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

                new Buchung
                {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2028,01,20),
                UhrzeitOut = new DateTime(2028,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=2, Standortname="Salzgitter" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

                new Buchung
                {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2012,01,20),
                UhrzeitOut = new DateTime(2012,01,20),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=3, Standortname="Zwickau" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

            };

            buchungContextMock.Setup(x => x.Buchungen).ReturnsDbSet(buchungen);
            var sut = new FahrzeugAbfrageService(buchungContextMock.Object);


            //Act
            var actuell = await sut.GetDriverCountByLocationWithinTimeFrame(location, startTimeFilter, endTimeFilter);


            //Assert
            actuell.Should().Be(result);

        }






        [TestCase(null, ("01/21/2023"), ("01/23/2023"), 1)]

        public async Task GetDriverCountByLocationWithTimeFilterShouldBeBad(string? location, DateTime? startTimeFilter, DateTime? endTimeFilter, int result)
        {
            //Arrange

            var buchungContextMock = new Mock<CGateMetricsDbContext>();

            IList<Buchung> buchungen = new List<Buchung>
            {
            new Buchung
            {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2004,01,22,9,15,0),
                UhrzeitOut = new DateTime(2004,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=1, Standortname="Wolfsburg" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },
            new Buchung
            {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2004,01,22,9,15,0),
                UhrzeitOut = new DateTime(2004,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=1, Standortname="Wolfsburg" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

                new Buchung
                {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2028,01,22),
                UhrzeitOut = new DateTime(2028,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=2, Standortname="Salzgitter" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

                new Buchung
                {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2028,01,20),
                UhrzeitOut = new DateTime(2028,01,22),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=2, Standortname="Salzgitter" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

                new Buchung
                {
                AusweisId = "123",
                BuchungsId = 123,
                UhrzeitIn = new DateTime(2012,01,20),
                UhrzeitOut = new DateTime(2012,01,20),
                Fahrgestellnummer = "XYZ",
                Standort = new Standort(){Id=3, Standortname="Zwickau" },
                GewichtOut = 15,
                Gefahrgut = "Benzin",
            },

            };

            buchungContextMock.Setup(x => x.Buchungen).ReturnsDbSet(buchungen);
            var sut = new FahrzeugAbfrageService(buchungContextMock.Object);


            //Act + Assert

            await sut.Invoking(x => x.GetDriverCountByLocationWithinTimeFrame(location, startTimeFilter, endTimeFilter)).Should().ThrowAsync<ArgumentNullException>();




        }
    }
}