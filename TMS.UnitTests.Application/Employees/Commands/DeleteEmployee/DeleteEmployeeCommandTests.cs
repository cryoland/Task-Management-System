using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Exceptions;
using TMS.Application.Employees.Commands.DeleteEmployee;
using TMS.UnitTests.Application.Common;
using Xunit;

namespace TMS.UnitTests.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidId_ShouldRemovePersistedEmployee()
        {
            var command = new DeleteEmployeeCommand
            {
                Id = 1,
            };

            var handler = new DeleteEmployeeCommand.DeleteEmployeeCommandHandler(Context);

            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Employees.Find(command.Id);

            entity.ShouldBeNull();
        }

        [Fact]
        public void Handle_GivenInvalidId_ThrowsException()
        {
            var command = new DeleteEmployeeCommand
            {
                Id = 99
            };

            var handler = new DeleteEmployeeCommand.DeleteEmployeeCommandHandler(Context);

            Should.ThrowAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
