using CGateMetricsData;
using CGateMetricsData.Models;
using CGateMetricsData.Services;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace CGateMetricsTests
{
    public class Test_GetBuchungByDriverId
    {
        private static IEnumerable<TestCaseData> GetBuchungByDriverId_ValidBuchungen_TestCases
        {
            get
            {
                //cases
                yield return new TestCaseData(new List<Buchung>() {
                    new Buchung() { BuchungsId = 1, AusweisId = "ABC"},
                    new Buchung() { BuchungsId = 2, AusweisId = "DEF"},
                    new Buchung() { BuchungsId = 3, AusweisId = "XYZ"},
                });
            }
        }
        [TestCaseSource(nameof(GetBuchungByDriverId_ValidBuchungen_TestCases))]
        [Test()]
        public async Task GetBuchungByDriverId(List<Buchung> buchungsListe)
        {
            //Setup
            var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

            cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungsListe);

            var sut  = new FahrzeugAbfrageService(cgateMetricsContext.Object);

            //Act
            var result = await sut.GetBuchungByDriverId("DEF");

            //Assert
            result.Should().ContainEquivalentOf(new Buchung() { BuchungsId = 2, AusweisId = "DEF" });
        }

        //[Test()]
        //public async Task GetBuchungByDriverId_IdÌsNull()
        //{
        //    Setup
        //    var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

        //    var buchungsListe = new List<Buchung>() {
        //        new Buchung()
        //        {
        //            BuchungsId = 1,
        //            AusweisId = "ABC"
        //        },
        //        new Buchung()
        //        {
        //            BuchungsId = 2,
        //            AusweisId = "CDE"
        //        },
        //        new Buchung()
        //        {
        //            BuchungsId = 3,
        //            AusweisId = "XYZ"
        //        }

        //    };

        //    cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungsListe);

        //    var sut = new FahrzeugAbfrageService(cgateMetricsContext.Object);

        //    Act
        //    var result = await sut.GetBuchungByDriverId(null);

        //    Assert
        //    result.Should().ContainEquivalentOf(new Buchung() { BuchungsId = 2, AusweisId = "CDE" });
        //}
    }
}