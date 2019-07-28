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
        public void LoadUser(List<User> users)
        {
            Console.WriteLine("Loaded User");
            _repository.AddRange(users);
        }
    }
}
