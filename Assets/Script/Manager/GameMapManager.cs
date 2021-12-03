using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
    /// <summary>
    /// ��Ϸ��ͼ���������̷߳ǰ�ȫ
    /// </summary>
    public class GameMapManager
    {
        const int MAP_WIDTH = 7;
        const int MAP_HEIGHT = 7;
        const int MIN_MIXED_NUM = 3;
        IslandType[,] gameMap;

        public GameMapManager() {
            gameMap = new IslandType[MAP_WIDTH,MAP_HEIGHT];
        }

        /// <summary>
        /// �����괦���õ���
        /// </summary>
        /// <param name="island">��������</param>
        /// <param name="pos">����</param>
        /// <returns>�Ƿ�ɹ����õ���</returns>
        public bool PlaceIsland(IslandType island,Vector2Int pos) {
            if (gameMap[pos.x, pos.y] != IslandType.EMPTY) {
                return false;
            }
            gameMap[pos.x, pos.y] = island;
            //TODO
            return true;
        }
        
        /// <summary>
        /// �����ĳ���괦�Ƿ�����ϳɵ���
        /// </summary>
        /// <param name="island">��������</param>
        /// <param name="pos">����</param>
        /// <param name="list">�������ϳɣ��򷵻�һ���úϳ��л�ʹ�õĵ��������б�����������򲻱�֤����ֵ</param>
        /// <returns>�Ƿ�����ϳɵ���</returns>

        public bool MixedIsAllow(IslandType island,Vector2Int pos,out List<Vector2Int> list) {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            bool[,] gameMapMemory = new bool[MAP_WIDTH, MAP_HEIGHT];
            queue.Enqueue(pos);
            //����˳��
            Vector2Int[] posMove = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
            list = new List<Vector2Int>();
            while (queue.Count>0) {
                Vector2Int nowPos = queue.Dequeue();
                foreach(Vector2Int move in posMove) {
                    nowPos += move;
                    if (nowPos.x >= 0 &&
                        nowPos.x < MAP_WIDTH &&
                        nowPos.y >= 0 &&
                        nowPos.y < MAP_HEIGHT &&
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
                if (MixedIsAllow(getNextType(island), pos,out tmp)) {
                    list.AddRange(tmp);
                }
                return true;
            } else {
                list = null;
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
        private IslandType getNextType(IslandType islandType) {
            if (islandType == IslandType.LARGE_ISLAND) return IslandType.EMPTY;
            return islandType + 1;
        }
    }
}


