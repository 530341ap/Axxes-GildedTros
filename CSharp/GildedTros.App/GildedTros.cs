using System.Collections.Generic;
using System.Linq;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        public static string legendaryItem = "B-DAWG Keychain";
        public static string goodWine = "Good Wine";
        public static string[] backstagePasses = { "Backstage passes for Re:factor", "Backstage passes for HAXX" };

        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            var increasingItems = Items.Where(i => IsIncreasingItem(i.Name)).ToList();
            var decreasingItems = Items.Where(i => !i.Name.Equals(legendaryItem) && !IsIncreasingItem(i.Name)).ToList();

            foreach (var item in decreasingItems)
            {
                if (item.Quality > 0)
                {
                    item.Quality = item.SellIn <= 0 && item.Quality > 1 ? item.Quality - 2 : item.Quality - 1;
                }
                item.SellIn = item.SellIn - 1;
            }
            foreach (var item in increasingItems)
            {
                if (backstagePasses.Contains(item.Name) && item.SellIn <= 0) 
                {
                    item.Quality = 0;
                    item.SellIn = item.SellIn - 1;
                    continue;
                }

                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;

                    if (backstagePasses.Contains(item.Name) && item.Quality < 50)
                    {
                        item.Quality = item.Quality < 49 && item.SellIn < 6 ? item.Quality + 2 :
                        (item.SellIn < 11 ? item.Quality + 1 : item.Quality);
                    }

                    if (item.Name == goodWine && item.Quality < 50 && item.SellIn <= 0)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
                item.SellIn = item.SellIn - 1;
            }
            //for (var i = 0; i < Items.Count; i++)
            //{
            //    if (Items[i].Name != "Good Wine"
            //        && Items[i].Name != "Backstage passes for Re:factor"
            //        && Items[i].Name != "Backstage passes for HAXX")
            //    {
            //        if (Items[i].Quality > 0)
            //        {
            //            if (Items[i].Name != "B-DAWG Keychain")
            //            {
            //                Items[i].Quality = Items[i].Quality - 1;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (Items[i].Quality < 50)
            //        {
            //            Items[i].Quality = Items[i].Quality + 1;

            //            if (Items[i].Name == "Backstage passes for Re:factor"
            //            || Items[i].Name == "Backstage passes for HAXX")
            //            {
            //                if (Items[i].SellIn < 11)
            //                {
            //                    if (Items[i].Quality < 50)
            //                    {
            //                        Items[i].Quality = Items[i].Quality + 1;
            //                    }
            //                }

            //                if (Items[i].SellIn < 6)
            //                {
            //                    if (Items[i].Quality < 50)
            //                    {
            //                        Items[i].Quality = Items[i].Quality + 1;
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    if (Items[i].Name != "B-DAWG Keychain")
            //    {
            //        Items[i].SellIn = Items[i].SellIn - 1;
            //    }

            //    if (Items[i].SellIn < 0)
            //    {
            //        if (Items[i].Name != "Good Wine")
            //        {
            //            if (Items[i].Name != "Backstage passes for Re:factor"
            //                && Items[i].Name != "Backstage passes for HAXX")
            //            {
            //                if (Items[i].Quality > 0)
            //                {
            //                    if (Items[i].Name != "B-DAWG Keychain")
            //                    {
            //                        Items[i].Quality = Items[i].Quality - 1;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                Items[i].Quality = Items[i].Quality - Items[i].Quality;
            //            }
            //        }
            //        else
            //        {
            //            if (Items[i].Quality < 50)
            //            {
            //                Items[i].Quality = Items[i].Quality + 1;
            //            }
            //        }
            //    }
            //}
        }

        private bool IsIncreasingItem(string name)
        {
            return name.Equals(goodWine) || backstagePasses.Contains(name);
        }
    }
}
