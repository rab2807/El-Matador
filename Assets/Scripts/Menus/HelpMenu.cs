﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public void GoBack()
    {
        MenuManager.GoTo("main");
    }
}