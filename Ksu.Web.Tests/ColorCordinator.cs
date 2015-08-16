using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arabia.Service.Tests
{
    public class ColorCordinator
    {
        public string shirtColor { get; set; }

        public void SuggestMyShirtColor(String pantsColor)
        {
            if ((pantsColor == "") || (pantsColor == "gray"))
            {
                this.shirtColor = "error";
                return;
            }

            switch (pantsColor)
            {
                case "red":
                    this.shirtColor = "pink";
                    break;

                case "blue":
                    this.shirtColor = "green";
                    break;

                case "green":
                    this.shirtColor = "blue";
                    break;

                case "pink":
                    this.shirtColor = "yellow";
                    break;

                case "black":
                    this.shirtColor = "white";
                    break;

                default:
                    this.shirtColor = "black";
                    break;
            }

        }
    }
}
