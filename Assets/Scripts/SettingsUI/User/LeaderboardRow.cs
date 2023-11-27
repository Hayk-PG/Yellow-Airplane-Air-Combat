using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Pautik;

public class LeaderboardRow : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _value;

    [Header("Image")]
    [SerializeField] private Image _outline;
    [SerializeField] private Image _selfIndicator;

    [Header("Color")]
    [SerializeField] private Color[] _colors;




    public void Display(string rank, string name, string value, bool isUser = false)
    {
        _rank.text = rank;        
        _name.text = name;
        _value.text = value;

        Conditions<bool>.Compare(isUser, delegate 
        {
            _selfIndicator.gameObject.SetActive(true);
            _outline.color = _colors[1];
        }, 
        delegate 
        {
            _selfIndicator.gameObject.SetActive(false);
            _outline.color = _colors[0];
        });
    }
}