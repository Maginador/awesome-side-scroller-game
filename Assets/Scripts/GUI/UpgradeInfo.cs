using UnityEngine;

public class UpgradeInfo
{
    public int healthpoints;
    public int energy;
    public int shootpower;
    public int firerate;
    public int armor;
    public int bullets;

    public static UpgradeInfo GetUpgradeFromJson(string json)
    {
        return JsonUtility.FromJson<UpgradeInfo>(json);
    }
}