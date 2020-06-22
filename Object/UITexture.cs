using System;
using UnityEngine;
using UnityEngine.UI;

namespace Object
{
    public class UITexture
    {
        private static UITexture _instance = null;
        private GameObject _text = GameObject.Find("Canvas/Image");
        private Image _textImage;
        private Color _textColor;
        private bool _isFade = true;
        private bool _test = true;
        const float _fadeSpeed = (float)0.8;
        const float _trueScale = (float)1.18;

        private UITexture()
        {

        }

        public static UITexture GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UITexture();
                _instance._textImage = _instance._text.GetComponent<Image>();
                _instance._textColor = _instance._textImage.color;
            }
            return _instance;
        }

        public Boolean Fade

        {
            get { return _isFade; }
            set { _isFade = value; }
        }

        public void Set(Sprite newSprite, double newX, double newY)
        {
            _textImage.sprite = newSprite;
            if(_test)
                _text.transform.localPosition = new Vector3((float)newX, (float)newY, 0);
            //else
                //_text.transform.localPosition = new Vector3((float)newX * _trueScale, (float)newY * _trueScale, 0);
        }

        public void Update()
        {
            if (!_isFade && _textColor.a <= 1)
            {
                _textColor.a += _fadeSpeed * Time.deltaTime;
                _textImage.color = _textColor;
            }

            else if (_isFade && _textColor.a >= 0)
            {
                _textColor.a -= _fadeSpeed * Time.deltaTime;
                _textImage.color = _textColor;
            }
        }
    }
}
