using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPosBasement : MonoBehaviour
{
    [Tooltip("��Χ����0~1")]
    public float heightInScreen = 0.2f;
    [Tooltip("��Χ����0~1")]
    public float widthInScreen = 0;
    public int mapHeight = 3;
    public int mapWidth = 3;
    private void OnEnable() {
        
    }
}
