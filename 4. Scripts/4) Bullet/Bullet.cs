using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon bullet_weapon;

    private Animator animator;
    private SpriteRenderer sprite_renderer;

    private Sprite bullet_sprite;
    private Transform shot_position;

    #region "Unity"

    private void Awake()
    {
        Initialize_Components();
    }

    #endregion

    #region "Initialize Methoed"

    private void Initialize_Components()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        bullet_sprite = sprite_renderer.sprite;
        shot_position = transform.parent;
    }

    #endregion

    #region "Shot Methoeds"

    public void Shot()
    {
        StopAllCoroutines();
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            transform.Translate(Vector2.right * Time.deltaTime * bullet_weapon.fire_speed);
            yield return null;
        }
    }

    #endregion

    #region "Hit Methoeds"

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        animator.SetTrigger("Hit");
    }

    public void Hit()
    {
        gameObject.SetActive(false);

        sprite_renderer.sprite = bullet_sprite;
        transform.parent = shot_position;
    }

    #endregion
}
