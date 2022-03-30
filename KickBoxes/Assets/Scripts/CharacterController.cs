using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : Singleton<CharacterController>
{
    private Vector3 prevmousePos;
    private Vector3 currentmousePos;
    private Vector3 deltamousePos;
    public float MovementSpeed;
    public float smoothTime;

    public float xMinLimit;
    public float xMaxLimit;
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;
    public GameObject mainObject;

    void Update()
    {
       
        if (GameManager.Instance.gameStatus == Enums.GameStatus.inGame)
        {
            ForwardMovement();
            if (Input.GetMouseButtonDown(0))
            {
                //UIManager.Instance.Vibration(MoreMountains.NiceVibrations.HapticTypes.SoftImpact);
                currentmousePos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                LeftRightPosition();
                RotationHands();
            }
            if (Input.GetMouseButtonUp(0))
            {
                mainObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                mainObject.transform.position = Vector3.SmoothDamp(mainObject.transform.position, mainObject.transform.position, ref velocity, 0);
            }
        }
        else if (GameManager.Instance.gameStatus == Enums.GameStatus.finish)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed*2);
            mainObject.transform.position = Vector3.Lerp(mainObject.transform.position, new Vector3(0, mainObject.transform.position.y, mainObject.transform.position.z), smoothTime*Time.deltaTime);
            mainObject.transform.localRotation = Quaternion.Slerp(mainObject.transform.localRotation, Quaternion.Euler(0,0,0), Time.deltaTime *10);
        }
    }
    public void ForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);
    }
    public void LeftRightPosition()
    {
        prevmousePos = currentmousePos;
        currentmousePos = Input.mousePosition;
        deltamousePos = currentmousePos - prevmousePos;
        target = new Vector3(deltamousePos.x, 0);
        float changePosition =transform.position.x;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x + target.x / 2, 
            transform.position.y, transform.position.z), ref velocity, smoothTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMinLimit, xMaxLimit), transform.position.y, transform.position.z);
    }
    public void RotationHands()
    {
        var rotYZ = Quaternion.Euler(0, 0, Mathf.Clamp(-deltamousePos.x * Time.deltaTime * 240, -20, 20));
        mainObject.transform.localRotation = Quaternion.Slerp(mainObject.transform.localRotation, rotYZ, Time.deltaTime * 5);
    }
   
}

