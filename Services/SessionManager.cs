using System.Collections.Concurrent;

namespace PresentationApp.Services
{

    /// <summary>
    /// Tracks, per SignalR connection, which presentation they are in, their nickname, and their role.
    /// </summary>
    public class SessionManager
    {
        // Key = ConnectionId
        // Value = (presentationId, nickname, role)
        private ConcurrentDictionary<string, (Guid PresentationId, string Nickname, string Role)> _connections
            = new();

        public void AddOrUpdate(string connectionId, Guid presentationId, string nickname, string role)
        {
            _connections[connectionId] = (presentationId, nickname, role);
        }

        public bool TryGetConnection(string connectionId, out (Guid PresentationId, string Nickname, string Role) info)
        {
            return _connections.TryGetValue(connectionId, out info);
        }

        public void RemoveConnection(string connectionId)
        {
            _connections.TryRemove(connectionId, out _);
        }

        public void UpdateRole(string connectionId, string newRole)
        {
            if (_connections.TryGetValue(connectionId, out var tuple))
            {
                _connections[connectionId] = (tuple.PresentationId, tuple.Nickname, newRole);
            }
        }

        public IEnumerable<(string ConnectionId, string Nickname, string Role)> GetUsersInPresentation(Guid presentationId)
        {
            foreach (var kvp in _connections)
            {
                var (pid, nick, role) = kvp.Value;
                if (pid == presentationId)
                    yield return (kvp.Key, nick, role);
            }
        }
    }
}
