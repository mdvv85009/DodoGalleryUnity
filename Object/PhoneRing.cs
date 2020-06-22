using Transmisson;
using UnityEngine;
namespace Object
{
    public class PhoneRing
    {
        private Server _server;
        private bool isCalling = false;
        private bool isCalled = false;
        public PhoneRing(Server server)
        {
            _server = server;
            _server._answeredPhone += AnsweredPhone;
        }

        public bool IsCalled{
            get { return isCalled; }
        }

        public void Call()
        {
            Debug.Log("[Phone Calling]");
            isCalling = true;
            _server.Broadcast("%Ring");
        }

        public bool IsHangOut(){
            if(!isCalling && isCalled)
                return true;
            return false;        
        }

        public void AnsweredPhone(){
            isCalling = false;
            isCalled = true;
        }
    }
}