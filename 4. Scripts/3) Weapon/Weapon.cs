using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public float damage;
    public float ammo;
    public float shot_delay;
    public float fire_speed;

    public Transform bullet;
    public Sprite weapon_sprite;
}
