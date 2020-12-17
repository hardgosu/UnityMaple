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
    int directionX = -1;

    public GameEnd gameEnd;

    float attackTimer = 0f;

    SimpleCharacterState characterState = SimpleCharacterState.Idle;
    private bool StatWindow_on;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Status>().mySubscribers += YouAreDead;
    }
    private void YouAreDead(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
        gameEnd.gameObject.SetActive(true);
    }
    IEnumerator DamagedSpriteColorAnimation()
    {

        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            yield return null;
            if (timer <= 0.1f)
            {
                Vector4 color = Vector4.zero;

                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = timer / 0.1f;

                GetComponent<SpriteRenderer>().color = color;
            }
            else if (timer <= 0.2f)
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = 1.0f - (timer / 0.2f);

                GetComponent<SpriteRenderer>().color = color;
            }
            else if (timer <= 0.3f)
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = timer / 0.3f;

                GetComponent<SpriteRenderer>().color = color;
            }
            else if (timer <= 0.4f)
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = 1.0f - (timer / 0.4f);

                GetComponent<SpriteRenderer>().color = color;
            }
            else
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = 1.0f;
                GetComponent<SpriteRenderer>().color = color;
                break;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Status>().SubCurrentHP(collision.gameObject.GetComponent<Status>().aTK);
            StartCoroutine(DamagedSpriteColorAnimation());
        }
    }

    public void StartReserveKill(GameObject gameObject, float delay)
    {
        var skill = gameObject.GetComponent<Skill>();
        if (skill != null)
        {
            skill.owner = this.gameObject;
        }

        StartCoroutine(ReserveKill(gameObject, delay));
    }
    IEnumerator ReserveKill(GameObject gameObject, float delay)
    {

        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("c"))
        {
            StatWindow_on = !StatWindow_on;
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
            directionX = 1;
        }
        else if (inputVector.x < 0)
        {
            StartCoroutine(Move());
            GetComponent<SpriteRenderer>().flipX = false;
            directionX = -1;
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

        Vector3 vector3 = Vector3.zero;

        vector3.x = transform.position.x + directionX * 1.2f;
        vector3.y = transform.position.y + 0.3f;
        vector3.z = transform.position.z;


        GameObject rush = KPU.Manager.ObjectPoolManager.Instance.Spawn("Rush", vector3);
        StartReserveKill(rush, 0.3f);

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
