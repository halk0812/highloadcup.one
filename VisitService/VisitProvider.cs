using Common;
using Common.Responce;
using LocationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Visit> GetByIdAsync(int id)
        {
            return await Task.Run(()=>_repository.Find(n => n.Id == id));

        }

        public async Task<UserVisits> GetByUserIdWithParametrsAsync(int userId, uint? fromDate, uint? toDate, string country, int? toDistance)
        {
           
                IQueryable<Visit> visits = _repository.AsQueryable<Visit>().Where(n => n.User == userId);
                if (fromDate != null)
                {
                    visits = visits.Where(n => n.Visited_at > fromDate);
                }
                if (toDate != 0)
                {
                    visits = visits.Where(n => n.Visited_at < toDate);
                }
                uint[] arrayIds = await _locationProvider.GetIdByCountryAndDistanceAsync(country, toDistance);
                if (arrayIds.Count() > 0)
                {
                    visits = visits.Where(n => arrayIds.Contains(n.Location));
                }
                UserVisits userVisits = new UserVisits();
                userVisits.Visits = visits.Select(n => new VisitDetail()
                {
                    Mark = n.Mark,
                    Visited_at = n.Visited_at,
                    Place = _locationProvider.GetByIdAsync(n.Location).GetAwaiter().GetResult().Place
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
