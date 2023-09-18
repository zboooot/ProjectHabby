using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player")]
public class PlayerStatScriptableObject : ScriptableObject
{
    public int maxhealth = 100;
    public int health = 100;
    public float speed = 1f;

    public float attackDamage = 10f;
    public float attackRange = 5f;
    public float attackSpeed = 0.8f;

    private void OnEnable()
    {
        health = maxhealth;
    }
}
