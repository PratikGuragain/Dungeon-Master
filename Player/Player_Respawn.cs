using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Player_Health playerHealth;
    private UI_Manager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Player_Health>();
        uiManager = FindObjectOfType<UI_Manager>();
    }

    public void CheckRespawn()
    {
        if(currentCheckpoint == null)
        {
            uiManager.Gameover();
            return;
        }
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
        Camera.main.GetComponent<Camera_Controller>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            Sound_Manager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
