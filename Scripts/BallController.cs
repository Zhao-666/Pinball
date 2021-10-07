using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public static BallController Instance;

    private const float SpeedDefault = 1f;
    private float _speed = SpeedDefault;
    private bool _ballErr;

    private Transform _transform;
    private AudioClip _pinEffect;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _transform = transform;
        _pinEffect = Resources.Load<AudioClip>("PinEffect");
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.magnitude > 20)
        {
            _rigidbody.velocity *= 0.9f;
        }
    }

    public void Play()
    {
        _speed = SpeedDefault;
        _transform.position = new Vector3(0, 0, -5);
        _rigidbody.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), 0, 1) * 300,
            ForceMode.Acceleration);
    }

    public void GameOver()
    {
        _rigidbody.Sleep();
    }

    private void OnCollisionEnter(Collision other)
    {
        AudioSource.PlayClipAtPoint(_pinEffect, Vector3.zero);
        if (other.gameObject.name == "WallDown")
        {
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag("Brick"))
        {
            CollisionBrick(other);
        }
    }

    private void CollisionBrick(Collision other)
    {
        if (_speed < 1.5f)
        {
            _speed += 0.02f;
            _rigidbody.velocity *= _speed;
        }

        Destroy(other.gameObject);
    }
}