using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombatScript : MonoBehaviour
{
    public UnityEvent<GameObject> OnEnemyDetected;

    [Range(0, 20)]
    public float radius;

    [Header("Gizmo Parameters")]
    public Color gizmoColor = Color.green;
    public bool showGizmos = true;
    Rigidbody2D rb2D;

    public bool EnemyDetected { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //var collider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log(hit.collider);
            }
        }
        else { return; }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
        }
    }
}
