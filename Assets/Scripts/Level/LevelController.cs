using System;
using Game;
using GUI;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Level
{
  public class LevelController : MonoBehaviour
  {
    //draft 
    private float _timeLimit, _timer, _spawnTime, _spawnTimer;
    private LevelData _currentLevelData;
    private LevelScriptableObject _currentLevelObject;
    [SerializeField] private GameObject endgameUI;

    private void Awake()
    {
      GetLevelData();
      ConfigureLevel();
    }

    private void Update()
    {
      if (!LevelRunner.Instance) return;
      TimeController();
      
    }

    private void SpawnEnemy()
    {
      var enemyIndex = Random.Range(0, _currentLevelObject.enemiesPrefabList.Length);
      var enemyPrefab = _currentLevelObject.enemiesPrefabList[enemyIndex];
    
      Instantiate(enemyPrefab).transform.position = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
      var index = Random.Range(0, _currentLevelObject.possiblePositions.Length); //TODO Replace with custom random to improve variability
      var position = _currentLevelObject.possiblePositions[index];

      return position;
    }

    void SpawnBoss()
    {
      //TODO Add boss instantiation with special UI
    }

    private void TimeController()
    {
      _timer += Time.deltaTime;
      _spawnTimer += Time.deltaTime;
      
      if (_spawnTimer > _spawnTime)
      {
        _spawnTimer = 0;
        SpawnEnemy();
      }

      if (_timer >= _timeLimit)
      {
        EndLevel();
      }
      
    }

    private void EndLevel()
    {
      CallEndGameUI();
      LevelRunner.FinishLevel(_currentLevelData);
      //TODO send data to Backend 
    }

    private void CallEndGameUI()
    {
      endgameUI.SetActive(true);
    }

    private void ConfigureLevel()
    {
      Debug.Log("ConfigureLevel");

      if (_currentLevelData == null) return;
      _timer = 0;
      if (_currentLevelData.IsEndless)
      {
        //TODO LIMITLESS LEVEL
        _timeLimit = -1;
      }
      else
      {
        _timeLimit = _currentLevelObject.levelTimer;
        _spawnTime = _timeLimit / _currentLevelObject.spaceShipsQuantity;
      }
      Debug.Log("_timeLimit : "  + _timeLimit);
      Debug.Log("_spawnTime : "  + _spawnTime);

      
    }
    void GetLevelData()
    {
      if (!LevelRunner.Instance) return;
      _currentLevelData = LevelRunner.Instance.Data;
      _currentLevelObject = LevelRunner.Instance.GetLevel(_currentLevelData.LevelIndex);
    
      //TODO set level time, difficulty, get prefabs and run level 
    }

    public void OnContinueButtonPressed()
    {
      GameController.Instance.DeepLinkMainGUI(MainGUI.Screens.Progress);
      SceneManager.LoadScene(1);
      
    }

  }
}
