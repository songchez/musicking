using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float GameSpeed = 1; //static 게임 스피드
    public static bool IsPlaying = false; //static 게임 ON/OFF
    public static int WaveCount = 0; //static 게임 난이도
    public GameObject game_over; //게임오버 패널
    //미구현
    public GameObject Character;
    public GameObject S_Manger;
    public GameObject Player;
    Vector2 P_StartPosition;
    void Start()
    {
        P_StartPosition = Player.transform.position;
    }
    public void GameStart()
    {
        game_over.SetActive(false);
        IsPlaying = true;
        Character.GetComponent<Player>().PlayerStart();
        SpawnManager.i = 0;
        SpawnManager.t = 0;
        SpawnManager.TutorialMode = true;
        SpawnManager.NormalMode = false;
        SpawnManager.MusicMode = false;
        for(int q = 0; q < S_Manger.transform.childCount; q++)
        {
            Destroy(S_Manger.transform.GetChild(q).gameObject);
        }
    }
    public void GameOver() //player에서 사용
    {
        game_over.SetActive(true);
        IsPlaying = false;
        GameSpeed = 1;
        Character.GetComponent<Animator>().SetBool("run",false);
        Character.GetComponent<Animator>().SetBool("die",true);
    }
    private void Update()
    {
        if (WaveCount > 1)
        {
            WaveCount =0;
            GameSpeed += 0.15f;
        }

        if (Input.GetKeyDown(KeyCode.R) && !IsPlaying)
        {
            GameStart();
        }
    }
}