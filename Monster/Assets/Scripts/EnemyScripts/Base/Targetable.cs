using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public enum EnemyType {  Building, Tank, BigBuilding, Car, Civilian, Soldier, Tree, }
    public EnemyType enemyType;

    private NewEnemyScript tankEnemy;

    private BigBuildingEnemy bigBEnemy;


    public PlayerHandler player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        CallScript(enemyType);
    }

    private void CallScript(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Tank:
                tankEnemy = GetComponent<NewEnemyScript>();
                break;

            case EnemyType.BigBuilding:
                bigBEnemy = GetComponent<BigBuildingEnemy>();
                break;
        }
    }

    public void TakeDamage()
    {
        switch (enemyType)
        {
            case EnemyType.Building:
                break;

            case EnemyType.Tank:
                tankEnemy.TakeDamage(player.playerData.attackDamage);
                break;

            case EnemyType.BigBuilding:
                bigBEnemy.TakeDamage(player.playerData.attackDamage);
                break;
        }
    }

}
