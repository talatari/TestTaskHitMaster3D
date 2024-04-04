using System.Threading;
using Codebase.Players;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
    public class UIStart : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _buttonClick;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private Mover _mover;
        
        private CancellationTokenSource _cancellationTokenSource = new ();
        private CancellationToken _cancellationToken => _cancellationTokenSource.Token;
        private bool _isClicked;

        private void Start() => 
            _buttonClick.onClick.AddListener(OnClick);

        private void OnDisable()
        {
            _buttonClick.onClick.RemoveListener(OnClick);
            
            _cancellationTokenSource.Cancel();
        }

        private void OnClick()
        {
            if (_isClicked == false)
                AsyncFadeEffectAlphaText();
            
            _mover.Run();
        }

        private async void AsyncFadeEffectAlphaText()
        {
            _isClicked = true;
            float minAlpha = 0;
            float maxAlpha = 1000;
            float elapsedTime = 0;
            int toFontSize = 100;
            int fromFontSize = 140;
            Color color = _text.color;
            
            while (elapsedTime < _duration)
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    SetMinAlphaValues(color, minAlpha);
                    
                    return;
                }
                
                color.a = Mathf.Lerp(maxAlpha, minAlpha, elapsedTime / _duration) / maxAlpha;
                _text.color = color;
                _text.fontSize = Mathf.Lerp(fromFontSize, toFontSize, elapsedTime / _duration);
                elapsedTime += Time.deltaTime;

                await UniTask.Yield(_cancellationToken).SuppressCancellationThrow();
            }

            SetMinAlphaValues(color, minAlpha);
        }

        private void SetMinAlphaValues(Color color, float minAlpha)
        {
            color.a = minAlpha;
            _text.color = color;
        }
    }
}