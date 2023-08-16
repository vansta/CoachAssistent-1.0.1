namespace CoachAssistent.Models.ViewModels
{
    public class OverviewViewModel<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
    }
}