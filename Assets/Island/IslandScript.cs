using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScript : MonoBehaviour
{
    //TODO �����Ͻ�������ռλ��
    public IslandMapManager islandMapManager;
    public IslandType islandType=IslandType.SMALL_ISLAND;
    public Sprite[] islandSprite;
    public bool isInterestIsland = false;
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// ��Manager���µ�ʱ�����
    /// </summary>
    public void UpdateByManager(int x,int y) {
        if (spriteRenderer == null) {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        transform.localScale =new Vector3(1,1,1);
        transform.localPosition = new Vector3(x, y);
        print((int)islandType - 1);
        if (islandType == IslandType.EMPTY) spriteRenderer.sprite = null;
        else spriteRenderer.sprite = islandSprite[(int)islandType - 1];
    }

    public void InitBySave() {

    }

    public Vector2Int GetIslandPosInMap() {
        return new Vector2Int((int)(transform.localPosition.x + 0.5f), (int)(transform.localPosition.y + 0.5f));
    }

    //TODO ûд�꣡ûд�꣡ûд�꣡������
    public void MixedAsMain(IslandScript islandA,IslandScript islandB) {
        islandType = Manager.GameMapManager.getNextIslandType(islandType);
        islandA.isInterestIsland = islandB.isInterestIsland = isInterestIsland = false;
    }
}
