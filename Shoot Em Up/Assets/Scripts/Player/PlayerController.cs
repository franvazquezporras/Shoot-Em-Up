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

    [Header("Audios Clips")]
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip noAmmo;
    [SerializeField] AudioClip reaload;
    private AudioSource audioSource;    

    Rigidbody2D rb2d;
    [SerializeField] private float speed;


    //GETTERS Y SETTERS DEL JUGADOR
    public void SetCurrentHealth(int health) { currentHealth = health; }
    public void SetMaxHealth(int health) { maxHealth = health; } 
    public void SetCurrentAmmo(int currentAmmo) { ammo = currentAmmo; }
    public void SetTotalAmmo(int tAmmo) { totalAmmo = tAmmo; }
    public int GetCurrentHealth() { return currentHealth; }
    public int GetMaxHealth() { return maxHealth; }

    public int GetTotalAmmo() { return totalAmmo; }
    public int GetCurrentAmmo() { return ammo; }



    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Asigna las referencias y comprueba la vida maxima del jugador                                                     */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        if (gameObject.name == ("Player(Clone)"))        
            currentHealth = maxHealth;
  
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
    }


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Controla el movimiento del jugador a traves de los Inputs y el disparo/recarga del mismo                          */
    /*********************************************************************************************************************************/
    void Update()
    {
        rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        rb2d.AddForce(new Vector2(0,Input.GetAxis("Vertical") * speed));

        if (Input.GetKey(KeyCode.Space) && delay>50 && Time.timeScale !=0 && !reloading) 
            Shoot();
        if (Input.GetKey(KeyCode.R) && !reloading)
        {
            reloading = true;
            Reload();            
        }
            
        delay++;
    }



    /*********************************************************************************************************************************/
    /*Funcion: PickAmmo                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:ammobox(Cantidad de municion obtenida)                                                                   */
    /*Descripción: Controla la municion que recibe el jugador de una caja y el limite maximo de municiones stackeable                */
    /*********************************************************************************************************************************/
    public void PickAmmo(int ammobox)
    {
        if (totalAmmo + ammobox >= 360)
            totalAmmo = 360;
        else
            totalAmmo += ammobox;
    }



    /*********************************************************************************************************************************/
    /*Funcion: GetDamage                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:dmg(daño recibido)                                                                                       */
    /*Descripción: Controla el daño que recibe el jugador y cuando este llega a 0 de vida los destruye                               */
    /*********************************************************************************************************************************/
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



    /*********************************************************************************************************************************/
    /*Funcion: Shoot                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla el disparo del jugador y el numero de balas en el cargador                                               */
    /*********************************************************************************************************************************/
    private void Shoot()
    {
        if (ammo > 0)
        {
            delay = 0;
            for (int i = 0; i < cannons.Length; i++)
                Instantiate(projectile, cannons[i].transform.position, Quaternion.Euler(0f, 0f, 90f));
            audioSource.clip = shoot;
            ammo--;
        }else if(ammo<=0 || (ammo <= 0 && totalAmmo <= 0))
        {
            audioSource.clip = noAmmo;

        }        
        audioSource.Play();
    }


    /*********************************************************************************************************************************/
    /*Funcion: Reload                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla la recarga del jugador, mientras  tenga municiones                                                       */
    /*********************************************************************************************************************************/
    private void Reload()
    {
        int aux = 30 - ammo;

        if (ammo == 30)
        {
            Debug.Log("RELOADED");
            reloading = false;
        }            
        else
        {
            if (totalAmmo >= 30 || ammo + totalAmmo > 30)
            {
                ammo = 30;
                totalAmmo -= aux;

            }
            else if (totalAmmo > 0)
            {
                ammo += totalAmmo;
                totalAmmo = 0;
            }
            audioSource.clip = reaload;
            audioSource.Play();
            StartCoroutine(ReloadCoolDown());
        }
        
    }




    /*********************************************************************************************************************************/
    /*Funcion: OnDestroy                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla que antes de ser destruida la nave setee los parametros del playerpreb ya sea para reiniciar o guardar   */
    /*              los ultimos valores del jugador                                                                                  */
    /*********************************************************************************************************************************/
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



    /*********************************************************************************************************************************/
    /*Funcion: ReloadCoolDown                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla el tiempo para volver a recargar                                                                         */
    /*********************************************************************************************************************************/
    IEnumerator ReloadCoolDown()
    {
        yield return new WaitForSeconds(2f);
        reloading = false;
    }


    /*********************************************************************************************************************************/
    /*Funcion: Hit                                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla el parpadeo de la nave al recibir daño                                                                   */
    /*********************************************************************************************************************************/
    IEnumerator Hit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }
}
