using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMover : MonoBehaviour
{

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float yOffset = 1f;


    bool isGrabbingItem = false;
    Rigidbody2D rigidBody;


    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HorizontalMovement();
        VerticalMovement();
        InteractWithEnviroment();
    }

    private void HorizontalMovement() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(controlThrow * movementSpeed , rigidBody.velocity.y);
    }

    private void VerticalMovement() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, controlThrow * movementSpeed);
    }


    private void OnTriggerStay2D(Collider2D collision) {

        if (CrossPlatformInputManager.GetButton("Jump") ) {
        collision.transform.position = new Vector2(transform.position.x,transform.position.y + yOffset);
        }

    }

    private void InteractWithEnviroment() {
        
    }
}
