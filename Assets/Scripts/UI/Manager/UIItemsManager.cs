using System;
using System.Collections.Generic;
using Content;
using UI.Widget;
using UnityEngine;

public class UIItemsManager : MonoBehaviour
{
//    public bool IsScrollEndless;
//    public int ItemsDataSize;
//    public Transform ItemsContainer;
//    public float WidthItemsPanel;
//    public float HeightItemsPanel;
//
//    public List<AbstractUIItemPresentation<T>> Items;
//    public UIItemObjectPool UIItemsObjectPool;
//    
//    private UIScrollViewCustom _uiScrollView;
//    private UIPanel _uiPanel;
//    
//    private void Start()
//    {
//        base.Start();
//    }
//    
//    private void UpdateItemsView()
//    {
//        AbstractUIItemPresentation<T> first = Items[0];
//        SortItems();
//        int index = Items.IndexOf(first);
//        int itemIndex = -1;
//        int dataIndex = -1;
//        if (index == Items.Count - 1)
//        {
//            var lastIndex = Items[Items.Count-2].Index;
//            lastIndex += 1;
//            if (lastIndex > UIItemsObjectPool.Objects.Count)
//                lastIndex = 0;
//            lastIndex -= 1;
//            if (lastIndex < 0)
//                lastIndex = 0;
//            itemIndex = index;
//            dataIndex = lastIndex;
//        }
//        else if (index == 1)
//        {    
//            var firstIndex = Items[1].Index;
//            firstIndex -= 2;
//            if (firstIndex < 0)
//                firstIndex = UIItemsObjectPool.Objects.Count - 1;
//            itemIndex = 0;
//            dataIndex = firstIndex;
//        }
//        if (dataIndex == -1) return;
//        ApplyDataToItem(itemIndex, dataIndex);
//        Redraw();
//        LockUnlockScroll();
//    }
//
//    private void ApplyDataToItem(int itemIndex, int dataIndex)
//    {
//        Items[itemIndex].ApplyData(UIItemsObjectPool.Objects[dataIndex]);
//    } 
//
//    private void LockUnlockScroll()
//    {
//        if (!IsScrollEndless)
//        {
//            if (_uiScrollView.movement == UIScrollView.Movement.Horizontal)
//            {
//                if (Items[Items.Count - 1].Index == UIItemsObjectPool.Objects[0].Index &&
//                    !_uiScrollView.IsRightScrollLocked)
//                {
//                    _uiScrollView.LockRightScroll();
//                }
//                else if (_uiScrollView.IsRightScrollLocked)
//                {
//                    _uiScrollView.UnlockRightScroll();
//                }
//
//                if (Items[0].Index == UIItemsObjectPool.Objects[UIItemsObjectPool.Objects.Count - 1].Index &&
//                    !_uiScrollView.IsLeftScrollLocked)
//                {
//                    _uiScrollView.LockLeftScroll();
//                }
//                else if (_uiScrollView.IsLeftScrollLocked)
//                {
//                    _uiScrollView.UnlockLeftScroll();
//                }
//            }
//
//            else if (_uiScrollView.movement == UIScrollView.Movement.Vertical)
//            {
//                if (Items[Items.Count - 1].Index == UIItemsObjectPool.Objects[0].Index &&
//                    !_uiScrollView.IsDownScrollLocked)
//                {
//                    _uiScrollView.LockDownScroll();
//                }
//                else if (_uiScrollView.IsDownScrollLocked)
//                {
//                    _uiScrollView.UnlockDownScroll();
//                }
//
//                if (Items[0].Index == UIItemsObjectPool.Objects[UIItemsObjectPool.Objects.Count - 1].Index &&
//                    !_uiScrollView.IsUpScrollLocked)
//                {
//                    _uiScrollView.LockUpScroll();
//                }
//                else if (_uiScrollView.IsUpScrollLocked)
//                {
//                    _uiScrollView.UnlockUpScroll();
//                }
//            }
//        }
//    }
//
//    private void Redraw()
//    {
//        int dataIndex = Items[0].Index - 1;
//        for (int i = 0; i < Items.Count; i++)
//        {
//            ApplyDataToItem(i, dataIndex);
//            dataIndex = dataIndex + 1 < UIItemsObjectPool.Objects.Count ? dataIndex + 1 : 0;
//        }
//    }
//
//    private void SortItems()
//    {
//        int shuffleDirection = (_uiScrollView.movement == UIScrollView.Movement.Horizontal) ? 0 : 1;
//        Items.Sort((first, second) => second.transform.localPosition[shuffleDirection]
//            .CompareTo(first.transform.localPosition[shuffleDirection]));
//    }
//    
//    private void CreateItemsDataPool(int itemsDataSize)
//    {
//        for (int i = 0; i < itemsDataSize; i++)
//        {
//            ItemData go = UIItemsObjectPool.NewFirst();
//            if (!UIItemsObjectPool.Objects.Exists(x => go.Id == x.Id))
//            {
//                UIItemsObjectPool.Objects.Add(go);
//            }
//        }
//    }
//
//    private void InitItemsView()
//    {
//        for (int i = 0; i < Items.Count; i++)
//        {
//            Items[i].Init(UIItemsObjectPool.Objects[i]);
//        }
//    }
}
