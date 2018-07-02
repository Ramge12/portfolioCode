using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler,IPointerUpHandler,IPointerDownHandler {

    private Image bgImg;            //백그라운드 이미지
    private Image joystickImg;      //조이스틱 이미지
    private Vector3 inputVector;    //조이스틱의 벡터
    public CameraCtr mainCam;       //메인카메라


    void Start () {
        bgImg = GetComponent<Image>();                              
        joystickImg = transform.GetChild(0).GetComponent<Image>();  
	}

	void Update () {
	}

    public virtual void OnDrag(PointerEventData ped)//조이스틱 드래그 이벤트
    {
        Vector2 pos;//조이스틱의 화면위 2차원이므로
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position,ped.pressEventCamera,out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);                                      //드래그 포인트를 백그라운드 이미지 크기로나눕니다 값을 (0~1)을 가지도록 합니다
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);                                      //드래그 포인트를 백그라운드 이미지 크기로나눕니다 값을 (0~1)을 가지도록 합니다
            inputVector = new Vector3(pos.x * 2-1, pos.y *2-1 , 0);                                 //음수 부분까지 포현하기 위해 2배를 곱한후 -1을 합니다(-1~1)의값
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;    //InputVector를 단위벡터로 만듭니다(크다면 정규화)
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3)
                , inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));                           //Joystick을 구한 좌표로 이동시킵니다.
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)//화면에 터치를 하는 순간
    {
        mainCam.playerControll = true;      
        OnDrag(ped);
    }

    public virtual void OnPointUp(PointerEventData ped)//화면에서 손을 떼는 순간
    {
        mainCam.playerControll = false;     
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;  //조이스틱 위치를 0으로 두고 초기화합니다
    }

    public float GetHorizontalValue()
    {
        return inputVector.x;
    }

    public float GetVerticalValue()
    {
        return inputVector.y;
    }

}
