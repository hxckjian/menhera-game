using UnityEngine;

public class SceneBuilderScript : MonoBehaviour
{
    private static bool hasSetResolution = false;

    private void Start()
    {
        // For now commented resolution fixing to see if other features can be expanded
        if (!hasSetResolution)
        {
            Screen.SetResolution(960, 720, false);
            hasSetResolution = true;
            // Debug.Log("Initial resolution set once!");
        }
    }
}
