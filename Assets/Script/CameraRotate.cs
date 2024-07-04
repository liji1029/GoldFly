//Created by wl on 2022
using UnityEngine;

/// <summary>
/// 该脚本挂在BystanderCamera上,单指触摸屏幕控制相机围绕G臂旋转
/// </summary>
public class CameraRotate : MonoBehaviour
{
    [Header("旋转控制")]
    public Transform target;
    private Vector3 offsetPosition;//位置偏移
    public bool isRotatingTouch = false;
    public bool isRotatingMouse = false;
    public float rotateSpeed = 0.1f;
    public float rotateSpeedforMouse = 1f;

    [Header("锁定手指触摸的区域")]
    public float mapWidth = 1920;
    public float mapHight = 1080;
    public bool isInRect = false;

    private void OnEnable()
    {
        offsetPosition = Vector3.zero;
        transform.LookAt(target.position);
        offsetPosition = (transform.position - target.position);
        rotateSpeed = 0.1f;
        mapWidth = 1920;
        mapHight = 1080;
    }

    public bool IsTouchUi(Vector3 pos)
    {
        isInRect = false;
        Vector3 newPos = new Vector3(0, 0, 0);

        if (pos.x < (newPos.x + mapWidth) && pos.x > newPos.x &&
            pos.y < (newPos.y + mapHight) && pos.y > newPos.y)
        {
            isInRect = true;
        }
        else
        {
            isInRect = false;
        }
        return isInRect;
    }

    void Update()
    {
        {
            transform.position = offsetPosition + target.position;
            RotateView();
            RotateViewForMouse();
        }

    }

    void RotateView()
    {
        if (Input.touchCount == 1)
        {
            if (IsTouchUi(Input.GetTouch(0).rawPosition))
            {
                isRotatingTouch = true;
            }
            else
            {
                isRotatingTouch = false;
            }
        }

        if (isRotatingTouch)
        {
            transform.RotateAround(target.position, target.up, rotateSpeed * Input.GetTouch(0).deltaPosition.x);
            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;
            transform.RotateAround(target.position, transform.right, -rotateSpeed * Input.GetTouch(0).deltaPosition.y);//影响的属性有两个 一个是position 一个是rotation

            float x = transform.eulerAngles.x;
            if (x < 10 || x > 80)
            {//当超出范围之后，我们将属性归位原来的，就是让旋转无效 
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }
        offsetPosition = transform.position - target.position;
    }

    void RotateViewForMouse()
    {
        //鼠标
        if (Input.GetMouseButtonDown(0))
        {
            isRotatingMouse = true;
            //DebugTool.Log("鼠标点击了", DebugColor.Green);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotatingMouse = false;
        }

        if (isRotatingMouse)
        {
            transform.RotateAround(target.position, target.up, rotateSpeedforMouse * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(target.position, transform.right, -rotateSpeedforMouse * Input.GetAxis("Mouse Y"));//影响的属性有两个 一个是position 一个是rotation

            float x = transform.eulerAngles.x;
            if (x < 10 || x > 80)
            {//当超出范围之后，我们将属性归位原来的，就是让旋转无效 
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }
        offsetPosition = transform.position - target.position;
    }
}