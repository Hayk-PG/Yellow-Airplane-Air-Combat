using PlayFab;

public abstract class BasePlayfabHandler<T, T1>
{
    protected T _request;
    protected object[] _data = new object[10];
    protected const string _leaderboardName = "Leaderboard";




    protected abstract void OnSucceed(T1 result);
    protected abstract void OnFailed(PlayFabError error);
}