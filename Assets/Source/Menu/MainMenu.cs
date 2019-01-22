using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public List<Button> menuButtons;

    public void LoadMainLevel()
    {
        StartCoroutine(LoadMainLevelEnumerator());
    }

    private IEnumerator LoadMainLevelEnumerator()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
}
