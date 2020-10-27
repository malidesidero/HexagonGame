
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif
using UnityEngine;


public class NodeManager : MonoBehaviour
{

    SpriteRenderer sprite;
    SpriteRenderer bordersprite;
    private GameObject[] selected;
    public GameObject currentNode;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector2 swipePos;
    private bool isDraging;
    public bool isSpining;
    private bool isClockwiseSpin;
    private bool isReverseClockwiseSpin;
    public bool access;
    private int cw;
    private int rcw;
    private int border;
    private HexGrid hexGridScript;
    public List<SpriteRenderer> ordersprite = new List<SpriteRenderer>();
    public List<GameObject> target = new List<GameObject>();
    public List<GameObject> currentNodeChild = new List<GameObject>();
    public List<GameObject> currentNodeHexs = new List<GameObject>();
    public bool control;


    void Start()
    {
        hexGridScript = FindObjectOfType<HexGrid>();
        access = false;
        isDraging = false;
    }

    void Update()
    {
        Swipe();
        if (isReverseClockwiseSpin)
        {
            RCwSpin();
        }
        if (isClockwiseSpin)
        {
            CwSpin();
        }
    }
    private void DestroyOldSelected()
    {
        selected = GameObject.FindGameObjectsWithTag("Selected");
        foreach (GameObject s in selected)
        {
            unSelectAnNode(s);
        }
    }
    IEnumerator spinTime()
    {
        yield return new WaitForSeconds(.15f);//dönme süresi
        isClockwiseSpin = false;
        isReverseClockwiseSpin = false;
        StartCoroutine(ControlTime());
    }
    IEnumerator ControlTime()
    {
        control = true;
        yield return new WaitForSeconds(.06f);//kontrol süresi
        control = false;
        yield return new WaitForSeconds(.2f);//bekleme süresi
        foreach (GameObject child in currentNode.GetComponent<Node>().nodeChilds)
        {
            if (child.GetComponent<Hex>().isMatched == true)
            {
                cw = 0;
                rcw = 0;
                clearChilds();
                currentNode.transform.eulerAngles = new Vector3(0, 0, 0);
                currentNode = null;
                DestroyOldSelected();
                isSpining = false;
                if (hexGridScript.isBombHere)
                {               
                        hexGridScript.bomb.GetComponent<BombHex>().bombCount--;                         
                }
                if(hexGridScript.isBombHere && hexGridScript.bomb == null)
                    hexGridScript.isBombHere = false;
                hexGridScript.DestroyMatches();
                yield break;
            }
        }
        if(cw > 0 && cw !=3)
            ClockwiseSpin();
        else if(rcw > 0 && rcw != 3)
            ReverseClockwiseSpin();
        else
        {
            clearChilds();
            cw = 0;
            rcw = 0;
            currentNode.transform.eulerAngles = new Vector3(0, 0, 0);
            isSpining = false;
        }
    }
    private void clearChilds()
    {       
        for (int i = 0; i < 3; i++)
        {
            GameObject cocuk = currentNode.GetComponent<Node>().nodeChilds[i];
            cocuk.GetComponent<Hex>().transform.parent = hexGridScript.transform; // çocukları silme
        }
    }
    private void SelectAnNode(GameObject selectedNode) // Düğüm seçme
    {
        
            DestroyOldSelected();
            if (selectedNode.GetComponent<Node>().column % 2 == 0 && selectedNode.GetComponent<Node>().row % 2 == 0)
                border = 2;
            else if (selectedNode.GetComponent<Node>().column % 2 == 1 && selectedNode.GetComponent<Node>().row % 2 == 1)
                border = 2;
            else if (selectedNode.GetComponent<Node>().column % 2 == 1 && selectedNode.GetComponent<Node>().row % 2 == 0)
                border = 1;
            else if (selectedNode.GetComponent<Node>().column % 2 == 0 && selectedNode.GetComponent<Node>().row % 2 == 1)
                border = 1;
            sprite = selectedNode.GetComponent<SpriteRenderer>();
            sprite.color = new Color(255, 255, 255, 1f);
        bordersprite = selectedNode.GetComponentsInChildren<SpriteRenderer>()[border];
        bordersprite.color = new Color(255, 255, 255, 1f);
        selectedNode.tag = "Selected";

    }

    private void unSelectAnNode(GameObject selectedNode) // Düğüm seçimini silme
    {
        if (selectedNode.GetComponent<Node>().column % 2 == 0 && selectedNode.GetComponent<Node>().row % 2 == 0)
            border = 2;
        else if (selectedNode.GetComponent<Node>().column % 2 == 1 && selectedNode.GetComponent<Node>().row % 2 == 1)
            border = 2;
        else if (selectedNode.GetComponent<Node>().column % 2 == 1 && selectedNode.GetComponent<Node>().row % 2 == 0)
            border = 1;
        else if (selectedNode.GetComponent<Node>().column % 2 == 0 && selectedNode.GetComponent<Node>().row % 2 == 1)
            border = 1;
        sprite = selectedNode.GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 255, 255, 0f);


        List<GameObject> orderNodeChild = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            if (selectedNode.GetComponent<Node>().nodeChilds[i] != null)
            {
                orderNodeChild.Add(selectedNode.GetComponent<Node>().nodeChilds[i]);
                ordersprite.Add(orderNodeChild[i].GetComponent<SpriteRenderer>());
                orderNodeChild[i].GetComponent<Hex>().transform.parent = hexGridScript.transform; // çocukları silme
                ordersprite[i].sortingOrder = 0;
            }
        }
        orderNodeChild.Clear();
        ordersprite.Clear();

        bordersprite = selectedNode.GetComponentsInChildren<SpriteRenderer>()[border];
        bordersprite.color = new Color(255, 255, 255, 0f);
        selectedNode.tag = "unSelected";
    }
    public void Clicked(GameObject node)
    {
        if (node.tag == "unSelected" && !isSpining)
        {
            currentNode = node;
            currentNode.GetComponent<Node>().FindChilds();
            if (node.GetComponent<Node>().nodeChilds[0] != null && node.GetComponent<Node>().nodeChilds[1] != null && node.GetComponent<Node>().nodeChilds[2] != null)
            {
                SelectAnNode(node);
            }
        }
        else if (node.tag == "Selected")
        {
            unSelectAnNode(node);
            currentNode = null;
        }  
    }
    
    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0)&& !isSpining)
        {
            access = true;
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            isDraging = true;
        }
        if (Input.GetMouseButtonUp(0) && isDraging && !isSpining)
        {     
            access = false;
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            if (currentNode!=null) 
            {
                if (Mathf.Abs(currentSwipe.x) > 150 || Mathf.Abs(currentSwipe.y) > 150)//kaydırmanın çalışması için alt sınır
                {
                    currentSwipe.Normalize();
                    swipePos = Vector2.Lerp(firstPressPos, secondPressPos, 1);
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && swipePos.y > 900f)
                    {
                        //Debug.Log("yukarıdan sag");
                        //Debug.Log("düz");
                        ClockwiseSpin();
                    }
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && swipePos.x < 550f)
                    {
                        //Debug.Log("soldan yukarı");
                        //Debug.Log("düz");
                        ClockwiseSpin();
                    }
                    if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && swipePos.x > 550f)
                    {
                        //Debug.Log("sagdan aşşa");
                        //Debug.Log("düz");
                        ClockwiseSpin();
                    }
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && swipePos.y < 900f)
                    {
                        //Debug.Log("aşagıdan sol");
                        //Debug.Log("düz");
                        ClockwiseSpin();
                    }
                    if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && swipePos.x < 550f)
                    {
                        ReverseClockwiseSpin();
                        //Debug.Log("soldan aşşa");
                        //Debug.Log("ters");
                    }
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && swipePos.y > 900f)
                    {
                        ReverseClockwiseSpin();
                        //Debug.Log("yukarıdan sol");
                        //Debug.Log("ters");
                    }
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && swipePos.y < 900f)
                    {
                        ReverseClockwiseSpin();
                        //Debug.Log("aşagıdan sag");
                        //Debug.Log("ters");
                    }
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && swipePos.x > 550f)
                    {
                        ReverseClockwiseSpin();
                        //Debug.Log("sagdan yukarı");
                        //Debug.Log("ters");
                    }
                }
            }
        }
    }
    private void ClockwiseSpin()
    {
        isDraging = false;
        isClockwiseSpin = true;
        isSpining = true;
        cw++;
        for (int i = 0; i < 3; i++)
        {
            currentNodeChild.Add(currentNode.GetComponent<Node>().nodeChilds[i]);
            target.Add(currentNodeChild[i].GetComponent<Hex>().targetHex);
            hexGridScript.allHexs[target[i].GetComponent<Hex>().column, target[i].GetComponent<Hex>().row] = currentNodeChild[i];
        }
        for (int i = 0; i < 3; i++)
        {
            currentNodeChild[i].GetComponent<Hex>().FindMyPos();
            currentNodeChild[i].GetComponent<Hex>().transform.parent = currentNode.GetComponent<Node>().transform;
        }


        List<GameObject> orderNodeChild = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            orderNodeChild.Add(currentNode.GetComponent<Node>().nodeChilds[i]);
            ordersprite.Add(orderNodeChild[i].GetComponent<SpriteRenderer>());
            ordersprite[i].sortingOrder = 3;
        }
        orderNodeChild.Clear();
        ordersprite.Clear();
        

        clear();
        StartCoroutine(spinTime());
    }
    
    private void ReverseClockwiseSpin()
    {
        isReverseClockwiseSpin = true;
        isSpining = true;
        isDraging = false;
        rcw++;
        for (int i = 0; i < 3; i++)
        {
            currentNodeChild.Add(currentNode.GetComponent<Node>().nodeChilds[i]);
            target.Add(currentNodeChild[i].GetComponent<Hex>().targetHexReverse);
            hexGridScript.allHexs[target[i].GetComponent<Hex>().column, target[i].GetComponent<Hex>().row] = currentNodeChild[i];         
        }
        for (int i = 0; i < 3; i++)
        {
            currentNodeChild[i].GetComponent<Hex>().FindMyPos();
            currentNodeChild[i].GetComponent<Hex>().transform.parent = currentNode.GetComponent<Node>().transform;
        }


        List<GameObject> orderNodeChild = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            orderNodeChild.Add(currentNode.GetComponent<Node>().nodeChilds[i]);
            ordersprite.Add(orderNodeChild[i].GetComponent<SpriteRenderer>());
            ordersprite[i].sortingOrder = 3;
        }
        orderNodeChild.Clear();
        ordersprite.Clear();


        clear();
        StartCoroutine(spinTime());

    }
    private void CwSpin()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, -120*cw));
        currentNode.transform.rotation = Quaternion.Slerp(currentNode.transform.rotation, rotation, 27 * Time.deltaTime);
    }
    private void RCwSpin()
    {

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, +120*rcw));
        currentNode.transform.rotation = Quaternion.Slerp(currentNode.transform.rotation, rotation, 27 * Time.deltaTime);
    }
    public void clear()
    {
        currentNodeChild.Clear();
        target.Clear();
    }
}
