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
    [SerializeField] private GameObject explotion;
    

    private AudioSource audioSource;    

    Rigidbody2D rb2d;
    [SerializeField] private float speed;

    public void SetCurrentHealth(int health) { currentHealth += health; }
    public void SetMaxHealth(int health) { maxHealth = health; } 
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }

    private void Awake()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
    }

   
    void Update()
    {
        rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        rb2d.AddForce(new Vector2(0,Input.GetAxis("Vertical") * speed));

        if (Input.GetKey(KeyCode.Space) && delay>50 && Time.timeScale !=0)
            Shoot();
        delay++;
    }


    public void GetDamage(int dmg)
    {
        SetCurrentHealth(dmg);
        StartCoroutine(Hit());
        if (currentHealth <= 0)
        {
            Instantiate(explotion, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.2f);
        }            
    }

    private void Shoot()
    {
        delay = 0;
        for (int i = 0; i < cannons.Length; i++)
            Instantiate(projectile, cannons[i].transform.position,Quaternion.Euler(0f,0f,90f));
        audioSource.Play();
    }


    IEnumerator Hit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }
}
