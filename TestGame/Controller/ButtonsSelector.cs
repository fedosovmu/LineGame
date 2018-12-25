using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    static class ButtonsSelector
    {
        public static event Action<String> Selected;
        public static event Action Deselected;

        public static String ButtonName { get; private set; }



        static ButtonsSelector ()
        {
            Deselect();
        }



        public static void Select(String buttonName)
        {
            ButtonName = buttonName;
            if (Selected != null)
                Selected(buttonName);
        }



        public static void Deselect()
        {
            ButtonName = null;
            if (Deselected != null)
                Deselected();
        }



        public static bool IsSelected()
        {
            return (ButtonName != null);
        }
    }
}
