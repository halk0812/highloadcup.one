using Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace UserService
{
    public class UsersProvider : IUserProvider
    {
        private List<User> _repository= new List<User>();
        public UsersProvider()
        {
        }

        public User GetById(int id)
        {
            return _repository.FirstOrDefault(n=>n.Id==id);
        }

        public void LoadUser(List<User> users)
        {
            _repository.AddRange(users);

        }

        public uint[] GetIdByAgeAndGender(string gender, int? fromAge, int? toAge)
        {

            if (string.IsNullOrEmpty(gender) && fromAge == null && toAge == null)
                return new uint[0];
            IQueryable<User> query = _repository.AsQueryable();
            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(n => n.Gender.ToLower() == gender.ToLower());
            }
            if (fromAge != null)
            {
                query = query.Where(n => n.Birth_date < GetYear(fromAge.Value));
            }
            if (toAge != null)
            {
                query = query.Where(n => n.Birth_date > GetYear(toAge.Value));
            }
            return query.Select(m => m.Id).ToArray();

        }

        private int GetYear(int age)
        {
            return (int)DateTimeOffset.UtcNow.AddYears(-1 * age).ToUnixTimeSeconds();
        }
    }
}
