using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IApiEndPointRepository
    {
        ApiEndpoint GetEndPoint(int apiKeyType);
        ApiEndpoint SaveEndPoint(ApiEndpoint apiEndpoint);
        IEnumerable<ApiEndpoint> GetApiEndpoints(int apiKeyType);
    }
}