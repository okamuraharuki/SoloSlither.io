using UnityEngine;

public class MouseVectorReader : MonoBehaviour
{
    static MouseVectorReader _instance;
    public static MouseVectorReader Instance=> _instance;

    private void Awake()
    {
        if(_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public Vector3 GetMousePosVector()
    {
        return new Vector3(Input.mousePosition.x - Screen.width * 0.5f, Input.mousePosition.y - Screen.height * 0.5f).normalized;
    }
}
