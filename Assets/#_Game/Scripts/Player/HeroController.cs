using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 350;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isMoving = false;

    private Vector3 startPos;
    private float yPos;

    private float horizontal;

    private string currentAnimName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGrounded();

        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("vertical");

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            else if (Input.GetMouseButtonDown(0) && isGrounded)
            {
                Attack();
            }
            else if (Input.GetKeyDown(KeyCode.E) && isGrounded)
            {
                SpeacialAttack();
            }
        }

        //check falling
        if (isGrounded)
        {
            isJumping = false;
            
        }

        //Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector3(horizontal * Time.deltaTime * speed, rb.velocity.y, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 90 : -90, 0));
            isMoving = true;
            
            //transform.localScale = new Vector3(horizontal, 1, 1);
        }
        else if (isGrounded)
        {
            isMoving = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        anim.SetBool("isMoving", isMoving);
        currentAnimName = isMoving ? "move" : "idle";
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.2f, Color.red);

        RaycastHit hit;

        return Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out hit, 0.4f, groundLayer);
    }

    private void SetIdle()
    {
        ChangeAnim("idle");   
    }

    private void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector3.up);
    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("attack1");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }

    private void SpeacialAttack()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("attack4");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
}