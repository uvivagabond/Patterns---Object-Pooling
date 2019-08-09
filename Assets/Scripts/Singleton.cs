using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T m_Instance;

    public static T SharedInstance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();
                if (m_Instance == null)
                {
                    GameObject gO = new GameObject();
                    gO.name = typeof(T).Name;
                    m_Instance = gO.AddComponent<T>();
                }
            }
            return m_Instance;
        }
    }

    public virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
