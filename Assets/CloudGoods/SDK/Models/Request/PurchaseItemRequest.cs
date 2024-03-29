﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace CloudGoods.SDK.Models
{
    public class PurchaseItemRequest : IRequestClass
    {
        public int ItemId;
        public int BuyAmount;
        public PaymentType PaymentOption;
        public int SaveLocation;
        public List<int> AccessLocations;
        public ConsumeUponPurchase Consume = null;

        public string ToHashable()
        {
            string locations = "";
            if (AccessLocations != null)
                AccessLocations.ForEach(l => locations += l);
            return string.Format("{0}{1}{2}{3}{4}", ItemId, BuyAmount, PaymentOption, SaveLocation, (Consume != null ? Consume.Amount.ToString() : ""));
        }

        public PurchaseItemRequest(int itemId, int buyAmount, PaymentType paymentOption, int saveLocation, List<int> accessLocation, ConsumeUponPurchase consume = null)
        {
            ItemId = itemId;
            BuyAmount = buyAmount;
            PaymentOption = paymentOption;
            SaveLocation = saveLocation;
            Consume = consume;
        }

        /// <summary>
        /// if not null will take the selected Amount of item and instantly consume them from the users purchase.
        /// </summary>
        public class ConsumeUponPurchase
        {
            public int Amount;

            public ConsumeUponPurchase(int amount)
            {
                Amount = amount;
            }
        }

        public enum PaymentType
        {
            NotValid = 0,
            Standard = 1,
            Premium = 2,
            Free = 3
        }
    }
}
