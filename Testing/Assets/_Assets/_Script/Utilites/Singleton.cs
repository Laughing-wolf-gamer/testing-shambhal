using UnityEngine;

/// <summary>
/// Generic Singleton base class for MonoBehaviour.
/// Set `persistThroughScenes` to true to keep the instance across scenes.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool isShuttingDown = false;
    private static object lockObj = new object();

    [SerializeField]
    private bool persistThroughScenes = false;

    public static T Instance
    {
        get
        {
            if (isShuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit.");
                return null;
            }

            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = (T)FindFirstObjectByType(typeof(T));

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    }

                    // Only persist if explicitly set to
                    var singletonComponent = instance as Singleton<T>;
                    if (singletonComponent != null && singletonComponent.persistThroughScenes)
                    {
                        DontDestroyOnLoad(instance.gameObject);
                    }
                }

                return instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;

            if (persistThroughScenes)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            isShuttingDown = true;
        }
    }
}
