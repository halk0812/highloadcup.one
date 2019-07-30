using Common;
using Common.Responce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VisitService
{
    public interface IVisitProvider
    {
        void LoadVisits(List<Visit> visits);
        Visit GetById(int id);
        UserVisits GetByUserIdWithParametrs(int userId, UInt32? fromDate, UInt32? toDate, string country, int? toDistance);
    }
}
