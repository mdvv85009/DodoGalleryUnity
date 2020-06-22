using UnityEngine;
using UnityEditor;
using Object;
using Transmisson;
using DodoCafe.Networking.Sockets;
using System.Threading.Tasks;
using System;

namespace State
{
    public class FirstSceneState : ISceneState
    {
        private GameObject _firstSceneWall;
        private VideoPlayerController _vpController;
        private PhoneRing _phone;
        private Server _server;
        private Sprite s_slogon = Resources.Load<Sprite>("Aslogon");
        private const string v_idle = "Assets/Resources/Videos/A_idle.mp4";
        private const string v_trans = "Assets/Resources/Videos/A_trans.mp4";
        private bool _isRaspberryPiInfraredSensorSignalReceived = false;

        public FirstSceneState() : base("FirstSceneState")
        {
            _firstSceneWall = GameObject.Find("Scene1");
            _vpController = new VideoPlayerController(_firstSceneWall);
            _server = new Server();
            _phone = new PhoneRing(_server);
        }

        public override void StateBegin()
        {
            _vpController.PlayVideo(v_idle);
            _server.Start();
            ReceiveRaspberryPiInfraredSensorSignal().ContinueWith(new Action<Task>(NotifyRaspberryPiInfraredSensorSignalReceived), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task ReceiveRaspberryPiInfraredSensorSignal()
        {
            var socket = new CSignalReceivingTcpSocket();
            await socket.ConnectAsync("10.120.3.13", 5005);
            await socket.ReceiveSignalAsync();
            socket.Disconnect();
        }

        private void NotifyRaspberryPiInfraredSensorSignalReceived(Task antecedentTask)
        {
            _isRaspberryPiInfraredSensorSignalReceived = true;
        }

        public override void StateUpdate() {
            
            _server.Update();
            if (_isRaspberryPiInfraredSensorSignalReceived)  // 紅外線接收到信息
            {
                _vpController.PlayVideo(v_trans);
                GameLoop._ui.Set(s_slogon, -270, 130);
                GameLoop._ui.Fade = false;
                _phone.Call();
                _isRaspberryPiInfraredSensorSignalReceived = false;
            }  
            else if (_phone.IsHangOut() && _phone.IsCalled) //電話已掛斷
            {
                _vpController.StopVideo();
                GameLoop._ui.Fade = true;
                TransTo(new SecondSceneState());
            }
        }

        public override void StateEnd()
        {
            _vpController.StopVideo();            GameLoop._ui.Fade = true;
        }
    }
}