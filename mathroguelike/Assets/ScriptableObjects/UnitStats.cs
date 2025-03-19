using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats", menuName = "Scriptable Objects/UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Health")]
    public int maxHealth;

    [Header("Attack")]
    public int minAttack;
    public int maxAttack;
    public float attackTime;
}
