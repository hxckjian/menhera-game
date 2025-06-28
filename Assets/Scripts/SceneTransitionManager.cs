// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SceneTransitionManager : MonoBehaviour
// {
//     public GameObject objectToDestroyBeforeLoad;

//     public void LoadSceneAndDestroy(string sceneName)
//     {
//         // Destroy this GameObject (the Start Canvas or a wrapper parent)
//         if (objectToDestroyBeforeLoad != null)
//         {
//             Destroy(objectToDestroyBeforeLoad);
//         }

//         // Load next scene
//         SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneTransitionManager : MonoBehaviour
{
    [Tooltip("List of objects to destroy before loading the next scene")]
    public List<GameObject> objectsToDestroy = new List<GameObject>();

    public void LoadSceneAndDestroy(string sceneName)
    {
        // Destroy each object in the list
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // Load the next scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Optional helper if you want to add objects dynamically in code
    public void AddObjectToDestroy(GameObject obj)
    {
        if (obj != null && !objectsToDestroy.Contains(obj))
        {
            objectsToDestroy.Add(obj);
        }
    }
}
