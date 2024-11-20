using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

/*
PlayerMotor just has basic functions to control movement and the like.
Author: Quincy Kapsner
*/

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    [SerializeField]
    private Camera cam;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Run every physics iteration
    void FixedUpdate() {
        PerformMovement();
        PerformRotation();
    }

    // get movement vector
    public void Move(Vector3 _velocity) {
        velocity = _velocity;
    }

    // perform movement based on velocity variable
    void PerformMovement() {
        if (velocity != Vector3.zero) {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    // get rotation vector
    public void Rotate(Vector3 _rotation) {
        rotation = _rotation;
    }

    // get camera rotation vector
    public void RotateCamera(Vector3 _cameraRotation) {
        cameraRotation = _cameraRotation;
    }

    // perform rotation (movement and camera)
    void PerformRotation() {
        // movement
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        // camera
        if (cam != null) {
            cam.transform.Rotate(cameraRotation);
        }
    }

    
}
