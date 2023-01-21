using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject explotion;

    [SerializeField] private float xSpeed = 3;
    [SerializeField] private float ySpeed;
    public int score;
    
    [SerializeField] private bool canShoot;
    [SerializeField] private float fireRate;
    [SerializeField] private float health;

    private ScoreControl scoreControl;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scoreControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
     
        if (canShoot)
        {
            fireRate = fireRate + (Random.Range(fireRate / -2, fireRate / 2));
            InvokeRepeating("Shoot", fireRate, fireRate);
        }
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
        if (health <= 0)
            Die();
    }
    private void Die()
    {
        Instantiate(explotion, transform.position, Quaternion.identity);
        scoreControl.SetPlayerScore(score);
        Destroy(gameObject);
    }
    private void Shoot()
    {
        GameObject temp = (GameObject)Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, 90f));
        temp.GetComponent<Projectile>().SetDirection();
    }
}
