using UnityEngine;
using UnityEngine.Video;
namespace Object
{
    public class VideoPlayerController
    {
        private GameObject _target;
        private VideoPlayer _vp;


        public VideoPlayerController(GameObject target)
        {
            _target = target;
            _vp = target.AddComponent<VideoPlayer>();
        }

        public void PlayVideo(string source){
            Debug.Log("[Play the source : " + source + " ]");
            _vp.url = source;
            _vp.Play();
        }

        public void StopVideo(){
            _vp.Stop();
        }

        public bool IsPlaying(int sceneNum){
            string targetName = "Scene" + sceneNum.ToString();
            VideoPlayer vp = GameObject.Find(targetName).GetComponent<VideoPlayer>();
            return vp.isPlaying;
        }
    }
}