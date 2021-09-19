using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Player : MonoBehaviour
    {
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