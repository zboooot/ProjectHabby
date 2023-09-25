using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    public Player Player { get; private set; }

    public PlayerInputHandler inputHandler;

    public PlayerStatScriptableObject SO_player;

    public LayerMask enemyLayer;

    public BoxCollider2D collider;

    private void Awake()
    {
        Player = GetComponentInParent<Player>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            inputHandler.CheckAttack(true);
        }

        else inputHandler.CheckAttack(false);
    }
}
