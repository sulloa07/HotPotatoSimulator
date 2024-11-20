using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/*
PlayerController controls stuff like whether you have the potato, what
way to look, etc.
Author: Quincy Kapsner
*/

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    // Start is called before the first frame update
    void Start() {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update() {
        // WASD movement
        // calculate movement velocity as a 3D vector
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 moveHorizontal = transform.right * xMove; 
        Vector3 moveVertical = transform.forward * zMove; 
        // final movement vector
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
        // apply movement
        motor.Move(velocity);

        // mouse movement (direction player is looking affects where they move)
        // calculate rotation as a 3D vector 
        float yRot = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
        // apply rotation
        motor.Rotate(rotation);

        // camera rotation (up and down)
        // calculate camera rotation as a 3D vector
        float xRot = Input.GetAxis("Mouse Y");
        Vector3 cameraRotation = new Vector3(-xRot, 0f, 0f) * lookSensitivity;
        // apply camera rotation
        motor.RotateCamera(cameraRotation);
    }
}
