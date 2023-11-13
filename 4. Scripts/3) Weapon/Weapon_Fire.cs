using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Fire : MonoBehaviour
{
    private Weapon_Controller weapon_controller;
    private Animator animator;
    private Object_Pooling[] bullet_pool;

    #region "Unity"

    private void Awake()
    {
        Initialize_Components();
    }

    private void Start()
    {
        Initialize_Bullets();
    }

    #endregion

    #region "Initialize Methoeds"

    private void Initialize_Bullets()
    {
        for (int i = 0; i < bullet_pool.Length; i++)
        {
            bullet_pool[i].Set_New_Prafab_And_Create(weapon_controller.weapons[i].bullet);
        }
    }

    private void Initialize_Components()
    {
        bullet_pool = GetComponentsInChildren<Object_Pooling>();
        animator = GetComponent<Animator>();
        weapon_controller = GetComponentInParent<Weapon_Controller>();
    }

    #endregion

    #region "Bullet Methoeds"

    public void Set_Bullet(Transform new_bullet)
    {
        bullet_pool[0].Remove_Pools();
        bullet_pool[0].Set_New_Prafab_And_Create(new_bullet);
    }

    public void Swap_Bullet()
    {
        Object_Pooling save_pool = bullet_pool[0];
        bullet_pool[0] = bullet_pool[1];
        bullet_pool[1] = save_pool;
    }

    #endregion

    #region "Fire Methoed"

    public void Fire()
    {
        animator.SetTrigger("Fire");

        Bullet new_bullet = bullet_pool[0].Pool().GetComponent<Bullet>();

        new_bullet.transform.position = transform.position;
        new_bullet.transform.eulerAngles = transform.eulerAngles;

        new_bullet.transform.parent = null;

        new_bullet.gameObject.SetActive(true);

        new_bullet.Shot();
    }

    #endregion
}