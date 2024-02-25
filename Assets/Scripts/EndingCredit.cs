using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Menu",60f);
    }
    void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    
}
