using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D _rb;
    Vector2 _movement;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat("moveX", _movement.x);
            _animator.SetFloat("moveY", _movement.y);
            _animator.SetBool("moving", true);
        }
        else
        {
            _animator.SetBool("moving", false);
        }

    }

    private void FixedUpdate()
    {
        moveCharacter(_movement);
    }

    void moveCharacter(Vector2 direction)
    {
        _rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
