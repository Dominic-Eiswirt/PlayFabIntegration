using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PasswordField : InputFieldOnEndEdit
{
    public override void OnEnable()
    {
        GetComponent<InputField>().textComponent.text = PlayFabLogin.Password;
    }

    public override void OnEndEdit()
    {
        UICenter.instance.PlayFabUpdatePassword(GetComponent<InputField>().textComponent);
    }

}
