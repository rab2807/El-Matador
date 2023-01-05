using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static ObjectPool pillarPool, mirrorPool, powerupPool, bulletPool, bombPool, chainsawPool;
    private int pillarPoolCapacity = 5;
    private int mirrorPoolCapacity = 5;
    private int powerupPoolCapacity = 4;
    private int bulletPoolCapacity = 25;
    private int bombPoolCapacity = 4;
    private int chainsawPoolCapacity = 4;

    private void Awake()
    {
        // ConfigurationData.GetData();
        ScreenData.Initialize();

        pillarPool = gameObject.AddComponent<ObjectPool>();
        pillarPool.Initialize(pillarPoolCapacity, "pillar");

        mirrorPool = gameObject.AddComponent<ObjectPool>();
        mirrorPool.Initialize(mirrorPoolCapacity, "mirror");

        powerupPool = gameObject.AddComponent<ObjectPool>();
        powerupPool.Initialize(powerupPoolCapacity, "powerup");

        bulletPool = gameObject.AddComponent<ObjectPool>();
        bulletPool.Initialize(bulletPoolCapacity, "bullet");

        bombPool = gameObject.AddComponent<ObjectPool>();
        bombPool.Initialize(bombPoolCapacity, "bomb");
        
        // chainsawPool = gameObject.AddComponent<ObjectPool>();
        // chainsawPool.Initialize(chainsawPoolCapacity, "chainsaw");

        // gameObject.AddComponent<ParticleRenderer>();

        // GameObject ship = Resources.Load<GameObject>("Ship");
        // Instantiate(ship, Vector3.zero, Quaternion.identity);
    }

    public static GameObject GetPillar()
    {
        return pillarPool.GetObject();
    }

    public static void ReturnPillar(GameObject pillar)
    {
        pillarPool.ReturnObject(pillar);
    }

    public static GameObject GetMirror()
    {
        return mirrorPool.GetObject();
    }

    public static void ReturnMirror(GameObject mirror)
    {
        mirrorPool.ReturnObject(mirror);
    }

    public static GameObject GetPowerUp()
    {
        GameObject obj = powerupPool.GetObject();
        int x = Random.Range(0, 3);
        if (x == 0)
            obj.tag = "gun";
        else if (x == 1)
        {
            obj.tag = "bomb";
            obj.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            obj.tag = "chainsaw";
            obj.GetComponent<SpriteRenderer>().color = Color.cyan;
        }

        return obj;
    }

    public static void ReturnPowerUp(GameObject powerUp)
    {
        powerupPool.ReturnObject(powerUp);
    }

    public static GameObject GetBullet()
    {
        return bulletPool.GetObject();
    }

    public static void ReturnBullet(GameObject obj)
    {
        bulletPool.ReturnObject(obj);
    }

    public static GameObject GetBomb()
    {
        return bombPool.GetObject();
    }

    public static void ReturnBomb(GameObject obj)
    {
        bombPool.ReturnObject(obj);
    }

    public static GameObject GetChainsaw()
    {
        return chainsawPool.GetObject();
    }

    public static void ReturnChainsaw(GameObject obj)
    {
        chainsawPool.ReturnObject(obj);
    }

    // public void Pause()
    // {
    //     MenuManager.GoTo(MenuName.Pause);
    // }
    //
    // private void Update()
    // {
    //     if (Input.GetKey(KeyCode.Escape))
    //         MenuManager.GoTo(MenuName.Pause);
    // }
}