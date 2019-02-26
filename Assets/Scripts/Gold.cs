
using UnityEngine;
using TMPro;

public class Gold : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int gold;
    private void OnValidate()
    {
        text.text = gold.ToString();
    }
    private void Update()
    {
        text.text = gold.ToString();
    }

    public void AddGold()
    {
        gold += 1000;
    }
    public void RemGold()
    {
        gold -= 1000;
    }
}
