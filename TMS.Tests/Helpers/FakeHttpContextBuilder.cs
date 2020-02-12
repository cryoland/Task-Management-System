using Microsoft.AspNetCore.Http;
using Moq;
using TMS.Models;

namespace TMS.Tests.Helpers
{
    public static class FakeHttpContextBuilder
    {
        public static Mock<HttpContext> Build(EmployeeRole role)
        {
            // Create a mock HttpContext
            var contextMock = new Mock<HttpContext>();

            // Mocking HttpContext
            contextMock.SetupGet(ctx => ctx.User).Returns(TestFunctions.GetUser(role));
            contextMock.SetupGet(ctx => ctx.User.Identity.Name).Returns(role.ToString());
            contextMock.SetupGet(ctx => ctx.User.Identity.IsAuthenticated).Returns(true);
            contextMock.Setup(ctx => ctx.User.IsInRole(EmployeeRole.Admin.ToString())).Returns(role.Equals(EmployeeRole.Admin));
            return contextMock;
        }
    }
}
