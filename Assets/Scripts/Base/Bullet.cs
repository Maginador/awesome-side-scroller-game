using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private int _power;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,8);
    }

    public void SetPower(int power)
    {
        _power = power;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime),Space.Self);
    }

    public int GetPower()
    {
        return _power;
    }
}
