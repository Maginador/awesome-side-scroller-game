using System.Collections;
using System.Collections.Generic;
using Level;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ProgressBuilder : MonoBehaviour
{
    
    //Scene References 
    [SerializeField] private RectTransform initialPosition;
    [SerializeField] private GameObject containerObject;
    [SerializeField] private ScrollRect scroll;

    //Lists
    [SerializeField] private List<GameObject> progressEdgesList;
    [SerializeField] private List<GameObject> progressNodeList;

    //Prefabs
    [SerializeField] private GameObject progressNodePrefab;
    [SerializeField] private GameObject progressEdgePrefab;

    //Fields
    [SerializeField] private int numberOfNodes;
    [SerializeField] private Vector3 defaultOffset;
    [SerializeField] private Vector2 yOffsetVariation;

    private ScreenOrientation _currentOrientation;
    
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _currentOrientation = Screen.orientation;
        progressEdgesList = new List<GameObject>();
        progressNodeList = new List<GameObject>();
        BuildNodes();
        BuildEdges();
        
    }

    void BuildNodes()
    {
        var sizeDelta = scroll.content.sizeDelta;
        sizeDelta = new Vector2(defaultOffset.x * numberOfNodes, sizeDelta.y);
        scroll.content.sizeDelta = sizeDelta;
        for (var i = 0; i < numberOfNodes;i++)
        {
            var node = Instantiate(progressNodePrefab, scroll.content, true);
            node.GetComponent<RectTransform>().position = initialPosition.position + defaultOffset * i + new Vector3(0,Random.Range(yOffsetVariation.x,yOffsetVariation.y),0);
            node.GetComponentInChildren<Text>().text = (i + 1).ToString();
            var i1 = i;
            node.GetComponent<Button>().onClick.AddListener(delegate{RunLevel(i1);});
            progressNodeList.Add(node);
            
        }
    }

    private void RunLevel(int index)
    {
        Debug.Log(gameObject.name);
        LevelRunner.Instance.RunLevel(index);
    }

    void BuildEdges()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
        #if UNITY_EDITOR
        Screen.orientation = Screen.width > Screen.height ? ScreenOrientation.Landscape : ScreenOrientation.Portrait;
        #endif
        
        if (_currentOrientation != Screen.orientation)
        {
            OnOrientationChanged();
        }
    }

    private void OnOrientationChanged()
    {
        _currentOrientation = Screen.orientation;
        if (Screen.orientation == ScreenOrientation.Landscape)
        {
            scroll.vertical = false;
            scroll.horizontal = true;
        }
        else
        {
            scroll.vertical = true;
            scroll.horizontal = false;
        }
    }
    
}
