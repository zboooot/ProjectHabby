using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player")]
public class PlayerStatScriptableObject : ScriptableObject
{
    public int maxhealth = 100;
    public int health = 100;
    public float speed = 1f;
    public PlayerAttackScriptableObject playerAttackSO;

    private void OnEnable()
    {
        health = maxhealth;
    }
}
