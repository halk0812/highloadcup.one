using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService
{
    public interface IUserProvider
    {
        void LoadUser(List<User> users);
    }
}
