using Common;
using Common.Responce;
using LocationService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VisitService
{
    public class VisitProvider : IVisitProvider
    {
        List<Visit> _repository = new List<Visit>();
        private readonly ILocationProvider _locationProvider;
        public VisitProvider(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }

        public Visit GetById(int id)
        {
            return _repository.Find(n => n.Id == id);

        }

        public UserVisits GetByUserIdWithParametrs(int userId, uint? fromDate, uint? toDate, string country, int? toDistance)
        {
            IQueryable<Visit> visits = _repository.AsQueryable<Visit>().Where(n => n.User == userId);
            if (fromDate != null)
            {
                visits = visits.Where(n => n.Visited_at > fromDate);
            }
            if (toDate != null)
            {
                visits = visits.Where(n => n.Visited_at < toDate);
            }
            if (!string.IsNullOrEmpty(country))
            {

            }
            if (toDistance != null)
            {

            }
            UserVisits userVisits = new UserVisits();
            userVisits.Visits = visits.Select(n => new VisitDetail()
            {
                Mark = n.Mark,
                Visited_at = n.Visited_at,
                Place = _locationProvider.GetById(n.Location).Place
            })
                .OrderBy(m => m.Visited_at).ToList();
            return userVisits;
        }

        public void LoadVisits(List<Visit> visits)
        {
            _repository.AddRange(visits);
        }
    }
}
