using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] float moveSpeed = 50f;
    [SerializeField] GameObject attackBoxCol;
    [SerializeField] HealthBar healthBar;

    //[SerializeField] Transform spawnPoint;
    [SerializeField] GameObject attackArea;

    private Rigidbody rb;
    private Vector3 moveVector = new Vector3();

    private float horizontal;

    private bool isPressJump;
    private bool isPressAttack;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isMoving = false;
    private bool isDeath = false;

    //private int coin = 0;

    private Vector3 savePoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SetSavePoint(transform.position);

/*        coin = PlayerPrefs.GetInt(UIManager.COIN_KEY, 0);
        UIManager.Instance.SetCoinText(coin);*/
    }

    public override void OnInit()
    {
        base.OnInit();

        isDeath = false;
        isAttack = false;
        healthBar.OnInit(maxHealth);

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
        if (isDeath) return;

        isGrounded = CheckOnGround();

        horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("vertical");


        //check falling
        if (isGrounded)
        {
            isJumping = false;

        }

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

        //Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector3(horizontal * moveSpeed, rb.velocity.y, 0);
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
        currentAnim = isMoving ? "move" : "idle";

    }


    private void Idle()
    {
        rb.velocity = Vector3.zero;
        ChangeAnim("idle");
    }

    private void Attack()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        isAttack = true;
        ChangeAnim("attack1");

        ActiveAttack();
        Invoke(nameof(DeactiveAttack), 0.5f);
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
        isDeath = true;
        OnDead();
    }

    private bool CheckOnGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.2f, Color.red);

        RaycastHit hit;

        return Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out hit, 0.4f, groundLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*        if (collision.gameObject.CompareTag("Coin"))
        {
            coin++;
            UIManager.Instance.SetCoinText(coin);
            Destroy(collision.gameObject);
        }*/

        if (collision.gameObject.CompareTag("DeathZone"))
        {
            OnDeath();
        }
    }


    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }

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

    public override void OnHit(float damage)
    {
        base.OnHit(damage);

        healthBar.SetNewHealth(health);
    }

    public override void OnNewGame()
    {
        base.OnNewGame();
        transform.position = savePoint;
    }

    public override void OnDead()
    {
        UIManager.Instance.SwitchToLosePanel();
    }
}
