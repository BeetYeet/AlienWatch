using UnityEngine;

[CreateAssetMenu(menuName = "Granade", fileName = "new Granade")]
[System.Serializable]
public class GranadeObject : ScriptableObject
{
    [Header("Floats")]
    public float TimeToExplode;
    public float SpeedOfGranade;
}
