using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace UI.LevelMap
{
    public class MapController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public EnhancedScroller myScroller;
        public MapRendererHorizontal mapHorizontalPrefab;
        public bool isReverse = true;
        private int MapCount => MapManager.Instance.MapCount;
        private int RowCount => MapCount / MapRendererHorizontal.MAXItem;

        private void Start()
        {
            myScroller.Delegate = this;
            myScroller.ReloadData();
            int dataIndex;
            if (isReverse)
            {
                dataIndex = RowCount - (MapManager.Instance.HighestLevel / MapRendererHorizontal.MAXItem);
            }
            else
            {
                dataIndex = (MapManager.Instance.HighestLevel / MapRendererHorizontal.MAXItem);
            }

            myScroller.ScrollPosition =
                myScroller.GetScrollPositionForDataIndex(dataIndex, EnhancedScroller.CellViewPositionEnum.Before);
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return RowCount;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 100f;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            if (isReverse)
            {
                dataIndex = RowCount - dataIndex - 1;
            }

            MapRendererHorizontal mapRendererHorizontal =
                scroller.GetCellView(mapHorizontalPrefab) as MapRendererHorizontal;
            if (mapRendererHorizontal is not null)
            {
                mapRendererHorizontal.Render(dataIndex);
            }

            return mapRendererHorizontal;
        }
    }
}