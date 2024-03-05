using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIState : MonoBehaviour
{
    //Activate the UI state
    public abstract void Activate();

    //Deactivate the UI state
    public abstract void Deactivate();
}
