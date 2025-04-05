using UnityEngine;

public class Arrow : MonoBehaviour
{
void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = collision.transform;
        Destroy(this); // Optional: remove script after hitting
    }
}