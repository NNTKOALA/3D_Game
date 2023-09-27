using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] float moveSpeed = 50f;

    //[SerializeField] Transform spawnPoint;
    //[SerializeField] GameObject attackArea;

    private Rigidbody rb;
    private Vector3 moveVector = new Vector3();

    private bool isPressJump;
    private bool isPressAttack;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isMoving = false;
    private bool isDead = false;

    //private int coin = 0;

    private Vector3 savePoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetSavePoint(transform.position);

/*        UIManager.Instance.onAttackButtonDown += UIManager_onAttackButtonDown;
        UIManager.Instance.onJumpButtonDown += UIManager_onJumpButtonDown;
        UIManager.Instance.onThrownButtonDown += UIManager_onThrowButtonDown;

        coin = PlayerPrefs.GetInt(UIManager.COIN_KEY, 0);
        UIManager.Instance.SetCoinText(coin);*/
    }

    public override void OnInit()
    {
        base.OnInit();

        isDead = false;
        isAttack = false;

        transform.position = savePoint;
        ChangeAnim("idle");
        //DeactiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        OnInit();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        //moveVector.x = Input.GetAxisRaw("Horizontal");
        isGrounded = CheckOnGround();

        if (isGrounded)
        {
            if (isAttack || isJumping)
            {
                return;
            }

            if (moveVector != Vector3.zero)
            {
                Move(1f); // fully control when on the ground
            }
            else
            {
                Idle();
            }

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
        else
        {
            if (isPressAttack)
            {
                isPressAttack = false;
            }

            if (rb.velocity.y < 0.1f)
            {
                isJumping = false;
            }
        }
    }


    private void Idle()
    {
        rb.velocity = Vector3.zero;
        ChangeAnim("idle");
    }

    private void Move(float controlFraction)
    {
        if (moveVector.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = new Vector3(moveVector.x * moveSpeed * controlFraction, rb.velocity.y, 0f);

        if (controlFraction < 1f)
        {
            return;
        }
        anim.SetBool("isMoving", isMoving);
        currentAnim = isMoving ? "move" : "idle";
    }



    private void Attack()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        isAttack = true;
        ChangeAnim("attack1");

/*        ActiveAttack();
        Invoke(nameof(DeactiveAttack), 0.5f);*/
    }

    private void SpeacialAttack()
    {
        rb.velocity = Vector3.zero;
        ChangeAnim("attack4");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Jump()
    {
        isGrounded = false;
        isJumping = true;
        rb.AddForce(Vector3.up * jumpForce);
        ChangeAnim("jump");
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        isDead = true;
    }

    private bool CheckOnGround()
    {
        RaycastHit hit;

        return Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out hit, 0.4f, groundLayer);
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coin++;
            UIManager.Instance.SetCoinText(coin);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("DeathZone"))
        {
            OnDeath();
        }
    }*/

/*    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }*/

/*    private void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }*/

    public void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("idle");
    }

    public void SetSavePoint(Vector3 position)
    {
        savePoint = position;
    }

    public void SetMove(float horizontal)
    {
        moveVector.x = horizontal;
    }
}
