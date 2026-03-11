using UserMgmntAPI.Models;
using System.Collections.Concurrent;

namespace UserMgmntAPI.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        bool Add(User user);
        bool Update(User user);
        bool Delete(int id);
    }

    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<int, User> _users = new();

        public InMemoryUserRepository()
        {
            _users.TryAdd(1, new User { Id = 1, Name = "Juan Pérez", Age = 25 });
            _users.TryAdd(2, new User { Id = 2, Name = "María García", Email = "maria.garcia@example.com", Age = 30 });
            _users.TryAdd(3, new User { Id = 3, Name = "Pedro López", Email = "pedro.lopez@example.com", Age = 40 });
        }

        public IEnumerable<User> GetAll() => _users.Values;

        public User? GetById(int id) => _users.TryGetValue(id, out var user) ? user : null;

        public bool Add(User user) => _users.TryAdd(user.Id, user);

        public bool Update(User user)
        {
            if (!_users.ContainsKey(user.Id))
                return false;

            _users[user.Id] = user;
            return true;
        }


        public bool Delete(int id) => _users.TryRemove(id, out _);
    }
}
