using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    // Player animation
    private Animator animate;

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

    // Weapon handling
    public GameObject bowObject; // Reference to bow object
    [SerializeField] private GameObject aimCamera;
    [SerializeField] private GameObject playerAim;
    private bool isWeaponEquipped = false;

    void Start()
    {
        animate = GetComponent<Animator>();
        if (animate == null) { Debug.LogError("Animator component not found!"); return; }

        // Initialize hashes
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
    }

    void Update()
    {
        HandleMovement();
        HandleActions();
    }

    void HandleMovement()
    {
        bool walk = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
        bool run = Input.GetKey(KeyCode.LeftShift);
        bool walkback = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
        bool walkL = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        bool walkR = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool bowwalk = (walk || walkback || walkL || walkR) && Input.GetMouseButton(1);

        animate.SetBool(WalkingHash, walk);
        animate.SetBool(RunningHash, (walk || walkback || walkL || walkR) && run);
        animate.SetBool(WalkingBackHash, walkback);
        animate.SetBool(WalkingLeftHash, walkL);
        animate.SetBool(WalkingRightHash, walkR);
        animate.SetBool(BowWalkingHash, bowwalk);
        animate.SetBool(JumpingHash, jump);
    }

    void HandleActions()
    {
        bool punch = Input.GetKey("e");
        bool kick = Input.GetKeyDown("r");
        bool aim = Input.GetMouseButton(1);
        bool fire = Input.GetMouseButton(0);
        bool swordequip = Input.GetKeyDown(KeyCode.Alpha1);
        bool swordunequip = Input.GetKey(KeyCode.LeftAlt);
        bool slash1 = Input.GetKeyDown("q");
        bool slash2 = Input.GetKey("q");
        bool slash3 = Input.GetKey("e");
        bool bowequip = Input.GetKeyDown(KeyCode.Alpha2);
        bool bowunequip = Input.GetKey(KeyCode.LeftAlt);

        // Sword equip
        animate.SetBool(SwordEquippingHash, swordequip);
        animate.SetBool(SwordUnequippingHash, swordunequip);

        // Bow equip
        animate.SetBool(EquippingHash, bowequip);
        animate.SetBool(UnequippingHash, bowunequip);

        if (bowequip && !isWeaponEquipped)
        {
            isWeaponEquipped = true;
            aimCamera.SetActive(false);
            playerAim.SetActive(true);
            if (bowObject != null) bowObject.SetActive(true);
        }

        if (bowunequip && isWeaponEquipped)
        {
            isWeaponEquipped = false;
            aimCamera.SetActive(true);
            playerAim.SetActive(false);
            if (bowObject != null) bowObject.SetActive(false);
        }

        // Combat animations
        animate.SetBool(PunchingHash, punch);
        animate.SetBool(KickingHash, kick);
        animate.SetBool(Slashing1Hash, slash1);
        animate.SetBool(Slashing2Hash, slash2);
        animate.SetBool(Slashing3Hash, slash3);

        // Aiming
        animate.SetBool(AimingHash, aim);

        // Firing
        animate.SetBool(FiringHash, fire);
    }
}
