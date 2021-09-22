using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Spaceship
{
    [SerializeField]
    private Camera mainCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
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
}
