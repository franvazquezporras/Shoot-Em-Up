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
    private int totalAmmo = 100;
    private int ammo = 30;
    private bool reloading;


    private AudioSource audioSource;    

    Rigidbody2D rb2d;
    [SerializeField] private float speed;

    public void SetCurrentHealth(int health) { currentHealth = health; }
    public void SetMaxHealth(int health) { maxHealth = health; } 
    public void SetCurrentAmmo(int currentAmmo) { ammo = currentAmmo; }
    public void SetTotalAmmo(int tAmmo) { totalAmmo = tAmmo; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }

    public int GetTotalAmmo() { return totalAmmo; }
    public int GetCurrentAmmo() { return ammo; }
    private void Awake()
    {
        if (gameObject.name == ("Player(Clone)"))        
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
        if (Input.GetKey(KeyCode.R) && !reloading)
        {
            reloading = true;
            Reload();            
        }
            
        delay++;
    }
    public void PickAmmo(int ammobox)
    {
        if (totalAmmo + ammobox >= 360)
            totalAmmo = 360;
        else
            totalAmmo += ammobox;
    }

    public void GetDamage(int dmg)
    {

        currentHealth += dmg;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if(dmg<0)
            StartCoroutine(Hit());
        if (currentHealth <= 0)
        {
            Instantiate(explotion, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.2f);
        }            
    }

    private void Shoot()
    {
        if (ammo > 0)
        {
            delay = 0;
            for (int i = 0; i < cannons.Length; i++)
                Instantiate(projectile, cannons[i].transform.position, Quaternion.Euler(0f, 0f, 90f));
            audioSource.Play();
            ammo--;
        }else if(ammo<=0 && totalAmmo > 0)
        {
            Debug.Log("RELOAD");
        }
        else
        {
            Debug.Log("Sin municion");
        }        
    }

    private void Reload()
    {
        int aux = 30 - ammo;

        if (totalAmmo >= 30 || ammo + totalAmmo > 30)
        {
            ammo = 30;
            totalAmmo -= aux;
        }
        else if(totalAmmo>0)
        {            
            ammo += totalAmmo;
            totalAmmo = 0;
        }
        else
        {
            Debug.Log("NO AMMO");
        }
        StartCoroutine(ReloadCoolDown());
    }

    private void OnDestroy()
    {
        if (currentHealth > 0)
        {         
            PlayerPrefs.SetInt("CurrentAmmo", ammo);
            PlayerPrefs.SetInt("TotalAmmo", totalAmmo);
            PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        }
        else
        {         
            PlayerPrefs.SetInt("CurrentAmmo", 30);
            PlayerPrefs.SetInt("TotalAmmo", 100);
            PlayerPrefs.SetInt("CurrentHealth", maxHealth);
        }            
    }
    IEnumerator ReloadCoolDown()
    {
        yield return new WaitForSeconds(2f);
        reloading = false;
    }
    IEnumerator Hit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }
}
