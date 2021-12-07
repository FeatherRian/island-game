using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaController : MonoBehaviour
{
    public GameObject map;
    public GameObject islandObj;
    MapPosBasement mapPosBasement;
    Manager.GameMapManager gameMapManager;
    /// <summary>
    /// �������ĵ�����
    /// </summary>
    List<IslandScript> interestIslandList;
    /// <summary>
    /// ��ʼ���򳤿�
    /// </summary>
    public const int START_LENGTH = 3;
    /// <summary>
    /// δ��⵽�浵�ļ�����0��ʼ��
    /// </summary>
    void InitByStart() {
        gameMapManager = new Manager.GameMapManager(START_LENGTH, START_LENGTH);
        mapPosBasement = map.GetComponent<MapPosBasement>();
        mapPosBasement.mapWidth = mapPosBasement.mapHeight = START_LENGTH;
        mapPosBasement.ResetMapPos();
    }

    private void Start() {
        Manager.InstanceManager.InputInstance.singleTouch += new Manager.ScreenInputEvent(SeaControlTouchEvent);
    }

    public void SeaControlTouchEvent(Vector2 pos) {
        IslandScript tmp = gameMapManager.touchIsland(mapPosBasement.ScreenToMapPoint(pos));
        if (tmp != null) {
            interestIslandList.Add(tmp);
        }
        interestIslandList.RemoveAll(
            delegate(IslandScript island) {
                return !island.isInterestIsland;
            }
        );
        if (interestIslandList.Count >= 3) {
            gameMapManager.MixedIsland(interestIslandList);
        }
    }
}
