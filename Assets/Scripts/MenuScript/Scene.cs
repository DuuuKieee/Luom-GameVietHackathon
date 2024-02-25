using UnityEngine;
using UnityEngine.SceneManagement;
 
public class Scene : MenuAnim
{
    public void ButtonMoveScene(int scene)
    {
        if(scene == 1)
        {
        GameObject.FindGameObjectWithTag("BALL").GetComponent<Animator>().SetBool("isExit", true);  
        GameObject.FindGameObjectWithTag("BLACK").GetComponent<Animator>().SetBool("isStart", true);
        Invoke("MoveScene",2);
        }
        else
        {
        Player.instance.BlackSceneEnd();
        Debug.Log("Loi");
        Invoke("MoveScene2",0.7f);
        }
    }
    void MoveScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    void MoveScene2()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    
}