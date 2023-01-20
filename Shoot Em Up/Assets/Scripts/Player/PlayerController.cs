using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 50;
    private int currentHealth;
    private int delay = 0;
    private bool isAlive = true;
    [SerializeField] private GameObject [] cannons;
    [SerializeField] private GameObject projectile;

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

        if (Input.GetKey(KeyCode.Space) && delay>50)
            Shoot();
        delay++;
    }


    public void GetDamage(int dmg)
    {
        SetCurrentHealth(dmg);        
        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    private void Shoot()
    {
        delay = 0;
        for (int i = 0; i < cannons.Length; i++)
            Instantiate(projectile, cannons[i].transform.position,Quaternion.Euler(0f,0f,90f));
    }
}
