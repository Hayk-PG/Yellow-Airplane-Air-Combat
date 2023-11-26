using UnityEngine;

public class AirplaneRendererColorUpdater : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color[] _colors;

    private object[] _data = new object[2];




    private void Start()
    {
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        _data[0] = transform;
        _data[1] = _colors[Random.Range(0, _colors.Length)]; 
        GameEventHandler.RaiseEvent(GameEventType.UpdateAirplaneColor, _data);
    }
}
