﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CloudGoods.SDK.Models;

namespace CloudGoods.SDK.Store
{
    public abstract class StoreLoader : MonoBehaviour
    {
        public int maxGridAmount = 32;

        protected int currentPage = 0;

        protected List<StoreItem> items = new List<StoreItem>();
        protected List<StoreItem> filteredList = new List<StoreItem>();
        protected List<GameObject> currentPageItems = new List<GameObject>();

        void OnEnable()
        {
            StoreInitializer.OnInitializedStoreItems += OnStoreListLoaded;
        }

        void OnDisable()
        {
            StoreInitializer.OnInitializedStoreItems -= OnStoreListLoaded;
        }


        protected void OnStoreListLoaded(List<StoreItem> listItems)
        {
            items = listItems;
            LoadStoreWithPaging(items, 0);
        }

        public abstract void LoadStoreWithPaging(List<StoreItem> listItems, int pageNum);


        protected int GetPageMax(int itemAmount, int pageNum)
        {
            int pageMax = 0;

            Debug.Log("Page num: " + pageNum + "   ItemAmount: " + (GetPageAmount(itemAmount)));

            if (pageNum < GetPageAmount(itemAmount) - 1)
            {
                pageMax = maxGridAmount;
            }
            else if((itemAmount % maxGridAmount) > 0)
            {
                pageMax = itemAmount % maxGridAmount;
            }
            else
            {
                pageMax = maxGridAmount;
            }

            return pageMax;
        }

        protected virtual void ClearCurrentGrid()
        {
            foreach (GameObject gridItemObj in currentPageItems)
            {
                Destroy(gridItemObj);
            }
            currentPageItems.Clear();
        }

        protected int GetPageAmount(int itemCount)
        {
            int calcPageAmount = 0;

            calcPageAmount = itemCount / maxGridAmount;

            if ((itemCount % maxGridAmount) > 0)
                calcPageAmount++;


            Debug.Log("calc page amount: " + calcPageAmount);


            return calcPageAmount;
        }

        public List<StoreItem> GetStoreItemList()
        {
            return items;
        }

        public void SetPage(int pageNum)
        {
            if (pageNum != currentPage)
            {
                currentPage = pageNum;
                Debug.Log("Store filtered list: " + filteredList.Count);
                LoadStoreWithPaging(filteredList, pageNum);
            }
        }

    }
}
