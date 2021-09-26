using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    
    public class PlayerData : MonoBehaviour
    {
        
        private int _healthPoint = 100;
        private int _power = 10;
    
        public int GetPower()
        {
            return _power;
        }

        public void SetPower(int power)
        {
            _power = power;
        }
        public static Profile CurrentProfile;
        public static void CreateNewPlayer()
        {
            CurrentProfile = new Profile(SystemInfo.deviceUniqueIdentifier);
        }
    }

    public class Profile
    {
        public Profile(string id)
        {
            ID = id;
        }

        private static string ID { get; set; }
      
        public static string GetID()
        {
            return ID;
        }
    }
}