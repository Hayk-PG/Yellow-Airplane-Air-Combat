
public class Crosshair : BaseSpriteReferenceLoader<Crosshair>
{
    protected override void CreateInstance()
    {
        if (Loader == null)
        {
            Loader = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    protected override void InitCallback()
    {
        GameEventHandler.RaiseEvent(GameEventType.CrosshairSpriteInit);
    }
}