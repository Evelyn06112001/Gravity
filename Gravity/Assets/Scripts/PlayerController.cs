using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _legs;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _sprite;
    private bool _isGrounded = false;
    private int _gravity = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Physics2D.gravity = new Vector2(0, -9);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            _gravity = _gravity * -1;
            _sprite.localScale = new Vector3(1, _gravity, 1);
            Physics2D.gravity = new Vector2(0, -9 * _gravity);
        }
        CheckGround();
    }

    void FixedUpdate()
    {
        Move();       
    }
    private void Move()
    {
        float _dir = Input.GetAxis("Horizontal");
        _rb.linearVelocity =  new Vector2(_dir * _speed, _rb.linearVelocityY);
    }
    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce * _gravity, ForceMode2D.Impulse);
    }
    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_legs.position, 0.15f, _groundLayer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
