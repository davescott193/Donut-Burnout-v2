/*********************************************************************
Bachelor of Software Engineering
Media Design School
Auckland
New Zealand
(c) 2022 Media Design School
File Name : PlateMagnetics.cs
Description : plate magnetically goes to the player or into the dishes
Author : Allister Hamilton
Mail : allister.hamilton @mds.ac.nz
**************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMagnetic : MonoBehaviour
{
    public int numberInt = 0;
    public bool FinishBool;

    void Update()
    {
        transform.Rotate(new Vector3(50, 50, 0) * Time.deltaTime);
        if (FinishBool)
        {
            transform.position = Vector3.MoveTowards(transform.position, MechanicsManager.instance.PlateSinkTransform.position, 10 * Time.deltaTime);

            if ((int)transform.position.y == (int)MechanicsManager.instance.PlateSinkTransform.position.y)
            {
                GameManager.instance.SoundPool.PlaySound(GameManager.instance.PlateInSinkSound, 0.5f, true, 0, false, MechanicsManager.instance.PlateSinkTransform);
                Destroy(gameObject);
            }
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, CharacterMotor.instance.transform.position + new Vector3(0, ((float)numberInt / 3) + 2f, 0), 10 * Time.deltaTime);
    }
}
