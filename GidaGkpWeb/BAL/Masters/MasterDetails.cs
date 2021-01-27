using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using GidaGkpWeb.Global;
using System.Data.Entity;

namespace GidaGkpWeb.BAL
{
    public class MasterDetails
    {
        GidaGKPEntities _db = null;

        public IEnumerable<object> GetLookupDetail(int? parentLookupId, string lookupTye)
        {
            _db = new GidaGKPEntities();
            return (from lookup in _db.Lookups.Where(x => (x.ParentLookupId == parentLookupId) && x.LookupType == lookupTye && x.IsActive == true)
                    select new
                    {
                        lookup.LookupId,
                        lookup.LookupName,
                        lookup.LookupType,
                        lookup.ParentLookupId,
                    }).OrderBy(x => x.LookupName).ToList();

        }

    }
}