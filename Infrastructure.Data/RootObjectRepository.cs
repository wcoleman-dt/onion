using System.Linq;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class RootObjectRepository : IRootObjectRepository
    {
        private readonly MonthlyReportingModel _model;

        public RootObjectRepository()
        {
            _model = new MonthlyReportingModel();
        }


        public RootObject SaveRootObject(RootObject rootObject)
        {
            if (rootObject != null)
            {
                _model.RootObjects.Add(rootObject);
                _model.SaveChanges();
            }

            return rootObject;
        }

        public RootObject GetRootObject(int id)
        {
            return _model.RootObjects.FirstOrDefault(r => r.Id == id);
        }


    }
}