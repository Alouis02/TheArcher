using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float maxForce = 60f;
    public float drawSpeed = 25f;

    [Header("Aiming")]
    public Transform cameraTransform;
    public LayerMask aimLayer;
    public float aimMaxDistance = 100f;

    [Header("References")]
    public Animator bowAnimator;
    public GameObject aimReticle;

    private GameObject currentArrow;
    private float currentDraw;
    private bool isDrawing;
    private bool isAiming;

    void Update()
    {
        HandleAiming();
        HandleDrawing();
    }

    void HandleAiming()
    {
        isAiming = Input.GetMouseButton(1);

        bowAnimator.SetBool("IsAiming", isAiming);
        aimReticle.SetActive(isAiming);

        if (isAiming && currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            currentArrow.GetComponent<Rigidbody>().isKinematic = true;
            currentArrow.transform.SetParent(arrowSpawnPoint);
        }

        if (!isAiming && currentArrow != null && !isDrawing)
        {
            Destroy(currentArrow);
            currentArrow = null;
        }
    }

    void HandleDrawing()
    {
        if (!isAiming) return;

        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            currentDraw = 0f;
            bowAnimator.SetBool("IsDrawing", true);
        }

        if (Input.GetMouseButton(0) && isDrawing)
        {
            currentDraw += drawSpeed * Time.deltaTime;
            currentDraw = Mathf.Clamp(currentDraw, 0, maxForce);
            // You can use currentDraw/maxForce to control draw animation blend trees
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            FireArrow();
        }
    }

    void FireArrow()
    {
        if (currentArrow == null) return;

        currentArrow.transform.SetParent(null);
        Rigidbody rb = currentArrow.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        Vector3 aimDirection = GetAimDirection();
        rb.linearVelocity = aimDirection * currentDraw;

        currentArrow = null;
        isDrawing = false;
        bowAnimator.SetBool("IsDrawing", false);
    }

    Vector3 GetAimDirection()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, aimMaxDistance, aimLayer))
        {
            return (hit.point - arrowSpawnPoint.position).normalized;
        }
        return cameraTransform.forward;
    }
}