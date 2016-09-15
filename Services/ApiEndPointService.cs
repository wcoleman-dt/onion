using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.NewRelic
{
    public class ApiEndPointService : IApiEndPointService
    {
        private readonly IApiEndPointRepository _apiEndPointRepository;

        public ApiEndPointService(IApiEndPointRepository apiEndPointRepository)
        {
            _apiEndPointRepository = apiEndPointRepository;
        }

        public ApiEndpoint GetEndPoint(int apiKeyType)
        {
            return _apiEndPointRepository.GetEndPoint(apiKeyType);
        }

        public ApiEndpoint SaveEndPoint(ApiEndpoint apiEndpoint)
        {
            return _apiEndPointRepository.SaveEndPoint(apiEndpoint);
        }
    }
}
