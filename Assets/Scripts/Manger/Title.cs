using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{

    private FadeManager theFade;
    private AudioManger theAudio;
    private PlayerManager thePlayer;
    private GameManager

    public string clickSound;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManger>();
        theFade = FindObjectOfType<FadeManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
