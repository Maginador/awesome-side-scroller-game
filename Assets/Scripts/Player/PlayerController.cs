using System;
using Level;
using UnityEngine;

namespace Player
{
    public class PlayerController : Spaceship
    {
        [SerializeField] private Camera mainCamera;

        [SerializeField] private LevelController level;

        private void Start()
        {
            if(mainCamera == null) mainCamera = Camera.main;
            if (level == null) level = FindObjectOfType<LevelController>();
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
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyBullet"))
            {
                OnTakeDamage(1);
                Destroy(other.gameObject);
            }
        }


        private void OnDestroy()
        {
            level.ShowGameOverScreen();
        }
    }
}
