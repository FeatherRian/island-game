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
    [SerializeField]
    private float screenToWorldScale;
    public int mapHeight = 3;
    public int mapWidth = 3;
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
        transform.localScale = new Vector3(screenToWorldScale / mapHeight, screenToWorldScale / mapWidth, 1);
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * widthInScreen, Screen.height * heightInScreen));
        transform.position = new Vector3(transform.position.x+transform.localScale.x*0.5f, transform.position.y+transform.localScale.y*0.5f, 0);
    }

    /// <summary>
    /// ����������ת��Ϊ��ͼ����
    /// </summary>
    /// <param name="pos">��������</param>
    /// <returns>��ͼ���꣬Ϊһ������</returns>
    public Vector2Int WorldToMapPoint(Vector3 pos) {
        pos=transform.InverseTransformPoint(pos);
        pos.x += 0.5f;
        pos.y += 0.5f;
        return new Vector2Int((int)pos.x, (int)pos.y);
    }
    /// <summary>
    /// ����Ļ����ת��Ϊ��ͼ����
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector2Int ScreenToMapPoint(Vector3 pos) {
        return WorldToMapPoint(Camera.main.ScreenToWorldPoint(pos));
    }
}
