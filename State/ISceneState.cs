namespace State
{
    public class ISceneState
    {
        private string m_StateName = "ISceneState";
        private SceneStateController m_controller;
        public string StateName
        {
            get { return m_StateName; }
            set { m_StateName = value; }
        }

        public ISceneState(string name)
        {
            m_StateName = name;
        }

        // Let Child class can trans to another state
        protected void TransTo(ISceneState nextState){
            this.m_controller.TransTo(nextState);
        }

        public void SetSceneController(SceneStateController controller){
            this.m_controller = controller;
        }

        public virtual void StateBegin() { }

        public virtual void StateUpdate() { }

        public virtual void StateEnd() { }

        public override string ToString()
        {
            return string.Format("[I_SceneState: StateName={0}]", StateName);
        }
    }
}