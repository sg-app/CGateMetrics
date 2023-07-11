using CGateMetricsData.Interfaces;
using CGateMetricsData.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGateMetricsTests.ControllerTests
{
    public class FahrzeugAbfrageControllerTests : ControllerTestBase
    {
        private Mock<IFahrzeugAbfrageService> _abfrageMock;
        protected override void OverrideServices(IServiceCollection services)
        {
            _abfrageMock = new();

            services.AddTransient(f => _abfrageMock.Object);
            base.OverrideServices(services);
        }

        [Test]
        public async Task ShouldReturnListOfBuchungenFromDriverId()
        {
            // Arrange
            var dataList = ControllerHelper.Buchungen();
            var expected = dataList[4];
            _abfrageMock.Setup(f => f.GetBuchungByDriverId(expected.AusweisId)).ReturnsAsync(new List<Buchung>() { expected });
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FahrzeugAbfrage/Buchung/ByDriver/{expected.AusweisId}");

            // Act
            HttpResponseMessage response = await HttpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Buchung>>(responseContent);

            // Assert
            _abfrageMock.Verify(f=>f.GetBuchungByDriverId(expected.AusweisId), Times.Once);
            actual[0].Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ShouldReturnBadRequest()
        {
            // Arrange
            _abfrageMock.Setup(f => f.GetBuchungByDriverId(It.IsAny<string>())).ThrowsAsync(new ArgumentNullException());
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FahrzeugAbfrage/Buchung/ByDriver/0815");

            // Act
            HttpResponseMessage response = await HttpClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ShouldReturnCountOfDriverWithTimeFilter()
        {
            // Arrange
            var expected = 5;
            _abfrageMock.Setup(f => f.GetDriverCountByLocationWithinTimeFrame(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).ReturnsAsync(expected);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FahrzeugAbfrage/CountBetween/Regensburg");

            // Act
            HttpResponseMessage response = await HttpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<int>(responseContent);

            // Assert
            _abfrageMock.Verify(f => f.GetDriverCountByLocationWithinTimeFrame(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()), Times.Once);
            actual.Should().Be(expected);
        }

        //[Test]
        //public async Task ShouldReturnCountOfDrivers()
        //{
        //    // Arrange
        //    var expected = 8;
        //    _abfrageMock.Setup(f => f.GetDriverCountByLocationAlltime(It.IsAny<string>())).ReturnsAsync(expected);
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"api/FahrzeugAbfrage/Count/Regensburg");

        //    // Act
        //    HttpResponseMessage response = await HttpClient.SendAsync(request);
        //    string responseContent = await response.Content.ReadAsStringAsync();
        //    var actual = JsonConvert.DeserializeObject<int>(responseContent);

        //    // Assert
        //    _abfrageMock.Verify(f => f.GetDriverCountByLocationAlltime(It.IsAny<string>()), Times.Once);
        //    actual.Should().Be(expected);
        //}

        [Test]
        public async Task ShouldReturnOverloadedLkws()
        {
            // Arrange
            var dataList = ControllerHelper.Fahrzeuge();
            var expected = dataList;
            _abfrageMock.Setup(f => f.GetOverloadedLKWs()).ReturnsAsync(dataList);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FahrzeugAbfrage/Overloaded");

            // Act
            HttpResponseMessage response = await HttpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Fahrzeug>>(responseContent);

            // Assert
            _abfrageMock.Verify(f => f.GetOverloadedLKWs(), Times.Once);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ShouldReturnCurrentOnLocation()
        {
            // Arrange
            var dataList = ControllerHelper.Fahrzeuge();
            var location = "Regensburg";
            var expected = dataList;
            _abfrageMock.Setup(f => f.GetCurrentLKWSOnLocation(location)).ReturnsAsync(dataList);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FahrzeugAbfrage/CurrentOnLocation/{location}");

            // Act
            HttpResponseMessage response = await HttpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Fahrzeug>>(responseContent);

            // Assert
            _abfrageMock.Verify(f => f.GetCurrentLKWSOnLocation(location), Times.Once);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task ShouldReturnIncorectDrivers()
        {
            // Arrange
            var dataList = ControllerHelper.Fahrer();
            var expected = dataList;
            _abfrageMock.Setup(f => f.GetDriversWithIncompleteData()).ReturnsAsync(dataList);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FahrzeugAbfrage/Driver/Incomplete");

            // Act
            HttpResponseMessage response = await HttpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Fahrer>>(responseContent);

            // Assert
            _abfrageMock.Verify(f => f.GetDriversWithIncompleteData(), Times.Once);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
