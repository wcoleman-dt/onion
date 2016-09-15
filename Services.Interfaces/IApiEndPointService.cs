using Domain.Entities;

namespace Services.Interfaces
{
    public interface IApiEndPointService
    {
        ApiEndpoint GetEndPoint(int apiKeyType);
        ApiEndpoint SaveEndPoint(ApiEndpoint apiEndpoint);
    }
}
