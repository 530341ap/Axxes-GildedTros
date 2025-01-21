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
        public static string[] smellyItems = { "Duplicate Code", "Long Methods", "Ugly Variable Names" };

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
                    item.Quality = DecreaseItem(item);
                    if (smellyItems.Contains(item.Name) && item.Quality > 0)
                    {
                        item.Quality = DecreaseItem(item);
                    } 
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
        }

        private bool IsIncreasingItem(string name)
        {
            return name.Equals(goodWine) || backstagePasses.Contains(name);
        }

        private int DecreaseItem(Item item)
        {
            return item.SellIn <= 0 && item.Quality > 1 ? item.Quality - 2 : item.Quality - 1;
        }
    }
}
