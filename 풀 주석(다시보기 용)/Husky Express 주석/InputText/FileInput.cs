using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileInput : MonoBehaviour {

    FileStream f;           //파일 입출력에 사용할 FileStream
    string source = "";     //경로를 적을 string, 읽을때 데이터를 저장할 string

    // Use this for initialization
    void Start () {
    }
    // Update is called once per frame
    void Update () {
	}
    public List<Vector3> ReadData(int n)//플레이어 정보에 따라 읽는 데이터를 바꿉니다
    {
        if (n == 1)
        {
            return ReadData1(); //Player1인경우
        }
        else if (n == 2)
        {
            return ReadData2(); //Player2인경우
        }
        else
        {
            return null;
        }
    }
    public List<Vector3> ReadData1()//Player1의 정보를 읽는 함수
    {
        List<Vector3> player_position = new List<Vector3>();    //포지션(벡터)를 담을 배열(리스트)를 만듭니다
        StreamReader sr;                                        //파일을 읽기위한 streamReader
        sr = new StreamReader(Application.dataPath + "/StreamingAssets" + "/" + "Replay_player1.txt");//파일 경로를 입력합니다
        while ((source = sr.ReadLine()) != null)//모든 라인을 다읽습니다
        {
            if (source == "Position")   //Position이라는 단어가 있으면
            {
                Vector3 dummy;
                source = sr.ReadLine();
                dummy.x = float.Parse(source);
                source = sr.ReadLine();
                dummy.y = float.Parse(source);
                source = sr.ReadLine();
                dummy.z = float.Parse(source);
                player_position.Add(dummy); //아래 라인을 읽어 벡터로 만들어 리스트에 추가합니다
            }
        }
        sr.Close();
        return player_position;     //Plaer1포지션을 반환합니다
    }
    public List<Vector3> ReadData2()//Player1과 동일합니다 Player2이 리플레이 데이터를 읽습니다
    {
        List<Vector3> player_position = new List<Vector3>();//포지션(벡터)를 담을 배열(리스트)를 만듭니다
        StreamReader sr;                                    //파일을 읽기위한 streamReader
        sr = new StreamReader(Application.dataPath + "/StreamingAssets" + "/" + "Replay_player2.txt");//파일 경로를 입력합니다
        while ((source = sr.ReadLine()) != null)//모든 라인을 다읽습니다
        {
            if (source == "Position")//Position이라는 단어가 있으면
            {
                Vector3 dummy;
                source = sr.ReadLine();
                dummy.x = float.Parse(source);
                source = sr.ReadLine();
                dummy.y = float.Parse(source);
                source = sr.ReadLine();
                dummy.z = float.Parse(source);
                player_position.Add(dummy); //아래 라인을 읽어 벡터로 만들어 리스트에 추가합니다
            }
        }
        sr.Close();
        return player_position;
    }
    public List<float> ReadData_RO(int n)//회전값을 읽을 플레이어를 구분합니다
    {
        if (n == 1) return ReadData1_RO();      //플레이어1의 회전값을 읽을 함수
        else if (n == 2) return ReadData2_RO(); //플레이어2의 회전값을 읽을 함수
        else return null;
    }
    public List<float> ReadData1_RO()//Player1의 회전값을 읽는 함수
    {
        List<float> player_rotation = new List<float>();//플레이어 회전값을 담을 리스트를 만듭니다
        StreamReader sr;
        sr = new StreamReader(Application.dataPath + "/StreamingAssets" + "/" + "Replay_player1.txt");
        while ((source = sr.ReadLine()) != null)
        {
            if (source == "Roation")
            {
                float dummy;
                source = sr.ReadLine();
                dummy= float.Parse(source);
                player_rotation.Add(dummy); //플레이어의 회전값을 리스트에 추가합니다
            }
        }
        sr.Close();
        return player_rotation;
    }
    public List<float> ReadData2_RO()//Player2의 회전값을 읽는 함수
    {
        List<float> player_rotation = new List<float>();//플레이어 회전값을 담을 리스트를 만듭니다
        StreamReader sr;
        sr = new StreamReader(Application.dataPath + "/StreamingAssets" + "/" + "Replay_player1.txt");
        while ((source = sr.ReadLine()) != null)
        {
            if (source == "Roation")
            {
                float dummy;
                source = sr.ReadLine();
                dummy = float.Parse(source);
                player_rotation.Add(dummy+180);//플레이어의 회전값을 리스트에 추가합니다(Player2의 펭귄은 fbx파일 자체가 180도 회전되어 있어 보정값을 넣어줍니다)
            }
        }
        sr.Close();
        return player_rotation;
    }
    public void Write_Player_Position(int ReplayCount, List<Vector3> PlayerPosition,List<float> rotation_y, int n)
    //리플레이 데이터의 개수, 플레이어 포지션 리스트, Y축 로테이션 리스트, 플레이어 구분 번호
    {
        if (n == 1)
        {
            f = new FileStream(Application.dataPath + "/StreamingAssets" + "/" + "Replay_player1.txt", FileMode.Create, FileAccess.Write);
        }
        if (n == 2)
        {
            f = new FileStream(Application.dataPath + "/StreamingAssets" + "/" + "Replay_player2.txt", FileMode.Create, FileAccess.Write);
        }
        //구분 번호로 구분하여 저장할 텍스트 파일을 정합니다.

        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine("ReplayCount_position");
        writer.WriteLine(ReplayCount.ToString());//총 리플레이 데이터 카운트
        for (int i = 0; i < ReplayCount; i++)
        {
            writer.WriteLine("Position");
            writer.WriteLine(PlayerPosition[i].x);
            writer.WriteLine(PlayerPosition[i].y);
            writer.WriteLine(PlayerPosition[i].z);//postion인걸 구분하기위해 position을 작성하고 벡터값을 저장합니다
            writer.WriteLine("Roation");
            writer.WriteLine(rotation_y[i]);//rotation인것을 구분하기위해 rotation을 쓰고 저장합니다
        }
        writer.Close();
    }
    public void Write_score(float timer)
    {
        //먼저 읽고
        List<float> m_RecordS = new List<float>();
        float m_record;
        StreamReader sr;
        sr = new StreamReader(Application.dataPath + "/StreamingAssets" + "/" + "ScoreRecord.txt");
        while ((source = sr.ReadLine()) != null)
        {
           source = sr.ReadLine();
           m_record = float.Parse(source);
           m_RecordS.Add(m_record); //score값을 가져옵니다
        }
        sr.Close();
        //쓰고
        if (m_RecordS.Count == 0)m_RecordS.Add(timer);
        for (int i = 0; i < m_RecordS.Count; i++)
        {
            if (timer < m_RecordS[i])
            {
                m_RecordS.Insert(i,timer);
                break;
            }
            if(i==(m_RecordS.Count-1))
            {
                m_RecordS.Add(timer);
                break;
            }
        }
        //기존의 점수를 리스트로 가져와서 새 점수를 리스트에 추가후 txt파일에 새로 기록합니다
        f = new FileStream(Application.dataPath + "/StreamingAssets" + "/" + "ScoreRecord.txt", FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        for (int i = 0; i < m_RecordS.Count; i++)writer.WriteLine(m_RecordS[i].ToString());
        writer.Close();
    }
}
