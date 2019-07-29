using System;
using System.Collections.Generic;
using Common;

namespace UserService
{
    public class UsersProvider : IUserProvider
    {
        private List<User> _repository = new List<User>();
        public UsersProvider()
        {
        }

        public User GetById(int id)
        {
            return _repository.Find(n => n.Id == id);
        }

        public void LoadUser(List<User> users)
        {
            _repository.AddRange(users);
        }
    }
}
