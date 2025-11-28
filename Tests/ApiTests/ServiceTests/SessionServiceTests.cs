
using API.Services;

namespace Tests.ApiTests.ServiceTests;

[TestFixture]
public class SessionServiceTests
{
    private SessionService sessionService = null!;

    [SetUp]
    public void SetUp()
    {
        this.sessionService = new SessionService();
    }

    [Test]
    public void CreateSession_ReturnsValidSessionId()
    {
        string sessionId = this.sessionService.CreateSession(userId: 1);

        Assert.That(sessionId, Is.Not.Null);
        Assert.That(sessionId, Is.Not.Empty);
    }

    [Test]
    public void CreateSession_AssignsCorrectUserId()
    {
        string sessionId = this.sessionService.CreateSession(userId: 42);

        int? userId = this.sessionService.GetUserId(sessionId);

        Assert.That(userId, Is.EqualTo(42));
    }

    [Test]
    public void GetUserId_WithInvalidSessionId_ReturnsNull()
    {
        int? userId = this.sessionService.GetUserId("invalid-session-id");

        Assert.That(userId, Is.Null);
    }

    [Test]
    public void GetUserId_WithExpiredSession_ReturnsNull()
    {
        string sessionId = this.sessionService.CreateSession(userId: 1, lifetime: TimeSpan.FromMilliseconds(1));

        Thread.Sleep(10); // Wait for expiration

        int? userId = this.sessionService.GetUserId(sessionId);

        Assert.That(userId, Is.Null);
    }

    [Test]
    public void RemoveSession_RemovesSession()
    {
        string sessionId = this.sessionService.CreateSession(userId: 1);

        this.sessionService.RemoveSession(sessionId);

        int? userId = this.sessionService.GetUserId(sessionId);
        Assert.That(userId, Is.Null);
    }

    [Test]
    public void RemoveSession_WithInvalidSessionId_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => this.sessionService.RemoveSession("invalid-session-id"));
    }

    [Test]
    public void CreateSession_WithCustomLifetime_RespectsLifetime()
    {
        string sessionId = this.sessionService.CreateSession(userId: 1, lifetime: TimeSpan.FromHours(5));

        int? userId = this.sessionService.GetUserId(sessionId);

        Assert.That(userId, Is.EqualTo(1));
    }

    [Test]
    public void CreateSession_MultipleSessions_AreIndependent()
    {
        string session1 = this.sessionService.CreateSession(userId: 1);
        string session2 = this.sessionService.CreateSession(userId: 2);

        Assert.That(session1, Is.Not.EqualTo(session2));
        Assert.That(this.sessionService.GetUserId(session1), Is.EqualTo(1));
        Assert.That(this.sessionService.GetUserId(session2), Is.EqualTo(2));
    }
}
