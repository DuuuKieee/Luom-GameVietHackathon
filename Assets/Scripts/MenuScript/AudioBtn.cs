using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBtn : MonoBehaviour
{
    public GameObject slider;
    public bool isChange = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ShowSlider()
    {
        slider.SetActive(isChange);
        isChange = !isChange;
    }
}
