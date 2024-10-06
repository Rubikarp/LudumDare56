using UnityEngine;
using NaughtyAttributes;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    [BoxGroup("Singleton Settings")]
    protected bool IsDontDestroyOnLoad = false;

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (!IsInstantiated) instance = FindObjectOfType<T>(true);
            if (!IsInstantiated) Instantiate();
            return instance;
        }
    }
    public static void Instantiate()
    {
        if (IsInstantiated) return;

        System.Type type = typeof(T);
        var go = new GameObject(type.Name, type);
        instance = go.GetComponent<T>();
    }
    public static bool IsInstantiated => instance != null;

    protected void MakeSingleton(bool makeUndestroyableOnLoad = false)
    {
        if (instance == null)
        {
            instance = this as T;
            if (makeUndestroyableOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (instance != this)
        {
            Debug.LogWarningFormat("[SingletonMonobehaviour] Instance not null at initialization: there can be only one instance of {0} on the map -> Destroying", this.GetType().ToString());
            Destroy(this.gameObject);
            return;
        }
    }

    protected virtual void Awake()
    {
        MakeSingleton(IsDontDestroyOnLoad);
    }
    protected virtual void OnDestroy()
    {
        instance = null;
    }

}

