namespace NIF.PT.Client.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    [Trait("Category", "Integration Tests")]
    public class NifClientIntegrationTests
        : IClassFixture<LaunchSettingsFixture>
    {
        private readonly NifClient _client;

        public NifClientIntegrationTests()
        {
            var key = Environment.GetEnvironmentVariable("NIFPT_KEY");
            this._client = new NifClient(key);
        }

        [Theory]
        [InlineData("502017210")]
        public async Task Integration_Search(string nif)
        {
            //Act
            var response = await this._client.Search(nif);

            //Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Integration_VerifyCredits()
        {
            //Act
            var response = await this._client.VerifyCredits();

            //Assert
            response.Should().NotBeNull();
            response.Credits.Month.Should().BeGreaterOrEqualTo(0);
            response.Credits.Day.Should().BeGreaterOrEqualTo(0);
            response.Credits.Hour.Should().BeGreaterOrEqualTo(0);
            response.Credits.Minute.Should().BeGreaterOrEqualTo(0);
            response.Credits.Paid.Should().BeGreaterOrEqualTo(0);
        }

        [Theory]
        [InlineData(1000)]
        public async Task Integration_BuyCredits(uint amount)
        {
            //Arrange
            var creditPrice = decimal.Parse(Environment.GetEnvironmentVariable("NIFPT_CREDIT_PRICE"));
            var nifPtEntity = Environment.GetEnvironmentVariable("NIFPT_ENTITY");

            //Act
            var response = await this._client.BuyCredits(amount);

            //Assert
            response.Should().NotBeNull();
            response.Credits.Should().Equals(amount);
            response.AtmReference.Should().NotBeNull();
            response.AtmReference.Amount.Should().Equals(creditPrice * amount);
            response.AtmReference.Reference.Should().NotBeNullOrEmpty();
            response.AtmReference.Entity.Should().Equals(nifPtEntity);
        }
    }
}