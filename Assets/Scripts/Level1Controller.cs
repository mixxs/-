using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Level1Controller : MonoBehaviour
{
    public List<GameObject> gameobjs;


    private int index;

    public Text tip;

    public GameObject overPanel;        //�������
    public Text aveTime;                //ƽ����Ӧʱ��
    public Text trueF;                  //��ȷ��

    public List<float> timers;      //��Ӧʱ������
    private float allTim;           //����Ŀ
    private float allFalse;         //������Ŀ
    private float fTimer;           //��Ӧʱ��

    private float tTimer;           //��ʱ��
    public Text timerTex;

    float value;

    private bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        tTimer = DataValue.timer;
        Rand(); 
        for (int i = 0; i < gameobjs.Count; i++)
        {
            
            gameobjs[i].gameObject.transform.localScale *= DataValue.arrowScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            return;
        }
        tTimer -= Time.deltaTime;
        timerTex.text = tTimer.ToString("0.00") + "s";
        if (tTimer < 0)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                value += timers[i];
            }
            overPanel.gameObject.SetActive(true);
            aveTime.text = "ƽ����Ӧʱ��" + (value / (timers.Count * 1.0f)).ToString() + "s";
            trueF.text = (((allTim - allFalse) / allTim) * 100).ToString() + "%";

            DataValue.level1Timer = value / (timers.Count * 1.0f);
            DataValue.level1True = (allTim - allFalse) / allTim *100;
            isOver = true;
        }
        fTimer += Time.deltaTime;
        if (index == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                timers.Add(fTimer);
                Rand();
                tip.gameObject.SetActive(true);
                tip.text = "��ȷ";
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            {
                tip.gameObject.SetActive(true);
                tip.text = "����";
                allFalse++;
                Rand();
            }

        }
        else if (index == 1)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                tip.gameObject.SetActive(true);
                timers.Add(fTimer);
                tip.text = "��ȷ";
                Rand(); 
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            {
                tip.gameObject.SetActive(true);
                allFalse++;
                tip.text = "����";
                Rand();
            }
        }
        else if (index == 2)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                tip.gameObject.SetActive(true);
                timers.Add(fTimer);
                tip.text = "��ȷ";
                Rand();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            {
                tip.gameObject.SetActive(true);
                allFalse++;
                tip.text = "����";
                Rand();
            }
        }
        else if (index == 3)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                tip.gameObject.SetActive(true);
                timers.Add(fTimer);
                tip.text = "��ȷ";
                Rand();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            {
                tip.gameObject.SetActive(true);
                allFalse++;
                tip.text = "����";
                Rand();
            }
        }
        else if (index == 4)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                tip.gameObject.SetActive(true);
                timers.Add(fTimer);
                tip.text = "��ȷ";
                Rand();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Q))
            {
                tip.gameObject.SetActive(true);
                allFalse++;
                tip.text = "����";
                Rand();
            }
        }
        else if (index == 5)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                tip.gameObject.SetActive(true);
                timers.Add(fTimer);
                tip.text = "��ȷ";
                Rand();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.E))
            {
                tip.gameObject.SetActive(true);
                allFalse++;
                tip.text = "����";
                Rand();
            }
        }

    }

    private void Rand()
    {
        for (int i = 0; i < gameobjs.Count; i++)
        {
            gameobjs[i].gameObject.SetActive(false);
        }
        Invoke("Init", 1);
    }

    private void Init()
    {
        allTim++;
        for (int i = 0; i < gameobjs.Count; i++)
        {
            gameobjs[i].gameObject.SetActive(false);
        }
        index = Random.Range(0, gameobjs.Count);
        gameobjs[index].gameObject.SetActive(true);
        fTimer = 0;
    }

    public void ExportTimersToFile()
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string filePath = Path.Combine(desktopPath, $"timers_{timestamp}.txt");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (float timer in timers)
            {
                writer.WriteLine(timer.ToString());
            }
            float percentage = allTim != 0 ? ((allTim - allFalse) / allTim) * 100 : 0;
            writer.WriteLine("��ȷ�ʣ�" + percentage.ToString() + "%");
        }
       
        Debug.Log("Timers exported to " + filePath);
    }

}
