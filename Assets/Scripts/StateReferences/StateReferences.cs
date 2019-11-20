using System;
using UnityEngine;

public abstract class StateReferences : MonoBehaviour
{
    [Header("Enable/disable gameobjects when state becomes active/inactive")]
    [SerializeField] private GameObject[] references;
    public GameObject[] GetReferencesOfState
    { 
        get
        {
                return references; 
        }
    }
}
