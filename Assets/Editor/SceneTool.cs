using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class SceneTool : MonoBehaviour{
[MenuItem("Open Scene/Contrast_Room")]
public static void LoadScene0()
{
 LoadScene("Assets/Scenes/Contrast_Room.unity");
}[MenuItem("Open Scene/Level0")]
public static void LoadScene1()
{
 LoadScene("Assets/Scenes/Level0.unity");
}[MenuItem("Open Scene/Level1")]
public static void LoadScene2()
{
 LoadScene("Assets/Scenes/Level1.unity");
}[MenuItem("Open Scene/Level2")]
public static void LoadScene3()
{
 LoadScene("Assets/Scenes/Level2.unity");
}[MenuItem("Open Scene/Level3")]
public static void LoadScene4()
{
 LoadScene("Assets/Scenes/Level3.unity");
}[MenuItem("Open Scene/Map Creator")]
public static void LoadScene5()
{
 LoadScene("Assets/Scenes/Map Creator.unity");
}[MenuItem("Open Scene/Sandbox_Amin")]
public static void LoadScene6()
{
 LoadScene("Assets/Scenes/Sandbox_Amin.unity");
}[MenuItem("Open Scene/Sandbox_Torcu")]
public static void LoadScene7()
{
 LoadScene("Assets/Scenes/Sandbox_Torcu.unity");
}[MenuItem("Open Scene/Start")]
public static void LoadScene8()
{
 LoadScene("Assets/Scenes/Start.unity");
}
public static void LoadScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}