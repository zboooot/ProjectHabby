using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1); //This stops diagonal movement from being faster
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.deltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);

       // CameraShake.Instance.ShakeCamera(5f, .1f);// This causes screenshake, just uncomment it

    }

}
  