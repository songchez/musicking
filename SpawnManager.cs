using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> Music = new List<GameObject>();
    public GameObject wall;//화살표 밑에 나오는 판정콜라이더
    public static bool MusicMode = false;//음악모드 //todo Arrow스크립트보다 여기있는게 맞는것같아서 옮김
    public static bool TutorialMode = true;//튜토리얼모드
    public static bool NormalMode = false;//일반모드
    public static bool Music_Playing = false;//음악 재생중확인을 위한 bool
    public static bool InfiniteMode = false;//무한모드
    public static float t = 0;//시간 측정용 변수
    public static float i = 0;//단계 시간 측정용 변수
    public static int Step = 1;//단계 확인용 변수
    int x = 1;//코루틴에서 쓰이는 변수
    public GameObject Note_Parent;
    void Update()
    {
        if (GameController.IsPlaying)
        {
            i += Time.deltaTime;
            t += Time.deltaTime;
            if (TutorialMode)//튜토리얼 모드일때
            {
                if (x <= (12 - (7 * (2 - Step))))//도~솔 이냐 도~2솔 이냐 구분
                {
                    if (t >= 1f)
                    {
                        t = 0;

                        StartCoroutine(Tutorial());
                    }
                }
                else
                {
                    if (i >= x * 2)//음계다 다 나오고나서 일정시간후에 일반모드 실행을 위해 //todo 바로 일반모드 실행 방지
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
                if (Step < 3)
                {
                    if (i < 10)//30초 동안
                    {
                        if (t >= 4)//5초마다
                        {
                            t = 0;
                            GameObject notes = Instantiate(wall);
                            int note_rand = Random.Range(1, 12 - (7 * (2 - Step)));//랜덤음표지정
                            Debug.Log(note_rand);
                            GameObject that_note = notes.transform.GetChild(note_rand).gameObject;//지정된 음표

                            that_note.tag = "Right"; //정답태그지정
                            that_note.GetComponent<SpriteRenderer>().enabled = true; //눈에보이게켜기
                            Debug.Log("Normal");
                            notes.transform.parent = Note_Parent.transform;
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
                else
                {
                    if (t >= 4)//5초마다
                    {
                        t = 0;
                        GameObject notes = Instantiate(wall);
                        int note_rand = Random.Range(1, 12);//랜덤음표지정
                        Debug.Log(note_rand);
                        GameObject that_note = notes.transform.GetChild(note_rand).gameObject;//지정된 음표

                        that_note.tag = "Right"; //정답태그지정
                        that_note.GetComponent<SpriteRenderer>().enabled = true; //눈에보이게켜기
                        Debug.Log("Normal");
                        notes.transform.parent = Note_Parent.transform;
                    }
                }
            }
            else if (MusicMode)//음악모드일때
            {
                if (!Music_Playing)
                {
                    Music_Playing = true;
                    GameObject M_notes = Instantiate(Music[Step - 1]);//비행기악보 오브젝트
                    M_notes.transform.parent = Note_Parent.transform;
                    for (int a = 0; a < M_notes.transform.childCount; a++)
                    {
                        GameObject that_M_note = M_notes.transform.GetChild(a).gameObject;//비행기악보 오브젝트의 자식 //todo 콜라이더들을 묶은 오브젝트(미,레,도,레~ 등등)
                        if (Random.Range(0, 1000) < 200)
                        {
                            for (int b = 1; b < that_M_note.transform.childCount - 1; b++)
                            {
                                that_M_note.transform.GetChild(b).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                            }
                        }
                    }
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
        if (Step == 1)
        {
            x++;
        }
        else
        {
            x += 2;
        }
        yield return new WaitForSeconds(3f);

    }
}
