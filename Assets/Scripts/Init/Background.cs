
public class Background : BaseSpriteReferenceLoader<Background>
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
        GameEventHandler.RaiseEvent(GameEventType.BackgroundSpritesInit);
    }
}