using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class Spaceship : Entity
{
    private const float MovementThreashold = 0.1f;
    public SpaceshipScriptableObject spaceshipData;
    private int _healthPoints;
    private int _speed;
    private int _energy;
    private float _shootRate;
    private float _shootTimer;
    private GameObject _destructionVFX;
    private GameObject _bullet;
    private string _bulletTag;
    
    private Rigidbody _rigidbody;
    public void Awake()
    {
        InitializeSpaceship();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        DoShoot();
        
    }

    private void InitializeSpaceship()
    {
        if (!spaceshipData) return;
        
        _healthPoints = spaceshipData.healthPoints;
        _speed = spaceshipData.speed;
        _energy = spaceshipData.energy;
        _shootRate = spaceshipData.shootRate;
        _destructionVFX = spaceshipData.destructionVFX;
        _bullet = spaceshipData.bullet;
        _bulletTag = spaceshipData.bulletTag;
    }

    protected void MoveToPosition(float x, float y)
    {
        _rigidbody.MovePosition(new Vector3(x, y, 0));

    }
    public void DoMovement(float x, float y)
    {
        _rigidbody.MovePosition(transform.position + new Vector3(x, y, 0));
    }

    private void DoShoot()
    {
        Debug.Log("DoShoot");

        if (_shootTimer <= 0)
        {
            OnShoot();
            _shootTimer = 1.0f/_shootRate;
        }
        else
        {
            _shootTimer -= Time.deltaTime;
        }
    }

    private void OnShoot()
    {
        Debug.Log("OnShoot");
        //TODO: Create pool helper to improve performance 
        if (!_bullet) return;
        Debug.Log("_bullet");

        var spaceshipTransform = transform;
        Instantiate(_bullet,spaceshipTransform.position + spaceshipTransform.up, spaceshipTransform.rotation).tag = _bulletTag;
    }
    public void OnTakeDamage(int damage)
    {
        _healthPoints -= damage;
        if (_healthPoints <= 0)
        {
            OnSpaceshipDestruction();
        }
    }

    private void OnSpaceshipDestruction()
    {
        if (_destructionVFX)
        {
            _destructionVFX.SetActive(true);
        }
        else
        {
            Instantiate(Resources.Load("VFX/DefaultExplosion"));
        }
    }
}
