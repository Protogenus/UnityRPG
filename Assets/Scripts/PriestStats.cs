using UnityEngine;

[CreateAssetMenu(fileName = "PriestStats", menuName = "RPG/Classes/PriestStats")]
public class PriestStats : ClassStatsBase
{
    [Header("Priest Core Stat")]
    public float grace = 1f;

    [Header("Priest Abilities")]
    public float healPerSecond = 3f;
    public float auraRadius = 5f;

    public override float GetCoreStat()
    {
        return grace;
    }
}
