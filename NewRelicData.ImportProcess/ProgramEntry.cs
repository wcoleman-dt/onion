using System;
using System.Threading;
using Domain.Entities;
using Domain.HashingAlgorithm;
using Domain.Interfaces;
using Domain.Utility;
using Infrastructure.Data;
using Microsoft.Practices.Unity;
using Services.Interfaces;
using Services.NewRelic;

namespace NewRelicData.ImportProcess
{
    public static class ProgramEntry
    {
        static void Main(string[] args)
        {
            var unity = CreateUnityContainerAndRegisterComponents();
            var importProcess = unity.Resolve<ImportProcess>();

            ThreadStart ongoingThreadStart = new ThreadStart(importProcess.GetOngoingEvents);
            ThreadStart historicalThreadstart = new ThreadStart(importProcess.GetHistoricalEvents);

            Thread ongoingThread = new Thread(ongoingThreadStart) {Name = "OngoingThread" };
            Thread historicalThread = new Thread(historicalThreadstart)
            {
                Name = "HistoricalThread",
                IsBackground = true
            };

            Console.WriteLine("ImportProcess.GetOngoingEvents() started.");
            ongoingThread.Start();
            historicalThread.Start();

            Console.ReadLine();
        }

        private static IUnityContainer CreateUnityContainerAndRegisterComponents()
        {
            IUnityContainer container = new UnityContainer();
            //Register database connectivity
            var instance = new MonthlyReportingModel();
            container.RegisterInstance(typeof(MonthlyReportingModel), instance,
                new TransientLifetimeManager());

            //Infrastructure.Data Items
            container.RegisterType<IApiEndPointRepository, ApiEndPointRepository>();
            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<IRootObjectRepository, RootObjectRepository>();
            
            //Domain.HashingAlgorithm
            container.RegisterType<IHashingAlgorithm, Murmur3>();

            //Domain.Utility
            container.RegisterType<IIntervalCalculator, IntervalCalculator>();

            //Service.NewRelic Items
            container.RegisterType<IApiEndPointService, ApiEndPointService>();
            container.RegisterType<INewRelicService, NewRelicService>();
            container.RegisterType<IEventService, EventService>();
            container.RegisterType<IRootObjectService, RootObjectService>();

            //NewRelicData.ImportProcess
            var intervalCalculator = container.Resolve<IIntervalCalculator>();
            var hashingAlgorithm = container.Resolve<IHashingAlgorithm>();
            var apiEndPointService = container.Resolve<IApiEndPointService>();
            var newRelicService = container.Resolve<INewRelicService>();
            var rootObjectService = container.Resolve<IRootObjectService>();
            var eventService = container.Resolve<IEventService>();
            int millisecondsToSleep =
               Int32.Parse(System.Configuration.ConfigurationManager.AppSettings[CommonStrings.MillisecondsToSleep]);
            var importProcess = new ImportProcess(millisecondsToSleep, intervalCalculator, hashingAlgorithm,
                                    apiEndPointService, newRelicService,rootObjectService, eventService);
            container.RegisterInstance(typeof(ImportProcess), importProcess,
                new ContainerControlledLifetimeManager());

            return container;
        }
    }
}