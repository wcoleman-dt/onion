using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.NewRelic
{
    public class RootObjectService : IRootObjectService
    {
        private readonly IRootObjectRepository _rootObjectRepository;

        public RootObjectService(IRootObjectRepository rootObjectRepository)
        {
            _rootObjectRepository = rootObjectRepository;
        }

        public RootObject SaveRootObject(RootObject rootObject)
        {
            var rootObj = _rootObjectRepository.SaveRootObject(rootObject);
            return rootObj;
        }

        public RootObject GetRootObject(int id)
        {
            return _rootObjectRepository.GetRootObject(id);
        }
    }
}