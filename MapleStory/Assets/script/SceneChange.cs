using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneChange : MonoBehaviour
{
    public void Scene_zero()
    {
        SceneManager.LoadScene(0); //intro
    }
    public void Scene_one()
    {
        SceneManager.LoadScene(1); //1.menuscene
    }
    public void Scene_two()
    {
        SceneManager.LoadScene(2); //2. tutoscene
        var dontdestroy_obj = GameObject.Find("Charactor_INFO");
        DontDestroyOnLoad(dontdestroy_obj);
    }
}