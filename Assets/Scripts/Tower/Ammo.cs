using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Header("Ammo Params")]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _rb;
    
    [Header("Audio")]
    [SerializeField] private AudioSource _hitAudioSource;
    
    //Private field
    private Vector3 _direction;
    private Enemy _ennemyAttach;
    public Enemy EnnemyAttach { get => _ennemyAttach; set => _ennemyAttach = value; }
    
    
    private void Start()
    {
        _direction = (_ennemyAttach.transform.position - this.transform.position);
        double angle = Math.Atan2(_direction.y,_direction.x) * (180/Math.PI);
        
        transform.rotation = Quaternion.Euler(0,0,(float)angle);
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        Vector3 velocity = _direction.normalized * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(transform.position + velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy en))
        {
            en.TakeDamage(_damage);
            if (_hitAudioSource)
            {
                _hitAudioSource.Play();
            }
            Destroy(this.gameObject);
        }
    }
}
