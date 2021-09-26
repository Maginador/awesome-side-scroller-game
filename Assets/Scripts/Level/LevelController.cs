using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class LevelController : MonoBehaviour
{
  //draft 
  private float _timeLimit, _timer;
  private LevelData _currentLevelData;
  private LevelScriptableObject _currentLevelObject;

  
  private void SpawnEnemy()
  {
    
  }

  void SpawnBoss()
  {
    
  }

  void TimerController()
  {
    if (_currentLevelData == null) return;
    if (_currentLevelData.IsEndless)
    {
      //TODO LIMITLESS LEVEL
    }
    else
    {
      _timeLimit = _currentLevelObject.levelTimer;
    }
  }

  void GetLevelData()
  {
    _currentLevelData = LevelRunner.Instance.data;
    _currentLevelObject = LevelRunner.Instance.GetLevel(_currentLevelData.LevelIndex);
    
    //TODO set level time, difficulty, get prefabs and run level 
  }

}
