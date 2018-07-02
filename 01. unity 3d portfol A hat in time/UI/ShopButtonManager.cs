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


    void Start () {
        menu1_sold=false;          
        menu2_sold=false;
        menu3_sold=false;
    }

	void Update () {
        starPoint = Player.GetComponent<PlayerCtr>().StarPoint; 
        if (menu1_sold == true) sold1.SetActive(true);          
        if (menu2_sold == true) sold2.SetActive(true);
        if (menu3_sold == true) sold3.SetActive(true);
        WeaponCheck();                                                     
    }

    public void menu1()
    {
        if(starPoint>=20 && !menu1_sold)   
        {
            menu1_sold = true;                                  
            Player.GetComponent<PlayerCtr>().StarPoint -= 20;         
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
