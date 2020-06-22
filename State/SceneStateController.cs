using UnityEngine;
using Object;
using System.Collections;
namespace State
{
    public class SceneStateController
    {
        private ISceneState m_State = null;
        private bool m_bRunBegin = false;
        private bool m_bEnd = false;

        public string CurrentState{
            get{
                return m_State.ToString();
            }
        }

        public void SetState(ISceneState State)
        {
            Debug.Log("SetState : " + State.ToString());
            if (m_State != null)    //直接把已有的State Terminate 掉
                m_State.StateEnd();
            m_bRunBegin = true;
            m_State = State;
            m_State.SetSceneController(this);
            m_State.StateBegin();
        }

        public void StateUpdate()
        {
            if (m_bEnd || !m_bRunBegin) {
                return;
            }
            if (m_State != null) {
                m_State.StateUpdate();
            }
        }

        public void TransTo(ISceneState nextState){
            if (m_bEnd) {
                Debug.LogError("[StateController] has been terminated");
                return;
            }
            if (!m_bRunBegin) {
                Debug.LogError("[StateController] has been started");
                return;
            }

            Debug.Log("[StateController] TransTo: " + m_State.ToString());
            SetState(nextState);
        }

        public void Terminate()
        {
            if (m_State != null){
                m_State.StateEnd();
                m_State = null;
            }
            m_bEnd = true;
        }
    }
}