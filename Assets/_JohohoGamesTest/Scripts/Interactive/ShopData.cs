using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop_Data", menuName = "Data/Shop Data", order = 51)]
public class ShopData : ScriptableObject
{
    public List<Shopitem> Items;
}
