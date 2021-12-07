using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
    /// <summary>
    /// ��Ϸ��ͼ���������̷߳ǰ�ȫ
    /// </summary>
    public class GameMapManager
    {
        public int mapWidth { get; private set; } = 3;
        public int mapHeight { get; private set; } = 3;
        const int MAX_MAP_LENGHT = 30;
        /// <summary>
        /// ��С����ϳ�����
        /// </summary>
        public const int MIN_MIXED_NUM = 3;
        IslandType[,] gameMap = new IslandType[MAX_MAP_LENGHT,MAX_MAP_LENGHT];
        public IslandScript[,] pIslandObj = new IslandScript[MAX_MAP_LENGHT,MAX_MAP_LENGHT];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        public GameMapManager(int mapWidth,int mapHeight) {
            if (mapHeight > MAX_MAP_LENGHT || mapWidth > MAX_MAP_LENGHT) {
                //����
                Debug.LogError("out of MAX_MAP_LENGHT");
            }
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
        }
        /// <summary>
        /// ����gameMap����pIslandObj
        /// </summary>
        private void UpdateIslandGameObject() {
            for(int i = 0; i < mapWidth; i++) {
                for(int r = 0; r < mapHeight; r++) {
                    
                }
            }
        }

        /// <summary>
        /// �����괦���õ���
        /// </summary>
        /// <param name="island">��������</param>
        /// <param name="pos">����</param>
        /// <returns>�Ƿ�ɹ����õ���</returns>
        public bool PlaceIsland(IslandType island, Vector2Int pos) {
            if (gameMap[pos.x, pos.y] != IslandType.EMPTY) {
                return false;
            }
            gameMap[pos.x, pos.y] = island;
            return true;
        }

        /// <summary>
        /// �ݻٵ���
        /// </summary>
        /// <param name="pos">���ݻٵ��������</param>
        public void DestroyIsland(Vector2Int pos) {
            gameMap[pos.x, pos.y] = IslandType.EMPTY;
        }

        /// <summary>
        /// �ϳɵ���
        /// </summary>
        /// <param name="list">���ϳɵĵ������������,�ϳɺ�ĵ��콫���������һ��������</param>
        /// <returns>�Ƿ�ɹ��ϳ�</returns>
        //���MIN_MIXED_NUM>3������������ĳɲ��鼯ʵ��
        public bool MixedIsland(List<Vector2Int> list) {
            if (list.Count != 3) return false;
            for(int i = 0; i < MIN_MIXED_NUM; ++i) {
                for (int r = 0; r < MIN_MIXED_NUM; ++r) {
                    Vector2Int pos = list[i] - list[r];
                    if (!(pos == Vector2Int.up ||
                        pos == Vector2Int.down ||
                        pos == Vector2Int.left ||
                        pos == Vector2Int.right)) {
                        return false;
                    }
                }     
            }
            Vector2Int t = list[MIN_MIXED_NUM - 1];
            gameMap[t.x, t.y] = getNextIslandType(gameMap[t.x,t.y]);
            for(int i = 0; i < MIN_MIXED_NUM-1; ++i) {
                gameMap[list[i].x, list[i].y] = IslandType.EMPTY;
            }
            return true;
        }

        /// <summary>
        /// �����ĳ���괦�Ƿ�����ϳɵ���
        /// </summary>
        /// <param name="island">��������</param>
        /// <param name="pos">����</param>
        /// <param name="list">�������ϳɣ��򷵻�һ���úϳ��л�ʹ�õĵ��������б�����������򲻱�֤����ֵ</param>
        /// <param name="finalIslandType">�������ϳɣ��򷵻ظúϳ����ջ����ɵĵ�������</param>
        /// <returns>�Ƿ�����ϳɵ���</returns>

        public bool MixedIsAllow(IslandType island,Vector2Int pos,out List<Vector2Int> list,out IslandType finalIslandType) {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            bool[,] gameMapMemory = new bool[mapWidth, mapHeight];
            queue.Enqueue(pos);
            //����˳��
            Vector2Int[] posMove = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
            list = new List<Vector2Int>();
            while (queue.Count>0) {
                Vector2Int nowPos = queue.Dequeue();
                foreach(Vector2Int move in posMove) {
                    nowPos += move;
                    if (nowPos.x >= 0 &&
                        nowPos.x < mapWidth &&
                        nowPos.y >= 0 &&
                        nowPos.y < mapHeight &&
                        gameMapMemory[nowPos.x, nowPos.y] == false) {
                        if (canMixed(gameMap[nowPos.x, nowPos.y], island)) {
                            queue.Enqueue(nowPos);
                            list.Add(nowPos);
                            gameMapMemory[nowPos.x, nowPos.y] = true;
                        }
                    }
                    nowPos -= move;
                }
            }
            if (list.Count >= MIN_MIXED_NUM - 1) {
                List<Vector2Int> tmp;
                finalIslandType = getNextIslandType(island);
                if (MixedIsAllow(getNextIslandType(island), pos,out tmp,out finalIslandType)) {
                    list.AddRange(tmp);
                }
                return true;
            } else {
                list = null;
                finalIslandType = IslandType.EMPTY;
                return false;
            }
        }
        
        /// <summary>
        /// �ж����������Ƿ���Խ��кϳɣ���ʱ��
        /// </summary>
        /// <param name="a">��һ������</param>
        /// <param name="b">�ڶ�������</param>
        /// <returns>�Ƿ���Ժϳ�</returns>
        private bool canMixed(IslandType a,IslandType b) {
            if(a>b) {
                IslandType tmp = b;
                b = a;
                a = tmp;
            }
            return a == b;
        }

        /// <summary>
        /// ��ȡ��һ������ȼ�(��ʱ��
        /// </summary>
        /// <param name="islandType">��ǰ����ȼ�</param>
        /// <returns>��һ������ȼ�</returns>
        private IslandType getNextIslandType(IslandType islandType) {
            if (islandType == IslandType.LARGE_ISLAND) return IslandType.EMPTY;
            return islandType + 1;
        }
    }
}


