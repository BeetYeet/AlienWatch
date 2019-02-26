using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] TextMeshProUGUI CostText;
    Item _Sword;
    public Item Sword
    {
        get { return _Sword; }
        set
        {
            _Sword = value;
            if(_Sword == null)
            {
                image.enabled = false;
                text.enabled = false;
                Description.enabled = false;
                CostText.enabled = false;
            }
            else
            {
                CostText.text = _Sword.cost.ToString();
                Description.text = _Sword.Description;
                text.text = _Sword.Name;
                image.sprite = _Sword.sprite;
                CostText.enabled = true;
                Description.enabled = true;
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
        if (Description == null)
            Description = GetComponentInChildren<TextMeshProUGUI>();
        if (CostText == null)
            CostText = GetComponentInChildren<TextMeshProUGUI>();
    }
}
