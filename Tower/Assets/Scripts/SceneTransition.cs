using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] LoadScene _sceneLoader;
    [SerializeField] RectTransform _transitionCircleTransform;
    [SerializeField] InputActionAsset _playerControls;
    [SerializeField] bool _startOnLoad;
    [SerializeField] int _sceneToLoadIndex;
    Animator _anim;
    private void Start()
    {
        if(_transitionCircleTransform != null) _anim = _transitionCircleTransform.GetComponent<Animator>();

        if (_startOnLoad)
        {


            if (_transitionCircleTransform != null) _anim.SetTrigger("FadeIn");
            _playerControls.Enable();
        }
    }

    public void Load()
    {
        _playerControls.Disable();
        StartCoroutine(TransitionCor());
    }
    IEnumerator TransitionCor()
    {
        if (_transitionCircleTransform != null) _anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.7f);
        _sceneLoader.LoadSceneWithIndex(_sceneToLoadIndex);

    }
}
