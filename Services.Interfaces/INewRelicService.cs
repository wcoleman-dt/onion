using System;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface INewRelicService
    {
        RootObject GetTransactionsSinceLastInterval(TimeSpan sinceLastDateTime, int resultLimit);
    }
}