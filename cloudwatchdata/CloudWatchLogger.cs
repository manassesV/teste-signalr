using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;

namespace signalrprojectacs.cloudwatchdata
{
    public class CloudWatchLogger 
    {
        private IAmazonCloudWatchLogs _client;
        private string _logGroup;
        private string _nextSequenceToken = null;
        private string _logStream;


        private CloudWatchLogger(string logGroup)
        {
            // If don't have an AWS Profile on your machine and application is hosted outside
            // of AWS infrastructure (where IAM roles cannot be assigned to infrastructure),
            // rather use:
            // _client = new AmazonCloudWatchLogsClient(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.AFSouth1);
            _client = new AmazonCloudWatchLogsClient();
            _logGroup = logGroup;
        }

        private async Task CreateLogStreamAsync()
        {
            _logStream = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

            _ = await _client.CreateLogStreamAsync(new CreateLogStreamRequest()
            {
                LogGroupName = _logGroup,
                LogStreamName = _logStream
            });
        }

        public static async Task<CloudWatchLogger> GetLoggerAsync(string logGroup)
        {
            var logger = new CloudWatchLogger(logGroup);

            // Create a log group for our logger
            await logger.CreateLogGroupAsync();

            return logger;
        }

        private async Task CreateLogGroupAsync()
        {
            var existingLogGroups = await _client.DescribeLogGroupsAsync();
            if (existingLogGroups.LogGroups.Any(x => x.LogGroupName == _logGroup))
                return;

            _ = await _client.CreateLogGroupAsync(new CreateLogGroupRequest()
            {
                LogGroupName = _logGroup
            });
        }

        public async Task LogMessageAsync(string message)
        {
            var response = await _client.PutLogEventsAsync(new PutLogEventsRequest()
            {
                LogGroupName = _logGroup,
                LogStreamName = _logStream,
                SequenceToken = _nextSequenceToken,
                LogEvents = new List<InputLogEvent>()
            {
                new InputLogEvent()
                {
                    Message = message,
                    Timestamp = DateTime.UtcNow
                }
            }
            });

            _nextSequenceToken = response.NextSequenceToken;
        }
    }
}
