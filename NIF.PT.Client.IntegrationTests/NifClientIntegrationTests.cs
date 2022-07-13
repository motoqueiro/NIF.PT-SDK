namespace NIF.PT.Client.IntegrationTests
{
    using FluentAssertions;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "Integration Tests")]
    public class NifClientIntegrationTests
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
            var response = await this._client.SearchAsync(nif);

            //Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Integration_VerifyCredits()
        {
            //Act
            var response = await this._client.VerifyCreditsAsync();

            //Assert
            response.Should().NotBeNull();
            response.Credits.Month.Should().BeGreaterOrEqualTo(0);
            response.Credits.Day.Should().BeGreaterOrEqualTo(0);
            response.Credits.Hour.Should().BeGreaterOrEqualTo(0);
            response.Credits.Minute.Should().BeGreaterOrEqualTo(0);
            response.Credits.Paid.Should().BeGreaterOrEqualTo(0);
        }

        [Theory]
        [InlineData(1000, 10, "10241")]
        public async Task Integration_BuyCredits(
            uint buy,
            uint expectedAmount,
            string expectedEntity)
        {
            //Act
            var response = await this._client.BuyCreditsAsync(buy);

            //Assert
            response.Should().NotBeNull();
            response.Credits.Should().Equals(buy);
            response.AtmReference.Should().NotBeNull();
            response.AtmReference.Amount.Should().Equals(expectedAmount);
            response.AtmReference.Reference.Should().NotBeNullOrEmpty();
            response.AtmReference.Entity.Should().Equals(expectedEntity);
        }
    }
}