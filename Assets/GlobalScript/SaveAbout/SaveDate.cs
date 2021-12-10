using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class SaveDate
{
    public int seaWidth;
    public int seaHeight;
    public System.UInt64 lastTickTime;
    public System.Int64 power;
    public System.Int64 gold;
    public List<IslandDate> islandDates=new List<IslandDate>();
}

[SerializeField]
public class IslandDate {
    public Vector2Int pos;
    public IslandType islandType;
    public List<BuildingDate> buildingDates=new List<BuildingDate>();
}

[SerializeField]
public class BuildingDate {
    public BuildingDate(Vector2Int pos,BuildingType buildingType) {
        this.pos = pos;
        this.buildingType = buildingType;
    }
    public BuildingDate(Vector3 pos,BuildingType buildingType) {
        this.pos = new Vector2Int((int)pos.x, (int)pos.y);
        this.buildingType = buildingType;
    }
    public Vector2Int pos;
    public BuildingType buildingType;
}