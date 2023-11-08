using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointVFX : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        // Calculate the new position with an upward movement
        Vector3 newPosition = currentPosition + Vector3.up * moveSpeed * Time.deltaTime;

        // Apply the new position to the object's transform
        transform.position = newPosition;
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
