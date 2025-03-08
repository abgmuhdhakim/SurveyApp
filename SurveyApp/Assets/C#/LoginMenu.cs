using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginMenu : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                if (response.Length > 0)
                {
                    if (response[0] == '0')
                    {
                        Debug.Log("Login successful");
                        DBManager.username = usernameField.text;

                        // Extract the role
                        string[] parts = response.Split('\t');
                        if (parts.Length > 1)
                        {
                            DBManager.role = parts[1];
                            Debug.Log("Role: " + DBManager.role);
                        }
                        else
                        {
                            Debug.Log("Role not found in response");
                        }
                    }
                    else
                    {
                        Debug.Log("Login failed. Error: " + response);
                    }
                }
                else
                {
                    Debug.Log("Login failed. Empty response");
                }
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (usernameField.text.Length > 0 && passwordField.text.Length > 0);
    }
}
