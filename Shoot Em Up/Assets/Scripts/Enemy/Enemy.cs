using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Variables
    public Rigidbody2D rb2d;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject explotion;


    [SerializeField] private float xSpeed = 3;
    [SerializeField] private float ySpeed;
    public int score;
    
    [SerializeField] private bool canShoot;
    [SerializeField] private float fireRate;
    [SerializeField] private float health;    
    [SerializeField] private GameObject[] cannons;
    private int delay;

    [Header("Drop")]
    [SerializeField] private GameObject ammoBox;

    [Header("Boss")]
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject[] bombCannons;
    [SerializeField] private bool isBoss;
    private bool bossDirection;
    [SerializeField] private Slider bossBar;
    private int countShoot;

    private AudioSource audioSource;
    private ScoreControl scoreControl;



    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Referencias de parametros del objeto                                                                              */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scoreControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreControl>();
        if(canShoot)
            audioSource = GetComponent<AudioSource>();
        if (isBoss)
            bossBar.maxValue = health;
    }




    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Si el enemigo puede disparar invocara de forma repetida cada varios segundos un proyectil                         */
    /*********************************************************************************************************************************/
    void Start()
    {
     
        if (canShoot)
        {
            fireRate = fireRate + (Random.Range(fireRate / -2, fireRate / 2));
            InvokeRepeating("Shoot", fireRate, fireRate);
        }
    }

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla el desplazamiento del enemigo en el eje x y su velocidad, si es boss controla tambien el eje y           */
    /*********************************************************************************************************************************/
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

        delay++;
    }



    /*********************************************************************************************************************************/
    /*Funcion: OnCollisionEnter2D                                                                                                    */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: collision (collider del objeto con el que colisiona)                                                    */
    /*Descripción: Controla el desplazamiento del enemigo en el eje x y su velocidad, si es boss controla tambien el eje y           */
    /*********************************************************************************************************************************/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == Layers.PLAYER)
        {
            collision.gameObject.GetComponent<PlayerController>().GetDamage(-10);
            GetDamage(10);
        }
            
    }



    /*********************************************************************************************************************************/
    /*Funcion: GetDamage                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: dmg (daño recibido)                                                                                     */
    /*Descripción: Controla el daño recibido en el enemigo                                                                           */
    /*********************************************************************************************************************************/
    public void GetDamage(int dmg)
    {
        health-=dmg;
        if (health <= 0)
            Die();
    }



    /*********************************************************************************************************************************/
    /*Funcion: Die                                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Destruye el enemigo cuando llega a 0 de vida e invoca las particulas y drop y por ultimo suma los puntos          */
    /*********************************************************************************************************************************/
    private void Die()
    {
        
        Instantiate(explotion, transform.position, Quaternion.identity);
        Drop();
        scoreControl.SetPlayerScore(score);
        Destroy(gameObject);
    }



    /*********************************************************************************************************************************/
    /*Funcion: Drop                                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Dropea una caja de municiones de forma aleatoria  (50% de posibilidades)                                          */
    /*********************************************************************************************************************************/
    private void Drop()
    {
        if(Random.Range(0,100)<50)
            Instantiate(ammoBox, transform.position, Quaternion.identity);
    }




    /*********************************************************************************************************************************/
    /*Funcion: Shoot                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Invoca por cada cañon del enemigo un proyectil, si es el boss modifica cada varios frames su velocidad en y       */
    /*              de forma aleatoria dentro de un rango                                                                            */
    /*********************************************************************************************************************************/
    private void Shoot()
    {
        for (int i = 0; i < cannons.Length; i++)
        {
            GameObject temp = (GameObject)Instantiate(bullet, cannons[i].transform.position, Quaternion.Euler(0f, 0f, 90f));
            temp.GetComponent<Projectile>().SetDirection();
        }
        countShoot++;
        if (countShoot >= 10)
        {
            for (int i = 0; i < bombCannons.Length; i++)
            {
                GameObject temp = (GameObject)Instantiate(bomb, bombCannons[i].transform.position, Quaternion.Euler(0f, 0f, 90f));
                temp.GetComponent<Projectile>().SetDirection();
            }
            countShoot = 0;
        }
        

        if (isBoss)
        {
            if (delay > 500)
            {
                if (transform.position.y >= 0)
                    ySpeed = Random.Range(-1, -0.1f);
                else
                    ySpeed = Random.Range(0.1f, 1);
                delay = 0;
            }   
        }
        audioSource.Play();
    }
}
