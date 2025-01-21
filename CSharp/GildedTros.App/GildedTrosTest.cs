using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        [Fact]
        public void QualityLowersOneWhenSellInPositive()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 1, Quality = 2 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(1, Items[0].Quality);
        }

        [Fact]
        public void QualityLowersTwoWhenSellInNegative()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = -1, Quality = 2 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void QualitySmellyItemsLowersTwoWhenSellInPositive()
        {
            IList<Item> Items = new List<Item> { new Item {Name = "Duplicate Code", SellIn = 3, Quality = 6},
                new Item {Name = "Long Methods", SellIn = 3, Quality = 6},
                new Item {Name = "Ugly Variable Names", SellIn = 3, Quality = 6} };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].Quality);
            Assert.Equal(4, Items[1].Quality);
            Assert.Equal(4, Items[2].Quality);
        }

        [Fact]
        public void QualitySmellyItemsLowersFourWhenSellInNegative()
        {
            IList<Item> Items = new List<Item> { new Item {Name = "Duplicate Code", SellIn = -1, Quality = 6},
                new Item {Name = "Long Methods", SellIn = -1, Quality = 6},
                new Item {Name = "Ugly Variable Names", SellIn = -1, Quality = 6} };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(2, Items[0].Quality);
            Assert.Equal(2, Items[1].Quality);
            Assert.Equal(2, Items[2].Quality);
        }

        [Fact]
        public void QualityNeverBelowZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void KeychainNeverChangesQuality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "B-DAWG Keychain", SellIn = 0, Quality = 80 }, 
                new Item { Name = "B-DAWG Keychain", SellIn = -1, Quality = 80 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(80, Items[0].Quality);
            Assert.Equal(80, Items[1].Quality);
        }

        [Fact]
        public void QualityGoodWineIncreasesOneWhenSellInPositive()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 1, Quality = 2 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(3, Items[0].Quality);
        }

        [Fact]
        public void QualityGoodWineIncreasesTwoWhenSellInNegative()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = -1, Quality = 2 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].Quality);
        }

        [Fact]
        public void QualityGoodWineNeverAbove50()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = -1, Quality = 49 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void QualityBackstagePassesNeverAbove50()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 12, Quality = 49 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void QualityBackstagePassesIncreasesTwoWhenSellInUnder11()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 2 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(4, Items[0].Quality);
        }

        [Fact]
        public void QualityBackstagePassesIncreasesThreeWhenSellInUnder6()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 2 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(5, Items[0].Quality);
        }

        [Fact]
        public void QualityBackstagePassesZeroWhenPastSellInDate()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 0, Quality = 50 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }
    }
}