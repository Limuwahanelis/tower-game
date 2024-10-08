using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignInfo : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _textShowSpeed;
    [SerializeField] float _textFadeSpeed;
    [SerializeField] ObjectDetection _detection;
    private float _textAlpha;
    private float _time;
    private Coroutine _cor;
    // Start is called before the first frame update
    void Start()
    {
        _detection.OnObjectDetectedUnity.AddListener(StartShowtextCor);
        _detection.OnObjectLeftdUnity.AddListener(StartHidetextCor);
    }
    private void StartShowtextCor()
    {
        if(_cor != null ) 
        {
            StopCoroutine( _cor );
        }
        _cor = StartCoroutine(ShowText());
    }
    private void StartHidetextCor()
    {
        if (_cor != null)
        {
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(HideText());
    }
    private IEnumerator ShowText()
    {
        while(_textAlpha<1)
        {
            _time += Time.deltaTime * _textShowSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _textAlpha = Mathf.Lerp(0, 1, _time);
            _text.alpha = _textAlpha;
            yield return null;
        }
    }
    private IEnumerator HideText()
    {
        while (_textAlpha > 0)
        {
            _time -= Time.deltaTime * _textFadeSpeed;
            _time = Mathf.Clamp(_time, 0, 1);
            _textAlpha = Mathf.Lerp(0, 1, _time);
            _text.alpha = _textAlpha;
            yield return null;
        }
    }
    private void OnDestroy()
    {
        _detection.OnObjectDetectedUnity.RemoveListener(StartShowtextCor);
        _detection.OnObjectLeftdUnity.RemoveListener(StartHidetextCor);
    }
}
