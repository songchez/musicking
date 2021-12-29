using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   public List<GameObject> Music = new List<GameObject>();
    public GameObject wall;//화살표 밑에 나오는 판정콜라이더
    public static bool MusicMode = false;//음악모드 //todo Arrow스크립트보다 여기있는게 맞는것같아서 옮김
    public static bool TutorialMode = true;//튜토리얼모드
    public static bool NormalMode = false;//일반모드
    public static bool Music_Playing = false;//음악 재생중확인을 위한 bool
    float t = 0;//시간 측정용 변수
    float i = 0;//단계 시간 측정용 변수
    public static int Step = 1;//단계 확인용 변수
    int x = 1;//코루틴에서 쓰이는 변수
    void Update()
    {
        if (GameController.IsPlaying)
        {
            i += Time.deltaTime;
            t += Time.deltaTime;
            if (TutorialMode)
            {
                if (x <= (12 - (7 * (2 - Step))))//도~솔 이냐 도~2솔 이냐 구분
                {
                    if (t >= 3)
                    {
                        t = 0;

                        StartCoroutine(Tutorial());
                    }
                }
                else
                {
                    if (i >= x*3+5)//음계다 다 나오고나서 일정시간후에 일반모드 실행을 위해 //todo 바로 일반모드 실행 방지
                    {
                        TutorialMode = false;//튜토리얼 종료
                        NormalMode = true;//일반모드 실행
                        t = 0;//초기화
                        i = 0;//초기화
                        x = 1;//초기화
                    }
                }
            }


            else if (NormalMode)//일반모드일때
            {
                if(i<30)//30초 동안
                {
                    if (t >= 5)//5초마다
                    {
                        t = 0;
                        GameObject notes = Instantiate(wall);
                        int note_rand = Random.Range(1, 12 - (7 * (2 - Step)));//랜덤음표지정
                        Debug.Log(note_rand);
                        GameObject that_note = notes.transform.GetChild(note_rand).gameObject;//지정된 음표

                        that_note.tag = "Right"; //정답태그지정
                        that_note.GetComponent<SpriteRenderer>().enabled = true; //눈에보이게켜기
                        Debug.Log("Normal");
                    }
                }
                else
                {
                    NormalMode = false;
                    MusicMode = true;
                    t = 0;
                    i = 0;
                }
            }
            else if(MusicMode)//음악모드일때
            {
                if(!Music_Playing)
                {
                   Music_Playing = true;
                   Instantiate(Music[Step-1]);
                }
                
            }
        }
    }
    IEnumerator Tutorial()//도부터 차례대로 음계 생성
    {
        GameObject t_notes = Instantiate(wall);
        GameObject t_that_note = t_notes.transform.GetChild(x).gameObject;//지정된 음표

        t_that_note.tag = "Right"; //정답태그지정
        t_that_note.GetComponent<SpriteRenderer>().enabled = true; //눈에보이게켜기
        x++;
        yield return new WaitForSeconds(3f);

    }
}
