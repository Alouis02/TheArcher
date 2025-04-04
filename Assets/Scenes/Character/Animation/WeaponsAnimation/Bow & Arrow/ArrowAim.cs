using UnityEngine;

public class ArrowAim : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Rotate the arrow to align with its velocity
        if (rb.linearVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Stop the arrow and "stick" it to the surface on impact
        rb.isKinematic = true; // Disable further physics simulation
        transform.parent = collision.transform; // Parent the arrow to the hit object
    }
}
