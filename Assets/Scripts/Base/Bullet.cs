using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,5);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(transform.up * (speed * Time.deltaTime));
    }
}
