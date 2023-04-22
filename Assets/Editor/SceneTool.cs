using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class SceneTool : MonoBehaviour{
    [MenuItem("Open Scene/Contrast_Room")]
    public static void LoadScene0()
    {
     LoadScene("Assets/Scenes/Contrast_Room.unity");
    }[MenuItem("Open Scene/Map Creator")]
    public static void LoadScene1()
    {
     LoadScene("Assets/Scenes/Map Creator.unity");
    }[MenuItem("Open Scene/Start")]
    public static void LoadScene2()
    {
     LoadScene("Assets/Scenes/Start.unity");
    }[MenuItem("Open Scene//Levels/Level0")]
    public static void LoadScene3()
    {
     LoadScene("Assets/Scenes/Levels/Level0.unity");
    }[MenuItem("Open Scene//Levels/Level1")]
    public static void LoadScene4()
    {
     LoadScene("Assets/Scenes/Levels/Level1.unity");
    }[MenuItem("Open Scene//Levels/Level2")]
    public static void LoadScene5()
    {
     LoadScene("Assets/Scenes/Levels/Level2.unity");
    }[MenuItem("Open Scene//Levels/Level3")]
    public static void LoadScene6()
    {
     LoadScene("Assets/Scenes/Levels/Level3.unity");
    }[MenuItem("Open Scene//Sandbox/Sandbox_Amin")]
    public static void LoadScene7()
    {
     LoadScene("Assets/Scenes/Sandbox/Sandbox_Amin.unity");
    }[MenuItem("Open Scene//Sandbox/Sandbox_Torcu")]
    public static void LoadScene8()
    {
     LoadScene("Assets/Scenes/Sandbox/Sandbox_Torcu.unity");
    }
    public static void LoadScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
        }
    }

    [MenuItem("Test Scene/Level0")]
    public static void TestScene0()
    {
        PlayerPrefs.SetString("LevelToTest", "Level0");
        LoadScene("Assets/Scenes/LevelTesting.unity");
    }

    [MenuItem("Test Scene/Level1")]
    public static void TestScene1()
    {
        PlayerPrefs.SetString("LevelToTest", "Level1");
        LoadScene("Assets/Scenes/LevelTesting.unity");

    }

    [MenuItem("Test Scene/Level2")]
    public static void TestScene2()
    {
        PlayerPrefs.SetString("LevelToTest", "Level2");
        LoadScene("Assets/Scenes/LevelTesting.unity");

    }

    [MenuItem("Test Scene/Level3")]
    public static void TestScene3()
    {
        PlayerPrefs.SetString("LevelToTest", "Level3");
        LoadScene("Assets/Scenes/LevelTesting.unity");

    }

    [MenuItem("Test Scene/Level4")]
    public static void TestScene4()
    {
        PlayerPrefs.SetString("LevelToTest", "Level4");
        LoadScene("Assets/Scenes/LevelTesting.unity");

    }
}

