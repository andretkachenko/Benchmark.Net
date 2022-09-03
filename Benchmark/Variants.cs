using System;
using System.Collections.Generic;

namespace Benchmark
{
    public class Variants
    {
        public static readonly List<Item> Items = new List<Item>
        {
            new Item { Jurisdiction = "AU", Date = DateTime.Now },
            new Item { Jurisdiction = "NZ", Date = DateTime.Now.AddDays(1) },
            new Item { Jurisdiction = "AU", Date = DateTime.Now },
            new Item { Jurisdiction = "NZ", Date = DateTime.Now.AddDays(1) },
        };

        public static bool A(Item item, string jurisdictionCode, DateTime? payRunDate)
        {
            if (item == null) return false;
            if (jurisdictionCode != item.Jurisdiction) return false;
            if (payRunDate != item.Date) return false;
            return true;
        }

        public static bool B(Item item, string jurisdictionCode, DateTime? payRunDate)
        {
            if (item == null ||
                jurisdictionCode != item.Jurisdiction ||
                payRunDate != item.Date) return false;
            return true;
        }

        public static bool C(Item item, string jurisdictionCode, DateTime? payRunDate)
        {
            return item == null ||
                jurisdictionCode != item.Jurisdiction ||
                payRunDate != item.Date;
        }
    }
}