using UnityEngine;

[CreateAssetMenu(fileName = "MageStats", menuName = "RPG/Classes/MageStats")]
public class MageStats : ClassStatsBase
{
    [Header("Mage Core Stat")]
    public float arcana = 1f;

    [Header("Mage Abilities")]
    public float projectileDamage = 25f;
    public float fireRate = 1f; // shots per second

    public override float GetCoreStat()
    {
        return arcana;
    }
}
