using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserService
{
    public interface IUserProvider
    {
        void LoadUser(List<User> users);

        User GetById(int id);
    }
}
