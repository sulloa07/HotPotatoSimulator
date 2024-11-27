using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoThrower : MonoBehaviour
{
    public GameObject potatoPrefab;  
    public Transform throwPoint;    // The position where the potato will spawn
    public float throwForce = 10f;  
    private GameObject currentPotato;

    void Update()
    {
        // Check for input to throw the potato
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentPotato == null) // left shift to throw
        {
            ThrowPotato();
        }
    }

    void ThrowPotato()
    {
        // Instantiate a potato at the throw point
        currentPotato = Instantiate(potatoPrefab, throwPoint.position, throwPoint.rotation);

        // Add force to the potato
        Rigidbody rb = currentPotato.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 throwDirection = transform.forward; // Adjust for aiming
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }
}
