using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    //Variables
    private Transform TablaContenedor;
    private Transform entradaRecord;

    private List<EntradaRecord> listaRecords;
    private List<Transform> posicionRecordEnTabla;


    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Obtiene la lista de los 10 primero mejores puntajes                                                               */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        TablaContenedor = transform.Find("RecordsList");
        entradaRecord = TablaContenedor.Find("Entry");
        entradaRecord.gameObject.SetActive(false);

        //listado de prueba
        listaRecords = new List<EntradaRecord>()
       {
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion1")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion2")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion3")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion4")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion5")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion6")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion7")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion8")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion9")},
           new  EntradaRecord{puntuacion = PlayerPrefs.GetInt("Puntuacion10")}

       };
        //genera las entradas de la puntuacion 
        posicionRecordEnTabla = new List<Transform>();
        foreach (EntradaRecord entradaRecord in listaRecords)
        {
            crearEntradaNueva(entradaRecord, TablaContenedor, posicionRecordEnTabla);
        }

    }


    /*********************************************************************************************************************************/
    /*Funcion: crearEntradaNueva                                                                                                     */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: entradarecord (puntaje),posicion(posicion obtenida),posicionEnTabla(posicion donde se visualizara)      */
    /*Descripción: Por cada llamada asigna y escribe en la tabla los valores de los 10 primeros puntajes posicionados uno bajo otro  */
    /*********************************************************************************************************************************/
    private void crearEntradaNueva(EntradaRecord entradarecord, Transform posicion, List<Transform> posicionEnTabla)
    {
        float posicionEntrada = 15f;
        Transform entradaNueva = Instantiate(entradaRecord, posicion);
        RectTransform entradaRT = entradaNueva.GetComponent<RectTransform>();
        entradaRT.anchoredPosition = new Vector2(0, -posicionEntrada * posicionEnTabla.Count);
        entradaNueva.gameObject.SetActive(true);

        int rangopos = posicionEnTabla.Count + 1;
        //Nombres de las posiciones
        string puesto;
        switch (rangopos)
        {
            default: puesto = rangopos + "TH"; break;
            case 1: puesto = "1ST"; break;
            case 2: puesto = "2ND"; break;
            case 3: puesto = "3RD"; break;
        }

        //puntuaciones posicionamiento en tabla
        int puntuacionrandom = entradarecord.puntuacion;
        entradaNueva.Find("Position").GetComponent<Text>().text = puesto;
        entradaNueva.Find("Score").GetComponent<Text>().text = puntuacionrandom.ToString();

        posicionEnTabla.Add(entradaNueva);
    }

    //Clase EntradaRecord la cual tiene el parametro de puntos
    private class EntradaRecord
    {
        public int puntuacion;

    }
}
