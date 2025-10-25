using UnityEngine;

[CreateAssetMenu(fileName = "PaladinStats", menuName = "RPG/Classes/PaladinStats")]
public class PaladinStats : ClassStatsBase
{
    [Header("Paladin Core Stat")]
    public float divinity = 1f;

    [Header("Paladin Abilities")]
    public float meleeDamage = 20f;
    public float healAmount = 5f;

    public override float GetCoreStat()
    {
        return divinity;
    }
}
