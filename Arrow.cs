using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void Update()
    {
        if (SpawnManager.NormalMode || SpawnManager.TutorialMode)//일반모드
        {
            if ((this.transform.position.x < -28 || !GameController.IsPlaying))
            {//파괴조건
                Destroy(this.gameObject);
            }
            else
            {//오브젝트 이동 기본
                transform.position = Vector2.MoveTowards(
                    this.transform.position, new Vector2(
                        -30, transform.position.y
                        ), Time.deltaTime * GameController.GameSpeed * 5
                    );
            }
        }
        else if (SpawnManager.MusicMode)//악보모드
        {
            if (GameController.IsPlaying)
            {
                transform.position = Vector2.MoveTowards(
                    this.transform.position, new Vector2(
                        -300, transform.position.y
                        ), Time.deltaTime * GameController.GameSpeed * 5
                    );
            }
            else if(!GameController.IsPlaying)
            {
            }
        }
    }
}
