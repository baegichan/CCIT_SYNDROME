using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using UnityEditor;
[CreateAssetMenu(fileName = "Map Data", menuName = "SYNDROME_MAP/Map_data")]
public class MapData : ScriptableObject
{
    public enum Map_Direction
    {
        x,
        y
    }
    //[SerializeField]
    //GameObject[] Back_Ground_Sprite;
    
    GameObject Tile_Map;
    GameObject Center;
   public MonsterSet MonsterSet;
    
    /// <summary>
    /// 세이브 시에 변경되게 해야됨
    /// </summary>
   // public int MapCode;
    public int MapTypeCode;

    public GameObject TileCollider;

    [Header("로드전 조정해주세요")]
    public BackGroundSprites[] BG;
    float Width;
    float Height;
    [Header("로드전 조정 불필요합니다.")]
    public GameObject Map_Event;
 
    public Potal[] Potals = new Potal[]{ new Potal(Potal.Potal_type.LeftPotal), new Potal(Potal.Potal_type.RightPotal), new Potal(Potal.Potal_type.TopPotal), new Potal(Potal.Potal_type.BottomPotal) };


    public void Save_TileCollider(GameObject TileCollider)
    {
     //   GameObject tile =  PrefabUtility.SaveAsPrefabAsset(TileCollider, "Assets/Test Scenes/BGC/TileMap/TileColliders/" + this.name+"_Tilecol" + ".prefab");
      //  Debug.Log(tile.name+"               adfafs");
      //  TileCollider = tile;
    }
    public GameObject Load_TileCollider(GameObject target)
    {
        return null;
       //return Instantiate(TileCollider, target.transform.position, Quaternion.identity, target.transform);
    }
    public GameObject Load_DefaultTileCollider(GameObject target)
    {
       return (GameObject)Instantiate(Resources.Load("DefaultTile"), target.transform.position, Quaternion.identity, target.transform);
    }
    public GameObject Load_AbyssMonsterParent(GameObject target)
    {
        return (GameObject)Instantiate(Resources.Load("AbyssMonsterP"), target.transform.position, Quaternion.identity, target.transform);
    }
    public GameObject Load_NormalMonsterParent(GameObject target)
    {
        return (GameObject)Instantiate(Resources.Load("NormalMonsterP"), target.transform.position, Quaternion.identity, target.transform);
    }
    public GameObject Load_DefaultMonster(GameObject target)
    {
        return (GameObject)Instantiate(Resources.Load("DefaultTile"), target.transform.position, Quaternion.identity, target.transform);
    }
    public void Load_MapData(GameObject target)
    {
        Batch_map(target);
        GameObject Potals_parents = (GameObject)Instantiate(Resources.Load("Potals"), target.transform.position,Quaternion.identity, target.transform);
        SpawnPotal(Potals_parents);
        Instantiate(Map_Event, target.transform);
    }










    public void Map_Code_Save(int Mapcode)
    {
        MapTypeCode = Mapcode;
    }
    public void MapDataLengthSet(int index)
    {
      //  Map_Event = new Event[index];
    }

    public Map_Direction direction = Map_Direction.x;
    public void Save_Potal(GameObject potal,int index)
    {
       
        Potals[index].Save_Potal_Location(potal);
    }
    public void DestroyPotal(int index)
    {
        Potals[index].DestroyPotal();
    }
    public void SpawnPotal(GameObject Parent)
    {
        for (int i = 0; i < Potals.Length; i++)
        {
            if (Potals[i].Potaltype != Potal.Potal_type.None)
            {
               Potals[i].Spawn_Potal_Object(Parent);
            }
        
        }
    }
    public void SpawnEvent(GameObject Parent)
    {
        /*
        for (int i = 0; i <Map_Event.Length; i++)
        {
           
               // Map_Event[i].Spawn_Event_Object(Parent,Map_Event[i].MapEventType);
           

        }*/
    }
    #region 신경안써도되는부분
    public void Get_center(GameObject center)
    {
        Center = center;
    }
    public float Get_Total_Sprite_Width(int j)
    {
        float total = 0;
        
        for (int i = 0; i < BG[j].BackGround.Length; i++)
        {
           total += BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
            return total;
    }
    public float Get_Total_Sprite_Height(int j)
    {
        float total = 0;
        for (int i = 0; i < BG[j].BackGround.Length; i++)
        {
            total += BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
        return total;
    }
    public void Batch_map()
    {
        if(Tile_Map==null)
        {
            Tile_Map = (GameObject)Resources.Load("Grid");
        }
        GameObject Grid=Instantiate(Tile_Map, Vector3.zero, Quaternion.identity, Center.transform);

        for (int j = 0; j < BG.Length; j++)
        {
            if (BG[j].Tilemap == null)
            {
                BG[j].Tilemap = (GameObject)Resources.Load("Tilemap");
            }
            GameObject Tile = Instantiate(BG[j].Tilemap, Vector3.zero, Quaternion.identity, Grid.transform);

            if(Tile.GetComponent<TilemapCollider2D>()==null )
            {
                Tile.AddComponent<TilemapCollider2D>();
            }
           
            if (BG[j].Its_Object)
            {
                Tile.GetComponent<TilemapCollider2D>().enabled=true;
            }
            else
            {
            
                Tile.GetComponent<TilemapCollider2D>().enabled = false;
            }
        
         
            Tile.name = BG[j].TilemapName;
            int Layer_binary = Convert.ToInt32(Convert.ToString(BG[j].Layer.value, 2));
            int Layer_count = 0;
            for (int L = 0; L < 32; L++)
            {

                if (Layer_binary % 2 == 1)
                {
                    Tile.layer = Layer_count;
                    break;
                }
                Layer_binary = Layer_binary / 10;

                Layer_count++;
            }
            Tile.GetComponent<TilemapRenderer>().sortingOrder = BG[j].OrderInLayer;
            float total = 0;
            switch (direction)
            {
                case Map_Direction.x:
                    total = Get_Total_Sprite_Width(j);

                    break;
                case Map_Direction.y:
                    total = Get_Total_Sprite_Height(j);

                    break;

            }
            for (int i = 0; i < BG[j].BackGround.Length; i++)
            {
                switch (direction)
                {
                    case Map_Direction.x:
                        
                        GameObject BackGround_x=Instantiate(BG[j].BackGround[i], new Vector3(total - BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity, Center.transform);
                        BackGround_x.GetComponent<SpriteRenderer>().sortingOrder = BG[j].OrderInLayer;
                        
                        /*
                        int Layer_binary =Convert.ToInt32(Convert.ToString(BG[j].Layer.value,2));
                        int Layer_count = 0;
                        for (int L = 0; L < 32; L++)
                        {
                           
                            if (Layer_binary % 2 == 1)
                            {
                                BackGround_x.layer = Layer_count;
                                break;
                            }
                            Layer_binary = Layer_binary / 10;
                            
                            
                            Layer_count++;
                           // LayerMask Layer = (BG[j].Layer.value);
                           // BackGround_x.layer = Layer;
                        }
                        */
                        if (i + 1 < BG[j].BackGround.Length)
                        {
                            total -= BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                        }
                        break;
                    case Map_Direction.y:

                        GameObject BackGround_y = Instantiate(BG[j].BackGround[i], new Vector3(0, total - BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / (2 * BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0), Quaternion.identity, Center.transform);
                        BackGround_y.GetComponent<SpriteRenderer>().sortingOrder = BG[j].OrderInLayer;
                        BackGround_y.layer = BG[j].Layer.value;
                        if (i + 1 < BG[j].BackGround.Length)
                        {
                            total -= BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                        }
                        break;

                }
                // Instantiate(Back_Ground_Sprite[i], new Vector3(total - Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity,Center.transform);
                //Back_Ground_Sprite[i].transform.localPosition = new Vector3(total-Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width/2*Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit, 0, 0);
                
            }
           
        }
    }

    public void Batch_map(GameObject target)
    {
        if (Tile_Map == null)
        {
            Tile_Map = (GameObject)Resources.Load("Grid");
        }
        GameObject Grid = Instantiate(Tile_Map, target.transform.position, Quaternion.identity, target.transform);


        for (int j = 0; j < BG.Length; j++)
        {
            if (BG[j].Tilemap == null)
            {
                BG[j].Tilemap = (GameObject)Resources.Load("Tilemap");
            }
            GameObject Tile = Instantiate(BG[j].Tilemap, target.transform.position, Quaternion.identity, Grid.transform);
            Tile.AddComponent<TilemapCollider2D>();
            
            Tile.AddComponent<LayerChecker>();
            
            if (BG[j].Its_Object)
            {
                Tile.GetComponent<TilemapCollider2D>().enabled = true;
                Tile.GetComponent<LayerChecker>().isFieldObject = false;
            }
            else
            {
                Tile.GetComponent<TilemapCollider2D>().enabled = false;
                Tile.GetComponent<LayerChecker>().isFieldObject = true;
            }


           // if(Tile.GetComponent<LayerChecker>()!=null)
         

            if(BG[j].isAbyss)
            {
                Tile.GetComponent<LayerChecker>().isAbyssLayer = true;
            }
             else
            {
             Tile.GetComponent<LayerChecker>().isAbyssLayer = false;
            }
            
            Tile.name = BG[j].TilemapName;
            int Layer_binary = Convert.ToInt32(Convert.ToString(BG[j].Layer.value, 2));
            Debug.Log(Layer_binary);
            int Layer_count = 0;
            for (int L = 0; L < 32; L++)
            {

                if (Layer_binary % 2 == 1)
                {
                    Tile.layer = Layer_count;
                    break;
                }
                Layer_binary = Layer_binary / 10;

                Layer_count++;
            }
            Tile.GetComponent<TilemapRenderer>().sortingOrder = BG[j].OrderInLayer;
            float total = 0;
            switch (direction)
            {
                case Map_Direction.x:
                    total = Get_Total_Sprite_Width(j);

                    break;
                case Map_Direction.y:
                    total = Get_Total_Sprite_Height(j);

                    break;

            }
            for (int i = 0; i < BG[j].BackGround.Length; i++)
            {
                switch (direction)
                {
                    case Map_Direction.x:

                        GameObject BackGround_x = Instantiate(BG[j].BackGround[i], new Vector3(total - BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity, Center.transform);
                        BackGround_x.GetComponent<SpriteRenderer>().sortingOrder = BG[j].OrderInLayer;

                        /*
                        int Layer_binary =Convert.ToInt32(Convert.ToString(BG[j].Layer.value,2));
                        int Layer_count = 0;
                        for (int L = 0; L < 32; L++)
                        {
                           
                            if (Layer_binary % 2 == 1)
                            {
                                BackGround_x.layer = Layer_count;
                                break;
                            }
                            Layer_binary = Layer_binary / 10;
                            
                            
                            Layer_count++;
                           // LayerMask Layer = (BG[j].Layer.value);
                           // BackGround_x.layer = Layer;
                        }
                        */
                        if (i + 1 < BG[j].BackGround.Length)
                        {
                            total -= BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                        }
                        break;
                    case Map_Direction.y:

                        GameObject BackGround_y = Instantiate(BG[j].BackGround[i], new Vector3(0, total - BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / (2 * BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0), Quaternion.identity, target.transform);
                        BackGround_y.GetComponent<SpriteRenderer>().sortingOrder = BG[j].OrderInLayer;
                        BackGround_y.layer = BG[j].Layer.value;
                        if (i + 1 < BG[j].BackGround.Length)
                        {
                            total -= BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                        }
                        break;

                }
                // Instantiate(Back_Ground_Sprite[i], new Vector3(total - Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity,Center.transform);
                //Back_Ground_Sprite[i].transform.localPosition = new Vector3(total-Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width/2*Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit, 0, 0);

            }

        }
    }
    #endregion
    public void Save_MapData(GameObject grid , GameObject Event)
    {
        for(int i=0; i<BG.Length;i++)
        {

         //  GameObject MapPrefab =PrefabUtility.SaveAsPrefabAsset(grid.transform.GetChild(i).gameObject,"Assets/Test Scenes/BGC/TileMap/TileMaps/"+BG[i].TilemapName+ ".prefab");
         //   BG[i].Tilemap = MapPrefab;
            //추가로 이벤트도 저장해야됨
            
        }
      //  Map_Event = PrefabUtility.SaveAsPrefabAsset(Event, "Assets/Test Scenes/BGC/Event/Events/" + this.name + "_Event" + ".prefab");
    }
    public void Save_Event(GameObject Event)
    {
   //     Map_Event = PrefabUtility.SaveAsPrefabAsset(Event, "Assets/Test Scenes/BGC/Event/Events/" + this.name + "_Event" + ".prefab");
    }

    public Sprite Get_Sprite(int index)
    {
        return BG[0].BackGround[index].GetComponent<SpriteRenderer>().sprite;
    }
    public int Get_length(int index)
    {
        return BG[index].BackGround.Length;
    }
    
   public void Save_WH(int j)
    {

        if(direction==Map_Direction.x)
        {
            Width = Get_Total_Sprite_Width(j);
            Height= BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.rect.height / BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
        else
        {

            Width = BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.rect.width / BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            Height = Get_Total_Sprite_Height(j);
        }
      

    }
}
[Serializable]
public class BackGroundSprites 
{
    [Header("인덱스 0 유지해주세요")]
   public GameObject[] BackGround;
    [Tooltip("null 상태로 로드해도됩니다")]
   public GameObject Tilemap;
    [Tooltip("어비스 레이어일경우 체크 배경일경우 False")]
    public bool isAbyss;
    [Tooltip("오브젝트일경우 체크 배경일경우 False")]
    public bool Its_Object=false;
    public String TilemapName;
    [Tooltip("-30~-50까지")]
    [Range(-30,-50)]public int OrderInLayer=-30;
    [Tooltip("Please do not chose multiple Layer")]
    public LayerMask Layer;
}
[Serializable]
public class Event
{


    public GameObject EventPrefab;
    /*
    public MapEvent.Event MapEventType = MapEvent.Event.None;
    public Vector2[] VertexPoints = new Vector2[5];
    public Vector2 EventLocation = new Vector2(0, 0);

 
    public Event(GameObject EventObject)
    {
        Save_Event_Location(EventObject);
    }
    public void Save_Event_Location(GameObject Event)
    {
        MapEventType = Event.GetComponent<MapEvent>().EventType;
         VertexPoints = Event.GetComponent<EdgeCollider2D>().points;
        EventLocation = Event.transform.position;
    }
    public void DestroyEvent()
    {
        MapEventType = MapEvent.Event.None;
        VertexPoints = null;
        EventLocation = new Vector2(0, 0);
    }
    public void Spawn_Event_Object(GameObject Parent)
    {

        GameObject potal = (GameObject)Resources.Load("DefaultEvent");
        GameObject SpawnedEvent = GameObject.Instantiate(potal, Parent.transform);
        SpawnedEvent.name = MapEventType.ToString();
        SpawnedEvent.GetComponent<MapLineDraw>().T_Area = VertexPoints[0].y;
        SpawnedEvent.GetComponent<MapLineDraw>().B_Area = VertexPoints[2].y;
        SpawnedEvent.GetComponent<MapLineDraw>().L_Area = VertexPoints[0].x;
        SpawnedEvent.GetComponent<MapLineDraw>().R_Area = VertexPoints[1].x;
        SpawnedEvent.GetComponent<EdgeCollider2D>().points = VertexPoints;
        SpawnedEvent.transform.position = EventLocation;

    }
    public void Spawn_Event_Object(GameObject Parent,MapEvent.Event even)
    {
            
            GameObject potal = (GameObject)Resources.Load("DefaultEvent");
            GameObject SpawnedEvent = GameObject.Instantiate(potal, Parent.transform);
            SpawnedEvent.name = MapEventType.ToString();
            SpawnedEvent.GetComponent<MapLineDraw>().T_Area = VertexPoints[0].y;
            SpawnedEvent.GetComponent<MapLineDraw>().B_Area = VertexPoints[2].y;
            SpawnedEvent.GetComponent<MapLineDraw>().L_Area = VertexPoints[0].x;
            SpawnedEvent.GetComponent<MapLineDraw>().R_Area = VertexPoints[1].x;
            SpawnedEvent.GetComponent<EdgeCollider2D>().points = VertexPoints;
            SpawnedEvent.transform.position = EventLocation;
        switch (even)
        {
            case MapEvent.Event.MapLock:
                SpawnedEvent.AddComponent<MapLockEvent>();
                break;
            case MapEvent.Event.MonsterSpawn:
                SpawnedEvent.AddComponent<MonsterSpawnEvent>();
                break;

            case MapEvent.Event.MapLockandMonsterSpawn:
                SpawnedEvent.AddComponent<MapLockEvent>();
                SpawnedEvent.AddComponent<MonsterSpawnEvent>();
                break;



        }


    }*/
}
[Serializable]
public class Potal
{
    public Potal(Potal_type type)
    {
        Potaltype = type;
    }
    public enum Potal_type
    {
        None,
        LeftPotal,
        RightPotal,
        BottomPotal,
        TopPotal
    }
    public bool EnablePotal = false;
    public Potal_type Potaltype=Potal.Potal_type.None;
    public Potal_type PotalDirection = Potal.Potal_type.None;
    public Vector2[] VertexPoints = new Vector2[5];
    public Vector2 PotalLocation = new Vector2(0, 0);
    
    public void Save_Potal_Location(GameObject Potal)
    {
        EnablePotal = true;
        VertexPoints = Potal.GetComponent<EdgeCollider2D>().points;
        PotalLocation = Potal.transform.localPosition;
    }
    public void DestroyPotal()
    {
        EnablePotal = false;
        VertexPoints = null;
        PotalLocation = new Vector2(0,0);
    }
    public void Spawn_Potal_Object(GameObject Parent)
    {
        if (EnablePotal)
        {
            //GameObject potal = (GameObject)Resources.Load("Potal");
            GameObject SpawnedPotal = GameObject.Instantiate((GameObject)Resources.Load("Potal"), Parent.transform.position, Quaternion.identity, Parent.transform);
            SpawnedPotal.name = Potaltype.ToString();
            var Potal = SpawnedPotal.GetComponent<MapLineDraw>();
            Potal.T_Area = VertexPoints[0].y;
            Potal.B_Area = VertexPoints[2].y;
            Potal.L_Area = VertexPoints[0].x;
            Potal.R_Area = VertexPoints[1].x;
            SpawnedPotal.GetComponent<EdgeCollider2D>().points = VertexPoints;
            SpawnedPotal.transform.localPosition = (Vector2)SpawnedPotal.transform.localPosition + PotalLocation;
            SpawnedPotal.GetComponent<PotalEvent>().Potal_setting(Potaltype);
            SpawnedPotal.GetComponent<PotalEvent>().PotalD_setting(PotalDirection);
            SpawnedPotal.GetComponent<PotalEvent>().SetinterBlock();


        }
    }
    
}