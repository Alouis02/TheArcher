using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    // Player animation
    private Animator animate;

    // Flag to prevent multiple arrows from being shot simultaneously
    private bool canShoot = true;

    // Player Animation Hashes
    private int WalkingHash;
    private int RunningHash;
    private int WalkingBackHash;
    private int WalkingLeftHash;
    private int WalkingRightHash;
    private int JumpingHash;
    private int KickingHash;
    private int PunchingHash;
    private int AimingHash;
    private int FiringHash;
    private int BowWalkingHash;
    private int EquippingHash;
    private int UnequippingHash;
    private int SwordEquippingHash;
    private int SwordUnequippingHash;
    private int Slashing1Hash;
    private int Slashing2Hash;
    private int Slashing3Hash;
    private int FallingHash;
    private int LandingHash;


    // Arrow Object
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;   
    [SerializeField] private float arrowSpeed = 30f;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private GameObject playerAim;

    // Flag to track if the weapon is equipped
    private bool isWeaponEquipped = false;

    void Start()
    {
        // Initialize animator
        animate = GetComponent<Animator>();

        if (animate == null)
        {
            Debug.LogError("Animator component not found!");
            return;
        }

        // Initialize animation hashes
        WalkingHash = Animator.StringToHash("IsWalking");
        RunningHash = Animator.StringToHash("IsRunning");
        WalkingBackHash = Animator.StringToHash("IsWalkingBack");
        WalkingLeftHash = Animator.StringToHash("IsWalkingLeft");
        WalkingRightHash = Animator.StringToHash("IsWalkingRight");
        JumpingHash = Animator.StringToHash("IsJumping");
        KickingHash = Animator.StringToHash("IsKicking");
        PunchingHash = Animator.StringToHash("IsPunching");
        AimingHash = Animator.StringToHash("IsAiming");
        FiringHash = Animator.StringToHash("IsFiring");
        BowWalkingHash = Animator.StringToHash("IsBowWalking");
        EquippingHash = Animator.StringToHash("IsEquipping");
        UnequippingHash = Animator.StringToHash("IsUnequipping");
        SwordEquippingHash = Animator.StringToHash("IsSwordE");
        SwordUnequippingHash = Animator.StringToHash("IsSwordU");
        Slashing1Hash = Animator.StringToHash("IsSlashing1");
        Slashing2Hash = Animator.StringToHash("IsSlashing2");
        Slashing3Hash = Animator.StringToHash("IsSlashing3");
        FallingHash = Animator.StringToHash("IsFalling");
        LandingHash = Animator.StringToHash("IsLanding");
    }

    void Update()
    {
        HandleMovement();
        HandleActions();
        HandleShooting();
    }

    void HandleMovement()
    {
        // Controller Booleans for Movement
        bool walk = (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow));
        bool run = Input.GetKey(KeyCode.LeftShift);
        bool walkback = (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow));
        bool walkL = (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow));
        bool walkR = (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow));
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool bowwalk = (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")) && Input.GetMouseButton(1);

        // Walking
        animate.SetBool(WalkingHash, walk);
        animate.SetBool(RunningHash, (walk || walkback || walkL || walkR) && run);
        animate.SetBool(WalkingBackHash, walkback);
        animate.SetBool(WalkingLeftHash, walkL);
        animate.SetBool(WalkingRightHash, walkR);
        animate.SetBool(BowWalkingHash, bowwalk);

        // Jumping
        if (!animate.GetBool(JumpingHash) && jump)
        {
            animate.SetBool(JumpingHash, true);
        }
        else if (animate.GetBool(JumpingHash) && !jump)
        {
            animate.SetBool(JumpingHash, false);
        }
        
        /*
        // Falling
        if (!animate.GetBool(FallingHash))
        {
            animate.SetBool(FallingHash, true);
            Debug.Log("Player is falling!");
        }
        else if (animate.GetBool(FallingHash))
        {
            animate.SetBool(FallingHash, false);
            Debug.Log("Player landed!");
        }

        // Landing
        if (!animate.GetBool(LandingHash))
        {
            animate.SetBool(LandingHash, true);
            Debug.Log("Player is falling!");
        }
        else if (animate.GetBool(LandingHash))
        {
            animate.SetBool(LandingHash, false);
            Debug.Log("Player landed!");
        }
        */
    }

    void HandleActions()
    {
        bool punch = Input.GetKeyDown("e");
        bool kick = Input.GetKeyDown("r");
        bool aim = Input.GetMouseButton(1);
        bool fire = Input.GetMouseButtonDown(0);
        bool swordequip = Input.GetKeyDown(KeyCode.Alpha1);
        bool swordunequip = Input.GetKey(KeyCode.LeftAlt);
        bool slash1 = Input.GetKeyDown("q");
        bool slash2 = Input.GetKey("q");
        bool slash3 = Input.GetKey("e");
        bool bowequip = Input.GetKeyDown(KeyCode.Alpha2);
        bool bowunequip = Input.GetKey(KeyCode.LeftAlt);

        // Sword Equip
        animate.SetBool(SwordEquippingHash, swordequip);
        animate.SetBool(SwordUnequippingHash, swordunequip);

        // Bow Equip
        animate.SetBool(EquippingHash, bowequip);
        animate.SetBool(UnequippingHash, bowunequip);

        if (bowequip && !isWeaponEquipped)
        {
            isWeaponEquipped = true;
            animate.SetBool(EquippingHash, true);
            aimCamera.SetActive(false);
            playerAim.SetActive(true);
        }

        if (bowunequip && isWeaponEquipped)
        {
            isWeaponEquipped = false;
            animate.SetBool(UnequippingHash, true);
            aimCamera.SetActive(true);
            playerAim.SetActive(false);
        }

        // Punching
        animate.SetBool(PunchingHash, punch);
        if (punch) { 
            Debug.Log("Punch"); 
        }

        // Kicking
        animate.SetBool(KickingHash, kick);
        if (kick) { 
            Debug.Log("Kick"); 
        }

        // Aiming
        animate.SetBool(AimingHash, aim);
        if (aim) { 
            Debug.Log("Aiming with right-click");
        }

        // Firing
        animate.SetBool(FiringHash, fire);
        if (fire) { 
            Debug.Log("Fired with left mouse button."); 
        }

        // Slashing
        animate.SetBool(Slashing1Hash, slash1);
        animate.SetBool(Slashing2Hash, slash2);
        animate.SetBool(Slashing3Hash, slash3);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            animate.SetTrigger("Shoot");
        }
    }

    public void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        arrow.transform.rotation = Quaternion.LookRotation(arrowSpawnPoint.forward);
        arrow.GetComponent<Rigidbody>().linearVelocity = arrowSpawnPoint.forward * arrowSpeed;
        Debug.Log("Shooting arrow");
    }


    private IEnumerator ResetShootFlag()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}