using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSound : MonoBehaviour
{    //소리를 먼저듣고 맞출때 소리나오게 하는 스크립트
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(SpawnManager.NormalMode)//노말모드일때
        {
            if(col.tag == "Right")//태그가 정답인 콜라이더를
            {
                col.gameObject.GetComponent<AudioSource>().Play();//소리재생맨맨
            }
        }
    }

}