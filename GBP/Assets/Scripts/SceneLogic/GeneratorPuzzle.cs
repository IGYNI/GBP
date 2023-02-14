using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GeneratorPuzzle : Puzzle
{
    [Serializable]
    public class RingTarget
    {
        public Transform targetRotation;
        public float speed;
    }
    
    [Serializable]
    public class RingHandler
    {
        public Collider collider;
        public GeneratorRing generatorRing;
        public List<RingTarget> inputPositions;
        public int currentPosition;
        public int puzzleTargetPosition;
        private Tween _tween;

        public void Advance()
        {
            if (_tween != null)
                return;
            
            if (currentPosition + 1 >= inputPositions.Count)
            {
                currentPosition = 0;
            }
            else
            {
                currentPosition++;
            }

            var target = inputPositions[currentPosition]; 
            generatorRing.SetSpeed(target.speed);
            _tween = collider.transform.DORotate(target.targetRotation.eulerAngles, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() => _tween = null);
        }

        public void InitHandler()
        {
            var target = inputPositions[currentPosition]; 
            generatorRing.SetSpeed(target.speed);
        }
        
        public void SetFullPower()
        {
            var target = inputPositions[puzzleTargetPosition]; 
            generatorRing.SetSpeed(target.speed);
        }
    }

    [SerializeField] private List<GameObject> offObjects;
    [SerializeField] private Camera raycastCamera;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private List<RingHandler> rings;
    [SerializeField] private CircuitStateView stateView;
    [SerializeField] private UnityEvent onPuzzleComplete;
    [SerializeField] private float completeDelayTime = 2f;
    [SerializeField] private Button backButton;
    [SerializeField] private Slider reactorPower;
    
    private Outline _currentHover;
    private float _checkTime;
    private bool[] _checkResult;

    private void Awake()
    {
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnEnable()
    {
        foreach (GameObject offObject in offObjects)
        {
            offObject.gameObject.SetActive(false);
        }
        state.Set(EState.Processing);
        InitPuzzle();
    }

    private void OnDisable()
    {
        foreach (GameObject offObject in offObjects)
        {
            if (offObject != null)
            {
                offObject.SetActive(true);
            }
        }
        state.Set(EState.Idle);
    }

    private void OnBackButtonClick()
    {
        gameObject.SetActive(false);
    }

    public void InitPuzzle()
    {
        _checkResult = new bool[rings.Count];
        foreach (var ringHandler in rings)
        {
            ringHandler.InitHandler();
        }
    }

    public void SetFullPower()
    {
        foreach (var ringHandler in rings)
        {
            ringHandler.SetFullPower();
        }
    }

    private void Update()
    {
        UpdatePowerGauge();
        if (state.Value != EState.Processing)
            return;

        var ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 100, 1<<layerMask))
        {
            UpdateHover(hit);
            if (Input.GetMouseButton(0))
            {
                foreach (RingHandler ring in rings)
                {
                    if (hit.collider != ring.collider) 
                        continue;
                    ring.Advance();
                    break;
                }
            }
        }
        else
        {
            UnHoverItem();
        }


        if (_checkTime > 2f)
        {
            CheckPuzzle();
            _checkTime = 0f;
        }
        

        _checkTime += Time.deltaTime;
    }

    private void UpdatePowerGauge()
    {
        float power = 0f;
        foreach (RingHandler ringHandler in rings)
        {
            var maxPowerSection = ringHandler.inputPositions[ringHandler.puzzleTargetPosition];
            power += ringHandler.generatorRing.CurrentSpeed / maxPowerSection.speed;
        }
        
        reactorPower.value = power / rings.Count;
    }

    private void UpdateHover(RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent(out Outline item))
        {
            if (item != _currentHover)
            {
                if (_currentHover != null)
                {
                    _currentHover.OutlineWidth = 0;
                }
                _currentHover = item;
            }
            _currentHover.OutlineWidth = 3;
        }
        else
            UnHoverItem();
    }

    private void UnHoverItem()
    {
        if (_currentHover == null)
            return;
        _currentHover.OutlineWidth = 0;
        _currentHover = null;
    }

    private void CheckPuzzle()
    {
        if (_checkResult == null)
        {
            _checkResult = new bool[rings.Count];
        }
        for (var i = 0; i < _checkResult.Length; i++)
        {
            _checkResult[i] = false;
        }

        for (var i = 0; i < rings.Count; i++)
        {
            var ring = rings[i];
            _checkResult[i] = ring.currentPosition == ring.puzzleTargetPosition;
        }

        var allMatch = true;
        for (var i = 0; i < _checkResult.Length; i++)
        {
            if (_checkResult[i] == false)
            {
                allMatch = false;
                break;
            }
        }

        if (allMatch)
        {
            state.Set(EState.Complete);
            StartCoroutine(CompletePuzzleCor());
        }
    }

    private IEnumerator CompletePuzzleCor()
    {
        onPuzzleComplete.Invoke();
        stateView.ActiveState();
        yield return new WaitForSeconds(completeDelayTime);
        Hide();
    }
}
