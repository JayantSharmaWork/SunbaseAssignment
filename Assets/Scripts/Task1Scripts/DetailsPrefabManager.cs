using UnityEngine;
using TMPro;

public class DetailsPrefabManager : MonoBehaviour
{
    public enum DetailType
    {
        NAME,
        POINTS,
        ADDRESS
    }

    public DetailType detailType;
    public TextMeshProUGUI detailKeyText;
    public TextMeshProUGUI detailValueText;

    private void Start()
    {
        switch(detailType)
        {
            case DetailType.NAME:
                {
                    detailKeyText.text = "Name";
                    break;
                }
            case DetailType.POINTS:
                {
                    detailKeyText.text = "Points";
                    break;
                }
            case DetailType.ADDRESS:
                {
                    detailKeyText.text = "Address";
                    break;
                }
        }
    }

    public void SetValueTextDetails(string detailValueData)
    {
        detailValueText.text = detailValueData;
    }
}
