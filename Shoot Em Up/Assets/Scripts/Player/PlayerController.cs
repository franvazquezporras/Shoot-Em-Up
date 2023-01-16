using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 50;
    private int currentHealth;
    private bool isAlive = true;

    Rigidbody2D rb2d;
    private float speed =3;

    public void SetCurrentHealth(int health) { currentHealth += health; }
    public void SetMaxHealth(int health) { maxHealth = health; } 
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }

    private void Awake()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        rb2d.AddForce(new Vector2(0,Input.GetAxis("Vertical") * speed));
    }

}
