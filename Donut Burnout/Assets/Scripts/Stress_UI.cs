/*********************************************************************
Bachelor of Software Engineering
Media Design School
Auckland
New Zealand
(c) 2022 Media Design School
File Name : Prompt.cs
Description : Holds stress variables and sets the bar
Author : Fetu'u Mafile'o / Allister Hamilton
Mail : allister.hamilton@mds.ac.nz
**************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stress_UI : MonoBehaviour
{
    public Scrollbar scrollbar;
    public Gradient gradient;
    public Image fill;
    float maxStress;
    public void SetMaxStress(float stress)
    {
        scrollbar.size = stress;
        maxStress = stress;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetStress(float stress)
    {
        scrollbar.size = stress / maxStress;
        //  Debug.Log((stress / maxStress) + " " + maxStress);
        fill.color = gradient.Evaluate(scrollbar.size);
    }

}
