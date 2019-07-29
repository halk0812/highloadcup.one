using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VisitService
{
    public class VisitProvider : IVisitProvider
    {
        List<Visit> _repository = new List<Visit>();
        public VisitProvider()
        {

        }

        public Visit GetById(int id)
        {
            return _repository.Find(n => n.Id == id);

        }

        public List<Visit> GetByUserIdWithParametrs(int userId, uint? fromDate, uint? toDate, string country, int? toDistance)
        {
            IQueryable<Visit> visits = _repository.AsQueryable<Visit>().Where(n => n.User == userId);
            if(fromDate!=null)
            {
                visits = visits.Where(n => n.Visited_at > fromDate);
            }
            if (toDate != null)
            {
                visits = visits.Where(n => n.Visited_at < toDate);
            }
            if(!string.IsNullOrEmpty(country))
            {
                
            }
            if(toDistance!=null)
            {
                
            }
            return visits.OrderBy(m=>m.Visited_at).ToList();
        }

        public void LoadVisits(List<Visit> visits)
        {
            _repository.AddRange(visits);
        }
    }
}
