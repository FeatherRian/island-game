using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandMapManager
{
    public int islandWidth;
    public int islandHeight;
    const int MAX_ISLAND_LENGHT=30;
    //ΪʲôҪ���������������ֱ��ʹ��BuildScript����洢��ͼ����
    //��Ҫ��Ϊ�˴浵,�����Ϊ�˼��ص���
    public BuildType[,] buildMap = new BuildType[MAX_ISLAND_LENGHT, MAX_ISLAND_LENGHT];
    public BuildScript[,] pBuildScript = new BuildScript[MAX_ISLAND_LENGHT, MAX_ISLAND_LENGHT];


}