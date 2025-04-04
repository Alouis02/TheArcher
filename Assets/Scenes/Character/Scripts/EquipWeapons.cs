using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject bow;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject bowHolder;
    [SerializeField] GameObject swordHolder;
    [SerializeField] GameObject arrowHolder;
    [SerializeField] GameObject bowSheath;
    [SerializeField] GameObject swordSheath;
    [SerializeField] GameObject arrowSheath;
    
    GameObject currentBow;
    GameObject currentSword;
    GameObject currentArrow;
    GameObject currentBowSheath;
    GameObject currentSwordSheath;
    GameObject currentArrowSheath;

    void Start()
    {
        SheathSword();
        SheathWeapon();
        SheathArrow();
    }

    // Sword
    public void DrawSword()
    {
        if (currentSwordSheath != null)
        {
            Destroy(currentSwordSheath);
            currentSwordSheath = null; // Ensure the reference is cleared
        }

        if (currentSword == null)
        {
            currentSword = Instantiate(sword, swordHolder.transform);
        }
    }

    public void SheathSword()
    {
        if (currentSword != null)
        {
            Destroy(currentSword);
            currentSword = null; // Ensure the reference is cleared
        }

        if (currentSwordSheath == null)
        {
            currentSwordSheath = Instantiate(sword, swordSheath.transform);
        }
    }

    // Bow
    public void DrawWeapon()
    {
        if (currentBowSheath != null)
        {
            Destroy(currentBowSheath);
            currentBowSheath = null; // Ensure the reference is cleared
        }

        if (currentBow == null)
        {
            currentBow = Instantiate(bow, bowHolder.transform);
        }
    }

    public void SheathWeapon()
    {
        if (currentBow != null)
        {
            Destroy(currentBow);
            currentBow = null; // Ensure the reference is cleared
        }

        if (currentBowSheath == null)
        {
            currentBowSheath = Instantiate(bow, bowSheath.transform);
        }
    }

    // Arrow
    public void PullArrow()
    {
        if (currentArrow != null)
        {
            Destroy(currentArrow);
            currentArrow = null; // Ensure the reference is cleared
        }

        if (currentArrow == null) 
        {
            currentArrow = Instantiate(arrow, arrowHolder.transform);
        }
    }
    
    public void SheathArrow()
    {
        if (currentArrow != null)
        {
            Destroy(currentArrow);
            currentArrow = null; // Ensure the reference is cleared
        }

        if (currentArrowSheath == null)
        {
            currentArrowSheath = Instantiate(arrow, arrowSheath.transform);
        }
    }
}