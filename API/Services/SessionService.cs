namespace API.Services;

public class SessionService
{
    private class SessionInfo
    {
        public int      UserId    { get; init; }
        
        public DateTime ExpiresAt { get; init; }
    }

    private readonly Dictionary<string, SessionInfo> sessions = new();

    public string CreateSession(int userId, TimeSpan? lifetime = null)
    {
        // SessionID lasts for 2 hours
        lifetime ??= TimeSpan.FromHours(2);

        string sessionId = Guid.NewGuid().ToString("N");

        this.sessions[sessionId] = new SessionInfo
        {
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.Add(lifetime.Value),
        };

        return sessionId;
    }

    public int? GetUserId(string sessionId)
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

    public void RemoveSession(string sessionId)
    {
        this.sessions.Remove(sessionId);
    }
}