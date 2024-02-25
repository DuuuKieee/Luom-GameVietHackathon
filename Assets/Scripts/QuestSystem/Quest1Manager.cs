using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest1Manager : MonoBehaviour
{
    public int QuestCounter = 0, questCount;

    public Image questItem;
    public Image questItem1;
    public Image questItem2;
    public Image questItem3;
    public Color completeColor, activeColor;
    [SerializeField] public GameObject arrow;
    public bool temp = false;

    [SerializeField] public Text Text;
    public static Quest1Manager instance;
    [SerializeField] public GameObject[] fish;
    public int completeCount = 0;
    public GameObject Dir1, Dir2, Dir3, Dir4, Dir5;
    public GameObject Quest1, Quest2, Quest3, Quest4;
    public GameObject NextRoom1, NextRoom2, NextRoom3;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ItemCount()
    {
        QuestCounter++;
        Text.text = $"Bắt 5 con cá {QuestCounter}/5";
        if (QuestCounter == 5) FinishQuest();
        Debug.Log(QuestCounter);
    }

    public void FinishQuest()
    {
        questItem.color = completeColor;
        GameController.instance.NextRoom(GameController.instance.arrow2);
        NextRoom2.GetComponent<NextRoom>().isFinish1 = true;
        if (completeCount == 1)
        {
            OnQuestClick();
            completeCount++;
        }
    }
    public void FinishQuest1()
    {
        questItem1.color = completeColor;
        if (completeCount == 0)
        {
            OnQuestClick();
            completeCount++;
        }
        Quest3.SetActive(true);

    }
    public void FinishQuest3()
    {

        questItem2.color = completeColor;
        if (completeCount == 2)
        {
            OnQuestClick();
            completeCount++;
        }
        Quest4.SetActive(true);
        NextRoom3.GetComponent<NextRoom>().isFinish2 = true;

    }
    public void StartQuest()
    {
        fish[0].SetActive(true);
        fish[1].SetActive(true);
        fish[2].SetActive(true);
        fish[3].SetActive(true);
        fish[4].SetActive(true);
    }
    public void OnQuestClick()
    {
        questCount++;
        if (questCount == 1)
        {
            arrow.GetComponent<Queslog>().target = Dir1.transform;
        }
        if (questCount == 2)
        {
            arrow.GetComponent<Queslog>().target = Dir2.transform;
        }
        if (questCount == 3)
        {
            arrow.GetComponent<Queslog>().target = Dir3.transform;
        }
        if (questCount == 4)
        {
            arrow.GetComponent<Queslog>().target = Dir4.transform;
        }
        if (questCount == 5)
        {
            arrow.GetComponent<Queslog>().target = Dir5.transform;
        }
        Debug.Log(questCount);


    }

}
