namespace NIF.PT.Client.UnitTests
{
    using AutoFixture;
    using FluentAssertions;
    using Flurl.Http.Testing;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "Unit Tests")]
    public class NifClientUnitTests
        : IDisposable
    {
        private readonly HttpTest _httpTest;

        private readonly Fixture _fixture;

        private readonly NifClient _nifClient;

        private const string ExpectedUrlPattern = NifClient.BaseAddress + "*";

        public NifClientUnitTests()
        {
            this._httpTest = new HttpTest();
            this._fixture = new Fixture();
            _nifClient = new NifClient(_fixture.Create<string>());
        }

        [Fact]
        [Trait("Category", "Search")]
        public async Task Search_ValidNif_ShouldReturnData()
        {
            //Arrange
            var nif = "509442013";
            await LoadResponseBody("SearchResponseBody");

            //Act
            var result = await _nifClient.SearchAsync(nif);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().Be("success");
            result.Record.Should().NotBeNull();
            result.Record.Nif.Should().Be(509442013);
            result.Record.SeoUrl.Should().Be("nexperience-lda");
            result.Record.Title.Should().Be("Nexperience Lda");
            result.Record.Address.Should().Be("Rua da Lionesa Nº 446, Edifício G20");
            result.Record.Pc4.Should().Be("4465");
            result.Record.Pc3.Should().Be("671");
            result.Record.City.Should().Be("Leça do Balio");
            result.Record.Activity.Should().Be("Desenvolvimento de software. Consultoria em informática. Comércio de equipamentos e sistemas informáticos. Exploração de portais web.");
            result.Record.Status.Should().Be("active");
            result.Record.Cae.Should().Be("62010");
            result.Record.Contacts.Should().NotBeNull();
            result.Record.Contacts.Email.Should().Be("info@nex.pt");
            result.Record.Contacts.Phone.Should().Be("220198228");
            result.Record.Contacts.Website.Should().Be("www.nex.pt");
            result.Record.Contacts.Fax.Should().Be("224 905 459");
            result.Record.Structure.Should().NotBeNull();
            result.Record.Structure.Nature.Should().Be("LDA");
            result.Record.Structure.Capital.Should().Be("5000.00");
            result.Record.Structure.CapitalCurrency.Should().Be("EUR");
            result.Record.Geo.Should().NotBeNull();
            result.Record.Geo.Region.Should().Be("Porto");
            result.Record.Geo.County.Should().Be("Matosinhos");
            result.Record.Geo.Parish.Should().Be("Leça do Balio");
            result.Record.Place.Should().NotBeNull();
            result.Record.Place.Address.Should().Be("Rua da Lionesa Nº 446, Edifício G20");
            result.Record.Place.Pc4.Should().Be("4465");
            result.Record.Place.Pc3.Should().Be("671");
            result.Record.Place.City.Should().Be("Leça do Balio");
            result.Record.Racius.Should().Be("http://www.racius.com/nexperience-lda/");
            result.Record.Alias.Should().Be("Nex - Nexperience, Lda");
            result.Record.Portugalio.Should().Be("http://www.portugalio.com/nex/");
            result.NifValidation.Should().Be(true);
            result.IsNif.Should().Be(true);
            result.Credits.Should().NotBeNull();
            result.Credits.Used.Should().Be("free");
            result.Credits.Left.Should().BeEmpty();
            this._httpTest.ShouldHaveCalled(ExpectedUrlPattern)
                .WithVerb(HttpMethod.Get)
                .WithQueryParam("json", 1)
                .WithQueryParam("q", nif)
                .WithAnyQueryParam("key")
                .Times(1);
        }

        [Fact]
        [Trait("Category", "Buy Credits")]
        public async Task BuyCredits_AllParameters_ShouldReturnATMReference()
        {
            //Arrange
            var creditsAmount = this._fixture.Create<uint>();
            var invoiceName = this._fixture.Create<string>();
            var invoiceNif = this._fixture.Create<string>();
            await LoadResponseBody("BuyCreditsResponseBody");

            //Act
            var result = await _nifClient.BuyCreditsAsync(
                creditsAmount,
                invoiceName,
                invoiceNif);

            //Assert
            result.Should().NotBeNull();
            result.Credits.Should().Be(1000);
            result.AtmReference.Entity.Should().Be("10241");
            result.AtmReference.Reference.Should().Be("000 000 000");
            result.AtmReference.Amount.Should().Be("10.00");
            this._httpTest.ShouldHaveCalled(ExpectedUrlPattern)
                .WithVerb(HttpMethod.Get)
                .WithQueryParam("json", 1)
                .WithQueryParam("buy", creditsAmount)
                .WithQueryParam("invoice_name", invoiceName)
                .WithQueryParam("invoice_nif", invoiceNif)
                .WithAnyQueryParam("key")
                .Times(1);
        }

        [Fact]
        [Trait("Category", "Buy Credits")]
        public void NifClient_BuyCredits_ShouldThrowException()
        {
            //Arrange
            var client = new NifClient();
            var creditsAmount = this._fixture.Create<uint>();

            //Act
            var act = async () => await client.BuyCreditsAsync(creditsAmount);

            //Assert
            act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("A chave da API é necessária para compra de créditos!")
                .WithParameterName("key");
        }

        [Fact]
        [Trait("Category", "Buy Credits")]
        public async Task BuyCredits_WithoutInvoiceParameters_ShouldReturnATMReference()
        {
            //Arrange
            var creditsAmount = this._fixture.Create<uint>();
            await LoadResponseBody("BuyCreditsResponseBody");

            //Act
            var result = await _nifClient.BuyCreditsAsync(creditsAmount);

            //Assert
            result.Should().NotBeNull();
            result.Credits.Should().Be(1000);
            result.AtmReference.Entity.Should().Be("10241");
            result.AtmReference.Reference.Should().Be("000 000 000");
            result.AtmReference.Amount.Should().Be("10.00");
            this._httpTest.ShouldHaveCalled(ExpectedUrlPattern)
                .WithVerb(HttpMethod.Get)
                .WithQueryParam("json", 1)
                .WithQueryParam("buy", creditsAmount)
                .WithAnyQueryParam("key")
                .Times(1);
        }

        [Fact]
        [Trait("Category", "Verify Credits")]
        public async Task VerifyCredits_ShouldReturnInfo()
        {
            //Arrange
            await this.LoadResponseBody("VerifyCreditsResponseBody");

            //Act
            var result = await _nifClient.VerifyCreditsAsync();

            //Assert
            result.Should().NotBeNull();
            result.Credits.Should().NotBeNull();
            result.Credits.Month.Should().Be(1000);
            result.Credits.Day.Should().Be(100);
            result.Credits.Hour.Should().Be(10);
            result.Credits.Minute.Should().Be(1);
            result.Credits.Paid.Should().Be(0);
            this._httpTest.ShouldHaveCalled(ExpectedUrlPattern)
                .WithVerb(HttpMethod.Get)
                .WithQueryParam("json", 1)
                .WithQueryParam("credits", 1)
                .WithAnyQueryParam("key")
                .Times(1);
        }

        public void Dispose() => this._httpTest.Dispose();

        private async Task LoadResponseBody(string fileName)
        {
            var filePath = Path.Combine("ResponseBodies", fileName + ".json");
            var responseBody = await File.ReadAllTextAsync(filePath);
            this._httpTest.RespondWith(responseBody);
        }
    }
}