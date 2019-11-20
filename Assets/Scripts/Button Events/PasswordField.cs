using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PasswordField : InputFieldOnEndEdit
{
    public override void OnEnable()
    {
        InputField inputField = GetComponent<InputField>();
        inputField.textComponent.text = PlayFabLogin.Password;
        if (inputField.textComponent.text.Length > 0)
        {
            inputField.text = inputField.textComponent.text;
        }        
    }

    public override void OnEndEdit()
    {
        UICenter.instance.PlayFabUpdatePassword(GetComponent<InputField>().textComponent);
    }

}
