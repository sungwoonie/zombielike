using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float movement_speed;
    public bool can_move;

    private Vector3 move_direction;

    private Animator animator;
    private SpriteRenderer sprite_renderer;

    private Weapon_Controller weapon_controller;

    #region "Unity"

    private void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        weapon_controller = GetComponentInChildren<Weapon_Controller>();
    }

    #endregion

    #region "Initialize"

    private void Initiaize_Components()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        weapon_controller = GetComponentInChildren<Weapon_Controller>();
    }

    #endregion

    #region "Outside call Methoed"

    public void Start_Move()
    {
        if (!can_move){return;}

        StartCoroutine(Move());

        animator.SetBool("Run", true);
    }

    public void Stop_Move()
    {
        move_direction = Vector3.zero;
        StopAllCoroutines();

        animator.SetBool("Run", false);
    }

    public void Set_Move_Direction(Vector3 direction)
    {
        move_direction = direction;
    }

    #endregion

    #region "Move Methoed"

    private IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(move_direction * Time.deltaTime * movement_speed);

            if (!weapon_controller.is_shooting)
            {
                weapon_controller.Set_Weapon_Rotation(true, move_direction);
            }

            if (move_direction.x < 0)
            {
                sprite_renderer.flipX = true;
            }
            else
            {
                sprite_renderer.flipX = false;
            }

            yield return null;
        }
    }

    #endregion
}
