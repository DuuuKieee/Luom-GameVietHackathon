using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class XSlider : MonoBehaviour
{
    [SerializeField] Slider Slider;
    [SerializeField] StaminaManager staminaVar;
    GameObject playerAnim;
    Animator anim , dashAnim;

    public float yPos;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("PlayerAnim");
        anim = GetComponentInChildren<Animator>(true);
        Slider.value = staminaVar.value;
        dashAnim = Slider.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Slider.transform.position = playerAnim.transform.position + new Vector3(1f, yPos, 0);
    }
    void FixedUpdate()
    {
        if (staminaVar.value < staminaVar.maxValue) staminaVar.value+=1;
        if(staminaVar.value == staminaVar.maxValue)
        {
            dashAnim.SetBool("isTired", false);
            
        }
        else if(staminaVar.value <= 5) 
        {
        dashAnim.SetBool("isTired", true);
        }
        Slider.value = staminaVar.value;

    }
}
