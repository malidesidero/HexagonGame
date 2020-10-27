using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int width = 8;
    public int height = 9;
    private float offsetX=0.465f;
    private float offsetY=0.53f;
    private float posY;
    private float posX;
    private float nodeoffSetX = 0.308f;
    private float nodeoffSetY = 0.794f;
    public GameObject nodePrefab;
    public GameObject[] hexPrefabs;
    public GameObject[] hexBombPrefabs;
    public GameObject[,] allHexs;
    public GameObject[,] allNodes;
    private NodeManager nodeManagerScript;
    private Game gameScript;
    public int scorePoint=0;
    private int scorePointBomb = 0;
    private bool GoBomb;
    public bool isBombHere;
    public GameObject bomb;


    private List<GameObject> currentNodeChildCheck = new List<GameObject>();
    public List<GameObject> targetCheck = new List<GameObject>();


    private void Awake()
    {
        gameScript = FindObjectOfType<Game>();
        nodeManagerScript = FindObjectOfType<NodeManager>();
        allHexs = new GameObject[width, height];
        allNodes = new GameObject[width - 1, (height - 1) * 2];
        GenerateGrid();
        GenerateNode();
        scorePoint = 0;
    }
    private int RandomHex() //Renkler arasında rastgele sayı döndürür
    {
        int hex = Random.Range(0, hexPrefabs.Length);
        return hex;
    }
    private void GenerateGrid() // Altıgenleri oluşturma
    {
        for (int x = 0; x < width; x++)
        {
            posY = 0;
            if (x % 2 == 0)
                posY = 0.264f;
            for (int y = 0; y < height; y++)
            {
                int random = RandomHex();
                posY += offsetY;
                int maxIterations = 0;
                while (MatchesAt(x,y,hexPrefabs[random]) && maxIterations < 100) // eşleşme olmadan grid oluşturma 
                {
                    random = RandomHex();
                    maxIterations++;
                    Debug.Log(maxIterations);//test sayısı
                }        
                maxIterations = 0;
                GameObject hex = Instantiate(hexPrefabs[random],new Vector3(posX,posY,0),Quaternion.identity);
                GridInfo(hex, x, y,random);
                allHexs[x, y] = hex;
            }
            posX += offsetX;
        }
    }
    void GenerateNode()// Düğümleri oluşturma
    {
        for (int x = 0; x < width - 1; x++)
        {
            nodeoffSetY = 0.794f;
            for (int y = 0; y < (height - 1) * 2; y++)
            {
                GameObject node = Instantiate(nodePrefab, new Vector3(nodeoffSetX, nodeoffSetY, 0), Quaternion.identity);
                nodeoffSetY += 0.265f;
                if (y % 2 == 0 && x % 2 == 0)
                    nodeoffSetX -= 0.15f;
                else if (y % 2 == 1 && x % 2 == 0)
                    nodeoffSetX += 0.15f;
                else if (y % 2 == 1 && x % 2 == 1)
                    nodeoffSetX -= 0.15f;
                else
                    nodeoffSetX += 0.15f;
                node.transform.parent = transform;
                node.name = "(" + x.ToString() + "," + y.ToString() + ")";
                node.tag = "unSelected";
                allNodes[x, y] = node;
            }
            if (x % 2 == 0)
            {
                nodeoffSetX += 0.3143f;
            }
            else
                nodeoffSetX += 0.6153f;
        }
    }
    private void GridInfo(GameObject hex,int x,int y,int Color)
    {
        hex.transform.parent = transform;
        hex.name = "("+x.ToString() + "," + y.ToString()+")";
    }
    private bool MatchesAt(int column,int row,GameObject newHex)
    {
        if (column > 0 && row > 0 && column < width && row < height - 1) 
        {
            if (allHexs[column - 1, row + 1].tag == newHex.tag && allHexs[column - 1, row].tag == newHex.tag)
            {
                return true;
            }
            if (allHexs[column - 1, row].tag == newHex.tag && allHexs[column, row-1].tag == newHex.tag)
            {
                return true;
            }
            if (allHexs[column - 1, row].tag == newHex.tag && allHexs[column - 1, row].tag == newHex.tag && allHexs[column, row - 1].tag == newHex.tag)
            {
                return true;
            }


            if (allHexs[column - 1, row].tag == newHex.tag && allHexs[column - 1, row - 1].tag == newHex.tag)
            {
                return true;
            }
            if (allHexs[column - 1, row - 1].tag == newHex.tag && allHexs[column, row - 1].tag == newHex.tag)
            {
                return true;
            }
            if (allHexs[column - 1, row].tag == newHex.tag && allHexs[column - 1, row - 1].tag == newHex.tag && allHexs[column, row - 1].tag == newHex.tag)
            {
                return true;
            }
        }
        if (column > 0 && row > 0 && column < width && row < height)
        {
            if (allHexs[column - 1, row].tag == newHex.tag && allHexs[column, row - 1].tag == newHex.tag)
            {
                return true;
            }
            if (allHexs[column - 1, row - 1].tag == newHex.tag && allHexs[column, row - 1].tag == newHex.tag)
            {
                return true;
            }
        }
        if (column%2==1 && row == height - 1)
        {
            if (allHexs[column - 1, row].tag == newHex.tag && allHexs[column - 1, row - 1].tag == newHex.tag)
            {
                return true;
            }
        }

        return false;
    }
    private void DestroyMatchesAt(int column,int row)
    {
        if (allHexs[column, row].GetComponent<Hex>().isMatched)
        {
            Destroy(allHexs[column, row]);
            allHexs[column, row]= null;
            scorePoint +=5 ;
            scorePointBomb +=5;
            if (scorePointBomb > 1000)
            {
                scorePointBomb = 0;
                GoBomb = true;
                isBombHere = true;
            }
        }
    }
    public void DestroyMatches()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(allHexs[x,y] != null)
                {
                    DestroyMatchesAt(x, y);
                }
            }
        }
        StartCoroutine(DecreaseRow());
    }
    private IEnumerator DecreaseRow() 
    {
        int nullCount = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allHexs[x, y] == null) 
                {
                    nullCount++;
                }
                else if(nullCount > 0)
                {
                    allHexs[x, y].GetComponent<Hex>().transform.position = new Vector3(allHexs[x, y].GetComponent<Hex>().transform.position.x, allHexs[x, y].GetComponent<Hex>().transform.position.y-(.53f*nullCount), allHexs[x, y].GetComponent<Hex>().transform.position.z);
                    allHexs[x, y].GetComponent<Hex>().row -= nullCount;
                    allHexs[x,y-nullCount] = allHexs[x, y];
                    allHexs[x, y].name = "(" + allHexs[x, y].GetComponent<Hex>().column.ToString() + "," + allHexs[x, y].GetComponent<Hex>().row.ToString() + ")";
                    allHexs[x, y] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.2f);
        nodeManagerScript.control = true;
        yield return new WaitForSeconds(.2f);
        nodeManagerScript.control = false;
        StartCoroutine(FillGrid());
    }
    private void RefillGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allHexs[x, y] == null)
                {
                    int random = RandomHex();
                    if (x % 2 == 0)
                    {
                        GameObject hex;
                        int maxIterations=0;
                        while (MatchesAt(x, y, hexPrefabs[random]) && maxIterations < 10) // eşleşme olmadan grid oluşturma 
                        {
                            random = RandomHex();
                            maxIterations++;
                            Debug.Log(maxIterations);//test sayısı
                        }
                        maxIterations = 0;
                        Vector3 pos = new Vector3(allHexs[x, y - 1].transform.position.x, 5.03f, -2);
                        if (GoBomb)
                        {
                            GoBomb = false;
                            hex = Instantiate(hexBombPrefabs[random], pos, Quaternion.identity);
                            bomb = hex;
                        }
                        else  hex = Instantiate(hexPrefabs[random], pos, Quaternion.identity);
                        
                        allHexs[x, y] = hex;
                        hex.transform.position = new Vector3(hex.transform.position.x, (hex.transform.position.y - ((height - 1) - y) * .53f), 0);
                        hex.transform.parent = transform;
                        hex.name = "(" + x.ToString() + "," + y.ToString() + ")";
                    }
                    else if (x % 2 == 1)
                    {
                        GameObject hex;
                        int maxIterations = 0;
                        while (MatchesAt(x, y, hexPrefabs[random]) && maxIterations < 10) // eşleşme olmadan grid oluşturma 
                        {
                            random = RandomHex();
                            maxIterations++;
                            Debug.Log(maxIterations);//test sayısı
                        }
                        maxIterations = 0;
                        Vector3 pos = new Vector3(allHexs[x, y - 1].transform.position.x, 4.77f, -2);
                        if (GoBomb)
                        {
                            GoBomb = false;
                            hex = Instantiate(hexBombPrefabs[random], pos, Quaternion.identity);
                            bomb = hex;
                        }
                        else  hex = Instantiate(hexPrefabs[random], pos, Quaternion.identity);
                        allHexs[x, y] = hex;
                        hex.transform.position = new Vector3(hex.transform.position.x, (hex.transform.position.y - ((height - 1) - y) * .53f), 0);
                        hex.transform.parent = transform;
                        hex.name = "(" + x.ToString() + "," + y.ToString() + ")";
                    }
                }
            }
        }
    }
    private bool MatchesOnGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allHexs[x, y] != null)
                {
                    if (allHexs[x, y].GetComponent<Hex>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private IEnumerator FillGrid()
    {
        RefillGrid();
        yield return new WaitForSeconds(.2f);
        while (MatchesOnGrid())
        {
            yield return new WaitForSeconds(.2f);
            DestroyMatches();
        }
    }
}
