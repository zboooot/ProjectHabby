using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject 
{
    public string enemyName;
    public string description;
    public int health;
    public float speed;
    public float attackDamage;
    public float attackSpeed;
}
