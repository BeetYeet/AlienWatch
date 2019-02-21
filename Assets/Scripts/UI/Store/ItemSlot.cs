using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;
    SwordObject _Sword;
    public SwordObject Sword
    {
        get { return _Sword; }
        set
        {
            _Sword = value;
            if(_Sword == null)
            {
                image.enabled = false;
                text.enabled = false;
            }
            else
            {
                text.text = _Sword.Name;
                image.sprite = _Sword.SwordArt;
                text.enabled = true;
                image.enabled = true;
            }
        }
    }
    GranadeObject _Granade;
    public GranadeObject Granade
    {
        get { return _Granade; }
        set
        {
            _Granade = value;
            if(_Granade == null)
            {
                image.enabled = false;
                text.enabled = false;
            }
            else
            {
                text.text = _Granade.Name;
                image.sprite = _Granade.GameArt;
                text.enabled = true;
                image.enabled = true;
            }
        }
    }

    private void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
        if (text == null)
            text = GetComponentInChildren<TextMeshProUGUI>();
    }
}
