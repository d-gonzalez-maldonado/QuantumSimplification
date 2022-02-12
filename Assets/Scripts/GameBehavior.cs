using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameBehavior : MonoBehaviour
{
    public Text textbox;

    private LevelGenerator levelGen;

    public Color selected;

    public float timeForHint = 5.0f;

    public GameObject gatesObject;
    public GameObject canvasObject;

    private HashSet<LinkedListNode<GateData>> selection = new HashSet<LinkedListNode<GateData>>();

    private int currLevel = 0;

    private float timeToNextLevel = float.MaxValue;
    private float hintTimer = 0;

    private const float TIME_BUFFER = 4f;

    private bool hintProvided = false;

    private int[] levelScores = new int[Constants.N_LEVELS];

    //public GameObject mainGUI;

    private bool started = false;

    private bool hintUsed = false;

    private int sortingIndexOffset = 0;



    private void Start()
    {
        levelGen = GetComponent<LevelGenerator>();
        levelGen.genLevel(GameData.CurrLevel);
        started = true;
        //for (int i = 0; i < Constants.N_LEVELS; i++)
        //{
        //    levelScores[i] = 0;
        //}

        //levelScores[10] = 0;
        //levelScores[0] = 0;
    }

    public void goToLevel(int l)
    {
        currLevel = l;
        levelGen = GetComponent<LevelGenerator>();
        levelGen.genLevel(currLevel);

        //mainGUI.SetActive(false);
        started = true;


    }

    public int getScore(int i)
    {
        return levelScores[i];
    }

    public void onMenuClicked()
    {
        selection = new HashSet<LinkedListNode<GateData>>();
        //mainGUI.SetActive(true);
    }



    public void checkSubstitution()
    {
        foreach (LinkedListNode<GateData> node in selection)
        {
            (GateData, int, int, HashSet<LinkedListNode<GateData>>) results = levelGen.checkSubstitution(node);
    
	    
            if (results.Item4 != null && selection.Count == results.Item4.Count)
            {
                Debug.Log($"Valid substitution at:{node.Value.lineIndex}, {node.Value.type}");
                HashSet<LinkedListNode<GateData>> toSubstitute = results.Item4;
                int originalLength = toSubstitute.Count;
                toSubstitute.IntersectWith(selection);
                int difference = toSubstitute.Count - originalLength;
                if (difference == 0)
                {
                    hintTimer = 0;
                    hintProvided = false;
                    levelGen.makeSubstition(results, node);
                    foreach (LinkedListNode<GateData> currNode in levelGen.allGateNodes())
                    {
                        levelGen.renderedGates[currNode].GetComponent<BaseGateBehavior>().reset();
                    }
                    selection = new HashSet<LinkedListNode<GateData>>();
                    break;
                }
            }
        }


        foreach (LinkedListNode<GateData> node in selection)
        {
            levelGen.renderedGates[node].GetComponent<BaseGateBehavior>().mistakeShake();
	    }

        //bool canBeReduced = false;
        //foreach (LinkedListNode<GateData> node in levelGen.allGateNodes())
        //{
        //    (GateData, int, int, HashSet<LinkedListNode<GateData>>) results = levelGen.checkSubstitution(node);
        //    if (results.Item4 != null && results.Item4.Count > 0)
        //    {
        //        canBeReduced = true;
        //        break;
        //    }
        //}

        //if (!canBeReduced)
        //{
        //    if (!hintUsed)
        //    {
        //        //levelScores[currLevel] = 3;
        //        GameData.CurrLevel += 1;
        //        //currLevel += 1;
        //    }
        //    //levelScores[currLevel] = Math.Max(0, levelScores[currLevel]);
        //    timeToNextLevel = Time.time + TIME_BUFFER;
        //}
    }


    public void tryRun()
    {
        foreach (LinkedListNode<GateData> node in selection)
        {
            (GateData, int, int, HashSet<LinkedListNode<GateData>>) results = levelGen.checkSubstitution(node);


            if (results.Item4 != null && selection.Count == results.Item4.Count)
            {
                Debug.Log($"Valid substitution at:{node.Value.lineIndex}, {node.Value.type}");
                HashSet<LinkedListNode<GateData>> toSubstitute = results.Item4;
                int originalLength = toSubstitute.Count;
                toSubstitute.IntersectWith(selection);
                int difference = toSubstitute.Count - originalLength;
                if (difference == 0)
                {
                    hintTimer = 0;
                    hintProvided = false;
                    levelGen.makeSubstition(results, node);
                    foreach (LinkedListNode<GateData> currNode in levelGen.allGateNodes())
                    {
                        levelGen.renderedGates[currNode].GetComponent<BaseGateBehavior>().reset();
                    }
                    selection = new HashSet<LinkedListNode<GateData>>();
                    break;
                }
            }
        }


        foreach (LinkedListNode<GateData> node in selection)
        {
            levelGen.renderedGates[node].GetComponent<BaseGateBehavior>().mistakeShake();
        }

        bool canBeReduced = false;
        foreach (LinkedListNode<GateData> node in levelGen.allGateNodes())
        {
            (GateData, int, int, HashSet<LinkedListNode<GateData>>) results = levelGen.checkSubstitution(node);
            if (results.Item4 != null && results.Item4.Count > 0)
            {
                canBeReduced = true;
                break;
            }
        }
    }


    private void updateSelection(BaseGateBehavior gate)
    {
        if (selection.Contains(gate.nodeRef)) { 
            selection.Remove(gate.nodeRef);
            gate.setSortingIndex(0);
        }
        else
        {
            selection.Add(gate.nodeRef);
            sortingIndexOffset++;
            gate.setSortingIndex(sortingIndexOffset);

        }
        gate.toggle();
    }

    public void flashHint()
    {
        hintUsed = true;
        foreach (LinkedListNode<GateData> node in levelGen.allGateNodes())
        {
            (GateData, int, int, HashSet<LinkedListNode<GateData>>) results = levelGen.checkSubstitution(node);
            if (results.Item4 != null && results.Item4.Count > 0)
            {
                foreach (LinkedListNode<GateData> currNode in results.Item4)
                {
                    levelGen.renderedGates[currNode].GetComponent<BaseGateBehavior>().highlight();
                }
                break;
            }
        }
    }


    public void toMenu() {
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (!started)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit.collider)
            {
                updateSelection(hit.collider.transform.parent.parent.GetComponent<BaseGateBehavior>());
            }
        }
        //hintTimer += Time.deltaTime;
        //if(!hintProvided && hintTimer >= timeForHint)
        //{

        //    foreach (LinkedListNode<GateData> node in levelGen.allGateNodes())
        //    {
        //        (GateData, int, int, HashSet<LinkedListNode<GateData>>) results = levelGen.checkSubstitution(node);
        //        if (results.Item4 != null && results.Item4.Count > 0)
        //        {
        //            foreach (LinkedListNode<GateData> currNode in results.Item4)
        //            {
        //                levelGen.renderedGates[currNode].GetComponent<BaseGateBehavior>().highlight();
        //            }
        //            break;
        //        }
        //    }
        //   if (finished && timeToNextLevel == float.MaxValue)
        //{
        //       currLevel += 1;
        //       timeToNextLevel = Time.time + TIME_BUFFER;
        //}

        //}
        if (Time.time >= timeToNextLevel)
        {
	        SceneManager.LoadScene(GameData.getNextScene());

            //hintTimer = 0;
            //levelGen.genLevel(currLevel);
            //timeToNextLevel = float.MaxValue;
            //hintUsed = false;
        }

    }
}
