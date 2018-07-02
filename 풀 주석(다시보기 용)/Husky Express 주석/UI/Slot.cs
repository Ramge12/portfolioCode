using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

    //인벤토리 칸마다 들어있는 slot클래스
	public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                //자식의 개수가 1개라도 있다면 가장 앞에있는 자식을 반환합니다
                return transform.GetChild (0).gameObject;
            }
            return null;    //자식이없으면 null값을 반환
        }
    }
    public void OnDrop(PointerEventData eventData)  //드래그를 마치고 드랍하는 순간
    {
        GameObject parent_tr;                                                   //부모 오브젝트 transform
        parent_tr = DragAndDrop.itemBeingDragged.transform.parent.gameObject;   //드랍위치에 있는 오브젝트를 부모로 둡니다
        DragAndDrop this_Slot;                                                  //만들어준 DragAndDrop
        this_Slot = transform.GetComponentInChildren<DragAndDrop>();            //고른 슬롯에서 dragAndDrop클래스를 가인 자식을 찾아 this_slot에 넣습니다
        this_Slot.transform.SetParent(parent_tr.transform);                     //고른 슬롯의 오브젝트의 부모를 드래그 전의 오브젝트와 바꾸어줍니다
        DragAndDrop.itemBeingDragged.transform.SetParent(transform);            //DragAndDrop의 Parent를 바꾸어줍니다

        // [ㅁ]   (X)  
        /*
         1. ㅁ오브젝트를 드래그해서 X오브젝트에 드랍합니다
         2. ㅁ오브젝트의 부모를 ()로 두고 X의 부모를 []로 두어 위치를 바꿉니다.
         3. 드랍위치에 아무것도 없다면 원래 위치로 돌아오도록합니다
         */
    }
}
