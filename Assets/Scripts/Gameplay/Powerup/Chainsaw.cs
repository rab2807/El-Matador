using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    private Timer timer;
    private float interval = 3f;
    private bool isOn;
    private int count = 3;

    void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.TargetTime = interval;
        
        GetComponent<CircleCollider2D>().radius =
            GetComponent<SpriteRenderer>().bounds.size.x / 2; // set collider radius from sprite size
    }

    public void Initiate()
    {
        isOn = false;
        timer.ScheduleTask(() => Toggle());
    }
    
    private float angle;

    private void Update()
    {
        if (isOn)
        {
            transform.Rotate(Vector3.back, 6.0f);

            Vector3 position = transform.position;
            position.y += Mathf.Sin(angle) * 0.005f;
            transform.position = position;
        }

        angle += (Time.deltaTime * 20) % 360.0f;
    }

    private void Toggle()
    {
        isOn = !isOn;
        GetComponent<SpriteRenderer>().color = isOn ? new Color(230, 100, 100) : Color.white;
        timer.ScheduleTask(() => Toggle());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOn)
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                // decrease life
                count--;
                if (count == 0)
                    GameManager.ReturnChainsaw(gameObject);
            }
            else if (other.gameObject.GetComponent<Villain>() != null)
            {
                // decrease life
                count--;
                if (count == 0)
                    GameManager.ReturnChainsaw(gameObject);
            }
        }
    }
}