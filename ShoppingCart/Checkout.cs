using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class Checkout
    {
        PricingRules pricingRules = new PricingRules();

        public static List<Gadgets> gadgets = new List<Gadgets>() {
                new Gadgets(){ SKU = "ipd", Name="Super iPad", Price = 549.99},
                new Gadgets(){ SKU = "mbp", Name="MacBook Pro", Price = 1399.99},
                new Gadgets(){ SKU = "atv", Name="Apple TV", Price = 109.50},
                new Gadgets(){ SKU = "vga", Name="VGA adapter", Price = 30.00}
            };
    
        public List<string> items = new List<string>();

        public int totalAppleTV = 0;
        public int totalIpad = 0;
        public int totalAdapter = 0;
        public int totalMAC = 0;

        public double AppleTVActualPrice = gadgets.Where(x => x.SKU == "atv").FirstOrDefault().Price;
        public double IpadActualPrice = gadgets.Where(x => x.SKU == "ipd").FirstOrDefault().Price;
        public double AdapterActualPrice = gadgets.Where(x => x.SKU == "vga").FirstOrDefault().Price;
        public double MacBookPrice = gadgets.Where(x => x.SKU == "mbp").FirstOrDefault().Price;

        double totalApplePrice = 0;
        double totalMACPrice = 0;
        double totalAdapterPrice = 0;
        double totalIpadPrice;

        public void Scan(string item)
        {
            items.Add(item);
        }

        public double TotalPrice()
        {
            ClearItems();
            foreach(var item in items)
            {
                switch(item)
                {
                    case "atv":
                        totalAppleTV++;
                        break;
                    case "ipd":
                        totalIpad++;
                        break;
                    case "vga":
                        totalAdapter++;
                        break;
                    case "mbp":
                        totalMAC++;
                        break;
                }
            }

            int appleQuotient = totalAppleTV / pricingRules.NoOfAppleTVBuy;
            int appleReminder = totalAppleTV % pricingRules.NoOfAppleTVBuy;

            totalApplePrice = (pricingRules.NoOfAppleTVPay * AppleTVActualPrice * appleQuotient) + (appleReminder * AppleTVActualPrice);

            if(totalIpad > pricingRules.MaxIpadCount)
            {
                totalIpadPrice = totalIpad * pricingRules.IpadDropPrice;
            }
            else
            {
                totalIpadPrice = totalIpad * IpadActualPrice;
            }

            if(pricingRules.isVGAFree && totalMAC > 0)
            {
                totalMACPrice = totalMAC * MacBookPrice;

                if(totalMAC == totalAdapter)
                {
                    totalAdapterPrice = 0;
                }
                else if(totalAdapter > totalMAC)
                {
                    totalAdapterPrice = (totalAdapter - totalMAC) * AdapterActualPrice;
                }
                else
                { /// Error
                    totalAdapterPrice = 0;
                }
            }
            else if(!pricingRules.isVGAFree && totalMAC > 0)
            {
                totalMACPrice = totalMAC * MacBookPrice;
                totalAdapterPrice = totalAdapter * AdapterActualPrice;
            }
            else
            {
                totalAdapterPrice = totalAdapter * AdapterActualPrice;
            }

            return (totalApplePrice + totalIpadPrice + totalMACPrice + totalAdapterPrice);
        }

        public void ClearItems()
        {
            totalApplePrice = 0;
            totalMACPrice = 0;
            totalAdapterPrice = 0;
            totalIpadPrice = 0;

            totalAppleTV = 0;
            totalIpad = 0;
            totalAdapter = 0;
            totalMAC = 0;
    }

        public void Delete()
        {
            items.Clear();
            
        }
    }
}
