using UnityEngine;
using System.Linq;

public class Singleton <T>: MonoBehaviour where T: MonoBehaviour
{
    private static T instance;
    
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<T>();
        }
   
        else if (instance != FindObjectOfType<T>())
        {
            Destroy(FindObjectOfType<T>());
        }
        DontDestroyOnLoad(FindObjectOfType<T>());
        return;
        
    }

}

