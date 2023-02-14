using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Variables
    [Header ("Projectile Parameter")]
    private int direction = 1;
    private Rigidbody2D rb2d;
    [SerializeField] private GameObject explotion;
    [SerializeField] private int speed;
    [SerializeField] private bool isBomb;
    private int damage = 1;
    private ScoreControl score;


    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: obtiene las referencias y el daño que hace el proyectil                                                           */
    /*********************************************************************************************************************************/
    private void Awake()
    {        
        rb2d = GetComponent<Rigidbody2D>();
        score = GameObject.Find("GameManager").GetComponent<ScoreControl>();
        if (score.GetPlayerScore() > 10000)
            damage = 3;
        else if (score.GetPlayerScore() > 5000)
            damage = 2;
    }



    /*********************************************************************************************************************************/
    /*Funcion: SetDirection                                                                                                          */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Cambia la direccion de movimiento en el eje X                                                                     */
    /*********************************************************************************************************************************/
    public void SetDirection(){direction *= -1;}


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción:  Controla el movimiento del proyectil                                                                             */
    /*********************************************************************************************************************************/
    private void Update()
    {
        rb2d.velocity = new Vector2(speed*direction,0);        
    }


    /*********************************************************************************************************************************/
    /*Funcion: OnTriggerEnter2D                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada:collision (Collider del objeto que colisiona con el proyectil)                                           */
    /*Descripción:  Segun la direccion controla si es el jugador o el enemigo quien ha disparado, y controla a quien le hace daño    */
    /*              al jugador o al enemigo con el que impacta, y luego se destruye                                                  */
    /*********************************************************************************************************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (direction == 1)
        {
            if (collision.gameObject.layer == Layers.ENEMY)
            {
                collision.GetComponent<Enemy>().GetDamage(damage);
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
