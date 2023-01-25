using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel.Components
{
     class Item
    { 
        public string Title { get; set; }
        public string Price { get; set; }
        public string Link { get; set; }

        public Item(string title, string price, string link) 
        {
            Title = title;
            Price = price;
            Link = link;
        }

        public override string ToString()
        {
            return $"{{\nTitle: {Title}\nPrice: {Price}\nLink: {Link}\n}}";

        }


    }
}
