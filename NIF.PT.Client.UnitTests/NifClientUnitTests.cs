namespace NIF.PT.Client.UnitTests
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Flurl.Http.Testing;
    using SimpleFixture;
    using Xunit;

    public class NifClientUnitTests
        : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly Fixture _fixture;

        public NifClientUnitTests()
        {
            this._httpTest = new HttpTest();
            this._fixture = new Fixture();
        }

        [Fact]
        [Trait("Category", "Constructor")]
        public void Constructor_ValidKey_ShouldBeOk()
        {
            //Arrange
            var key = this._fixture.Generate<string>();

            //Act
            var client = new NifClient(key);

            //Assert
            client.Should().NotBeNull();
            client.Key.Should().BeSameAs(key);
        }

        [Fact]
        [Trait("Category", "Constructor")]
        public void Constructor_InvalidKey_ShouldThrowException()
        {
            //Arrange
            var key = string.Empty;

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => new NifClient(key));

            //Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Contain(nameof(key));
        }

        [Fact]
        [Trait("Category", "Search")]
        public async Task Search_ValidNif_ShouldReturnData()
        {
            //Arrange
            var client = GenerateClient(out string key);
            var nif = "509442013";
            var jsonBody = await LoadResponseBody("SearchResponseBody");
            this._httpTest.RespondWith(jsonBody);

            //Act
            var result = await client.Search(nif);

            //Assert
            result.Should().NotBeNull();
            this._httpTest.ShouldHaveCalled(NifClient.BaseAddress)
                .WithVerb(HttpMethod.Get)
                .WithContentType("application/json")
                .WithQueryParamValue("json", 1)
                .WithQueryParamValue("q", nif)
                .WithQueryParamValue("key", key);
        }

        [Fact]
        [Trait("Category", "Buy Credits")]
        public async Task BuyCredits_AllParameters_ShouldReturnATMReference()
        {
            //Arrange
            var client = GenerateClient(out string key);
            var creditsAmount = this._fixture.Generate<int>();
            var invoiceName = this._fixture.Generate<string>();
            var invoiceNif = this._fixture.Generate<string>();
            var responseBody = await LoadResponseBody("BuyCreditsResponseBody");
            this._httpTest.RespondWith(responseBody);

            //Act
            var result = await client.BuyCredits(
                creditsAmount,
                invoiceName,
                invoiceNif);

            //Assert
            result.Should().NotBeNull();
            this._httpTest.ShouldHaveCalled(NifClient.BaseAddress)
                .WithVerb(HttpMethod.Get)
                .WithContentType("application/json")
                .WithQueryParamValue("json", 1)
                .WithQueryParamValue("buy", creditsAmount)
                .WithQueryParamValue("invoice_name", invoiceName)
                .WithQueryParamValue("invoice_nif", invoiceNif)
                .WithQueryParamValue("key", key);
        }

        [Fact]
        [Trait("Category", "Buy Credits")]
        public async Task BuyCredits_WithoutInvoiceParameters_ShouldReturnATMReference()
        {
            //Arrange
            var client = GenerateClient(out string key);
            var creditsAmount = this._fixture.Generate<int>();
            var responseBody = await LoadResponseBody("BuyCreditsResponseBody");
            this._httpTest.RespondWith(responseBody);

            //Act
            var result = await client.BuyCredits(creditsAmount);

            //Assert
            result.Should().NotBeNull();
            this._httpTest.ShouldHaveCalled(NifClient.BaseAddress)
                .WithVerb(HttpMethod.Get)
                .WithContentType("application/json")
                .WithQueryParamValue("json", 1)
                .WithQueryParamValue("buy", creditsAmount)
                .WithQueryParamValue("key", key);
        }

        [Fact]
        [Trait("Category", "Search")]
        public async Task VerifyCredits_ShouldReturnInfo()
        {
            //Arrange
            var client = GenerateClient(out string key);
            var responseBody = await this.LoadResponseBody("ConsultCreditsResponseBody");
            this._httpTest.RespondWith(responseBody);

            //Act
            var result = await client.VerifyCredits();

            //Assert
            result.Should().NotBeNull();
        }

        public void Dispose()
        {
            this._httpTest.Dispose();
        }

        private NifClient GenerateClient(out string key)
        {
            key = this._fixture.Generate<string>();
            return new NifClient(key);
        }

        private async Task<string> LoadResponseBody(string fileName)
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(directory, "ResponseBodies", fileName + ".json");
            return await File.ReadAllTextAsync(filePath);
        }
    }
}