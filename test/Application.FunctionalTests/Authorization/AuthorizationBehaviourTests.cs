using ASE3040.Application.Common.Behaviours;
using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Security;
using MediatR;
using Moq;

namespace Application.FunctionalTests.Authorization;

[TestClass]
public class AuthorizationBehaviourTests : BaseTestClass
{
    [TestMethod]
    public async Task ShouldNotThrowExceptionIfAuthorized()
    {
        var userMock = new Mock<IUser>();
        userMock.Setup(u => u.UserId).Returns("user-123");

        var behaviour = new AuthorizationBehaviour<AuthorizedRequest, Unit>(userMock.Object);
        var request = new AuthorizedRequest();
        var next = new RequestHandlerDelegate<Unit>(() => Task.FromResult(Unit.Value));

        await behaviour.Handle(request, next, CancellationToken.None);
    }

    [TestMethod]
    public async Task ShouldThrowExceptionIfNotAuthorized()
    {
        var userMock = new Mock<IUser>();
        userMock.Setup(u => u.UserId).Returns((string)null);

        var behaviour = new AuthorizationBehaviour<AuthorizedRequest, Unit>(userMock.Object);
        var request = new AuthorizedRequest();
        var next = new RequestHandlerDelegate<Unit>(() => Task.FromResult(Unit.Value));

        await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() =>
            behaviour.Handle(request, next, CancellationToken.None));
    }

    [TestMethod]
    public async Task ShouldNotThrowExceptionIfNoAuthorizeAttribute()
    {
        var userMock = new Mock<IUser>();
        userMock.Setup(u => u.UserId).Returns((string)null);

        var behaviour = new AuthorizationBehaviour<NoAuthorizeAttributeRequest, Unit>(userMock.Object);
        var request = new NoAuthorizeAttributeRequest();
        var next = new RequestHandlerDelegate<Unit>(() => Task.FromResult(Unit.Value));

        await behaviour.Handle(request, next, CancellationToken.None);
    }

    [Authorize]
    private class AuthorizedRequest : IRequest<Unit>
    {
    }

    private class NoAuthorizeAttributeRequest : IRequest<Unit>
    {
    }
}