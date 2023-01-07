using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 8f;
    private string isCarrying = "none"; // the player carrying any pickup or not

    private float pickupTime = 0.5f; // just after picking up an object, player have to wait some time to place it down
    private bool pickupFlag = true;
    private Timer pickupTimer;

    private bool isPushed;
    private Timer pushTimer;
    private float pushDuration = 0.5f;

    private PlayerAnimation animation;

    public bool IsPushed
    {
        get => isPushed;
        set => isPushed = value;
    }
    
    public bool PickupFlag => pickupFlag;

    void Start()
    {
        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        collider2D.radius = GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size

        pickupTimer = gameObject.AddComponent<Timer>();
        pickupTimer.TargetTime = pickupTime;

        pushTimer = gameObject.AddComponent<Timer>();
        pushTimer.TargetTime = pushDuration;

        animation = GetComponent<PlayerAnimation>();
    }

    private bool frameFlag; // prevents multiple inputs taken in a single frame

    void Update()
    {
        Vector3 position = transform.position;

        float input1 = Input.GetAxis("Horizontal");
        float input2 = Input.GetAxis("Vertical");
        float input3 = Input.GetAxis("Fire1");

        if (!frameFlag)
        {
            frameFlag = true;
            if (input1 != 0)
                position.x += input1 * speed * Time.deltaTime;
            if (input2 != 0)
                position.y += input2 * speed * Time.deltaTime;

            if (isCarrying == "none")
            {
                if (input1 > 0)
                    animation.ChangeState("right");
                else if (input1 < 0)
                    animation.ChangeState("left");
                else if (input2 > 0)
                    animation.ChangeState("rear");
                else if (input2 < 0)
                    animation.ChangeState("front");
            }
            else if (isCarrying == "pillar")
            {
                if (input1 > 0)
                    animation.ChangeState("rightPillar");
                else if (input1 < 0)
                    animation.ChangeState("leftPillar");
                else if (input2 > 0)
                    animation.ChangeState("rearPillar");
                else if (input2 < 0)
                    animation.ChangeState("frontPillar");
            }
            else if (isCarrying == "mirror")
            {
                if (input1 > 0)
                    animation.ChangeState("rightBouncer");
                else if (input1 < 0)
                    animation.ChangeState("leftBouncer");
                else if (input2 > 0)
                    animation.ChangeState("rearBouncer");
                else if (input2 < 0)
                    animation.ChangeState("frontBouncer");
            }

            if (input3 > 0 && isCarrying != "none" && pickupFlag)
            {
                print("placed it here!");
                AudioManager.Play("crush");

                GameObject obj = null;
                if (isCarrying == "pillar")
                    obj = GameManager.GetPillar();
                else if (isCarrying == "mirror")
                    obj = GameManager.GetMirror();
                obj.transform.position = transform.position;

                ToggleIsCarrying("none");
            }
        }
        else frameFlag = false;

        transform.position = position;
    }

    public void ToggleIsCarrying(string objectName)
    {
        isCarrying = objectName;
        pickupFlag = !pickupFlag;
        pickupTimer.ScheduleTask(() => { pickupFlag = !pickupFlag; });
    }

    public void PushPlayer()
    {
        if (!isPushed)
        {
            isPushed = true;
            pushTimer.ScheduleTask(() => { isPushed = false; });
        }
    }
}