using CGateMetricsData.Models;
using CGateMetricsData.Services;
using CGateMetricsData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq.EntityFrameworkCore;

namespace CGateMetricsTests
{
    public class Test_GetOverloadedLKWs
    {
        private static IEnumerable<TestCaseData> Test_GetOverloadedLKWs_
        {
            get
            {
                //cases
                yield return new TestCaseData(new List<Fahrzeug>()
                {
                    new Fahrzeug(){ Fahrgestellnummer = "000IQKV3JGOF51985",Hersteller = "Maserati", Kennzeichen = "PM-05-111", ZulGesamtGewicht = 34 }
                   
                   
                 
                }
                ,new List<Buchung>()

                {
                    new Buchung() { BuchungsId = 1, AusweisId = "ABC",Fahrzeug =new Fahrzeug(){Fahrgestellnummer = "000IQKV3JGOF51985",Hersteller = "Maserati", Kennzeichen = "PM-05-111", ZulGesamtGewicht = 34}}
                }
                );
            }
        }

        [TestCaseSource(nameof(Test_GetOverloadedLKWs_))]
        [Test()]
        public async Task Test_GetOverloadedLKWs_Should_Be_Null(List<Fahrzeug> fahrzeugs, List<Buchung> buchungs)
        {
            //Setup
            var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

            cgateMetricsContext.Setup(m => m.Fahrzeuge).ReturnsDbSet(fahrzeugs);
            cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungs); ;

            var sut = new FahrzeugAbfrageService(cgateMetricsContext.Object);

            //Act
            var result = await sut.GetOverloadedLKWs();

            //Assert
            result.Should().BeNull();
        }


        [TestCaseSource(nameof(Test_GetOverloadedLKWs_))]
        [Test()]
        public async Task Test_GetOverloadedLKWs_Should_Contain_Fahrzeug(List<Fahrzeug> fahrzeugs, List<Buchung> buchungs)
        {
            //Setup
            var cgateMetricsContext = new Mock<CGateMetricsDbContext>();

            cgateMetricsContext.Setup(m => m.Fahrzeuge).ReturnsDbSet(fahrzeugs);
            cgateMetricsContext.Setup(m => m.Buchungen).ReturnsDbSet(buchungs); ;

            var sut = new FahrzeugAbfrageService(cgateMetricsContext.Object);

            //Act
            var result = await sut.GetOverloadedLKWs();

            //Assert
            result.Should().Contain(fahrzeugs);

        }



    }

}