using UnityEngine;

[CreateAssetMenu(fileName = "WarriorStats", menuName = "RPG/Classes/WarriorStats")]
public class WarriorStats : ClassStatsBase
{
    [Header("Warrior Core Stat")]
    public float fortitude = 1f;

    [Header("Warrior Abilities")]
    public float meleeDamage = 30f;
    public float tauntRadius = 4f;

    public override float GetCoreStat()
    {
        return fortitude;
    }
}
