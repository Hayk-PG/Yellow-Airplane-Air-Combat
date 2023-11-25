
public class CrosshairSpriteAssigner : ImageAssigner
{
    protected override void Awake()
    {
        _image.sprite = Crosshair.Loader.Sprites[0];
    }
}