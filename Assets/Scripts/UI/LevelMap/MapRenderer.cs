using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.LevelMap
{
    public class MapRenderer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image tutorial;
        [SerializeField] private Text txtLevel;
        [SerializeField] private StarRenderer starRenderer;
        [SerializeField] private Image lockLevel;
        [SerializeField] private Image line;

        private int _level = 0;
        public int Level => _level;

        private int _starCount = 0;
        public int StarCount => _starCount;

        private bool _isLock = true;
        public bool IsLock => _isLock;

        private bool _hasLine = false;
        public bool HasLine => _hasLine;
        

        private void Start()
        {
            RenderLevel();
            RenderStar();
            RenderLock();
        }

        public void SetLevel(int level)
        {
            _level = level;
            RenderLevel();
        }

        public void SetStar(int starCount)
        {
            _starCount = starCount;
            RenderStar();
        }

        public void SetLock(bool isLock)
        {
            _isLock = isLock;
            RenderLock();
        }

        public void SetLine(bool hasLine)
        {
            _hasLine = hasLine;
            RenderLine();
        }

        private void RenderLevel()
        {
            if (_level == 1)
            {
                tutorial.enabled = true;
                txtLevel.enabled = false;
            }
            else
            {
                tutorial.enabled = false;
                txtLevel.enabled = true;
                txtLevel.text = _level.ToString();
            }
        }

        private void RenderStar()
        {
            starRenderer.Render(_starCount);
        }

        private void RenderLock()
        {
            lockLevel.enabled = _isLock;
        }

        private void RenderLine()
        {
            line.enabled = _hasLine;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isLock) return;
            MapManager.Instance.CurrentLevel = _level;
            SceneManager.LoadScene($"Level");
        }
    }
}