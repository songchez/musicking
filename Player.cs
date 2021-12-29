using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject game_manager;
    public Text ScoreUI; //점수_ui
    int Score = 0; // 점수
    public Slider HpSlider; //체력 슬라이더UI
    public static int Hp = 100; //체력
    GameObject kirat;
    //맞췄을떄 나올 이펙트
    GameObject kirat2;
    //맞췄을떄 나올 두번째 이펙트
    Animator ani;
    //애니메이션

    void Start()
    {
        ani = GetComponent<Animator>();
        PlayerStart();
    }

    public void PlayerStart()
    {
        //초기화
        Score = 0;
        Hp = 100;
        ScoreUI.text = Score.ToString();
        HpSlider.value = Hp;
        ani.SetBool("die", false);
        ani.SetBool("run", true);
    }
    void Update()
    {
        if (GameController.IsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < 3f) //위로 이동
            {
                this.transform.position = new Vector2(transform.position.x, transform.position.y + 0.525f);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -2.7f) //아래로 이동
            {
                this.transform.position = new Vector2(transform.position.x, transform.position.y - 0.525f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {    
        if (col.tag == "Wrong")//잘못된 위치에 있을때
        {
            if(!SpawnManager.TutorialMode)
            {
                Hp -= 20;
                HpSlider.value = Hp;
                if (Hp <= 0)
                {
                    game_manager.GetComponent<GameController>().GameOver();//게임메니저에 들어있는 게임오버 실행
                    

                }
                if (GameController.IsPlaying)
                    {

                        for (int i = 0; i < col.transform.parent.gameObject.transform.childCount; i++)//충돌한 콜라이더의 부모의 자식갯수만큼 실행
                            Destroy(col.transform.parent.gameObject.transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>());//충돌한 콜라이더의 부모의 자식들의 콜라이더를 파괴
                    }
            }
        }
        else if (col.tag == "Right")//옳은 위치에 있을때(다른 컨텐츠 추가도 있을거 같애서 else if 사용)
        {
            kirat = col.gameObject.transform.GetChild(0).gameObject;
            kirat.SetActive(true);//이팩트 활성화
            //kirat2 = col.gameObject.transform.GetChild(1).gameObject;
            //kirat2.SetActive(true);//2번째이팩트 활성화
            Debug.Log("정답");
            Score += 10;
            ScoreUI.text = Score.ToString();
            col.gameObject.GetComponent<AudioSource>().Play();//Todo소리재생맨맨//맨맨은 선넘었지
            if (GameController.IsPlaying)
            {
                for (int i = 0; i < col.transform.parent.gameObject.transform.childCount; i++)//충돌한 콜라이더의 부모의 자식갯수만큼 실행
                    Destroy(col.transform.parent.gameObject.transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>());//충돌한 콜라이더의 부모의 자식들의 콜라이더를 파괴
            } 
        }
        if(col.tag == "MusicOver")
        {
            SpawnManager.Music_Playing = false;//음악종료
            SpawnManager.TutorialMode = true;//튜토리얼모드실행
            SpawnManager.MusicMode = false;//음악모드 종료
            SpawnManager.Step ++;//1단계 상승
        }
    }
}