using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject[] cannons;

    [SerializeField] private bool isBoss;
    private bool bossDirection;
    [SerializeField] private Slider bossBar;

    private AudioSource audioSource;
    private ScoreControl scoreControl;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scoreControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreControl>();
        if(canShoot)
            audioSource = GetComponent<AudioSource>();
        if (isBoss)
            bossBar.maxValue = health;
    }
    
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

        if (isBoss)
        {
            if (transform.position.x < 0)
                bossDirection = false;
            else if (transform.position.x >= 8)
                bossDirection = true;

            if(bossDirection == false)
                rb2d.velocity = new Vector2(xSpeed * 1, ySpeed);
            else
                rb2d.velocity = new Vector2(xSpeed * -1, ySpeed);

            bossBar.value = health;
        }            
        else
            rb2d.velocity = new Vector2(xSpeed * -1, ySpeed);
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
        for (int i = 0; i < cannons.Length; i++)
        {
            GameObject temp = (GameObject)Instantiate(bullet, cannons[i].transform.position, Quaternion.Euler(0f, 0f, 90f));
            temp.GetComponent<Projectile>().SetDirection();
        }
        
        if(isBoss)
        {
            if (transform.position.y >= 0)
                ySpeed = Random.Range(-1, -0.1f);
            else
                ySpeed = Random.Range(0.1f, 1);            
        }
        audioSource.Play();
    }
}
