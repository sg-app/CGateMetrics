namespace CGateMetricsGui.Interfaces
{
    public interface IPageHistoryService
    {
        ValueTask Back();
        ValueTask Forward();
        ValueTask Go(int index);
        ValueTask<int> Length();
        ValueTask PushState<T>(T data, string page, string url);
        ValueTask ReplaceState<T>(T data, string page, string url);
        ValueTask<T> State<T>();
    }
}
