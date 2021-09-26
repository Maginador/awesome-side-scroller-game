using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Spaceship", menuName = "ScriptableObjects/Space Ship", order = 1)]
    public class LevelScriptableObject : ScriptableObject
    {
        public string levelName;
        public int spaceShipsQuantity;
        public int difficulty;
        public int bossCount;
        public float levelTimer;
        public GameObject[] enemiesPrefabList; //TODO if Empty, use complete random (at first we have only one enemy spaceship)
        public GameObject bossPrefab;
    }
}