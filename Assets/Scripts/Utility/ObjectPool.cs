using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject[] objects;
    private GameObject prefabObject;
    private int num;

    public void Initialize(int num, string name)
    {
        objects = new GameObject[num];
        prefabObject = Resources.Load<GameObject>(name);
        for (int i = 0; i < num; i++)
            objects[i] = GetNewObject();
    }

    private GameObject GetNewObject()
    {
        GameObject obj = Instantiate(prefabObject, Vector3.up, Quaternion.identity);
        obj.SetActive(false);
        return obj;
    }

    public GameObject GetObject()
    {
        foreach (var t in objects)
            if (!t.activeSelf)
            {
                t.SetActive(true);
                return t;
            }

        Debug.Log(prefabObject.name + " not available");
        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}