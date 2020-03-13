using Shouldly;
using TMS.Domain.Exceptions;
using TMS.Domain.ValueObjects;
using Xunit;

namespace TMS.Domain.UnitTests.ValueObjects
{
    public class AdAccountTests
    {
        [Fact]
        public void Should_Have_Correct_Domain_And_Name()
        {
            const string accountString = "DomainName\\UserName";

            var account = AdAccount.For(accountString);

            account.Domain.ShouldBe("DomainName");
            account.Name.ShouldBe("UserName");
        }

        [Fact]
        public void ToString_Returns_Correct_Format()
        {
            const string accountString = "DomainName\\UserName";

            var account = AdAccount.For(accountString);

            var result = account.ToString();

            result.ShouldBe(accountString);
        }

        [Fact]
        public void Implicit_Conversion_To_String_Results_In_Correct_String()
        {
            const string accountString = "DomainName\\UserName";

            var account = AdAccount.For(accountString);

            string result = account;

            result.ShouldBe(accountString);
        }

        [Fact]
        public void Explicit_Conversion_From_String_Sets_Domain_And_Name()
        {
            const string accountString = "DomainName\\UserName";

            var account = (AdAccount)accountString;

            account.Domain.ShouldBe("DomainName");
            account.Name.ShouldBe("UserName");
        }

        [Fact]
        public void Should_Throw_AdAccountInvalidException_For_Invalid_AdAccount()
        {
            var exception = Assert.Throws<AdAccountInvalidException>(() => (AdAccount)"DomainNameUserName");

            Assert.Equal("AD Account \"DomainNameUserName\" is invalid.", exception.Message);
        }
    }
}
