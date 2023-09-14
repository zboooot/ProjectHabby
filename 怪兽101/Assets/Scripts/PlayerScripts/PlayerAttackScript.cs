using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public PlayerAttackScriptableObject playerSO;
    IsometricCharacterRenderer isoRenderer;
    //public float movementSpeed = 1f;d
    Rigidbody2D rbody;
    public float rayCastLength;
    private Vector2 faceDir;


    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    private void Attack()
    {
        Vector2 inputVector = isoRenderer.faceDirection;
        inputVector = Vector2.ClampMagnitude(inputVector, 1); //This stops diagonal movement from being faster
        isoRenderer.SetDirection(inputVector);
        Debug.Log("Attacking");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rayCastLength = playerSO.attackRange;
        faceDir = isoRenderer.lastFaceDir;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, faceDir, rayCastLength);
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.tag);
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }

        Debug.Log(faceDir);

        Debug.DrawRay(transform.position, faceDir * rayCastLength, Color.red);
    }
}
