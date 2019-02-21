using UnityEngine;

[CreateAssetMenu(menuName = "Items/Granade", fileName = "new Granade")]
[System.Serializable]
public class GranadeObject : ScriptableObject
{
    [Header("Strings")]
    [Space]
    public string Name; 
    public string Description;
    [Header("sprite")]
    [Space]
    public Sprite GameArt;
    [Header("Floats")]
    [Space]
    public float TimeToExplode;
    public float SpeedOfGranade;
}
