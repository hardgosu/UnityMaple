using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum SimpleCharacterState
{
    Idle,
    Jump,
    Walk,
    Attack
}


public class PlayerController : MonoBehaviour, @PlayerControl.IWarriorActions
{

    PlayerControl inputAction;
    Vector2 inputVector;
    bool isGrounded = true;
    public float gravity = 9.8f;
    public Transform feetPosition;
    public LayerMask layerMask;

    public float jumpPower = 100.0f;
    public float moveSpeed = 2.0f;
    public float height = 0;
    float fallingSpeed = 0;


    float attackTimer = 0f;

    SimpleCharacterState characterState = SimpleCharacterState.Idle;
    private bool StatWindow_on;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //height = other.transform.position.y + other.size
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("c"))
        {
            StatWindow_on =! StatWindow_on;
            GameObject.Find("Stat_Canvas").transform.GetChild(0).gameObject.SetActive(StatWindow_on);
        }


        transform.Translate(transform.right * inputVector.x * Time.deltaTime * moveSpeed);





    }
    void OnEnable()
    {
        if (inputAction == null)
        {
            inputAction = new PlayerControl();
        }
        inputAction.Warrior.SetCallbacks(this);
        inputAction.Warrior.Enable();
    }

    void OnDisable()
    {
        inputAction.Disable();

    }

    public void OnMove(InputAction.CallbackContext context)
    {



        //print(context.ReadValue<Vector2>());
        inputVector = context.ReadValue<Vector2>();

        if (inputVector.x > 0)
        {
            StartCoroutine(Move());
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (inputVector.x < 0)
        {
            StartCoroutine(Move());
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            StartCoroutine(Idle());
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {

        if (characterState == SimpleCharacterState.Attack)
        {
            return;
        }

        bool jumpButtonDown = context.ReadValueAsButton();
        if (jumpButtonDown)
        {
            characterState = SimpleCharacterState.Jump;
        }
        if (jumpButtonDown && GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower));
        }

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (characterState == SimpleCharacterState.Attack)
        {
            return;
        }

        bool attackButtonDown = context.ReadValueAsButton();
        if (attackButtonDown)
        {
            StartCoroutine(Attack());
        }


    }
    IEnumerator Move()
    {
        characterState = SimpleCharacterState.Walk;
        GetComponent<Animator>().PlayInFixedTime("move", 0, 0);
        yield return null;

    }

    IEnumerator Idle()
    {
        characterState = SimpleCharacterState.Idle;
        GetComponent<Animator>().PlayInFixedTime("stand", 0, 0);
        yield return null;
    }

    IEnumerator Attack()
    {

        //inputVector = Vector2.zero;
        characterState = SimpleCharacterState.Attack;

        GetComponent<Animator>().PlayInFixedTime("attack_1", 0, 0);





        yield return new WaitForSeconds(0.25f);
        if (inputVector == Vector2.zero)
        {
            StartCoroutine(Idle());
        }
        else
        {
            StartCoroutine(Move());
        }
    }
}
