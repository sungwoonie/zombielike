using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    public bool is_shooting;
    public Weapon[] weapons;
    public Transform cross_hair;
    public Vector2[] shot_position;

    private SpriteRenderer sprite_renderer;
    private Monster_Find monster_find;
    private Weapon_Magazine magazine;
    private Weapon_Fire weapon_fire;

    private float current_shot_delay;

    #region "Unity"

    private void Awake()
    {
        Initialize_Components();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!is_shooting) 
            {
                Start_Shot();
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Stop_Shot();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Swap_Weapon();
        }
    }

    #endregion

    #region "Initialize"

    private void Initialize_Components()
    {
        magazine = GetComponent<Weapon_Magazine>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        monster_find = GetComponent<Monster_Find>();
        weapon_fire = GetComponentInChildren<Weapon_Fire>();
    }

    #endregion

    #region "Rotation Methoed"

    public void Set_Weapon_Rotation(bool run, Vector3 look_position)
    {
        float look_rotation = Mathf.Atan2(look_position.y, look_position.x) * Mathf.Rad2Deg;

        if (look_rotation < -90.0f || look_rotation > 90.0f)
        {
            sprite_renderer.flipY = true;
            weapon_fire.transform.localPosition = shot_position[0];
        }
        else
        {
            sprite_renderer.flipY = false;
            weapon_fire.transform.localPosition = shot_position[1];
        }

        transform.eulerAngles = new Vector3(0, 0, look_rotation);
    }

    #endregion

    #region "Crosshair Methoed"

    private void Set_Cross_Hair(bool set, Transform target)
    {
        if (set)
        {
            if (!cross_hair.gameObject.activeSelf)
            {
                cross_hair.gameObject.SetActive(true);
            }

            cross_hair.transform.position = target.position;
        }
        else
        {
            cross_hair.gameObject.SetActive(false);
        }
    }

    #endregion

    #region "Shot Methoeds"

    private IEnumerator Shot()
    {
        while (is_shooting)
        {
            Transform target = monster_find.Target();

            if (!target) //if can't find any target, stop shot
            {
                is_shooting = false;
                Set_Cross_Hair(false, null);
                yield break;
            }

            Set_Cross_Hair(target, target);

            Set_Weapon_Rotation(false, target.position - transform.position);

            Check_Shot_Delay();

            yield return null;
        }
    }

    private void Check_Shot_Delay()
    {
        if (current_shot_delay > 0)
        {
            current_shot_delay -= Time.deltaTime;
        }
        else
        {
            current_shot_delay = weapons[0].shot_delay;

            Shot_Bullet();
        }
    }

    private void Shot_Bullet()
    {
        if (magazine.Can_Use_Ammo(weapons[0].ammo))
        {
            magazine.Use_Ammo(weapons[0].ammo);

            weapon_fire.Fire();
        }
    }

    public void Start_Shot()
    {
        current_shot_delay = weapons[0].shot_delay; //initialize shot delay

        is_shooting = true;
        StartCoroutine(Shot());
    }

    public void Stop_Shot()
    {
        is_shooting = false;
        Set_Cross_Hair(false, null);
    }

    #endregion

    #region "Weapon Change Methoeds"

    private void Change_Weapon_Sprite(Sprite new_weapon_sprite)
    {
        sprite_renderer.sprite = new_weapon_sprite;
    }

    public void Change_Weapon(Weapon new_weapon)
    {
        weapons[0] = new_weapon;
        weapon_fire.Set_Bullet(new_weapon.bullet);

        Change_Weapon_Sprite(new_weapon.weapon_sprite);
    }

    public void Swap_Weapon()
    {
        Weapon save_weapon = weapons[0];
        weapons[0] = weapons[1];
        weapons[1] = save_weapon;

        Change_Weapon_Sprite(weapons[0].weapon_sprite);

        weapon_fire.Swap_Bullet();

        if (is_shooting)
        {
            current_shot_delay = weapons[0].shot_delay;
        }
    }

    #endregion
}