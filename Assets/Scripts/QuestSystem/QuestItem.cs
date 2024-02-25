using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestItem : MonoBehaviour
{
    public AudioSource collectsfx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collectsfx.GetComponent<AudioSource>().Play();
            Quest1Manager.instance.ItemCount();
            Destroy(gameObject);
        }

    }
}
