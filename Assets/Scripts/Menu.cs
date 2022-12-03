using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion Singleton
    private AudioManger theAudio;
    private OrderManager theOrder;
    private PlayerStat thePlayerStat;

    private float timeCount = 0;

    public string callSound;
    public string cancelSound;

    public TMP_Text hp;
    public TMP_Text atk;

    public GameObject go;

    private bool activated;

    void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();
        theAudio = FindObjectOfType<AudioManger>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        activated = false;
        go.SetActive(false);
        theAudio.Play(cancelSound);
    }

    public void StatShow()
    {
        hp.text = thePlayerStat.currentHp.ToString() + "    /    " + thePlayerStat.hp.ToString();
        atk.text = thePlayerStat.atk.ToString();
    }

    void Update()
    {
        StatShow();
        timeCount += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;

            if(activated)
            {
                go.SetActive(true);
                theAudio.Play(callSound);
            }
            else
            {
                go.SetActive(false);
                theAudio.Play(cancelSound);
            }
        }
    }
}
