namespace Domain.Entities
{
    public class PerformanceStats
    {
        public int Id { get; set; }
        public int fileReadCount { get; set; }
        public int inspectedCount { get; set; }
        public int omittedCount { get; set; }
        public int matchCount { get; set; }
        public int processCount { get; set; }
        public int rawBytes { get; set; }
        public int decompressedBytes { get; set; }
        public int responseBodyBytes { get; set; }
        public int fileProcessingTime { get; set; }
        public int mergeTime { get; set; }
        public int ioTime { get; set; }
        public int decompressionTime { get; set; }
        public int wallClockTime { get; set; }
        public int fullCacheHits { get; set; }
        public int partialCacheHits { get; set; }
        public int cacheMisses { get; set; }
        public int cacheSkipped { get; set; }
        public int maxInspectedCount { get; set; }
        public int minInspectedCount { get; set; }
        public int slowLaneFiles { get; set; }
        public int slowLaneFileProcessingTime { get; set; }
        public int slowLaneWaitTime { get; set; }
        public double sumSubqueryWeight { get; set; }
        public double sumFileProcessingTimePercentile { get; set; }
        public int subqueryWeightUpdates { get; set; }
        public int sumSubqueryWeightStartFileProcessingTime { get; set; }
        public int runningQueriesTotal { get; set; }
        public int ignoredFiles { get; set; }
    }
}