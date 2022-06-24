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
    public bool petDog;
    public bool staffDoor;
    public Animation DoorAnimation;

    public void PlaySoundVoid()
    {
        GameManager.instance.SoundPool.PlaySound(GameManager.instance.PlayerFootStepSound, 0.3f, true, 0, false, transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (petDog)
        {
            if (other.GetComponent<CharacterMotor>())
            {
                CharacterMotor.instance.HealStress(5);

                int barkInt = Random.Range(0, 3);

                if (barkInt == 0)
                    GameManager.instance.SoundPool.PlaySound(GameManager.instance.BarkOneSound, .8f, true, 0, false, transform);

                if (barkInt == 1)
                    GameManager.instance.SoundPool.PlaySound(GameManager.instance.BarkTwoSound, .8f, true, 0, false, transform);

                if (barkInt == 2)
                    GameManager.instance.SoundPool.PlaySound(GameManager.instance.BarkThreeSound, .8f, true, 0, false, transform);
            }
        }
        else if (staffDoor)
        {
            if (other.GetComponent<CharacterMotor>() && !works)
            {
                GameManager.instance.SoundPool.PlaySound(GameManager.instance.DoorOpenSound, .8f, true, 0, false, transform);

                works = true;
                float length = GameManager.AnimationChangeDirection(DoorAnimation, "", false).length;

                CharacterMotor.instance.enabled = false;
                CharacterMotor.instance.m_animator.enabled = false;
                Invoke("DoorCloseSound", length);

            }
        }
        else
        {
            GameManager.instance.SoundPool.PlaySound(GameManager.instance.CustomerEntersSound, 0.1f, true, 0, false, transform);
        }
    }

    void DoorCloseSound()
    {
        GameManager.instance.SoundPool.PlaySound(GameManager.instance.DoorSlamSound, 1f, true, 0, false, transform);

        Invoke("ScreamSound", 1);
    }

    void ScreamSound()
    {
        GameManager.instance.SoundPool.PlaySound(GameManager.instance.ScreamingSound, 1f, true, 0, false, transform);
        CharacterMotor.instance.HealStress(20);
        Invoke("FinishedSound", 6);
    }

    void FinishedSound()
    {
        CharacterMotor.instance.enabled = true;
        CharacterMotor.instance.m_animator.enabled = true;
        GameManager.AnimationChangeDirection(DoorAnimation, "", true);

        GameManager.instance.SoundPool.PlaySound(GameManager.instance.DoorOpenSound, .8f, true, 0, false, transform);

        Invoke("worksAgain", 5);
    }

    bool works;
    void worksAgain()
    {
        works = false;
    }
}
