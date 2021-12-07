using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaController : MonoBehaviour
{
    public GameObject map;
    MapPosBasement mapPosBasement;
    Manager.GameMapManager gameMapManager;
    List<GameObject> islandList;
    /// <summary>
    /// �������ĵ�����
    /// </summary>
    List<GameObject> interestIslandList;
    /// <summary>
    /// ��ʼ���򳤿�
    /// </summary>
    public const int START_LENGTH = 3;
    /// <summary>
    /// δ��⵽�浵�ļ�����0��ʼ��
    /// </summary>
    void InitByStart() {
        mapPosBasement = map.GetComponent<MapPosBasement>();
        mapPosBasement.mapWidth = mapPosBasement.mapHeight = START_LENGTH;
        mapPosBasement.ResetMapPos();

    }
}
