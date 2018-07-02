using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonManager : MonoBehaviour {

    public GameObject Player;       //플레이어
    public GameObject sold1;        //1번상품 품절 마크
    public GameObject sold2;        //2번상품 품절 마크
    public GameObject sold3;        //3번상품 품절 마크
    public GameObject umberella;    //플레이어 1번 무기 우산
    public GameObject fishingRob;   //플레이어 교체 무기 낚시대

    int starPoint;                  //플레이어의 별획득 점수
    bool menu1_sold;                //1번 상품 품절여부
    bool menu2_sold;                //2번 상품 품절여부
    bool menu3_sold;                //3번 상품 품절여부

    // Use this for initialization
    void Start () {
        menu1_sold=false;           //모든 상품 품절 초기화
        menu2_sold=false;
        menu3_sold=false;
    }
	// Update is called once per frame
	void Update () {
        starPoint = Player.GetComponent<PlayerCtr>().StarPoint; //별획득점수는 플레이어 스크립트에서 가져온다
        if (menu1_sold == true) sold1.SetActive(true);          //물건이 팔리면 품절마크를 달아둔다
        if (menu2_sold == true) sold2.SetActive(true);
        if (menu3_sold == true) sold3.SetActive(true);
        WeaponCheck();                                          //조건이 갖춰지면 무기를 바꾸어준다
    }
    public void menu1()
    {
        if(starPoint>=20 && !menu1_sold)    //플레이어가 가진 별 획득 점수가 메뉴의 가격보다 더 있을 경우에만 구매가능
        {
            menu1_sold = true;                                  //품절 여부 체크
            Player.GetComponent<PlayerCtr>().StarPoint -= 20;   //가격만큼 차감한다
        }
    }
    public void menu2()
    {
        if (starPoint >= 30 && !menu2_sold)
        {
            menu2_sold = true;
            Player.GetComponent<PlayerCtr>().StarPoint -= 30;
        }
    }
    public void menu3()
    {
        if (starPoint >= 50&& !menu3_sold)
        {
            menu3_sold = true;
            Player.GetComponent<PlayerCtr>().StarPoint -= 50;
        }
    }
    void WeaponCheck()
    {
        if(menu3_sold==true && menu1_sold==true && menu2_sold==true)
        {
            umberella.SetActive(false); //모든 물건을 다 구매하면 무기를 우산에서 낚시대로 바꾸어준다
            fishingRob.SetActive(true);
        }
    }
}
