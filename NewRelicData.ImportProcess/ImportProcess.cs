using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Utility;
using Services.Interfaces;

namespace NewRelicData.ImportProcess
{
    public class ImportProcess
    {
        private readonly int _millisecondsToSleep;
        private readonly IIntervalCalculator _intervalCalculator;
        private readonly IHashingAlgorithm _hashingAlgorithm;
        private readonly IApiEndPointService _apiEndPointService;
        private readonly INewRelicService _newRelicService;
        private readonly IRootObjectService _rootObjectService;
        private readonly IEventService _eventService;

        public List<Event> HistoricalEvents { get; set; }
        public Stack<NewRelicHttpRequest> HistoricalWorkLoad { get; set; }
        public Stack<NewRelicHttpRequest> CurrentWorkLoad { get; set; }
        public int QUERYLIMIT { get; private set; } = 1000;

        public ImportProcess(int millisecondsToSleep, IIntervalCalculator intervalCalculator,
            IHashingAlgorithm hashingAlgorithm,
            IApiEndPointService apiEndPointService,
            INewRelicService newRelicService,
            IRootObjectService rootObjectService,
            IEventService eventService)
        {
            _millisecondsToSleep = millisecondsToSleep;
            _intervalCalculator = intervalCalculator;
            _hashingAlgorithm = hashingAlgorithm;
            _newRelicService = newRelicService;
            _rootObjectService = rootObjectService;
            _eventService = eventService;
            _apiEndPointService = apiEndPointService;

            HistoricalEvents = _eventService.GetEvents()?.ToList();
            HistoricalWorkLoad = new Stack<NewRelicHttpRequest>();
            CurrentWorkLoad = new Stack<NewRelicHttpRequest>();

            Console.WriteLine("ImportProcess created.");
        }

        public void GetHistoricalEvents()
        {
            Console.WriteLine("ImportProcess.GetHistoricalEvents() started.");

            //read time interval for catching up from database or app.config
            //calculate minutes (buckets) of data to process via calls to New Relic API
            var timeIntervals = _intervalCalculator.CalculateTimeIntervals();

            //issue a http request for each timeInterval to New Relic API
            var apiKeyType = 2;
            var apiEndpoint = _apiEndPointService.GetEndPoint(apiKeyType);

            if (timeIntervals > 0)
            {
                for (double x = 1; x <= timeIntervals; x++)
                {
                    var nRsqlQuery = string.Format(apiEndpoint.NRSQLSyntax, x, QUERYLIMIT);
                    var newRelicHttpRequest = new NewRelicHttpRequest(apiEndpoint, new RootObject(),
                                                    nRsqlQuery);
                    var interval = Convert.ToInt32(Math.Floor(timeIntervals));
                    newRelicHttpRequest.RootObject =
                        _newRelicService.GetTransactionsSinceLastInterval(new TimeSpan(0, interval, 0), QUERYLIMIT);
                    //calculate hashed value for each returned set of data
                    var didGenerateHashByteValue = newRelicHttpRequest.RootObject.IterateOverEventExecutingAction<Event>
                                            ((e) => { e.HashedByteValue = e.GenerateHashedByteValue(_hashingAlgorithm); });
                    //store values to database
                    if (didGenerateHashByteValue && newRelicHttpRequest?.RootObject != null)
                    {
                        //weed out duplicate events within rootobject
                        WeedOutDuplicateEvents(HistoricalEvents, newRelicHttpRequest.RootObject);

                        //save rootobject to database
                        var rootObject = _rootObjectService.SaveRootObject(newRelicHttpRequest.RootObject);

                        //add events to HistoricalEvents
                        var listOfNewEvents = newRelicHttpRequest?.RootObject?.results?.First()?.events?.ToList();
                        if (listOfNewEvents != null)
                        {
                            HistoricalEvents.AddRange(listOfNewEvents);
                            Console.WriteLine("Added {0} events.", listOfNewEvents.Count);
                        }
                    }
                }
            }
            Console.WriteLine("ImportProcess.GetHistoricalEvents() completed for {0} intervals.", timeIntervals);
        }

        private void WeedOutDuplicateEvents(List<Event> historicalEvents, RootObject rootObject)
        {
            var historicalEventByteArrays = historicalEvents.Select(r => r.HashedByteValue)?.ToList();
            rootObject.IterateOverEventExecutingAction<Event>
                                            ((e) =>
                                            {
                                                foreach (var byteArray in historicalEventByteArrays)
                                                {
                                                    if (byteArray.SequenceEqual(e.HashedByteValue))
                                                    {
                                                        e.IsDuplicate = true;
                                                    }
                                                }
                                            });
            var duplicates = rootObject?.results?.First().events?.Where(r => r.IsDuplicate == true)?.Select(r => r)?.ToList();
            if (duplicates != null)
            {
                foreach (var duplicate in duplicates)
                {
                    rootObject?.results?.First()?.events.Remove(duplicate);
                }
            }
        }

        private void EliminateDuplicateRootObjects(RootObject rootObject)
        {
            if (rootObject != null)
            {
                var events = HistoricalWorkLoad.Select(r => r.RootObject.results.First().events);

                //rootObject.results.First().events.Except()
            }
        }

        public void GetOngoingEvents()
        {
            while (true)
            {
                Console.WriteLine(string.Concat("Sleeping until ",
                    DateTime.Now.AddMilliseconds(_millisecondsToSleep).ToLongTimeString()));
                GetOngoingEvents(_millisecondsToSleep);
                Thread.Sleep(_millisecondsToSleep);
            }
        }


        public void GetOngoingEvents(int millisecondsToSleep)
        {
            //read time interval for catching up from database or app.config
            //calculate minutes (buckets) of data to process via calls to New Relic API
            var timeIntervals = 1.0D;

            //issue a http request for each timeInterval to New Relic API
            var apiKeyType = 2;
            var apiEndpoint = _apiEndPointService.GetEndPoint(apiKeyType);

            if (timeIntervals > 0)
            {
                for (double x = 1; x <= timeIntervals; x++)
                {
                    var nRsqlQuery = string.Format(apiEndpoint.NRSQLSyntax, x, QUERYLIMIT);
                    var newRelicHttpRequest = new NewRelicHttpRequest(apiEndpoint, new RootObject(),
                                                    nRsqlQuery);
                    var interval = Convert.ToInt32(Math.Floor(timeIntervals));
                    newRelicHttpRequest.RootObject =
                        _newRelicService.GetTransactionsSinceLastInterval(new TimeSpan(0, interval, 0), QUERYLIMIT);
                    //calculate hashed value for each returned set of data
                    var didGenerateHashByteValue = newRelicHttpRequest.RootObject.IterateOverEventExecutingAction<Event>
                                            ((e) => { e.HashedByteValue = e.GenerateHashedByteValue(_hashingAlgorithm); });
                    //store values to database
                    if (didGenerateHashByteValue && newRelicHttpRequest?.RootObject != null)
                    {
                        //weed out duplicate events within rootobject
                        WeedOutDuplicateEvents(HistoricalEvents, newRelicHttpRequest.RootObject);

                        //save rootobject to database
                        var rootObject = _rootObjectService.SaveRootObject(newRelicHttpRequest.RootObject);

                        //add events to HistoricalEvents
                        var listOfNewEvents = newRelicHttpRequest?.RootObject?.results?.First()?.events?.ToList();
                        if (listOfNewEvents != null)
                        {
                            foreach (var eventObj in listOfNewEvents)
                            {
                                if (!HistoricalEvents.Contains(eventObj))
                                {
                                    HistoricalEvents.Add(eventObj);
                                }
                            }
                            Console.WriteLine("Added {0} events.", listOfNewEvents.Count);
                        }
                    }
                }
            }
            Console.WriteLine("ImportProcess.GetOngoingEvents() completed for {0} intervals.", timeIntervals);

        }
    }
}


