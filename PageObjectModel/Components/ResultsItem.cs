using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel.Components
{
     class ResultsItem
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public ResultsItem(string title, double price) 
        {
            Title = title;
            Price = price;
        }

        public override string ToString()
        {
            return $"[Title: {Title}, \nPrice: {Price}]\n";

        }


    }
}
