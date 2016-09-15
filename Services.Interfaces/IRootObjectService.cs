using Domain.Entities;

namespace Services.Interfaces
{
    public interface IRootObjectService
    {
        RootObject SaveRootObject(RootObject rootObject);
        RootObject GetRootObject(int id);
    }
}