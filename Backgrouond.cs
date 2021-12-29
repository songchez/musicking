using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrouond : MonoBehaviour
{
    public SpriteRenderer[] maps; //벽
    //public Sprite[] papers; //스프라이트 안씀
    // public float speed; 안씀
    void Start()
    {
        temp = maps[0];
    }
    SpriteRenderer temp;
    void Update()
    {
        if (GameController.IsPlaying)
        {
            for (int i = 0; i < maps.Length; i++)
            {
                maps[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * GameController.GameSpeed * 2);
                if (-40 >= maps[i].transform.position.x) //자리이동
                {
                    for (int q = 0; q < maps.Length; q++) //갯수만큼실행
                    {
                        if (temp.transform.position.x < maps[q].transform.position.x)
                            temp = maps[q]; //x가 temp보다 높은 친구를 찾아서 temp에 넣기
                    }
                    maps[i].transform.position = new Vector3(temp.transform.position.x + 45, 0, 0);
                    //maps[i].sprite = papers[Random.Range(0,papers.Length)];//랜덤한 맵
                }
            }
        }
    }
}