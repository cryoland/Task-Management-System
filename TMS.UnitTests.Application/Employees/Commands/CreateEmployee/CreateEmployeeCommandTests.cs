using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Employees.Commands.CreateEmployee;
using TMS.UnitTests.Application.Common;
using Xunit;

namespace TMS.UnitTests.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandTests : CommandTestBase
    {
        [Theory]
        [InlineData("FullnameTest")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Handle_ShouldPersistEmployee(string fullname)
        {
            var command = new CreateEmployeeCommand
            {
                FullName = fullname,
            };

            var handler = new CreateEmployeeCommand.CreateEmployeeCommandHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.Employees.Find(result);

            entity.ShouldNotBeNull();
            entity.FullName.ShouldBe(command.FullName);
        }

    }
}
