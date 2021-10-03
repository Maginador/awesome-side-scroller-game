using UnityEngine;

namespace Info
{
    public class UpgradeInfo: Base.Info<UpgradeInfo>
    {
        public int HealthPoints;
        public int Energy;
        public int ShootPower;
        public int FireRate;
        public int Armor;
        public int Bullets;

        public static UpgradeInfo GetUpgradeFromJson(string json)
        {
            return JsonUtility.FromJson<UpgradeInfo>(json);
        }
    }
}