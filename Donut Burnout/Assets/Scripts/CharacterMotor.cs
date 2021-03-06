/*********************************************************************
Bachelor of Software Engineering
Media Design School
Auckland
New Zealand
(c) 2022 Media Design School
File Name : CharacterMotor.cs
Description : player movement and stress
Author : Allister Hamilton / David Scott 
Mail : allister.hamilton @mds.ac.nz
**************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 
 Bachelor of Software Engineering
 Media Design School
 Auckland
 New Zealand

 (c) 2022 Media Design School

 File Name    : GameManager.cs
 Description  : holds vitial functions and loading scenes
 Author       : David Scott

*/

public class CharacterMotor : MonoBehaviour
{
    // Headers for the unity editor for visibility
    public static CharacterMotor instance;

    [Header("Attached Components")]
    public CharacterController m_controller;
    public Animator m_animator;
    public MouseLook m_look;

    [Header("Motion Values")]
    public float m_movespeed = 8.0f;
    public float m_gravity = 32.0f;
    public float m_jumpspeed = 10.0f;

    [Header("Current State")]
    public Vector3 m_velocity = new Vector3(0.0f, 0.0f, 0.0f);

    public bool m_grounded = false;

    //Code for Stress Bar

    private Rigidbody _rb;

    public Stress_UI stressBar;
    public float maxStress = 100;
    public float currentStress;

    private void Awake()
    {
        instance = this;

    }
    float doubler = 1;
    private void Start()
    {


        if (PlayerPrefs.GetInt("Debug") == 1)
        {
            List<string> layerMaskNamesList = GameManager.ReturnLayerMaskNames(m_look.GetComponent<Camera>().cullingMask);
            layerMaskNamesList.Add("Debug");
            m_look.GetComponent<Camera>().cullingMask = GameManager.ReturnBitShift(layerMaskNamesList.ToArray());

        }

        //stress Startup
        currentStress = maxStress;
        stressBar.SetMaxStress(currentStress);

        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //More Stress Code
        stressBar.SetStress(currentStress);

        float x = 0.0f;
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 1.0f;
        }

        float z = 0.0f;
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            z += 1.0f;
        }

        if (Input.GetKey(KeyCode.Space) && m_grounded)
        {
            m_velocity.y = m_jumpspeed;
            m_grounded = false;
            GameManager.instance.SoundPool.PlaySound(GameManager.instance.PlayerJumpSound, 1, true, 0, false, transform);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            z += 1.1f;
        }
        currentStress -= doubler * Time.deltaTime;
        doubler += (0.02f * Time.deltaTime);
        Vector3 inputMove = new Vector3(x, 0.0f, z);
        // Making sure that you go where you're looking, changing global z and x to local z and x
        inputMove = Quaternion.Euler(0.0f, m_look.m_Spin, 0.0f) * inputMove;

        m_velocity.x = inputMove.x * m_movespeed;
        m_velocity.y -= m_gravity * Time.deltaTime;
        m_velocity.z = inputMove.z * m_movespeed;

        m_controller.Move(m_velocity * Time.deltaTime);

        m_animator.SetBool("Walk", inputMove != Vector3.zero);

        if (inputMove != Vector3.zero)
            m_animator.transform.rotation = Quaternion.Slerp(m_animator.transform.rotation, Quaternion.LookRotation(m_velocity), Time.deltaTime * 10f);

        if ((m_controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            m_velocity.y = -1.0f;
            m_grounded = true;
        }
        else
        {
            m_grounded = false;
        }

        if ((m_controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            m_velocity.y = -1.0f;
        }

        //Taking Damage + Damage Overtime
    }
    public void DamageStress(int damage)
    {
        currentStress -= damage;
    }

    public void HealStress(int heal)
    {
        currentStress += heal;
        if (currentStress >= maxStress)
            currentStress = maxStress;
    }

}