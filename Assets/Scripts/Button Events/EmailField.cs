using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class EmailField : InputFieldOnEndEdit
{
    public override void OnEnable()
    {
        GetComponent<InputField>().textComponent.text = PlayFabLogin.Email;
    }

    public override void OnEndEdit()
    {
        UICenter.instance.PlayFabUpdateEmail(GetComponent<InputField>().textComponent);
    }

}
