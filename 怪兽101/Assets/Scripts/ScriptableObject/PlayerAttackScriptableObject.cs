using UnityEngine;


[CreateAssetMenu(fileName = "PlayerAttackScriptableObject", menuName = "ScriptableObject/PlayerAttack")]
public class PlayerAttackScriptableObject : ScriptableObject
{
    public float attackDamage = 10f;
    public float attackRange = 5f;
    public float attackSpeed = 0.8f;
}
