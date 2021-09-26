using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/New Level", order = 1)]
    public class LevelScriptableObject : ScriptableObject
    {
        public string levelName;
        public int spaceShipsQuantity;
        public int difficulty;
        public int bossCount;
        public float levelTimer;
        public GameObject[] enemiesPrefabList; //TODO if Empty, use complete random (at first we have only one enemy spaceship)
        public Vector3[] possiblePositions;
        public GameObject bossPrefab;
    }
}