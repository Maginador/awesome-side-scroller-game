using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelRunner : MonoBehaviour
    {

        [SerializeField] private LevelScriptableObject[] levels;
        public static LevelRunner Instance { get; private set; }
    
        public LevelData Data;
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
        
            SceneManager.LoadScene(sceneBuildIndex: 2);
            Data = new LevelData(index, isEndless);
        }


        public LevelScriptableObject GetLevel(int levelIndex)
        {
            Debug.Log(levelIndex);
            return levels[levelIndex];
        }

        public static void FinishLevel(LevelData currentLevelData)
        {
            PlayerPrefs.SetInt("LevelConcluded" + currentLevelData.LevelIndex,1 );
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
}