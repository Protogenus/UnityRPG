using UnityEngine;

[System.Serializable]
public struct AbilityStats
{
    [Tooltip("Name of the ability.")]
    public string abilityName;

    [Tooltip("Base damage before scaling with core stat.")]
    public float baseDamage;

    [Tooltip("Cooldown in seconds between uses.")]
    public float cooldown;
}

public abstract class ClassStatsBase : ScriptableObject
{
    [Header("General Stats")]
    [Tooltip("The type of this class.")]
    public ClassType classType;

    [Tooltip("Base health of this class.")]
    public float baseMaxHealth = 100f;

    [Tooltip("Base movement speed of this class.")]
    public float baseMoveSpeed = 5f;

    [Tooltip("Each class has a core stat used for scaling abilities (Mage->Arcana, Paladin->Divinity, Priest->Grace, Warrior->Fortitude).")]
    public abstract float GetCoreStat();

    [Header("Base Combat Stats")]
    [Tooltip("Base projectile damage for attacks.")]
    public float baseProjectileDamage = 25f;

    [Tooltip("Base attack rate (shots per second).")]
    public float baseFireRate = 1f;

    [Header("Abilities")]
    [Tooltip("Array of abilities this class has.")]
    public AbilityStats[] abilities;

    /// <summary>
    /// Returns the scaled damage of an ability using the core stat.
    /// </summary>
    public float GetScaledDamage(AbilityStats ability)
    {
        return ability.baseDamage * GetCoreStat();
    }
}
