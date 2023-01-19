using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private float xSpeed = 3;
    [SerializeField] private float ySpeed;

    [SerializeField] private float speed;
    [SerializeField] private bool canShoot;
    [SerializeField] private float fireRate;
    [SerializeField] private float health;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(xSpeed*-1, ySpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == Layers.PLAYER)
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage(-25);
            GetDamage();
        }
            
    }

    public void GetDamage()
    {
        health--;
        if (health == 0)
            Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
