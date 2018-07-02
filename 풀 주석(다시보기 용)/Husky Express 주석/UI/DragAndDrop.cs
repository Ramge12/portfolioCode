using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler {
    //드래그 드랍 이벤트를 사용하기위해 상속받습니다

    public static GameObject itemBeingDragged;              //드래그할 오브젝트
    Vector3 startPosition;                                  //드래그 시작지점
    public Transform startParent;                           //드래그한 오브젝트의 부모 

    public void OnBeginDrag(PointerEventData eventData)     //드래그가 시작될떄
    {
        itemBeingDragged = gameObject;                      //클릭한 오브젝트가 Drag오브젝트가 된다
        startPosition = transform.position;                 //시작 지점을 기록해둡니다
        startParent = transform.parent;                     //시작 오브젝트의 부모를 저장해둡니다
        GetComponent<CanvasGroup>().blocksRaycasts = false; //CanvasGroup의 blocksRaycast를 꺼둡니다
    }
    public void OnDrag(PointerEventData eventData)//드래그하는동안
    {
        transform.position = Input.mousePosition;           //오브젝트의 위치는 마우스의 위치가됩니다.
    }
    public void OnEndDrag(PointerEventData eventData)       //드래그가 끝난순간 Drop은 다른 클래스 slot에서 처리합니다
    {
        itemBeingDragged = null;                            //드래그 오브젝트를 비워줍니다
        GetComponent<CanvasGroup>().blocksRaycasts = true;  //blockRayCast활성화
        if(transform.parent == startParent)                 //오브젝트의 부모가 startParent와 같을떄, startParent는 slot클래스에서 바꾸어줍니다      
        {
            transform.position = startPosition;             //오브젝트 위치를 startPosition으로 하비다
        }
    }
}
