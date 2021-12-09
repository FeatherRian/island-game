using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaController : MonoBehaviour
{
    public GameObject gameMap;
    public GameObject islandObj;
    public GameObject islandObjInHand;
    MapPosBasement mapPosBasement;
    Manager.GameMapManager gameMapManager;
    /// <summary>
    /// �������ĵ�����
    /// </summary>
    List<IslandScript> interestIslandList=new List<IslandScript>();
    /// <summary>
    /// ��ʼ���򳤿�
    /// </summary>
    public const int START_LENGTH = 3;
    /// <summary>
    /// δ��⵽�浵�ļ�����0��ʼ��
    /// </summary>
    void InitByStart() {
        gameMapManager = new Manager.GameMapManager(START_LENGTH, START_LENGTH);
        mapPosBasement = gameMap.GetComponent<MapPosBasement>();
        mapPosBasement.mapWidth = mapPosBasement.mapHeight = START_LENGTH;
        mapPosBasement.ResetMapPos();
    }

    void InitBySave() {
        SaveDate sd = Saver.saveDate;
        gameMapManager = new Manager.GameMapManager(sd.seaWidth, sd.seaHeight);
        mapPosBasement = gameMap.GetComponent<MapPosBasement>();


    }

    private void Start() {
        Manager.InstanceManager.InputInstance.singleTouch += new Manager.ScreenInputEvent(SeaControlTouchEvent);
        if (GameStateManager.instance.isLoadDate) {

        }
        else {
            InitByStart();
        }
        
    }

    private void Update() {
        if (islandObjInHand == null) {
            islandObjInHand = Instantiate(islandObj);
            islandObjInHand.transform.parent = mapPosBasement.transform;
            islandObjInHand.SetActive(false);
        }
    }

    public void SeaControlTouchEvent(Vector2 pos) {
        //��ֹ�ÿ�
        if (islandObjInHand == null) return;
        IslandScript tmp = gameMapManager.touchIsland(mapPosBasement.ScreenToMapPoint(pos),islandObjInHand);
        if (tmp != null) {
            if(tmp.gameObject==islandObjInHand) {
                islandObjInHand = null;
                gameMapManager.UpdateEffectByController(gameMap.transform);
                return;
            }
            interestIslandList.Add(tmp);
        } else {
            foreach(IslandScript i in interestIslandList) {
                i.isInterestIsland = false;
            }
        }
        //������з���Ȥ����
        interestIslandList.RemoveAll(
            delegate(IslandScript island) {
                return !island.isInterestIsland;
            }
        );
        //print(interestIslandList.Count);
        if (interestIslandList.Count >= Manager.GameMapManager.MIN_MIXED_NUM) {
            gameMapManager.MixedIsland(interestIslandList);
            //ȫ�����
            interestIslandList.RemoveAll(
                delegate(IslandScript island) {
                    return true;
                }
            );
        }
        gameMapManager.UpdateEffectByController(gameMap.transform);
    }
}
