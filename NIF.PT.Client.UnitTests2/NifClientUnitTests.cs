using System;
using Flurl.Http.Testing;
using Xunit;

namespace NIF.PT.Client.UnitTests
{
    public class NifClientUnitTests
    {
        private readonly HttpTest _httpTest;

        public NifClientUnitTests()
        {
            this._httpTest = new HttpTest();
        }

        public void Constructor_ValidKey_ShouldBeOk()
        {
            //Arrange
            var key = "";

            //Act
            var client = new NifClient(key);

            //Assert
            Assert.NotNull(client);
            Assert.Equal(key, client.Key);
        }

        [Trait("Category", "Constructor")]
        public void Constructor_InvalidKey_ShouldThrowException()
        {
            //Arrange
            var key = string.Empty;

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => new NifClient(key));

            //Assert
            Assert.NotNull(exception);
        }
    }
}