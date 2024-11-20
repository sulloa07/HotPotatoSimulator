using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

/*
PlayerMotor has basic functions to control movement and the like. 
Doesn't deal with player input or player statuses (like holding the potato).
Author: Quincy Kapsner
*/

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float jumpForce = 0f;

    private bool isGrounded = true; // is the player on the ground?

    private float currentCameraAngle = 0f; // tracks the camera's pitch (up/down rotation)
    [SerializeField]
    private float minCameraAngle = -60f;
    [SerializeField]
    private float maxCameraAngle = 60f;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Run every physics iteration
    void FixedUpdate() {
        PerformMovement();
        PerformRotation();
    }

    // set movement vector
    public void Move(Vector3 _velocity) {
        velocity = _velocity;
    }

    // perform movements 
    void PerformMovement() {
        // wasd movement
        if (velocity != Vector3.zero) {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        // jump
        if (jumpForce > 0f) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpForce = 0f;
            isGrounded = false;
        }
    }

    // set rotation vector
    public void Rotate(Vector3 _rotation) {
        rotation = _rotation;
    }

    // set camera rotation vector
    public void RotateCamera(Vector3 _cameraRotation) {
        currentCameraAngle += _cameraRotation.x;
        currentCameraAngle = Mathf.Clamp(currentCameraAngle, minCameraAngle, maxCameraAngle);
    }

    // perform rotation (movement and camera)
    void PerformRotation() {
        // movement
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        
        // camera
        if (cam != null) {
            cam.transform.localEulerAngles = new Vector3(currentCameraAngle, 0f, 0f);
        }
        
    }

    // set jump vector
    public void Jump(float _jumpForce) {
        if (isGrounded){
            jumpForce = _jumpForce;
        }
    }

    // check if player is on the ground    
    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    // check if player is off the ground
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
