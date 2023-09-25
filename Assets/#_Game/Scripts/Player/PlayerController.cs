using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 350;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;

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

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            //Attack
            if (Input.GetMouseButtonDown(0) && isGrounded)
            {
                Attack();
            }
            else
            {
                ResetAttack();
            }

            if (Input.GetMouseButtonDown(1) && isGrounded)
            {
                ChangeGunMode();
            }

            //Thorw
            if (Input.GetKeyDown(KeyCode.R) && isGrounded)
            {
                Reload();
            }
        }

        //check falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            isJumping = false;
        }

        //Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector3(horizontal * Time.deltaTime * speed, rb.velocity.y, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 90 : -90, 0));
            //transform.localScale = new Vector3(horizontal, 1, 1);
        }
        else if (isGrounded)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }

    private bool CheckGrounded()
    {
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.5f, Color.red);

        RaycastHit hit;

        return Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, groundLayer);
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
        ChangeAnim("shoot");
        ChangeAnim("autoshot");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Reload()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("reload");
        isAttack = false;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void ResetAttack()
    {
        ChangeAnim("ilde");
        isAttack = false;
    }

    private void ChangeGunMode()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("singleshot");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
}
