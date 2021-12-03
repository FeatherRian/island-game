using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager {
    public class GameMapManager
    {
        const int MAP_WIDTH = 7;
        const int MAP_HEIGHT = 7;
        IslandType[,] gameMap;

        public GameMapManager() {
            gameMap = new IslandType[MAP_HEIGHT,MAP_WIDTH];
        }

        /// <summary>
        /// �����괦���õ���
        /// </summary>
        /// <param name="island">��������</param>
        /// <param name="x">x������</param>
        /// <param name="y">y������</param>
        /// <returns>�����Ƿ�ɹ����õ���</returns>
        public bool PlaceIsland(IslandType island,int x,int y) {
            if (gameMap[x, y] != IslandType.EMPTY) {
                return false;
            }
            gameMap[x, y] = island;

            return true;
        }

        /// <summary>
        /// �����ĳ���괦�Ƿ�����ϳɵ���
        /// </summary>
        /// <param name="island">��������</param>
        /// <param name="x">x������</param>
        /// <param name="y">y������</param>
        /// <param name="list">�������ϳɣ��򷵻�һ���úϳ��л�ʹ�õĵ��������б�����������򲻱�֤����ֵ</param>
        /// <returns>�����Ƿ�����ϳɵ���</returns>

        public bool MixedIsAllow(IslandType island,int x,int y,out List<KeyValuePair<int,int>> list) {
            


            list = null;
            return false;
        }
    }
}


