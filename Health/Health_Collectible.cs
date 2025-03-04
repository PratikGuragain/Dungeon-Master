using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Collectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Sound_Manager.instance.PlaySound(pickupSound);
            collision.GetComponent<Player_Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
