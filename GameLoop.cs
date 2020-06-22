using State;
using Object;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameLoop : MonoBehaviour {


    //設置場景控制器
    SceneStateController m_SceneStateController = new SceneStateController();
    public static GameObject _controller;
    public static UITexture _ui;
    void Awake()
    {
        //使 GameLoop 不會因為換場景而被刪除
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        _controller = GameObject.Find("Scene2/Scene2(Controller)");
        _controller.SetActive(false);
        _ui = UITexture.GetInstance();
        m_SceneStateController.SetState(new FirstSceneState());    //設置當前場景為 WaitingCafeState，並直接開始
    }

    void Update()
    {
        _ui.Update();
        m_SceneStateController.StateUpdate();   //場景控制更新
        SkipTo();
    }

    private void SkipTo(){
        if (Input.GetKey(KeyCode.F1))
            m_SceneStateController.SetState(new FirstSceneState());
        else if (Input.GetKey(KeyCode.F2))
            m_SceneStateController.SetState(new SecondSceneState());
    }
}