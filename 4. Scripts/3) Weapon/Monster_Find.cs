using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Find : MonoBehaviour
{
    public Transform Target()
    {
        Transform result = null;
        GameObject[] activating_monsters = GameObject.FindGameObjectsWithTag("Monster");
        List<GameObject> ray_hitted_monsters = new List<GameObject>();

        foreach (GameObject monster in activating_monsters)
        {
            int except_layer = ((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Bullet")));
            except_layer = ~except_layer;

            RaycastHit2D ray_hit = Physics2D.Raycast(transform.position, monster.transform.position - transform.position, 1000, except_layer);

            if (ray_hit.collider.tag == "Monster")
            {
                ray_hitted_monsters.Add(ray_hit.collider.gameObject);
            }
        }

        if (ray_hitted_monsters.Count > 0)
        {
            result = ray_hitted_monsters[0].transform;

            float distance_with_monster = Vector2.Distance(transform.position, result.transform.position);

            foreach (GameObject monster in ray_hitted_monsters)
            {
                float distance = Vector2.Distance(transform.position, monster.transform.position);

                if (distance < distance_with_monster)
                {
                    result = monster.transform;
                    distance_with_monster = distance;
                }
            }
        }

        return result;
    }
}