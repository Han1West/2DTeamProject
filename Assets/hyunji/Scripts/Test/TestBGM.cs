using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBGM : MonoBehaviour
{
    BGMManger BGM;

    public int playMusicTrack;

    
    // Start is called before the first frame update
    void Start()
    {
        BGM = FindObjectOfType<BGMManger>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BGM.Play(playMusicTrack);
        this.gameObject.SetActive(false);
    }
}
