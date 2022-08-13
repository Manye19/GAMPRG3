using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Health unitHealth;
    [SerializeField] private string enemyName;
    [SerializeField] protected private float speed;
    [SerializeField] private int damage;
    [SerializeField] protected private float distance;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        unitHealth = GetComponent<Health>();
        unitHealth.onDeathEvent.AddListener(Death);
        spriteRend = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnDestroy()
    {
        unitHealth.onDeathEvent.RemoveListener(Death);
    }

    private void Update()
    {
        TurnDir();
    }
    private void FixedUpdate()
    {
        Move();
    }
    protected virtual void Move()
    {
        if (Vector2.Distance(transform.position, PlayerManager.instance.playerTransform.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.instance.playerTransform.position, speed * Time.deltaTime);
        }
    }

    private void TurnDir()
    {
        if (transform.position.x > PlayerManager.instance.playerTransform.position.x)
        {
            spriteRend.flipX = true;
        }
        else
        {
            spriteRend.flipX = false;
        }
    }

    protected virtual void Attack()
    {
        Debug.Log(enemyName + "is Attacking!");
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(PlayerManager.instance.player))
        {
            Debug.Log("Damage: " + damage);
            PlayerManager.instance.playerHealth.ModifyHealth(-damage);
        }
    }
}
