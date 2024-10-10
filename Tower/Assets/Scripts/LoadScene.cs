using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] bool _loadOnStart;
    [SerializeField] int _sceneIndex;
    private void Start()
    {
        if (_loadOnStart)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
    public void LoadSceneWithIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
