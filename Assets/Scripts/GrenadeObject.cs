using UnityEngine;

[CreateAssetMenu(menuName = "Items/Granade", fileName = "new Granade")]
[System.Serializable]
public class GrenadeObject : Item
{
    
    [Header("Floats")]
    [Space]
    public float TimeToExplode;
    public float SpeedOfGranade;
}
