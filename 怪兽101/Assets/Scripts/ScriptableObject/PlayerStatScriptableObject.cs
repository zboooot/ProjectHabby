using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player")]
public class PlayerStatScriptableObject : ScriptableObject
{
    public int health = 100;
    public float speed = 1f;
    public PlayerAttackScriptableObject playerAttackSO;
}
