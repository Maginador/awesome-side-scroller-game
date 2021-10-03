using System;
using Info;
using Level;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerController : Spaceship
    {
        [SerializeField] private Camera mainCamera;

        [SerializeField] private LevelController level;

        private SpaceshipScriptableObject _playerReferenceSpaceship;
        private void Start()
        {
            if(mainCamera == null) mainCamera = Camera.main;
            if (level == null) level = FindObjectOfType<LevelController>();
            SetSpaceshipData(PlayerData.GetPlayerUpgradeData());
        }

        private void SetSpaceshipData(UpgradeInfo getPlayerUpgradeData)
        {
            _playerReferenceSpaceship = spaceshipData;
            spaceshipData = ScriptableObject.CreateInstance<SpaceshipScriptableObject>();
            spaceshipData.healthPoints = _playerReferenceSpaceship.healthPoints * (getPlayerUpgradeData.HealthPoints+1);
            spaceshipData.energy = _playerReferenceSpaceship.energy * (getPlayerUpgradeData.Energy+1);
            spaceshipData.shootPower = _playerReferenceSpaceship.shootPower * (getPlayerUpgradeData.ShootPower+1);
            spaceshipData.shootRate = _playerReferenceSpaceship.shootRate * (getPlayerUpgradeData.FireRate + 1);
            spaceshipData.bullet = _playerReferenceSpaceship.bullet;
            spaceshipData.bulletTag = _playerReferenceSpaceship.bulletTag;
        }

        // Update is called once per frame
        private new void Update()
        {
            base.Update();
            if (Input.GetButton("Fire1") || Input.touches.Length>0)
            {
#if UNITY_EDITOR
                var position = Input.mousePosition;
#elif UNITY_ANDROID
            var position = Input.GetTouch(0).position;
#endif
                var destination =  mainCamera.ScreenToWorldPoint(position);
                Debug.DrawLine(mainCamera.transform.position, destination, Color.red, 10);
                MoveToPosition(destination.x, destination.y);
            }
        }
    
        private void OnTriggerEnter(Collider hitCollider)
        {
            if (hitCollider.CompareTag("EnemyBullet"))
            {
                var bullet = hitCollider.GetComponent<Bullet>();
                OnTakeDamage(bullet.GetPower()); 
                Destroy(hitCollider.gameObject);
            }
        }


        private void OnDestroy()
        {
            level.ShowGameOverScreen();
        }
    }
}
