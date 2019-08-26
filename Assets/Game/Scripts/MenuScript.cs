using Game.Code;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public InputField seedInputField;
    
    public void PlaySeed()
    {
        Statics.Seed = seedInputField.text;
        SceneManager.LoadScene("Ingame");
    }

    public void PlayChallenge()
    {
        Statics.Seed = "EXTRAZOEY";
        SceneManager.LoadScene("Ingame");
    }
}
