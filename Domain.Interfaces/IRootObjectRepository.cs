using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRootObjectRepository
    {
        RootObject SaveRootObject(RootObject rootObject);
        RootObject GetRootObject(int id);
    }
}