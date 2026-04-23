using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;
    public float jumpForce = 450;//Jump

    private Rigidbody2D _rb;
    private float _moveInputValue; // A = (-1) , D = 1
    private bool _isGrounded;
    private SpriteRenderer _spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null)
        {
            _moveInputValue = (Keyboard.current.dKey.isPressed ? 1 : 0)
                              - (Keyboard.current.aKey.isPressed ? 1 : 0);
        }
        _rb.linearVelocity = new Vector2(_moveInputValue * speed, _rb.linearVelocity.y);
        if (_moveInputValue < 0) { _spriteRenderer.flipX = true; }
        if (_moveInputValue > 0) { _spriteRenderer.flipX = false; }


        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded) 
        {
            _rb.AddForce(new Vector2(_rb.linearVelocity.x, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
