using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public bool isRotating = true;
    public Vector3 rotationAngle = new Vector3(0, 1, 0); // Default rotate around Y axis
    public float rotationSpeed = 50f; // Default rotation speed
    
    void Update()
    {
        if(isRotating)
        {
            transform.Rotate(rotationAngle * rotationSpeed * Time.deltaTime);
        }
    }
}