using System.Collections;
using UnityEngine;

public enum Direction
{
    IDLE,
    LEFT,
    RIGHT
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private bool _canMoveLeft;
    private bool _canMoveRight;
    private Direction _direction;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private Coroutine _moveCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Play()
    {
        _direction = Direction.IDLE;
        _transform.position = new Vector3(0, 0, -7);
        _canMoveLeft = true;
        _canMoveRight = true;
    }

    public void GameOver()
    {
        MoveStop();
        _canMoveLeft = false;
        _canMoveRight = false;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && _canMoveLeft)
        {
            MoveStop();
            _rigidbody.AddForce(Vector3.left * 1000, ForceMode.Acceleration);
            _direction = Direction.LEFT;
        }

        if (Input.GetKey(KeyCode.RightArrow) && _canMoveRight)
        {
            MoveStop();
            _rigidbody.AddForce(Vector3.right * 1000, ForceMode.Acceleration);
            _direction = Direction.RIGHT;
        }

        if (!Input.GetKey(KeyCode.RightArrow)
            && !Input.GetKey(KeyCode.LeftArrow)
            && _direction != Direction.IDLE)
        {
            MoveStop();
            _direction = Direction.IDLE;
        }
    }

    private void MoveStop()
    {
        _rigidbody.Sleep();
    }
}