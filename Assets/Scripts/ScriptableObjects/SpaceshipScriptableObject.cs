using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Spaceship", menuName = "ScriptableObjects/Space Ship", order = 1)]
    public class SpaceshipScriptableObject : ScriptableObject
    {
        public string spaceshipName;
        public int healthPoints;
        public int speed;
        public int energy;
        public int shootPower;
        public float shootRate;
        public GameObject destructionVFX;
        public GameObject bullet;
        public string bulletTag;
    }
}