/*********************************************************************
Bachelor of Software Engineering
Media Design School
Auckland
New Zealand
(c) 2022 Media Design School
File Name : PictureMechanics.cs
Description : Creates the donut image UI by creating a prefab outside of the scene with a camera and projecting it into a render texture
Author : Allister Hamilton
Mail : allister.hamilton @mds.ac.nz
**************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureMechanics : MonoBehaviour
{
    public Camera ActiveCamera;

    public CustomRenderTexture ActiveCustomRenderTexture;

    public Transform ReturnChildTransform()
    {
        return transform.childCount > 1 ? transform.GetChild(1) : null;
    }

    private void Update()
    {
        ReturnChildTransform().Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }

    public Texture ReturnPicture(bool realtimeBool = false, float cameraShiftFloat = 1)
    {
        int resolutionInt = 600;

        CustomRenderTexture activeRenderTexture = ActiveCustomRenderTexture ? ActiveCustomRenderTexture : new CustomRenderTexture(resolutionInt, resolutionInt);
        activeRenderTexture.initializationColor = new Color(0, 0, 0, 0);

        ActiveCamera.enabled = true;

        if (realtimeBool)
        {
            activeRenderTexture.updateMode = CustomRenderTextureUpdateMode.Realtime;
            activeRenderTexture.doubleBuffered = true;
        }
        else
        {
            activeRenderTexture.updateMode = CustomRenderTextureUpdateMode.OnDemand;
        }

        // CalculateCameraToObject(ActiveCamera, ReturnChildTransform(), cameraShiftFloat);

        ActiveCamera.targetTexture = activeRenderTexture;
        ActiveCamera.Render();

        if (!realtimeBool)
        {
            ActiveCamera.enabled = false;
        }

        ActiveCustomRenderTexture = activeRenderTexture;
        //LoadManager.singleton.GlobalCustomRenderTextureList.Add(ActiveCustomRenderTexture);
        //  Destroy(ActiveCustomRenderTexture, 1);
        return ActiveCamera.targetTexture;
    }
    private void OnDestroy()
    {
        if (ActiveCustomRenderTexture && GameManager.instance)
        {
            Destroy(ActiveCustomRenderTexture);
        }
    }
}
