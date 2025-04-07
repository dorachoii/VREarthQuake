using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EJSceneManager : MonoBehaviour
{
    //public static EJSceneManager instance;


    private void Awake()
    {
        //instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9)) {

            Scene0to1();
        }
    }


    //0: 지진방송 TV화면
    //1: 지진 일어나는 방 전경 (zoom out)
    //2: GameScene
    //3: GameScene2 (Respawn)
    //4: 탈출씬

    // 정훈 수정

    //1: 게임 스타트 씬
    //2: 게임 메인씬 
    //3. 게임 엔딩 씬 

    public void Scene0to1()
    {
        //정훈 수정
        //GameStart 버튼 누를시 메인으로 이동
        SceneManager.LoadScene(1);
    }

    public void Scene1to2() 
    { 
        //정훈수정
        //큐브에 닿으면 엔딩씬으로 이동
        SceneManager.LoadScene(2);
    }

    public void Scene2to3() 
    {
        //메인씬2)
        SceneManager.LoadScene(3);
    }

    public void Scene3to4()
    {
        //탈출씬
        SceneManager.LoadScene(4);
    }

    //박정훈 추가
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Scene1to2();
            print("Player가 Trigger에 닿았다");
        }
    }

}
