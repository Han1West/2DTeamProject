using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //맵들을 다 관리한다.
    public MapObject[] MapObjs;
    
    //인스펙터 창에서 설정해서 시작하는 맵을 바꿀 수 있음.
    public int StartID;
    
    //플레이어를 가져옴.
    public Player MyPlayer;
    
    //설정된 START ID에 맞는 MAP OBJECT를 켜준다.
    //START ID 값의 범위 밖까지 확인.
    private void OnEnable()
    {
        if (StartID > MapObjs.Length - 1)
            StartID = MapObjs.Length - 1;
        else if (StartID < 0)
            StartID = 0;
        
        OnMove();
    }

    //해당 ID에 맞는 MAP으로 플레이어 오브젝트 이동한다.
    void OnMove()
    {
        for (int i = 0; i < MapObjs.Length; i++)
        {
            if(i != StartID)
                MapObjs[i].DisableMap();
        }
        MapObjs[StartID].EnableMap();
        MyPlayer.transform.position = MapObjs[StartID].StartPoint.position;
    }

    //문이랑 연결된 맵을 넘겨받음 
    //넘겨받은 맵을 제외한 맵들은 비활성화
    //넘겨받은 맵만 활성화한다.
    public void UpdateMap(MapObject map)
    {
        if (map == null)
        {
            Debug.Log("해당 문에서 넘겨받은 맵의 데이터가 없습니다.");
        }
        
        for (int i = 0; i < MapObjs.Length; i++)
        {
            if(MapObjs[i] != map)
                MapObjs[i].DisableMap();
        }
        
        map.EnableMap();
        MyPlayer.transform.position = map.StartPoint.position;
    }
}
