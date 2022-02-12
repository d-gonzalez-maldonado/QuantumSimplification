using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class LevelGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    private GameObject prevLevel;
    private GameObject currLevel;
    private System.Random rng;
    private HashSet<string> typesToExpand;
    private Dictionary<string, List<LinkedListNode<GateData>>> gatesToExpand;
    public Dictionary<LinkedListNode<GateData>, GameObject> renderedGates;
    private GameObject[] renderedLines;
    public Text uiText;
    private LevelData ld;
    private void Start()
    {

    }


    public void genLevel(int l)
    {
        rng = new System.Random();

        prevLevel = currLevel;
        currLevel = new GameObject($"GatesLevel-{l}");
        clearPrevLevel();
        typesToExpand = new HashSet<string>();
        foreach (GateData g in Gates.VALID_GATES)
        {
            typesToExpand.Add(g.type);
        }
        int nLines;
        Func<int>[] levels = {
            () => {
                nLines = 1;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.H, 0);
                uiText.text = "Remember:\nTwo H Gates cancel each other out";
                return 1;

            },
            () => {
                nLines = 1;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.H, 0);
                uiText.text = "";
                return 1;

            },
            () => {
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.H, 1);
                ld.addGate(Gates.H, 0);
                uiText.text = "Only two H gates connected to each other can cancel out";
                return 1;

            },
            ()=>{
                nLines = 2;
                GateData[] noiseGates = {Gates.Z, Gates.NOT};
                ld = new LevelData(nLines);
                int substitutions = 2;
                int noise = 2;
                for (int i = 0; i < substitutions; i++) {
                    int line = rng.Next(nLines);
                    ld.addGate(Gates.H, line);
                    ld.addGate(Gates.H, line);
                    for (int j = 0; j < noise; j++) {
                        line = rng.Next(nLines);
                        ld.addGate(noiseGates[rng.Next(noiseGates.Length)], line);

					}
				}
                return 1;
            },
            ()=>{
                nLines = 2;
                GateData[] noiseGates = {Gates.Z, Gates.NOT};
                ld = new LevelData(nLines);
                int substitutions = 3;
                int noise = 3;
                for (int i = 0; i < substitutions; i++) {
                    int line = rng.Next(nLines);
                    ld.addGate(Gates.H, line);
                    ld.addGate(Gates.H, line);
                    for (int j = 0; j < noise; j++) {
                        line = rng.Next(nLines);
                        ld.addGate(noiseGates[rng.Next(noiseGates.Length)], line);

                    }
                }
                return 1;
            },
            ()=>{
                nLines = 3;
                GateData[] noiseGates = {Gates.Z, Gates.NOT};
                ld = new LevelData(nLines);
                int substitutions = 4;
                int noise = 3;
                for (int i = 0; i < substitutions; i++) {
                    int line = rng.Next(nLines);
                    ld.addGate(Gates.H, line);
                    ld.addGate(Gates.H, line);
                    for (int j = 0; j < noise; j++) {
                        line = rng.Next(nLines);
                        ld.addGate(noiseGates[rng.Next(noiseGates.Length)], line);

                    }
                }
                return 1;
            },
            ()=>{
                nLines = 4;
                GateData[] noiseGates = {Gates.Z, Gates.NOT};
                ld = new LevelData(nLines);
                int substitutions = 5;
                int noise = 3;
                for (int i = 0; i < substitutions; i++) {
                    int line = rng.Next(nLines);
                    ld.addGate(Gates.H, line);
                    ld.addGate(Gates.H, line);
                    for (int j = 0; j < noise; j++) {
                        line = rng.Next(nLines);
                        ld.addGate(noiseGates[rng.Next(noiseGates.Length)], line);

                    }
                }
                return 1;
            },

            ()=>{
                nLines = 1;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.NOT, 0);
                ld.addGate(Gates.H, 0);
                uiText.text = "A NOT Gate surrounded by H Gates is equivalent to a single Z Gate\nClick on all the redundant gates and then make the substitution!";
                return 1;
            },

            ()=>{
                nLines = 1;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.NOT, 0);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.NOT, 0);
                ld.addGate(Gates.H, 0);
                uiText.text = "Sometimes there are multiple ways to simplify the program!";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.NOT, 0);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.NOT, 0);
                ld.addGate(Gates.H, 0);

                ld.addGate(Gates.H, 1);
                ld.addGate(Gates.NOT, 1);
                ld.addGate(Gates.H, 1);
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 1;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.Z, 0);
                ld.addGate(Gates.H, 0);
                uiText.text = "A Z Gate surrounded by H Gates is equivalent to a single NOT Gate";
                return 1;
            },
            ()=>{
                nLines = 1;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.Z, 0);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.Z, 0);
                ld.addGate(Gates.H, 0);
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.Z, 0);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.Z, 0);
                ld.addGate(Gates.H, 0);

                ld.addGate(Gates.H, 1);
                ld.addGate(Gates.Z, 1);
                ld.addGate(Gates.H, 1);
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT};
                int gatesToAdd = 3;

                uiText.text = "Sometimes programs are too large to fit in the screen,\n use WASD to move the camera!";
                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 2;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT};
                int gatesToAdd = 4;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 2;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT};
                int gatesToAdd = 4;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 3;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT};
                int gatesToAdd = 5;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 4;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 1);
                ld.addGate(Gates.CNOT, 0);
                ld.addGate(Gates.H, 1);
                uiText.text = "Controlled Gates work the same as their basic counterparts\n A Controlled NOT whose 'Target' inputs are surrounded by H Gates is equivalent to a Controlled Z!";
                return 1;
            },
            () => {
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 1);
                ld.addGate(Gates.CZ, 0);
                ld.addGate(Gates.H, 1);
                uiText.text = "Likewise, a Controlled Z Gate is equivalent to a Controlled NOT Gate if its target input is surrounded by H Gates";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT, Gates.CNOT, Gates.CZ};
                int gatesToAdd = 5;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 4;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT, Gates.CNOT, Gates.CZ};
                int gatesToAdd = 6;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 4;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT, Gates.CNOT, Gates.CZ};
                int gatesToAdd = 6;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 5;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT, Gates.CNOT, Gates.CZ};
                int gatesToAdd = 6;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 5;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.CNOT, 0);
                ld.addGate(Gates.H, 0);
                ld.setGateAtIndex(Gates.H, 1, 0);
                ld.setGateAtIndex(Gates.H, 1, 2);
                uiText.text = "If a Controlled NOT Gate is completely surrounded by H Gates, the H Gates cancel out!\n";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.H, 0);
                ld.addGate(Gates.CZ, 0);
                ld.addGate(Gates.H, 0);
                ld.setGateAtIndex(Gates.H, 1, 0);
                ld.setGateAtIndex(Gates.H, 1, 2);
                uiText.text = "The same ISN'T! true for a Controlled Z GATE though\nHowever, there still is a valid equivalency in this level, can you find it?";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT, Gates.CNOT, Gates.CZ};
                int gatesToAdd = 7;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 5;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT, Gates.CNOT, Gates.CZ};
                int gatesToAdd = 8;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 6;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.Z, Gates.NOT, Gates.CNOT, Gates.CZ};
                int gatesToAdd = 6;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 5;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 2;
                ld = new LevelData(nLines);
                ld.addGate(Gates.SWAP, 0);
                buildGateCache();
                expandGate(gatesToExpand["SWAP"][0]);
                uiText.text = "Three Controlled NOT Gates are equivalent to a single SWAP Gate";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.CNOT, Gates.CZ, Gates.SWAP};
                int gatesToAdd = 4;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 3;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.CNOT, Gates.CZ, Gates.SWAP};
                int gatesToAdd = 4;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 3;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.NOT, Gates.Z, Gates.CNOT, Gates.CZ, Gates.SWAP};
                int gatesToAdd = 5;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 3;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.NOT, Gates.Z, Gates.CNOT, Gates.CZ, Gates.SWAP};
                int gatesToAdd = 6;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 3;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.NOT, Gates.Z, Gates.CNOT, Gates.CZ, Gates.SWAP};
                int gatesToAdd = 6;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 4;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                GateData[] validGates = {Gates.NOT, Gates.Z, Gates.CNOT, Gates.CZ, Gates.SWAP};
                int gatesToAdd = 6;

                for (int i = 0; i < gatesToAdd; i++)
                {
                    GateData gateToAdd;
                    for (int j = 0; j < 5; j++)
                    {
                        gateToAdd = validGates[rng.Next(0, validGates.Length)];
                        if (nLines - gateToAdd.height + 1 < 0)
                            continue;
                        int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                        ld.addGate(gateToAdd, startingLine);
                        break;
                    }
                }

                int expansions = 5;
                for (int _ = 0; _ < 100; _++)
                {
                    buildGateCache();
                    string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                    int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                    LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                    if (expandGate(gateToExpand))
                    {
                        expansions -= 1;
                    }
                    if (expansions <= 0)
                    {
                        break;
                    }
                }
                uiText.text = "";
                return 1;
            },
            ()=>{
                nLines = 3;
                ld = new LevelData(nLines);
                ld.addGate(Gates.CNOTL, 0);
                buildGateCache();
                expandGate(gatesToExpand["CNOTL"][0]);
                uiText.text = "Four Controlled NOT Gates with staggered inputs are equivalent to a single Controlled NOT with a pass-through value";
                return 1;
            }
        };

        if (l < levels.Length)
        {
            levels[l]();
        }
        else
        {
            uiText.text = "";
            nLines = rng.Next(3, 7);
            ld = new LevelData(nLines);
            int gatesToAdd = rng.Next(7, 14);

            for (int i = 0; i < gatesToAdd; i++)
            {
                GateData gateToAdd;
                for (int j = 0; j < 5; j++)
                {
                    gateToAdd = Gates.VALID_GATES[rng.Next(0, Gates.VALID_GATES.Length)];
                    if (nLines - gateToAdd.height + 1 < 0)
                        continue;
                    int startingLine = rng.Next(0, nLines - gateToAdd.height + 1);

                    ld.addGate(gateToAdd, startingLine);
                    break;
                }
            }

            int expansions = rng.Next(3, 4);
            for (int _ = 0; _ < 100; _++)
            {
                buildGateCache();
                string targetGateType = gatesToExpand.Keys.ToArray()[rng.Next(0, gatesToExpand.Count)];
                int index = rng.Next(0, gatesToExpand[targetGateType].Count);
                LinkedListNode<GateData> gateToExpand = gatesToExpand[targetGateType][index];
                if (expandGate(gateToExpand))
                {
                    expansions -= 1;
                }
                if (expansions <= 0)
                {
                    break;
                }
            }
        }

        renderLevel();

    }

    private int getNodeIndex(LinkedListNode<GateData> node, int currIndex = 0)
    {
        if (node.Previous == null)
            return currIndex;
        else
            return getNodeIndex(node.Previous, currIndex: currIndex + 1);
    }

    private bool expandGate(LinkedListNode<GateData> g)
    {
        //payload is an array of tuples containing a string which corresponds to the 
        //type of gate to add, and two ints. The first int corresponds to the relative
        //line index of g (ie if it is 1 and g's line index is 2 then the gate will be 
        //added at line 2+1=3. Similarly, the second int corresponds to the offset of the
        //new gate's index. If it is 2 and g's index is 4 the new gate will be added at index 2+4=6
        List<(GateData, int, int)> payload = new List<(GateData, int, int)>();
        switch (g.Value.type)
        {
            case "Z":
                payload.Add((Gates.H, 0, -1));
                payload.Add((Gates.NOT, 0, 0));
                payload.Add((Gates.H, 0, 1));
                break;
            case "NOT":
                payload.Add((Gates.H, 0, -1));
                payload.Add((Gates.Z, 0, 0));
                payload.Add((Gates.H, 0, 1));
                break;
            case "CZ":
                payload.Add((Gates.H, 1, -1));
                payload.Add((Gates.CNOT, 0, 0));
                payload.Add((Gates.H, 1, 1));
                break;
            case "CNOT":
                if (rng.NextDouble() > .5f)
                {
                    payload.Add((Gates.H, 1, -1));
                    payload.Add((Gates.CZ, 0, 0));
                    payload.Add((Gates.H, 1, 1));
                }
                else
                {
                    payload.Add((Gates.H, 0, -1));
                    payload.Add((Gates.H, 1, -1));
                    payload.Add((Gates.H, 0, 1));
                    payload.Add((Gates.H, 1, 1));


                }
                break;
            case "SWAP":
                payload.Add((Gates.CNOT, 0, 0));
                payload.Add((Gates.CNOT, 0, 1));
                payload.Add((Gates.CNOT, 0, 2));
                break;
            case "CNOTL":
                GateData nullGate = new GateData(null, Gates.CNOTL.height);
                payload.Add((nullGate, 0, 0));
                for (int i = 0; i < 4; i++)
                {
                    int lineIndex = i % 2;
                    payload.Add((Gates.CNOT, lineIndex, i));
                }
                break;
            default:
                return false;
        }
        foreach ((GateData, int, int) currPayload in payload)
        {
            int nodeIndex = getNodeIndex(g);
            GateData newGate = currPayload.Item1;
            int lineIndex = currPayload.Item2 + g.Value.lineIndex;
            int index = currPayload.Item3 + nodeIndex;
            if (currPayload.Item2 == 0 && currPayload.Item3 == 0)
            {
                //if both offsets are 0 we replace g
                //Debug.Log($"ni:{nodeIndex} - li {lineIndex}");
                ld.setGateAtIndex(newGate, lineIndex, nodeIndex, replace: true);
                g = ld.getNthNodes(nodeIndex)[lineIndex];
            }
            else if (!ld.setGateAtIndex(newGate, lineIndex, index))
            {
                ld.insertGateBeforeIndex(newGate, lineIndex, index > 0 ? index : index + 1);
            }



        }
        return true;
    }

    //returns tuple containing the GateData that will be the substituted (if no
    // substituion is possible then it will return null gate), the lineIndex offset from root at which to place
    //substitute, the index offset from root, and a HashSet of all the nodes that will be replaced. 
    public (GateData, int, int, HashSet<LinkedListNode<GateData>>) checkSubstitution(LinkedListNode<GateData> node)
    {
        //(GateData, int, int, HashSet<LinkedListNode<GateData>>) output = (new GateData(null), 0, 0, null);
        HashSet<LinkedListNode<GateData>> gatesToReplace;
        int n;
        try
        {
            LinkedListNode<GateData> nextNode = nextGateNode(node);
            LinkedListNode<GateData> nextNextNode = nextGateNode(nextNode);

            n = getNodeIndex(node);
            LinkedListNode<GateData> nodeBelow = null;
            LinkedListNode<GateData> nextBelow = null;
            LinkedListNode<GateData> nextNextBelow = null;
            LinkedListNode<GateData> prevNode = null;
            LinkedListNode<GateData> prevBelow = null;
            if (node.Value.lineIndex + 1 < ld.lines.Length)
            {
                nodeBelow = ld.getNthNodes(n)[node.Value.lineIndex + 1];
                nextBelow = nextGateNode(nodeBelow);
                nextNextBelow = nextGateNode(nextBelow);
            }

            switch (node.Value.type)
            {
                //case "NOT":
                //    if(nextNode != null && nextNode.Value.type == "NOT")
                //    {
                //        gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                //        gatesToReplace.Add(node);
                //        gatesToReplace.Add(nextNode);
                //        return (new GateData(null), 0, 0, gatesToReplace);

                //    }
                //    break;
                case "H":

                    if (nextNode != null && nextNode.Value.type == "H")
                    {
                        gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                        gatesToReplace.Add(node);
                        gatesToReplace.Add(nextNode);
                        return (new GateData(null), 0, 0, gatesToReplace);
                    }

                    if (nextNextNode != null && nextNextNode.Value.type == "H")
                    {
                        string nextGate = nextNode.Value.type;
                        LinkedListNode<GateData> nodeAbove;
                        LinkedListNode<GateData> nextAbove;
                        switch (nextGate)
                        {
                            case "NOT":
                                gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                                gatesToReplace.Add(node);
                                gatesToReplace.Add(nextNode);
                                gatesToReplace.Add(nextNextNode);
                                return (Gates.Z, 0, 1, gatesToReplace);
                            case "Z":
                                gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                                gatesToReplace.Add(node);
                                gatesToReplace.Add(nextNode);
                                gatesToReplace.Add(nextNextNode);
                                return (Gates.NOT, 0, 1, gatesToReplace);
                            //case "CNOT":


                            //    if (nodeBelow.Value.type == "H" && nextNextBelow.Value.type == "H")
                            //    {
                            //        gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                            //        gatesToReplace.Add(node);
                            //        gatesToReplace.Add(nextNode);
                            //        gatesToReplace.Add(nextNextNode);
                            //        gatesToReplace.Add(nodeBelow);
                            //        gatesToReplace.Add(nextBelow);
                            //        gatesToReplace.Add(nextNextBelow);
                            //        return (Gates.CNOT, 0, 1, gatesToReplace);
                            //    }
                            //    else
                            //    {
                            //        break;

                            //}
                            case "CNOT-1":
                                n = getNodeIndex(node);
                                gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                                gatesToReplace.Add(node);
                                //gatesToReplace.Add(node.Next);
                                gatesToReplace.Add(nextNextNode);
                                //we need to add the first half of the CNOT-1 which is located one line above and one index to the right of node;
                                nodeAbove = ld.getNthNodes(n)[node.Value.lineIndex - 1];
                                nextAbove = nextGateNode(nodeAbove);
                                gatesToReplace.Add(nextAbove);
                                return (Gates.CZ, -1, getNodeIndex(nextNode) - n, gatesToReplace);
                            case "CZ-1":
                                n = getNodeIndex(node);
                                gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                                gatesToReplace.Add(node);
                                //gatesToReplace.Add(node.Next);
                                gatesToReplace.Add(nextNextNode);
                                //we need to add the first half of the CZ-1 which is located one line above and one index to the right of node;
                                nodeAbove = ld.getNthNodes(n)[node.Value.lineIndex - 1];
                                nextAbove = nextGateNode(nodeAbove);
                                gatesToReplace.Add(nextAbove);

                                return (Gates.CNOT, -1, getNodeIndex(nextNode)- n, gatesToReplace);
                        }
                    }
                    break;
                case "CNOT":



                    if (nextNode != null && nextNode.Value.type == "CNOT" && nextNextNode != null && nextNextNode.Value.type == "CNOT" &&
                        nextBelow != null && nextBelow.Value.type == "CNOT-1" && nextNextBelow != null && nextNextBelow.Value.type == "CNOT-1")
                    {
                        gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                        gatesToReplace.Add(node);
                        gatesToReplace.Add(nextNode);
                        gatesToReplace.Add(nextNextNode);
                        return (Gates.SWAP, 0, 1, gatesToReplace);
                    }
                    else
                    {
                        LinkedListNode<GateData> nodeBelowBelow = null;
                        LinkedListNode<GateData> nextBelowBelow = null;
                        LinkedListNode<GateData> nextNextBelowBelow = null;

                        if (node.Value.lineIndex + 2 < ld.lines.Length)
                        {
                            nodeBelowBelow = ld.getNthNodes(n)[node.Value.lineIndex + 2];
                            nextBelowBelow = nextGateNode(nodeBelowBelow);
                            nextNextBelowBelow = nextGateNode(nextBelowBelow);

                        }
                        LinkedListNode<GateData> nextNextNextBelow = nextGateNode(nextNextBelow);
                        if (nextNode != null && nextNode.Value.type == "CNOT" && nextBelow != null && nextBelow.Value.type == "CNOT" &&
                            nextNextBelow != null && nextNextBelow.Value.type == "CNOT-1" && nextNextNextBelow != null && nextNextNextBelow.Value.type == "CNOT" &&
                            nextBelowBelow != null && nextBelowBelow.Value.type == "CNOT-1" && nextNextBelowBelow != null && nextNextBelowBelow.Value.type == "CNOT-1")
                        {
                            gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                            gatesToReplace.Add(node);
                            gatesToReplace.Add(nextNode);
                            gatesToReplace.Add(nextBelow);
                            gatesToReplace.Add(nextNextNextBelow);
                            return (Gates.CNOTL, 0, 2, gatesToReplace);
                        }
                        prevNode = prevGateNode(node);
                        prevBelow = prevGateNode(nodeBelow);
                        if (nextNode != null && nextNode.Value.type == "H" && nextBelow != null && nextBelow.Value.type == "H" &&
                            nodeBelow != null && nodeBelow.Value.type == "CNOT-1" && prevBelow != null && prevBelow.Value.type == "H" && prevNode != null && prevNode.Value.type == "H")
                        {
                            gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                            gatesToReplace.Add(node);
                            gatesToReplace.Add(nextNode);
                            gatesToReplace.Add(prevNode);
                            gatesToReplace.Add(nextBelow);
                            gatesToReplace.Add(prevBelow);
                            return (Gates.CNOT, 0, 0, gatesToReplace);
                        }

                        //if (nextNode != null && nextNode.Value.type == "CNOT" && nextBelow.Value.type == "CNOT-1")
                        //{
                        //    gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                        //    gatesToReplace.Add(node);
                        //    gatesToReplace.Add(nextNode);
                        //    return (new GateData(null), 0, 0, gatesToReplace);

                        //}
                        break;

                    }
                case "CZ":

                    prevNode = prevGateNode(node);
                    prevBelow = prevGateNode(nodeBelow);
                    if (nextBelow != null && nextBelow.Value.type == "H" && prevBelow != null && prevBelow.Value.type == "H")
                    {
                        gatesToReplace = new HashSet<LinkedListNode<GateData>>();
                        gatesToReplace.Add(node);
                        gatesToReplace.Add(prevBelow);
                        gatesToReplace.Add(nextBelow);
                        return (Gates.CNOT, 0, 0, gatesToReplace);
                    }
                    break;
                default:
                    break;
            }
        }
        catch { return (new GateData(null), 0, 0, null); }

        return (new GateData(null), 0, 0, null);

    }
    private HashSet<LinkedListNode<GateData>> findSubstitution()
    {
        return null;
    }

    private LinkedListNode<GateData> prevGateNode(LinkedListNode<GateData> node)
    {
        if (node == null)
        {
            return null;
        }
        LinkedListNode<GateData> prevNode = node.Previous;
        while (prevNode != null && prevNode.Value.type == null)
        {
            prevNode = prevNode.Previous;
        }
        return prevNode;
    }


    private LinkedListNode<GateData> nextGateNode(LinkedListNode<GateData> node)
    {
        if (node == null)
        {
            return null;
        }
        LinkedListNode<GateData> nextNode = node.Next;
        while (nextNode != null && nextNode.Value.type == null)
        {
            nextNode = nextNode.Next;
        }
        return nextNode;
    }

    public void makeSubstition((GateData, int, int, HashSet<LinkedListNode<GateData>>) subInfo, LinkedListNode<GateData> root)
    {
        if (subInfo.Item4 == null)
        {
            return;
        }
        int lineIndex = root.Value.lineIndex + subInfo.Item2;
        //lineIndex = root.Value.lineIndex;
        Debug.Log($"NEW INDEX! {lineIndex}, Root index:{root.Value.lineIndex}");
        int index = getNodeIndex(root) + subInfo.Item3;
        Vector3 rootTransform = renderedGates[root].GetComponent<Transform>().localPosition;
        foreach (LinkedListNode<GateData> node in subInfo.Item4)
        {
            GameObject toDestroy = renderedGates[node];
            renderedGates.Remove(node);
            Destroy(toDestroy);
            int n = getNodeIndex(node);
            ld.setGateAtIndex(new GateData(null, node.Value.height), node.Value.lineIndex, n, replace: true);

        }
        if (subInfo.Item1.type != null && ld.setGateAtIndex(subInfo.Item1, lineIndex, index))
        {
            GameObject newGate = Instantiate(Gates.loadGate(subInfo.Item1.type));
            newGate.transform.parent = renderedLines[lineIndex].GetComponent<Transform>();
            newGate.transform.localPosition = rootTransform + new Vector3(subInfo.Item3 * Constants.gridResolution_w * 3, 0f, 0f);
            LinkedListNode<GateData> nodeRef = ld.getNthNodes(index)[lineIndex];
            newGate.GetComponent<BaseGateBehavior>().nodeRef = nodeRef;
            renderedGates[nodeRef] = newGate;
        }
        Debug.Log(ld.ToString());

    }

    private void renderLevel()
    {

        ld.simplify();
        float linesOffset = (float)ld.lines.Length / 2.0f * Constants.gridResolution_h - Constants.gridResolution_h / 2f;
        float gateOffset = Mathf.Ceil((float)ld.lines[0].Count / 2.0f) * Constants.gridResolution_w * 3 * -1;
        renderedGates = new Dictionary<LinkedListNode<GateData>, GameObject>();
        HashSet<string> validGates = new HashSet<string>(Gates.VALID_GATES.Select(x => x.type));
        renderedLines = new GameObject[ld.lines.Length];
        for (int i = 0; i < ld.lines.Length; i++)
        {
            GameObject newLine = Instantiate(linePrefab);
            renderedLines[i] = newLine;
            newLine.transform.parent = currLevel.transform;
            LineRenderer lr = newLine.GetComponent<LineRenderer>();
            lr.name = $"Line-{i}";
            float yPosition = linesOffset - i * Constants.gridResolution_h;
            newLine.transform.localPosition = new Vector3(0f, yPosition, 0f);
            lr.SetPosition(0, new Vector3(-50f * Constants.gridResolution_w, yPosition, 0f));
            lr.SetPosition(1, new Vector3(50f * Constants.gridResolution_w, yPosition, 0f));
            LinkedListNode<GateData> currNode = ld.lines[i].First;
            for (int j = 0; j < ld.lines[i].Count; j++)
            {
                GateData currGate = currNode.Value;
                if (validGates.Contains(currGate.type))
                {
                    GameObject newGate = Instantiate(Gates.loadGate(currGate.type));
                    newGate.transform.parent = newLine.transform;
                    float xPosition = gateOffset + j * Constants.gridResolution_w * 3;
                    newGate.transform.localPosition = new Vector3(xPosition, -Constants.gridResolution_h / 2f, 0f);
                    Debug.Log($"Rendering Node at: line {i}, x_index:{j}\n\tNode: {currNode.Value.lineIndex}, {currNode.Value}");
                    newGate.GetComponent<BaseGateBehavior>().nodeRef = currNode;
                    renderedGates.Add(currNode, newGate);
                }
                currNode = currNode.Next;
            }
        }

    }

    private void clearPrevLevel()
    {
        if (prevLevel == null)
            return;
        else
        {
            //Debug.Log("TODO CLEAR PREV LEVEL");
            Destroy(prevLevel);
            prevLevel = null;
        }

    }

    private void buildGateCache()
    {
        gatesToExpand = new Dictionary<string, List<LinkedListNode<GateData>>>();

        for (int i = 0; i < ld.lines.Length; i++)
        {
            LinkedListNode<GateData> currNode = ld.lines[i].First;
            for (int j = 0; j < ld.lines[i].Count; j++)
            {
                if (typesToExpand.Contains(currNode.Value.type))
                {
                    if (!gatesToExpand.ContainsKey(currNode.Value.type))
                    {
                        gatesToExpand[currNode.Value.type] = new List<LinkedListNode<GateData>>();
                    }
                    gatesToExpand[currNode.Value.type].Add(currNode);
                }
                currNode = currNode.Next;
            }
        }
    }

    public List<LinkedListNode<GateData>> allGateNodes()
    {
        return renderedGates.Keys.ToList();
    }


}

public struct GateData
{
    public string type;
    public int height;
    public int lineIndex;
    public GateData(string t, int h, int li)
    {
        type = t;
        height = h;
        lineIndex = li;
    }

    public GateData(string t, int h) : this(t, h, -1) { }

    public GateData(string t) : this(t, 1, -1) { }


    public override string ToString()
    {
        return type != null ? $"[{type}]" : "-";
    }
}

public struct LevelData
{
    public LinkedList<GateData>[] lines;

    public LevelData(int nLines)
    {
        lines = new LinkedList<GateData>[nLines];
        for (int i = 0; i < nLines; i++)
        {
            lines[i] = new LinkedList<GateData>();
        }
    }

    public override string ToString()
    {
        return string.Join("\n", lines.Select(y => string.Join("\t\t", y.Select(x => x.ToString()))));
    }

    public void simplify() {

        //Debug.Log("Start Simplify");
        for (int i = 0; i < lines.Length; i++)
        {
            LinkedListNode<GateData> node = lines[i].First;
            string outstirng = "";
            int offset = 0;
            LinkedListNode<GateData>[] prevNodes = new LinkedListNode<GateData>[lines[i].Count];
            for (int j = 0; j < lines[i].Count; j++)
            {
                prevNodes[j] = node;
                if(node.Value.type == null) {
                    offset += 1;
		        }
                else {
                    string type = node.Value.type;
                    int height = node.Value.height;
                    if(height > 1) {
                        offset = 0;
		            }
                    int newIndex = j - offset;
                    int lineIndex = node.Value.lineIndex;
                    //Debug.Log(newIndex);
                    node.Value = new GateData(null);
                    prevNodes[newIndex].Value = new GateData(type, height, lineIndex);
                    //Debug.Log($"line index:{i}");
		        }
                //outstirng += node.Value;
                node = node.Next;
            }
            node = lines[i].First;
            for (int j = 0; j < lines[i].Count; j++)
            {
                outstirng += $"{node.Value}, {node.Value.lineIndex} --";
                node = node.Next;
            }
            //Debug.Log(outstirng);
        }
        //Debug.Log(ToString());
    }

    public bool addGate(GateData newGate, int lineIndex, bool addLast = true)
    {
        if (lineIndex + newGate.height > lines.Length)
            return false;
        for (int i = 0; i < lines.Length; i++)
        {
            GateData gateToAdd;
            if (i < lineIndex || i >= lineIndex + newGate.height || newGate.type == null)
            {
                gateToAdd = new GateData(null);
            }
            else
            {
                gateToAdd = new GateData(i > lineIndex ? $"{newGate.type}-{i - lineIndex}" : newGate.type, newGate.height, i);
            }
            if (addLast)
                lines[i].AddLast(gateToAdd);
            else
                lines[i].AddFirst(gateToAdd);


        }
        return true;
    }

    public LinkedListNode<GateData>[] getNthNodes(int index)
    {
        if (lines.Length <= 0 || index >= lines[0].Count)
            return null;
        LinkedListNode<GateData>[] output = new LinkedListNode<GateData>[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            LinkedListNode<GateData> curr = lines[i].First;
            for (int j = 1; j <= index; j++)
                curr = curr.Next;
            output[i] = curr;
        }

        return output;
    }
    public bool setGateAtIndex(GateData newGate, int lineIndex, int index, bool replace = false)
    {
        int height = newGate.height;
        string type = newGate.type;
        if (lineIndex + height > lines.Length)
        {
            return false;
        }
        if (index < 0 || index >= lines[0].Count())
        {
            return addGate(newGate, lineIndex, addLast: index >= lines[0].Count());
        }
        if (!replace)
        {
            for (int i = lineIndex; i < lineIndex + height; i++)
            {
                if (lines[i].ElementAt<GateData>(index).type != null)
                {
                    return false;
                }
            }
        }
        LinkedListNode<GateData>[] gatesToChange = getNthNodes(index);
        if (gatesToChange == null)
        {
            return false;
        }
        for (int i = lineIndex; i < lineIndex + height; i++)
        {
            LinkedListNode<GateData> newNode;
            if (type == null)
            {
                newNode = new LinkedListNode<GateData>(new GateData(null, 1, i));
            }
            else
            {
                newNode = new LinkedListNode<GateData>(new GateData(i > lineIndex ? $"{type}-{i - lineIndex}" : type, height, i));
            }
            lines[i].AddBefore(gatesToChange[i], newNode);
            lines[i].Remove(gatesToChange[i]);
        }


        return true;
    }

    public bool insertGateBeforeIndex(GateData newGate, int lineIndex, int index)
    {
        int height = newGate.height;
        string type = newGate.type;
        if (lineIndex + height > lines.Length)
        {
            return false;
        }
        if (index < 0 || index >= lines[0].Count())
        {
            return addGate(newGate, lineIndex, addLast: index >= lines[0].Count());
        }

        LinkedListNode<GateData>[] gatesAfter = getNthNodes(index);
        if (gatesAfter == null)
        {
            return false;
        }
        for (int i = 0; i < gatesAfter.Length; i++)
        {
            LinkedListNode<GateData> newNode;


            if (i < lineIndex || i >= lineIndex + newGate.height || type == null)
            {
                newNode = new LinkedListNode<GateData>(new GateData(null));
            }
            else
            {
                newNode = new LinkedListNode<GateData>(new GateData(i > lineIndex ? $"{type}-{i - lineIndex}" : type, height, i));
            }
            lines[i].AddBefore(gatesAfter[i], newNode);
        }


        return true;
    }


}

