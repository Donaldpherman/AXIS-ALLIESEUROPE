using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AxisAndAlliesEurope
{
    class WorldPower
    {
        private string name;
        private int IPC;
        private List<Unit> listOfPurchasedUnits;
        private int NPCValue;
        private Texture2D mousePointerSprite;

        public WorldPower(string name, int IPC, int NPCValue, string locationOfMousePointerSprite, ContentManager theContent)
        {
            this.name = name;
            this.IPC = IPC;
            this.NPCValue = NPCValue;
            mousePointerSprite = theContent.Load<Texture2D>(locationOfMousePointerSprite); // loading the mouse sprite.
            listOfPurchasedUnits = new List<Unit>();
        }

        public string getName()
        {
            return name;
        }

        public Texture2D getMousePointerSprite()
        {
            return mousePointerSprite;
        }

        public List<Unit> getListOfPurchasedUnits()
        {
            return listOfPurchasedUnits;
        }

        public int getIPC()
        {
            return IPC;
        }

        public void addUnitToPurchaseList(Unit unit)
        {
            listOfPurchasedUnits.Add(unit);
        }

        public void reduceIPC(int IPC)
        {
            this.IPC = this.IPC - IPC;
        }

        public void removeUnitFromPurchaseList(Unit unit)
        {
            listOfPurchasedUnits.Remove(unit);
        }
    }
}
