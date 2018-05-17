using System;
using System.IO;
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
using System.Collections;

namespace AxisAndAlliesEurope
{
    abstract public class Territory
    {
        private GraphicsDeviceManager graphics;
        private Vector2 position; // position of Button to Open Unit selected screen
        private float rotation; // rotation of MapScreen object.
        protected ArrayList arrayListOfUnits = new ArrayList();
        private ArrayList arrayListOfSelectedUnits = new ArrayList();
        private String nameOfTerritory;
        // Dictionary of Adjacent Territories, contains index of main arrayListOfTerritories
        private Dictionary<String, int> dictionaryOfAdjacentTerritories = new Dictionary<String, int>();
        private Texture2D sprite;
        private String whoControlsTerritory;
        private int iPCValue;
        private String originalOwnerOfTerritory;

        public Territory(ContentManager theContent, int x, int y, String nameOfTerritory, String whoControlsTerritory, int iPCValue)
        {
            position = new Vector2(x, y);
            this.nameOfTerritory = nameOfTerritory;
            setWhoControlsTerritory(whoControlsTerritory, theContent);
            originalOwnerOfTerritory = whoControlsTerritory;
            this.iPCValue = iPCValue;
        }

        public Texture2D getSprite()
        {
             return sprite;
        }

        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 position)
        {
            this.position = position;
        }
        public void setPosition(float x, float y)
        {
            Vector2 tempPosition;
            tempPosition = new Vector2(x, y);
            setPosition(tempPosition);
        }
        public float getRotation()
        {
            return rotation;
        }
         
        public ArrayList getArrayListOfUnits()
        {
            return arrayListOfUnits;
        }
        public void addUnitToArrayListOfUnits(Unit unit)
        {
            (getArrayListOfUnits()).Add(unit);
        }
        public void addUnitToArrayListOfUnits(ArrayList arrayListOfUnits)
        {
            foreach (Unit unit in arrayListOfUnits)
            {
                addUnitToArrayListOfUnits(unit);
            }
        }
        public void removeUnitFromArrayListOfUnits(Unit unit)
        {
            arrayListOfUnits.Remove(unit);
        }
        public void setArrayListOfUnits(ArrayList arrayListOfUnits)
        {
            this.arrayListOfUnits.Clear();
            this.arrayListOfUnits.AddRange(arrayListOfUnits);
        }
        public ArrayList getArrayListOfSelectedUnits()
        {
            return arrayListOfSelectedUnits;
        }

        public void addUnitToArrayListOfSelectedUnits(Unit unit)
        {
            getArrayListOfSelectedUnits().Add(unit);
        }
        public void setArrayListOfSelectedUnits(ArrayList ArrayListOfSelectedUnits)
        {
            this.arrayListOfSelectedUnits.Clear();
            this.arrayListOfSelectedUnits.AddRange(ArrayListOfSelectedUnits);
        }

        public void removeUnitFromArrayListOfSelectedUnits(Unit unit)
        {
            arrayListOfSelectedUnits.Remove(unit);
        }
        public Rectangle getRectangleofTerritory()
        {
            return new Rectangle(
                  ((int)this.position.X),
                  (int)this.position.Y,
                  this.sprite.Width,
                  this.sprite.Height);
        }
        public String getNameOfTerritory()
        {
            return nameOfTerritory;
        }
        public Dictionary<String, int> getDictionaryOfAdjacentTerritories()
        {
            return dictionaryOfAdjacentTerritories;
        }
        public void addTerritoryToDictionaryOfAdjacentTerritories(Territory territory, int indexOfTerritory)
        {
            dictionaryOfAdjacentTerritories.Add(territory.getNameOfTerritory(), indexOfTerritory);
        }
        public int CompareTo(object v)
        {
            return String.Compare(((Territory)v).getNameOfTerritory(), nameOfTerritory, true);
        }
        public void clearArrayListOfSelectedUnits()
        {
            arrayListOfSelectedUnits.Clear();
        }

        public void reduceMovementLeft()
        {
            for (int i = 0; i < arrayListOfSelectedUnits.Count; ++i)
            {
                ((Unit)arrayListOfSelectedUnits[i]).decreasementMovementLeft();
            }
        }
        public virtual bool isSeaTerritory()
        {
            return true;
        }

        public void externalSetWhoControlsTerritory(String whoControlsTerritory, ContentManager theContent)
        {

            if (isSeaTerritory())
                return;

            if ((String.Compare(whoControlsTerritory, "Germany", true) == 0))
                setWhoControlsTerritory(whoControlsTerritory, theContent);

            if ((String.Compare(this.originalOwnerOfTerritory , "Germany", true) != 0))
            {
            if ((String.Compare(whoControlsTerritory, "Germany", true) != 0))
                setWhoControlsTerritory(this.originalOwnerOfTerritory, theContent);
            }
            else
                setWhoControlsTerritory(whoControlsTerritory, theContent);


        }

        private void setWhoControlsTerritory(String whoControlsTerritory, ContentManager theContent)
        {
            this.whoControlsTerritory = whoControlsTerritory;

            if ((String.Compare(this.whoControlsTerritory, "Germany", true) == 0))
                sprite = theContent.Load<Texture2D>("Button\\GermanyControlMarker");

            if ((String.Compare(this.whoControlsTerritory, "Great Britain", true) == 0))
                sprite = theContent.Load<Texture2D>("Button\\GreatBritainControlMarker");

            if ((String.Compare(this.whoControlsTerritory, "Soviet Union", true) == 0))
                sprite = theContent.Load<Texture2D>("Button\\SovietUnionControlMarker");

            if ((String.Compare(this.whoControlsTerritory, "United States", true) == 0))
                sprite = theContent.Load<Texture2D>("Button\\UnitedStatesControlMarker");

            if ((String.Compare(this.whoControlsTerritory, "SeaTerritory", true) == 0))
                sprite = theContent.Load<Texture2D>("Button\\RedButton");

            if ((String.Compare(this.whoControlsTerritory, "Neutral", true) == 0))
                sprite = theContent.Load<Texture2D>("Button\\RedButton");
        }
        public String getWhoControlsTerritory()
        {
            return whoControlsTerritory;
        }
        public void resetUnitMovement()
        {
            for (int i = 0; i < arrayListOfUnits.Count; ++i)
            {
                ((Unit)arrayListOfUnits[i]).resetMovementLeft();
            }
        }
        public bool getContainsIndustrialComplex()
        {
            foreach (Unit unit in arrayListOfUnits)
            {
                if (unit.getType() == "IndustrialComplex")
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool containsTransport()
        {
            return true;
        }

        public virtual bool addUnitToEmptyTransport(Unit landUnit)
        {
            return false;
        }
    }
}