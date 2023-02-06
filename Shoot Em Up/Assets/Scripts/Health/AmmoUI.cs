using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    PlayerController player;
    [SerializeField] private Text ammoText;
    

    private void Update()
    {        
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        else
            UpdateAmmo();
    }

    private void UpdateAmmo()
    {
        
        if (player.GetCurrentAmmo() <= 0 && Input.GetKey(KeyCode.Space))
            StartCoroutine(Hit());
        ammoText.text = "Ammo: " + player.GetCurrentAmmo() + "/" + player.GetTotalAmmo();
    }

    IEnumerator Hit()
    {        
        ammoText.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(1f);
        ammoText.color = new Color(1, 1, 1);        
    }
}
