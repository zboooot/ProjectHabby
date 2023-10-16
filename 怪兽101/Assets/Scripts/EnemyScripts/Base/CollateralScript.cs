using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollateralScript : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    private Targetable targetType;

    private GameObject entity;

    // Start is called before the first frame update
    void Start()
    {
        targetType = GetComponent<Targetable>();
        entity = this.gameObject;
    }

    private void Update()
    {
        switch (targetType.enemyType)
        {
            case Targetable.EnemyType.BigBuilding:
                BigBuildingEnemy buildingStats = entity.GetComponent<BigBuildingEnemy>();
                buildingStats.Death();
                break;
        }
    }
}
