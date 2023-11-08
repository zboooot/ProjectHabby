using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public enum EnemyType {  Building, Tank, BigBuilding, Car, Civilian, Soldier, Tree, PowerPlant, FoodBuilding, }
    public EnemyType enemyType;

    private NewEnemyScript tankEnemy;

    private BigBuildingEnemy bigBEnemy;

    private PowerPlant powerPlant;

    private FoodBuilding foodBuilding;

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

            case EnemyType.PowerPlant:
               powerPlant = GetComponent<PowerPlant>();
                break;

            case EnemyType.FoodBuilding:
                foodBuilding = GetComponent<FoodBuilding>();
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

            case EnemyType.PowerPlant:
                powerPlant.TakeDamage(player.playerData.attackDamage);
                break;

            case EnemyType.FoodBuilding:
                //foodBuilding.TakeDamage(player.playerData.attackDamage);
                break;
        }
    }

}
