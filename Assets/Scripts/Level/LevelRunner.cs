using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRunner : MonoBehaviour
{

    [SerializeField] private LevelScriptableObject[] levels;
    public static LevelRunner Instance;
    public LevelData data;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    public void RunLevel(int index = 0, bool isEndless = false)
    {
        
        SceneManager.LoadScene("Scenes/Game");
        data = new LevelData(index, isEndless);
    }


    public LevelScriptableObject GetLevel(int levelIndex)
    {
        return levels[levelIndex];
    }
}

public class LevelData
{
    public int LevelIndex { get; }
    public bool IsEndless { get; }

    public string Seed { get; }
    public LevelData(int index, bool endless)
    {
        LevelIndex = index;
        IsEndless = endless;
        //TODO GET SEED FROM BACKEND
    }
    
    
}
