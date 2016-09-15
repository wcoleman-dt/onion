using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Utility
{
    public static class ModelExtensions
    {
        public static string HtmlEncode(this string s)
        {
            return Uri.EscapeDataString(s);
        }

        public static byte[] GenerateHashedByteValue(this Event e, IHashingAlgorithm hashingAlgorithm)
        {
            var eventObjAsStringArray = ConverMembersToStringArray(e);
            var eventObjAsString = string.Join(",", eventObjAsStringArray);
            var eventObjAsByteArray = Encoding.ASCII.GetBytes(eventObjAsString);
            var hashedByteArray = hashingAlgorithm.ComputeHash(eventObjAsByteArray); 
            return hashedByteArray;
        }

        public static string IterateOverEventExecutingAction(this Event e, IHashingAlgorithm hashingAlgorithm)
        {
            var hashedByteArray = GenerateHashedByteValue(e, hashingAlgorithm);

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            hashedByteArray.ToList().ForEach(r =>
            {
                sb.Append(r.ToString());
            });
            return sb.ToString();
        }

        private static string[] ConverMembersToStringArray(Event e)
        {
            string[] eventObj =
            {
                e.appName,
                e.errorMessage,
                e.errorType,
                e.host,
                e.name,
                e.transactionSubType,
                e.transactionType,
                e.tripId,
                e.databaseDuration.ToString(CultureInfo.InvariantCulture),
                e.appId.ToString(),
                e.duration.ToString(CultureInfo.InvariantCulture),
                e.externalDuration.ToString(CultureInfo.InvariantCulture),
                e.queueDuration.ToString(CultureInfo.InvariantCulture),
                e.realAgentId.ToString(),
                e.timestamp.ToString(),
                e.totalTime?.ToString(CultureInfo.InvariantCulture) ?? string.Empty,
                e.webDuration.ToString(CultureInfo.InvariantCulture)
            };
            var result = string.Join(",", eventObj);
            return eventObj;
        }

        public static bool IterateOverEventExecutingAction<T>(this RootObject rootObject, Action<Event> action)
        {
            bool retVal;
            try
            {
                if (rootObject != null)
                {
                    foreach (var result in rootObject?.results)
                    {
                        if (result != null)
                        {
                            foreach (var eventObj in result?.events)
                            {
                                action(eventObj);
                            }
                        }
                    }
                }
                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return retVal;
        }
    }
}
