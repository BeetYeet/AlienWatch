using UnityEngine;

[CreateAssetMenu(menuName = "Items/Granade", fileName = "new Granade")]
[System.Serializable]
public class GranadeObject : Item
{
    
    [Header("Floats")]
    [Space]
    public float TimeToExplode;
    public float SpeedOfGranade;
}
