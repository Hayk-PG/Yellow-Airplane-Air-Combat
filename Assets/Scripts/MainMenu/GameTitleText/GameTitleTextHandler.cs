using UnityEngine;
using TMPro;
using Pautik;

public class GameTitleTextHandler : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text _title;




    private void Start()
    {
        _title.text = GlobalFunctions.TextWithColorCode("#FFA200", GlobalFunctions.TextWithFontSize(72, "yellow ")) +
                      GlobalFunctions.WhiteColorText(GlobalFunctions.TextWithFontSize(72, "airplane:\n" + GlobalFunctions.TextWithFontSize(52, "air combat!")));
    }
}
