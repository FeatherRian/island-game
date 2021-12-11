using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapPosBasement : MonoBehaviour
{
    [Tooltip("��Χ����0~1")]
    public float heightInScreen = 0.2f;
    [Tooltip("��Χ����0~1")]
    public float widthInScreen = 0;
    [Tooltip("��ͼ�����Ļխ�����ű���")]
    public float mapToScreenScale=1f;
    [SerializeField]
    private float screenToWorldScale;
    public int mapHeight = 3;
    public int mapWidth = 3;
    public GameObject square;
    public Color clrA = new Color(0xc4, 0xb2, 0x70);
    public Color clrB = new Color(0xf0, 0xe2, 0xac);
    private void OnEnable() {
        ResetMapPos();
    }

    /// <summary>
    /// ���µ�ͼ����
    /// </summary>
    public void ResetMapPos() {
        screenToWorldScale = Mathf.Min(
            Camera.main.ScreenToWorldPoint(
                new Vector3(0, Screen.width)).y 
            - Camera.main.ScreenToWorldPoint(
                new Vector3(0, 0)).y, 
            Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.height,0)).x 
            - Camera.main.ScreenToWorldPoint(
                new Vector3(0, 0)).x);
        screenToWorldScale *= mapToScreenScale;
        transform.localScale = new Vector3(screenToWorldScale / mapHeight, screenToWorldScale / mapWidth, 1);
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * widthInScreen, Screen.height * heightInScreen));
        transform.position = new Vector3(transform.position.x+transform.localScale.x*0.5f, transform.position.y+transform.localScale.y*0.5f, 0);
    }

    public void SpawnSquare() {
        for(int i = 0; i < mapWidth; ++i) {
            for(int r = 0; r < mapHeight; ++r) {
                GameObject tmp = GameObject.Instantiate(square);
                tmp.transform.parent = transform;
                tmp.transform.localPosition =new Vector3(i, r, 1);
                tmp.transform.localScale = Vector3.one;
                tmp.GetComponent<SpriteRenderer>().color = (i + r) % 2 == 1 ? clrA : clrB;
            }
        }
    }

    /// <summary>
    /// ����������ת��Ϊ��ͼ����
    /// </summary>
    /// <param name="pos">��������</param>
    /// <returns>��ͼ���꣬Ϊһ������</returns>
    public Vector2Int WorldToMapPoint(Vector3 pos) {
        //print("World"+pos);
        pos=transform.InverseTransformPoint(pos);
        //print("Local" + pos);
        pos.x += 0.5f;
        pos.y += 0.5f;
        //print("Final"+new Vector2Int((int)pos.x, (int)pos.y));
        return new Vector2Int((int)Mathf.Floor(pos.x), (int)Mathf.Floor(pos.y));
    }
    /// <summary>
    /// ����Ļ����ת��Ϊ��ͼ����
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector2Int ScreenToMapPoint(Vector3 pos) {
        //print("Screen"+pos);
        return WorldToMapPoint(Camera.main.ScreenToWorldPoint(pos));
    }
}
