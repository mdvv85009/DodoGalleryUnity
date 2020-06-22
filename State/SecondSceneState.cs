using Object;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace State
{
    public class SecondSceneState : ISceneState
    {
        private GameObject _secondSceneView;
        private Color materialColor;
        private GameObject _charactor;
        private VideoPlayerController _vpController;
        private bool _isStartFadingDone = false;
        private bool _isHintShown = false;
        private bool _isSlogonShown = false;
        private bool _isTimerOpened = false;
        private Sprite s_hint = Resources.Load<Sprite>("BHint");
        private Sprite s_slogon = Resources.Load<Sprite>("BSlogon");
        private const string v_cloud = "Assets/Resources/Videos/B_Cloud.mp4";
        private float _timePassed;

        public SecondSceneState() : base("SecondSceneState")
        {
            _secondSceneView = GameObject.Find("Scene2(View)");
            _vpController = new VideoPlayerController(_secondSceneView);
            materialColor = _secondSceneView.GetComponent<Renderer>().material.color;
        }

        public override void StateBegin() {

            GameLoop._ui.Set(s_hint, 430, 80);
            GameLoop._ui.Fade = false;
            _charactor = GameLoop._controller;
            _charactor.SetActive(true);
        }

        public override void StateUpdate() {
            if (_isTimerOpened)
            {
                _timePassed += Time.deltaTime;
                MovingCharactor();
            }
            else
                ShowingView();
        }

        private void ShowingView()
        {
            if (!_isStartFadingDone && materialColor.a > 0)
            {
                materialColor.a -= Time.deltaTime * 0.8f;
                _secondSceneView.GetComponent<Renderer>().material.color = materialColor;
            }
            else
            {
                if (HasMovingInput() && !_isTimerOpened)
                {
                    GameLoop._ui.Fade = true;
                    _isTimerOpened = true;
                }
                _isStartFadingDone = true;
            }
        }

        private void MovingCharactor() {

            if ((int)_timePassed > 5 && (int)_timePassed < 10)
                _charactor.GetComponent<FirstPersonController>().SlowDown();
            else if ((int)_timePassed >= 10)
            {
                if (materialColor.a <= 1)
                {
                    GameLoop._ui.Set(s_slogon, 520, 80);
                    GameLoop._ui.Fade = false;
                    materialColor.a += Time.deltaTime * 0.8f;
                    _secondSceneView.GetComponent<Renderer>().material.color = materialColor;
                }
                else
                {
                    GameLoop._ui.Fade = true;
                    _charactor.SetActive(false);
                    // Transfer to C part
                }
            }
        }

        private bool HasMovingInput()
        {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
        }

        public override void StateEnd(){
        }
    }
}