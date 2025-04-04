using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void Start() {
        Destroy(gameObject, 8);
    }

    //Destroys the arrow when it hits a object
    private void OnTriggerEnter(Collider other) {
        Destroy(transform.GetComponent<Rigidbody>());
    }
}