namespace API.Services;

public class SessionService
{
    private readonly Dictionary<string, SessionInfo> sessions = new();
    private readonly Lock sessionsLock = new();

    public string CreateSession(int userId, TimeSpan? lifetime = null)
    {
        lifetime ??= TimeSpan.FromHours(2);

        string sessionId = Guid.NewGuid().ToString("N");

        lock (this.sessionsLock)
        {
            this.sessions[sessionId] = new SessionInfo
            {
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.Add(lifetime.Value),
            };
        }

        return sessionId;
    }

    public int? GetUserId(string sessionId)
    {
        lock (this.sessionsLock)
        {
            if (!this.sessions.TryGetValue(sessionId, out SessionInfo? info))
            {
                return null;
            }

            if (info.ExpiresAt < DateTime.UtcNow)
            {
                this.sessions.Remove(sessionId);

                return null;
            }

            return info.UserId;
        }
    }

    public void RemoveSession(string sessionId)
    {
        lock (this.sessionsLock)
        {
            this.sessions.Remove(sessionId);
        }
    }

    private class SessionInfo
    {
        public int UserId { get; init; }

        public DateTime ExpiresAt { get; init; }
    }
}