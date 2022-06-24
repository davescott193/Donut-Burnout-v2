/*********************************************************************
Bachelor of Software Engineering
Media Design School
Auckland
New Zealand
(c) 2022 Media Design School
File Name : StepSound.cs
Description : plays a sound when agent walks through door or if player animation event triggers
Author : Allister Hamilton
Mail : allister.hamilton @mds.ac.nz
**************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    public void PlaySoundVoid()
    {
        GameManager.instance.SoundPool.PlaySound(GameManager.instance.PlayerFootStepSound, 0.3f, true, 0, false, transform);
    }

    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.SoundPool.PlaySound(GameManager.instance.CustomerEntersSound, 0.1f, true, 0, false, transform);
    }
}
