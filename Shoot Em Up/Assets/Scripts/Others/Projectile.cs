using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int direction = 1;
    private Rigidbody2D rb2d;
    [SerializeField] private GameObject explotion;
    [SerializeField] private int speed;
    [SerializeField] private bool isBomb;

    private void Awake()
    {        
        rb2d = GetComponent<Rigidbody2D>();
    
    }
    
    public void SetDirection(){direction *= -1;}

    private void Update()
    {
        rb2d.velocity = new Vector2(speed*direction,0);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (direction == 1)
        {
            if (collision.gameObject.layer == Layers.ENEMY)
            {
                collision.GetComponent<Enemy>().GetDamage();
                Instantiate(explotion, transform.position, Quaternion.identity);
                Destroy(gameObject);  
            }   
        }
        else
        {
            if (collision.gameObject.layer == Layers.PLAYER)
            {
                if (isBomb)
                    collision.GetComponent<PlayerController>().GetDamage(-5);
                else
                    collision.GetComponent<PlayerController>().GetDamage(-1);
                Instantiate(explotion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        
    }
}
