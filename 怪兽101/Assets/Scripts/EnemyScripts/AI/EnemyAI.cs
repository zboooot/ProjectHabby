using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private List<SteeringBehaviour> steeringBehaviours;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f, aiUpdateDelay = 0.06f, attackDelay = 1f;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextSolver movementDirectionSolver;

    [SerializeField]
    private float attackDistance = 1f;

    public float moveSpeed = 2f;

    private Rigidbody2D rb2d;

    //This will check if we are following the player or not
    bool following = false; 

    private void Start()
    {
        //Detecting Player & Obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void PerformDetection()
    {
        //Detecting each detector inside the detectors 
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }

    }

    private void Update()
    {
        //Enemy AI movement based on Target availability 
        if (aiData.currentTarget != null)
        {
            if (following == false)
            {
                following = true;
                StartCoroutine(SearchAndDestroy());
            }
        }
        else if(aiData.GetTargetsCount() >0)
        {
            //Target Acquisition Logic
            aiData.currentTarget = aiData.targets[0];
        }
        //Moving the agent
        SearchAndDestroy();
    }

    private IEnumerator SearchAndDestroy()
    {
        if(aiData.currentTarget == null)
        {
            //Stop logic
            Debug.Log("stopping");
            movementInput = Vector2.zero;
            following = false;
            yield return null;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);
            
            if (distance < attackDistance)
            {
                //AttackLogic
                Debug.Log("inAttackRange");
                movementInput = Vector2.zero;

                //Attack();
                yield return new WaitForSeconds(attackDelay);
                StartCoroutine(SearchAndDestroy());
            }
            else 
            {
                //ChaseLogic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviours, aiData);
                rb2d.velocity = new Vector2(movementInput.x * moveSpeed, movementInput.y * moveSpeed);
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(SearchAndDestroy());
            }
        }
    }

    
}
