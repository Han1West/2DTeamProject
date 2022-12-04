using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    private FadeManager theFade;
    private AudioManger theAudio;
    private PlayerManager thePlayer;
    private GameManager theGM;

    public string clickSound;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManger>();
        theFade = FindObjectOfType<FadeManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theGM = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        theFade.FadeOut();
        theAudio.Play(clickSound);
        yield return new WaitForSeconds(2f);
        Color color = thePlayer.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        thePlayer.GetComponent<SpriteRenderer>().color = color;
        thePlayer.currentMapName = "Start";

        theGM.LoadStart();
        SceneManager.LoadScene("Start");
    }

    public void ExitGame()
    {
        theAudio.Play(clickSound);
        Application.Quit();
    }

}
