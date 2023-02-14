using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxSpawner : MonoBehaviour
{
    //Variables
    [Header ("Box Parameters")]
    [SerializeField] private float rate;
    [SerializeField] private GameObject[] ammoBoxs;

    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Invoca de forma repetida cada varios segundos una caja de municiones                                              */
    /*********************************************************************************************************************************/
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    /*********************************************************************************************************************************/
    /*Funcion: SpawnEnemy                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Invoca de forma aleatoria una de las posibles caja de municiones o vida en el area de juego                       */
    /*********************************************************************************************************************************/
    private void SpawnEnemy()
    {
        Instantiate(ammoBoxs[Random.Range(0, ammoBoxs.Length)], new Vector3(Random.Range(-7f, 7f), Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, -90f));
    }
}
