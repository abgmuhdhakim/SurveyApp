using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RegisterMenu : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                if (www.downloadHandler.text == "0")
                {
                    Debug.Log("User created successfully");
                }
                else
                {
                    Debug.Log("User creation failed. Error #" + www.downloadHandler.text);
                }
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (usernameField.text.Length > 0 && passwordField.text.Length > 0);
    }
}
