using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;

namespace UserService
{
    public class UsersProvider : IUserProvider
    {
        private List<User> _repository = new List<User>();
        public UsersProvider()
        {
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await Task.Run(()=>_repository.Find(n => n.Id == id));
        }

        public void LoadUser(List<User> users)
        {
            _repository.AddRange(users);
        }
    }
}
