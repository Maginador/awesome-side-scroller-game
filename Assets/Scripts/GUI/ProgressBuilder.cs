using System.Collections.Generic;
using Info;
using Level;
using Playfab;
using PlayFab.DataModels;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;


namespace GUI
{
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

        private int _currentLevel = 0;
    
        // Start is called before the first frame update
        private void Start()
        {
            _currentOrientation = Screen.orientation;
            progressEdgesList = new List<GameObject>();
            progressNodeList = new List<GameObject>();
    
            BuildNodes();
            BuildEdges();
        }

        private void OnEnable()
        {
            PlayfabManager.Instance.AddProgressResultListener(OnProgressResult);
            PlayfabManager.Instance.GetPlayerProgress();
        }

        private void OnProgressResult(Dictionary<string, ObjectResult> obj)
        {
            Debug.Log("OnProgressResult");

            foreach (var progress in obj)
            {
                if (progress.Key == "ProgressData")
                {
                    Debug.Log("key : " + progress.Key +" : value : " + progress.Value.DataObject);
                    var info = ProgressInfo.GetProgressFromJson(progress.Value.DataObject.ToString());

                    _currentLevel = info.progress;
                    Debug.Log("Current Level : " + _currentLevel);
                }
            }

            UpdateProgressUI();
        }

        private void UpdateProgressUI()
        {
            //TODO Update ui with new progress
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
            LevelRunner.Instance.RunLevel(index);
        }

        void BuildEdges()
        {
        
        }
        // Update is called once per frame

        public void UpdateCurrentLevel(int level)
        {
            _currentLevel = level;
            PlayfabManager.Instance.SetProgress(level);
        }
    }
}