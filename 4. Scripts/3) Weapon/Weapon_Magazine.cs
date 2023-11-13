using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Magazine : MonoBehaviour
{
    public float max_magazine;
    public float current_magazine;

    public void Set_Max_Magazine(float magazine)
    {
        max_magazine = magazine;
    }

    public bool Can_Use_Ammo(float ammo)
    {
        return current_magazine - ammo >= 0 ? true : false;
    }

    public void Use_Ammo(float ammo)
    {
        current_magazine -= ammo;
        CancelInvoke();
        Invoke("Charge_To_Full_Magazine", 5.0f);
    }

    public void Charge_To_Full_Magazine()
    {
        current_magazine = max_magazine;
    }

    public void Charge_Magazine(float ammo)
    {
        current_magazine += ammo;

        if (current_magazine > max_magazine)
        {
            current_magazine = max_magazine;
        }
    }
}
