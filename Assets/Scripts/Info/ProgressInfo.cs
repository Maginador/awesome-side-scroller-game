using UnityEngine;

namespace Info
{
    public class ProgressInfo
    {
        public int progress;

        public static ProgressInfo GetProgressFromJson(string json)
        {
            return JsonUtility.FromJson<ProgressInfo>(json);
        }
    }
}