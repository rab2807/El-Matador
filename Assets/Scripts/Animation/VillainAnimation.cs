using UnityEngine;

public class VillainAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private string state;
    private float interval = 0.2f;
    private float currentTime;
    private Sprite sprite1, sprite2;
    private bool spriteFlag;

    private void Start()
    {
        ChangeState("left");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(string name)
    {
        if (name == "front")
        {
            sprite1 = sprites[0];
            sprite2 = sprites[1];
        }
        else if (name == "rear")
        {
            sprite1 = sprites[6];
            sprite2 = sprites[7];
        }
        else if (name == "right")
        {
            sprite1 = sprites[12];
            sprite2 = sprites[13];
        }
        else if (name == "left")
        {
            sprite1 = sprites[18];
            sprite2 = sprites[19];
        }
        else if (name == "frontAngry")
        {
            sprite1 = sprites[2];
            sprite2 = sprites[3];
        }
        else if (name == "rearAngry")
        {
            sprite1 = sprites[8];
            sprite2 = sprites[9];
        }
        else if (name == "leftAngry")
        {
            sprite1 = sprites[20];
            sprite2 = sprites[21];
        }
        else if (name == "rightAngry")
        {
            sprite1 = sprites[14];
            sprite2 = sprites[15];
        }
        else if (name == "frontBomb")
        {
            sprite1 = sprites[4];
            sprite2 = sprites[5];
        }
        else if (name == "rearBomb")
        {
            sprite1 = sprites[10];
            sprite2 = sprites[11];
        }
        else if (name == "leftBomb")
        {
            sprite1 = sprites[22];
            sprite2 = sprites[23];
        }
        else if (name == "rightBomb")
        {
            sprite1 = sprites[16];
            sprite2 = sprites[17];
        }
        else if (name == "leftCollision")
        {
            sprite1 = sprite2 = sprites[24];
        }
        else if (name == "rightCollision")
        {
            sprite1 = sprite2 = sprites[25];
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= interval)
        {
            spriteRenderer.sprite = spriteFlag ? sprite1 : sprite2;
            spriteFlag = !spriteFlag;
            currentTime = 0;
        }
    }
}