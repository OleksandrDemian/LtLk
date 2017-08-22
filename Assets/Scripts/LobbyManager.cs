using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.InputField characterName;

    public void LoadScene(int index)
    {
        PlayerPrefs.SetString("character", characterName.text);
        SceneLoader.LoadScene(index);
    }
}
