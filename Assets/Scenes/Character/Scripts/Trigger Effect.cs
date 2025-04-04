using UnityEngine;

public class TriggerEffect : MonoBehaviour
{
    // ParticleSystem
    [SerializeField] private ParticleSystem ParticleEffect = null;

    private void Update()
    {
        // Play particle effect for specific key inputs
        if (Input.GetKey(KeyCode.Q) || Input.GetKeyDown(KeyCode.Q))
        {
            Effect();
        }
    }

    private void Effect()
    {
        // Check if ParticleEffect is assigned before playing
        if (ParticleEffect != null)
        {
            ParticleEffect.Play();
        }
        else
        {
            Debug.LogWarning("ParticleEffect is not assigned!");
        }
    }
}
