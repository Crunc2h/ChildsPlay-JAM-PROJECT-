using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Vector2 _movement;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource _walkingSound = null;
    private bool _isMoving = false;
    private bool _walkAudioPlaying = false;
    private Direction direction = Direction.Right;
    private Animator _anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        
        if(_movement.x != 0 || _movement.y != 0)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Direction.Up;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Direction.Down;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Direction.Left;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Direction.Right;
        }

        _anim.SetInteger("direction", (int)direction);
        _anim.SetBool("isMoving", _isMoving);

        if(_isMoving && !_walkAudioPlaying)
        {
            _walkAudioPlaying = true;
            _walkingSound.Play();
        }
        else if(!_isMoving && _walkAudioPlaying)
        {
            _walkAudioPlaying = false;
            _walkingSound.Pause();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }


}
