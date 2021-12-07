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

    private void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
    }
    /// <summary>
    /// ��Manager���µ�ʱ�����
    /// </summary>
    public void UpdateByManager() {
        spriteRenderer.sprite = islandSprite[(int)islandType - 1];
    }
    public Vector2Int GetIslandPosInMap() {
        return new Vector2Int((int)(transform.localPosition.x + 0.5f), (int)(transform.localPosition.y + 0.5f));
    }
    //TODO ûд�꣡ûд�꣡ûд�꣡������
    public void MixedAsMain(IslandScript islandA,IslandScript islandB) {
        islandType = Manager.GameMapManager.getNextIslandType(islandType);
    }
}
