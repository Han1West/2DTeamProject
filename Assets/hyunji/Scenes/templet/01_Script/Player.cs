using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //플레이어와 맵을 관리하는 스크립트를 연결하여
    //플레이어가 OUT POINT와 충돌 시
    //해당 POINT와 연결된 DOOR의 MAP OBJECT를 넘겨받아 처리함
    public MapController mapController;
    
    //플레이어의 이동속도를 조절
    public float Speed;

    //X축 키값을 넘겨받음 
    public float vertical;

    //Y축 키값을 넘겨받음
    public float horizontal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical   = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        //플레이어의 이동 처리
        transform.Translate(new Vector3(horizontal,vertical,0) * Speed);
    }

    
    #region 충돌 이벤트 함수들
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col == null)
            return;
        if (col.CompareTag("Door"))
        {
            Door door = col.GetComponent<Door>();
            mapController.UpdateMap(door.TargetMap);

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col);
        if (col == null)
            return;
        Debug.Log(col.collider.name);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        Debug.Log(col);
        if (col == null)
            return;
        Debug.Log(col.collider.name);
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log(col);
        if (col == null)
            return;
        Debug.Log(col.collider.name);
    }

    #endregion
    
}
