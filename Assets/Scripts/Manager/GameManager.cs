using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static ObjectPool pillarPool, mirrorPool, powerupPool;
    private int pillarPoolCapacity = 5;
    private int mirrorPoolCapacity = 5;
    private int powerupPoolCapacity = 5;

    private void Awake()
    {
        // ConfigurationData.GetData();
        ScreenData.Initialize();
            
        pillarPool = gameObject.AddComponent<ObjectPool>();
        pillarPool.Initialize(pillarPoolCapacity, "pillar");

        mirrorPool = gameObject.AddComponent<ObjectPool>();
        mirrorPool.Initialize(mirrorPoolCapacity, "mirror");

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
        return powerupPool.GetObject();
    }

    public static void ReturnPowerUp(GameObject powerUp)
    {
        powerupPool.ReturnObject(powerUp);
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