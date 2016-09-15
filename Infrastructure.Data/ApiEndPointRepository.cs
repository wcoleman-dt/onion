using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class ApiEndPointRepository : IApiEndPointRepository
    {
        private readonly MonthlyReportingModel _model;

        public ApiEndPointRepository()
        {
            _model = new MonthlyReportingModel();
        }

        public ApiEndpoint GetEndPoint(int apiKeyType)
        {
            return _model.ApiEndpoints.FirstOrDefault(r => r.ApiKeyType == apiKeyType);
        }

        public ApiEndpoint SaveEndPoint(ApiEndpoint apiEndpoint)
        {
            ApiEndpoint retVal = null;
            if (apiEndpoint != null)
            {
             retVal = _model.ApiEndpoints.Add(apiEndpoint);
                _model.SaveChanges();
            }
            return retVal;
        }

        public IEnumerable<ApiEndpoint> GetApiEndpoints(int apiKeyType)
        {
            var endpoints = _model.ApiEndpoints.Where(r => r.ApiKeyType == apiKeyType).ToList();
            return endpoints;
        }
    }
}
