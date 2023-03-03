namespace FitbitAuthSample.Services
{
    public class ActivityListItem
    {
        public int ActiveDuration { get; set; }

        public TimeSpan ActiveDurationTimeSpan
            => TimeSpan.FromMilliseconds(ActiveDuration);

        public string ActivityName { get; set; }
            = string.Empty;

        public int Calories { get; set; }

        public DateTimeOffset LastModified { get; set; }
    }
}
