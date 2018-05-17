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
    public class MapController
    {
        private Territory[] arrayOfTerritories = new Territory[94];
        private KeyboardState previousKeyboardState;
        private MouseState currentMouseState;
        private Texture2D mousePointerSprite;
        private Texture2D gameBoard;
        private Vector2 positionOfGameBoard;

        private int indexOfSelectedTerritory; // index of territory that the user has clicked on.

        private int highLightedUnits;

        private int indexOfSelectedAdjacentTerritory;
        private int indexOfHighlightedTerritory;

        public enum Movement
        {
            Combat,
            NonCombat,
            PlaceNewUnits
        }

        public Movement movement;

        public MapController(ContentManager theContent)
        {
            gameBoard = theContent.Load<Texture2D>("Maps\\GameBoard"); // loading the gameBoard sprite.

            positionOfGameBoard = new Vector2(0, 0);

            // mousePointerSprite = theContent.Load<Texture2D>("mousePointers\\buy_usa_48_star_flag"); // loading the mouse sprite.

            arrayOfTerritories[0] = new LandTerritory(theContent, 200, 250, "Canada", "Great Britain", 3); // Canada
            arrayOfTerritories[1] = new LandTerritory(theContent, 100, 550, "United States", "United States", 30); // United States
            arrayOfTerritories[2] = new SeaTerritory(theContent, 430, 170, "Davis Strait"); // Davis Strait.

            arrayOfTerritories[3] = new SeaTerritory(theContent, 220, 600, "U.S. Eastern Coast"); //U.S. East Coast

            arrayOfTerritories[4] = new SeaTerritory(theContent, 166, 944, "Central Atlantic"); // Central Atlantic

            arrayOfTerritories[5] = new SeaTerritory(theContent, 360, 1280, "South Atlantic"); // South Atlantic

            arrayOfTerritories[6] = new SeaTerritory(theContent, 560, 180, "Denmark Strait"); // Denmark Strait

            arrayOfTerritories[7] = new LandTerritory(theContent, 686, 127, "Iceland", "Great Britain", 0); // Iceland

            arrayOfTerritories[8] = new SeaTerritory(theContent, 849, 71, "North Atlantic"); // North Atlantic

            arrayOfTerritories[9] = new SeaTerritory(theContent, 1064, 63, "Berents Sea"); //Berents Sea

            arrayOfTerritories[10] = new SeaTerritory(theContent, 1596, 32, "White Sea"); // White Sea

            arrayOfTerritories[11] = new LandTerritory(theContent, 1763, 76, "Archangel", "Soviet Union", 1); //Archangel

            arrayOfTerritories[12] = new LandTerritory(theContent, 1910, 150, "Siberia", "Soviet Union", 1); //Siberia

            arrayOfTerritories[13] = new ConvoyTerritory(theContent, 499, 371, "British Convoy Near Canada", "Great Britain", 5); // British Convoy near Davis Strait

            arrayOfTerritories[14] = new ConvoyTerritory(theContent, 987, 171, "Soviet Convoy", "Soviet Union", 4); // Soviet Convoy

            arrayOfTerritories[15] = new ConvoyTerritory(theContent,306 ,576 , "North U.S. Convoy", "United States",6 );

            arrayOfTerritories[16] = new ConvoyTerritory(theContent, 209, 791, "South U.S. Convoy", "United States", 4);

            arrayOfTerritories[17] = new ConvoyTerritory(theContent,479 ,783 , "Mid-Atlantic British Convoy", "Great Britain",5 );

            arrayOfTerritories[18] = new ConvoyTerritory(theContent,324 ,990 ,  "Central-Atlantic British Convoy", "Great Britain",3 );

            arrayOfTerritories[19] = new ConvoyTerritory(theContent,321 , 1193, "South-Atlantic British Convoy", "Great Britain",3 );

            arrayOfTerritories[20] = new SeaTerritory(theContent, 662, 391, "Atlantic");
            
            arrayOfTerritories[21] = new SeaTerritory(theContent, 405,439 , "Halifax Sea");
            arrayOfTerritories[22] = new SeaTerritory(theContent,371 ,650 , "Mid Atlantic");
            arrayOfTerritories[23] = new SeaTerritory(theContent, 492, 897, "Azores Sea");
            arrayOfTerritories[24] = new SeaTerritory(theContent, 717,846 , "Bay of Biscay");
            arrayOfTerritories[25] = new LandTerritory(theContent, 657, 996, "Spain", "Neutral", 0);
            arrayOfTerritories[26] = new SeaTerritory(theContent,756 ,1166 , "Strait of Gibraitar" );
            arrayOfTerritories[27] = new LandTerritory(theContent, 632, 1248, "Spanish Morocco", "Neutral", 1);
            arrayOfTerritories[28] = new LandTerritory(theContent, 507,1296, "Morocco", "Germany",1);
            arrayOfTerritories[29] = new LandTerritory(theContent, 794, 1244, "Algeria", "Germany", 1);
            arrayOfTerritories[30] = new SeaTerritory(theContent, 845,1127 , "Western Mediterranean");
            arrayOfTerritories[31] = new LandTerritory(theContent, 1086, 988, "Corsica", "Germany", 0);
            arrayOfTerritories[32] = new LandTerritory(theContent, 986, 1019, "Sardria", "Germany", 0);
            arrayOfTerritories[33] = new SeaTerritory(theContent, 1025,1101 , "Tyrrheanian Sea");
            arrayOfTerritories[34] = new LandTerritory(theContent, 1078,1220, "Tunisia", "Germany",1);
            arrayOfTerritories[35] = new LandTerritory(theContent, 829,896, "France", "Germany",3);
            arrayOfTerritories[36] = new LandTerritory(theContent,926 ,824, "Eastern France", "Germany",2);
            arrayOfTerritories[37] = new SeaTerritory(theContent, 836,706, "English Channel");
            arrayOfTerritories[38] = new LandTerritory(theContent, 858,561, "United Kingdom", "Great Britain",6);
            arrayOfTerritories[39] = new SeaTerritory(theContent, 612,657 , "Celtic Sea" );
            arrayOfTerritories[40] = new LandTerritory(theContent, 689, 595, "Eire", "Neutral", 0);
            arrayOfTerritories[41] = new SeaTerritory(theContent, 914,407 , "North Sea" );
            arrayOfTerritories[42] = new LandTerritory(theContent, 999,639, "Netherland Belgium", "Germany",2);
            arrayOfTerritories[43] = new LandTerritory(theContent, 903, 877, "Vichy France", "Germany", 1);
            arrayOfTerritories[44] = new SeaTerritory(theContent, 919,382 , "Norwegian Sea" );
            arrayOfTerritories[45] = new LandTerritory(theContent,1064 ,335, "Norway", "Germany",2);
            arrayOfTerritories[46] = new SeaTerritory(theContent, 1035,490 , "Danish Sea" );
            arrayOfTerritories[47] = new LandTerritory(theContent, 1096,505, "Denmark", "Germany",1);
            arrayOfTerritories[48] = new LandTerritory(theContent, 1070,675, "Germany", "Germany",6);
            arrayOfTerritories[49] = new LandTerritory(theContent, 1076, 825, "Swetzerland", "Neutral", 0);
            arrayOfTerritories[50] = new LandTerritory(theContent, 1141,927, "Northern Italy", "Germany",4);
            arrayOfTerritories[51] = new LandTerritory(theContent, 1237,793, "Austria", "Germany",2);
            arrayOfTerritories[52] = new LandTerritory(theContent, 1292,761, "Czechosovaria", "Germany",2);
            arrayOfTerritories[53] = new LandTerritory(theContent, 1359,610, "Poland", "Germany",2);
            arrayOfTerritories[54] = new SeaTerritory(theContent, 1224,560 , "Baltic Sea" );
            arrayOfTerritories[55] = new LandTerritory(theContent, 1184, 426, "Sweden", "Neutral", 0);
            arrayOfTerritories[56] = new LandTerritory(theContent, 1349,281, "Finland", "Germany",1);
            arrayOfTerritories[57] = new LandTerritory(theContent, 1469,267, "Vyborg", "Soviet Union",0);
            arrayOfTerritories[58] = new LandTerritory(theContent, 1413,443, "Baltic States", "Soviet Union",1);
            arrayOfTerritories[59] = new LandTerritory(theContent, 1492,580, "East Poland", "Soviet Union",1);
            arrayOfTerritories[60] = new LandTerritory(theContent, 1336,847, "Hungary", "Germany",2);
            arrayOfTerritories[61] = new LandTerritory(theContent, 1287,886, "Yugoslavia", "Germany",1);
            arrayOfTerritories[62] = new SeaTerritory(theContent, 1203, 926, "Adriatic Sea" );
            arrayOfTerritories[63] = new LandTerritory(theContent, 1216, 1000, "Southern Italy", "Germany",2);
            arrayOfTerritories[64] = new LandTerritory(theContent, 1247, 1147, "Sicily", "Germany", 0);
            arrayOfTerritories[65] = new SeaTerritory(theContent, 1114, 1141, "Sicilian Sea" );
            arrayOfTerritories[66] = new LandTerritory(theContent, 1187,1224, "Malta", "Great Britain",0);
            arrayOfTerritories[67] = new SeaTerritory(theContent, 1329, 1222, "Central Mediterranean" );
            arrayOfTerritories[68] = new LandTerritory(theContent, 1395,1013, "Greece", "Germany",1);
            arrayOfTerritories[69] = new LandTerritory(theContent, 1463,957, "Bulgaria", "Germany",1);
            arrayOfTerritories[70] = new LandTerritory(theContent, 1457,883, "Rumania", "Germany",1);
            arrayOfTerritories[71] = new LandTerritory(theContent, 1548,727, "Bessarabia", "Soviet Union",0);
            arrayOfTerritories[72] = new LandTerritory(theContent, 1538,385, "Lenningrad", "Soviet Union",1);
            arrayOfTerritories[73] = new LandTerritory(theContent, 1459,145, "Karelia", "Soviet Union",1);
            arrayOfTerritories[74] = new LandTerritory(theContent, 1640,295, "Russia", "Soviet Union",1);
            arrayOfTerritories[75] = new LandTerritory(theContent, 1734,377, "Moscow", "Soviet Union",2);
            arrayOfTerritories[76] = new LandTerritory(theContent, 1580,470, "Belorussia", "Soviet Union",1);
            arrayOfTerritories[77] = new LandTerritory(theContent, 1838,568, "Stalingrad", "Soviet Union",1);
            arrayOfTerritories[78] = new LandTerritory(theContent, 1923,428, "Turkestan", "Soviet Union",2);
            arrayOfTerritories[79] = new LandTerritory(theContent, 1628,652, "Ukraine S.S.R", "Soviet Union",2);
            arrayOfTerritories[80] = new LandTerritory(theContent, 1899,752, "Caucasus", "Soviet Union",4);
            arrayOfTerritories[81] = new SeaTerritory(theContent, 1701,868 , "Black Sea" );
            arrayOfTerritories[82] = new LandTerritory(theContent, 1686,1038, "Turkey", "Neutral",0);
            arrayOfTerritories[83] = new SeaTerritory(theContent, 1574,1152 , "Aegean Sea" );
            arrayOfTerritories[84] = new LandTerritory(theContent, 1536,1211, "Crete", "Germany",0);
            arrayOfTerritories[85] = new SeaTerritory(theContent, 1686,1240 , "Eastern Mediterranean" );
            arrayOfTerritories[86] = new LandTerritory(theContent, 1496,1374, "Libya", "Germany",1);
            arrayOfTerritories[87] = new LandTerritory(theContent, 1635,1362, "Egypt", "Great Britain",1);
            arrayOfTerritories[88] = new LandTerritory(theContent, 1908,1294, "Palestine", "Great Britain",0);
            arrayOfTerritories[89] = new LandTerritory(theContent, 1952,1191, "Trans-Jordan", "Great Britain",2);
            arrayOfTerritories[90] = new LandTerritory(theContent, 1959,1350, "Saudi Arabia", "Neutral",0);
            arrayOfTerritories[91] = new LandTerritory(theContent, 1935,1115, "Syria", "Great Britain",1);
            arrayOfTerritories[92] = new LandTerritory(theContent, 2007,1144, "Irag", "Great Britain",2);
            arrayOfTerritories[93] = new LandTerritory(theContent, 2007,934, "Iran", "Soviet Union",2);

            // Canada's adjacent list
            arrayOfTerritories[0].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[1], 1); // add United States to Canada adjacent list
            arrayOfTerritories[0].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[2], 2); // add Davis Strait to Canada adjacent list

            //United States adjacent list
            arrayOfTerritories[1].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[0], 0); // add Canada to United States adjacent list
            arrayOfTerritories[1].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[3], 3); // add U.S. East Coast to United States adjacent list

            //David Strait adjacent list
            arrayOfTerritories[2].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[0], 0); // add Canada to Davis Strait.
            arrayOfTerritories[2].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[13], 13); //add British Convoy near Davis Strait to Davis Strait adjacent list
            arrayOfTerritories[2].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[6], 6);
            arrayOfTerritories[2].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[3],3 );

            //U.S. Eastern Coast adjacent list 1 4 16 15 21 
            arrayOfTerritories[3].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[1], 1); // add United States to U.S. Eastern Coast adjacent list
            arrayOfTerritories[3].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[4], 4); // add Central Atlantic to U.S. Eastern Coast adjacent list
            arrayOfTerritories[3].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[16],16 );
            arrayOfTerritories[3].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[15], 15);
            arrayOfTerritories[3].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[21], 21);

            //Central Atlantic adjacent list 3 16 18 19 23 5 
            arrayOfTerritories[4].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[3], 3); // add U.S. Eastern Coast to Central Atlantic adjacent list
            arrayOfTerritories[4].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[5], 5); // add South Atlantic to Central Atlantic adjacent list
            arrayOfTerritories[4].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[16], 16);
            arrayOfTerritories[4].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[18], 18);
            arrayOfTerritories[4].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[19], 19);
            arrayOfTerritories[4].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23],23 );

            //South Atlantic adjacent list 19 28 23 4
            arrayOfTerritories[5].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[4], 4); // Central Atlantic to South Atlantic adjacent list
            arrayOfTerritories[5].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23],23 );
            arrayOfTerritories[5].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[19],19 );
            arrayOfTerritories[5].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[28],28 );

            //Denmark Strait adjacent list 2 13 21 20 7 8 
            arrayOfTerritories[6].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[2], 2); // David Strait to Denmark Strait adjacent list 
            arrayOfTerritories[6].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[7], 7); // Iceland to Denmark Strait adjacent list
            arrayOfTerritories[6].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[8], 8); // North Atlantic to Denmark Strait adjacent list
            arrayOfTerritories[6].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[13], 13); //add British Convoy near Davis Strait to Denmark Strait adjacent list
            arrayOfTerritories[6].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[21], 21);
            arrayOfTerritories[6].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[20], 20);

            //Iceland adjacent list 6
            arrayOfTerritories[7].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[6], 6); // Denmark to Iceland adjacent list

            // North Atlantic adjacent list 6 14 20 41 44 9 
            arrayOfTerritories[8].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[6], 6); // Denmark Strait to North Atlantic adjacent list
            arrayOfTerritories[8].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[9], 9); // Berents Sea to North Atlantic adjacent list
            arrayOfTerritories[8].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[14],14 );
            arrayOfTerritories[8].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[20], 20);
            arrayOfTerritories[8].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);
            arrayOfTerritories[8].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[44], 44);

            // Berents Sea adjacent list 8 14 44 45 56 10 
            arrayOfTerritories[9].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[8], 8); // North Atlantic to Berents Sea adjacent list
            arrayOfTerritories[9].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[10], 10); //White Sea to Berents Sea adjacent list
            arrayOfTerritories[9].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[14],14 );
            arrayOfTerritories[9].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[44], 44);
            arrayOfTerritories[9].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[45], 45);
            arrayOfTerritories[9].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[56], 56);

            // White Sea adjacent list 9 73 11
            arrayOfTerritories[10].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[9], 9); // Berents Sea to White Sea adjacent list
            arrayOfTerritories[10].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[73],73 );
            arrayOfTerritories[10].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[11], 11);

            // Archangel adjacent list 10 73 74 12
            arrayOfTerritories[11].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[10], 10); //White Sea to Archangel adjacent list
            arrayOfTerritories[11].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[12], 12); //Siberia to Archangel adjacent list
            arrayOfTerritories[11].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[73], 73);
            arrayOfTerritories[11].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[74], 74);

            // Siberia adjacent list 11 74 78
            arrayOfTerritories[12].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[11], 11); //Archangel to Siberia adjacent list
            arrayOfTerritories[12].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[74], 74);
            arrayOfTerritories[12].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[78], 78);

            // British Convoy near Davis Strait adjacent list 2 6 21 
            arrayOfTerritories[13].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[2], 2); // Davis Strait to British Convoy near Davis Strait adjacent list
            arrayOfTerritories[13].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[6], 6); // Denmark Strait to British Convoy near Davis Strait adjacent list
            arrayOfTerritories[13].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[21], 21);

            // Soviet Convoy adjacent list 8 44 9 
            arrayOfTerritories[14].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[8], 8); // North Atlantic to Soviet Convoy adjacent list
            arrayOfTerritories[14].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[9], 9); // Berents Sea to Soviet Convoy adjacent list
            arrayOfTerritories[14].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[44], 44);

            //15 US  convoy adjacent list 3 22 21 
            arrayOfTerritories[15].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[3], 3);
            arrayOfTerritories[15].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[22],22 );
            arrayOfTerritories[15].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[21], 21);
            
            //16 US convoy adjacent list 3 4 22 
            arrayOfTerritories[16].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[3], 3);
            arrayOfTerritories[16].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[4], 4);
            arrayOfTerritories[16].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[22], 22);
            
            //17 British Convoy adjacent list 22 39 23 
            arrayOfTerritories[17].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[22], 22);
            arrayOfTerritories[17].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);
            arrayOfTerritories[17].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23], 23);

            //18 British Convoy adjacent list 4 23 
            arrayOfTerritories[18].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[4], 4);
            arrayOfTerritories[18].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23],23 );
            
            //19 British Convoy adjacent list 4 23 5 
            arrayOfTerritories[19].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[4], 4);
            arrayOfTerritories[19].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23],23 );
            arrayOfTerritories[19].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[5], 5);
            
            //20 Atlantic adjacent list 6 21 40 39 41 8
            arrayOfTerritories[20].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[6], 6);
            arrayOfTerritories[20].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[21],21 );
            arrayOfTerritories[20].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[40], 40);
            arrayOfTerritories[20].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);
            arrayOfTerritories[20].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);
            arrayOfTerritories[20].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[8], 8);

            //21 Halifax Sea adjacent list 13 20 6 22 15 39
            arrayOfTerritories[21].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[13], 13);
            arrayOfTerritories[21].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[20], 20);
            arrayOfTerritories[21].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[6], 6);
            arrayOfTerritories[21].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[22],22 );
            arrayOfTerritories[21].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[15], 15);
            arrayOfTerritories[21].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);

            //22 Mid-Atlantic adjacent list 15 3 16 4 23 17 39 21
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[15], 15);
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[3], 3);
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[16],16 );
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[4], 4);
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23],23 );
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[17], 17);
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);
            arrayOfTerritories[22].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[21], 21);

            //23 Azores Sea adjacent list 17 39 24 25 5 19 4 18 22
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[17], 17);
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[24], 24);
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[25], 25);
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[5], 5);
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[19], 19);
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[4], 4);
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[18],18 );
            arrayOfTerritories[23].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[22], 22);

            //24 Bay Of Biscay adjacent list 39 37 35 25 23 
            arrayOfTerritories[24].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);
            arrayOfTerritories[24].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[37], 37);
            arrayOfTerritories[24].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[35], 35);
            arrayOfTerritories[24].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[25], 25);
            arrayOfTerritories[24].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23],23 );

            //25 Spain adjacent list 24 35 43 30 26 23
            arrayOfTerritories[25].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[24],24 );
            arrayOfTerritories[25].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[35],35 );
            arrayOfTerritories[25].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[43], 43);
            arrayOfTerritories[25].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[30], 30);
            arrayOfTerritories[25].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[26], 26);
            arrayOfTerritories[25].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23], 23);

            //26 Strait of Gibraitar adjacent list 25 30 29 28 27 5 23 
            arrayOfTerritories[26].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[25], 25);
            arrayOfTerritories[26].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[30], 30);
            arrayOfTerritories[26].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[29], 29);
            arrayOfTerritories[26].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[28], 28);
            arrayOfTerritories[26].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[27], 27);
            arrayOfTerritories[26].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[5], 5);
            arrayOfTerritories[26].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23], 23);

            //27 Spanish Morocco adjacent list 26 28 
            arrayOfTerritories[27].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[26], 26);
            arrayOfTerritories[27].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[28], 28);

            //28 Morocco adjacent list 5 27 29 
            arrayOfTerritories[28].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[5], 5);
            arrayOfTerritories[28].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[27], 27);
            arrayOfTerritories[28].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[29], 29);

            //29 Algeria adjacent list 30 34 28 26 
            arrayOfTerritories[29].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[30], 30);
            arrayOfTerritories[29].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[34], 34);
            arrayOfTerritories[29].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[28], 28);
            arrayOfTerritories[29].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[26], 26);

            //30 Western Mediterranean adjacent list 43 33 65 29 26 25
            arrayOfTerritories[30].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[43], 43);
            arrayOfTerritories[30].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[33], 33);
            arrayOfTerritories[30].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[65], 65);
            arrayOfTerritories[30].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[29], 29);
            arrayOfTerritories[30].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[26], 26);
            arrayOfTerritories[30].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[25], 25);

            //31 Corsica adjacent list 33
            arrayOfTerritories[31].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[33], 33);

            //32 Sardria adjacent list 33
            arrayOfTerritories[32].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[33], 33);

            //33 Tyrrheanian adjacent list 43 50 63 65 30 
            arrayOfTerritories[33].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[43], 43);
            arrayOfTerritories[33].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[50], 50);
            arrayOfTerritories[33].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[63], 63);
            arrayOfTerritories[33].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[65], 65);
            arrayOfTerritories[33].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[30], 30);

            //34 Tunisia adjacent list 29 65 86
            arrayOfTerritories[34].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[29], 29);
            arrayOfTerritories[34].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[65], 65);
            arrayOfTerritories[34].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[86], 86);

            //35 France adjacent list 37 42 36 43 25 24 
            arrayOfTerritories[35].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[37], 37);
            arrayOfTerritories[35].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[42], 42);
            arrayOfTerritories[35].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[36], 36);
            arrayOfTerritories[35].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[43], 43);
            arrayOfTerritories[35].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[25], 25);
            arrayOfTerritories[35].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[24], 24);

            //36 Eastern France adjacent list 42 48 49 43 35 
            arrayOfTerritories[36].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[42], 42);
            arrayOfTerritories[36].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);
            arrayOfTerritories[36].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[49], 49);
            arrayOfTerritories[36].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[43], 43);
            arrayOfTerritories[36].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[35], 35);

            //37 English Channel adjacent list 38 42 35 24 39 41 
            arrayOfTerritories[37].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[38], 38);
            arrayOfTerritories[37].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[42], 42);
            arrayOfTerritories[37].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[35], 35);
            arrayOfTerritories[37].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[24], 24);
            arrayOfTerritories[37].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);
            arrayOfTerritories[37].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);

            //38 United Kingdom adjacent list 41 37 39 40 
            arrayOfTerritories[38].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);
            arrayOfTerritories[38].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[37], 37);
            arrayOfTerritories[38].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);
            arrayOfTerritories[38].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[40], 40);

            //39 Celtic Sea adjacent list 20 40 38 37 24 23 17 22 21 
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[20], 20);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[40], 40);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[38], 38);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[37], 37);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[24], 24);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[23], 23);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[17], 17);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[22], 22);
            arrayOfTerritories[39].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[21], 21);

            //40 Eire adjacent list 20 41 38 39 
            arrayOfTerritories[40].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[20], 20);
            arrayOfTerritories[40].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);
            arrayOfTerritories[40].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[38], 38);
            arrayOfTerritories[40].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[39], 39);

            //41 North Sea adjacent list 14 44 45 46 37 40 20 
            arrayOfTerritories[41].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[14], 14);
            arrayOfTerritories[41].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[44], 44);
            arrayOfTerritories[41].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[45], 45);
            arrayOfTerritories[41].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[46], 46);
            arrayOfTerritories[41].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[37], 37);
            arrayOfTerritories[41].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[40], 40);
            arrayOfTerritories[41].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[20], 20);

            //42 Netherland Belgium adjacent list 37 46 48 36 35
            arrayOfTerritories[42].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[37], 37);
            arrayOfTerritories[42].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[46], 46);
            arrayOfTerritories[42].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);
            arrayOfTerritories[42].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[36], 36);
            arrayOfTerritories[42].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[35], 35);

            //43 Vichy France adjacent list 36 49 50 33 30 25 35 
            arrayOfTerritories[43].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[36],36 );
            arrayOfTerritories[43].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[49], 49);
            arrayOfTerritories[43].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[50], 50);
            arrayOfTerritories[43].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[33], 33);
            arrayOfTerritories[43].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[30], 30);
            arrayOfTerritories[43].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[25], 25);
            arrayOfTerritories[43].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[35], 35);

            //44 Norwegian adjacent list 9 45 41 14 
            arrayOfTerritories[44].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[9], 9);
            arrayOfTerritories[44].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[45],45 );
            arrayOfTerritories[44].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);
            arrayOfTerritories[44].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[14], 14);

            //45 Norway adjacent list 9 56 55 46 41 44
            arrayOfTerritories[45].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[9], 9);
            arrayOfTerritories[45].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[56], 56);
            arrayOfTerritories[45].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[55], 55);
            arrayOfTerritories[45].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[46], 46);
            arrayOfTerritories[45].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);
            arrayOfTerritories[45].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[44], 44);

            //46 Danish Sea adjacent list 45 55 47 54 48 42 37 41 
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[45],45 );
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[55],55 );
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[47], 47);
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[42], 42);
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[37], 37);
            arrayOfTerritories[46].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[41], 41);

            //47 Denmark adjacent list 46 48 
            arrayOfTerritories[47].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[46], 46);
            arrayOfTerritories[47].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);

            //48 Germany adjacent list 46 47 54 53 52 51 49 36 42 
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[46], 46);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[47], 47);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[53], 53);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[52], 52);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[51], 51);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[49], 49);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[36], 36);
            arrayOfTerritories[48].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[42], 42);

            //49 Swetzerland adjacent list 48 51 50 43 36 
            arrayOfTerritories[49].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);
            arrayOfTerritories[49].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[51], 51);
            arrayOfTerritories[49].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[50], 50);
            arrayOfTerritories[49].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[43], 43);
            arrayOfTerritories[49].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[36], 36);

            //50 Northen Italy adjacent list 51 61 62 63 33 43 49
            arrayOfTerritories[50].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[51], 51);
            arrayOfTerritories[50].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[61], 61);
            arrayOfTerritories[50].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[62], 62);
            arrayOfTerritories[50].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[63], 63);
            arrayOfTerritories[50].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[33], 33);
            arrayOfTerritories[50].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[43], 43);
            arrayOfTerritories[50].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[49], 49);

            //51 Austria adjacent list 52 60 61 50 49 48
            arrayOfTerritories[51].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[52], 52);
            arrayOfTerritories[51].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[60], 60);
            arrayOfTerritories[51].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[61], 61);
            arrayOfTerritories[51].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[50], 50);
            arrayOfTerritories[51].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[49], 49);
            arrayOfTerritories[51].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);

            //52 Czechosovaria adjacent list 53 60 51 48
            arrayOfTerritories[52].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[53], 53);
            arrayOfTerritories[52].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[60], 60);
            arrayOfTerritories[52].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[51], 51);
            arrayOfTerritories[52].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);

            //53 Poland adjacent list 58 59 60 52 48 54
            arrayOfTerritories[53].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[58], 58);
            arrayOfTerritories[53].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[59], 59);
            arrayOfTerritories[53].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[60], 60);
            arrayOfTerritories[53].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[52], 52);
            arrayOfTerritories[53].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);
            arrayOfTerritories[53].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);

            //54 Baltic Sea adjacent list 56 57 72 58 53 48 46 55
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[56],56 );
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[57],57 );
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[72], 72);
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[58], 58);
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[53], 53);
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[48], 48);
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[46], 46);
            arrayOfTerritories[54].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[55], 55);

            //55 Sweden adjacent list 56 54 46 45
            arrayOfTerritories[55].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[56], 56);
            arrayOfTerritories[55].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);
            arrayOfTerritories[55].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[46], 46);
            arrayOfTerritories[55].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[45], 45);

            //56 Finland adjacent list 9 10 73 57 54 55
            arrayOfTerritories[56].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[9], 91);
            arrayOfTerritories[56].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[10], 10);
            arrayOfTerritories[56].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[73], 73);
            arrayOfTerritories[56].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[57], 57);
            arrayOfTerritories[56].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);
            arrayOfTerritories[56].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[55], 55);

            //57 Vyborg adjacent list 73 72 54 56
            arrayOfTerritories[57].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[73], 73);
            arrayOfTerritories[57].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[72], 72);
            arrayOfTerritories[57].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);
            arrayOfTerritories[57].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[56], 56);

            //58 Baltic States adjacent list 54 72 76 59 53 
            arrayOfTerritories[58].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);
            arrayOfTerritories[58].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[72], 72);
            arrayOfTerritories[58].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);
            arrayOfTerritories[58].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[59], 59);
            arrayOfTerritories[58].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[53], 53);

            //59 East Poland adjacent list 76 79 71 60 53 58
            arrayOfTerritories[59].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);
            arrayOfTerritories[59].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[79], 79);
            arrayOfTerritories[59].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[71], 71);
            arrayOfTerritories[59].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[60], 60);
            arrayOfTerritories[59].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[53], 53);
            arrayOfTerritories[59].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[58], 58);

            //60 Hungary adjacent list 59 71 70 61 51 52 53 
            arrayOfTerritories[60].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[59], 59);
            arrayOfTerritories[60].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[71], 71);
            arrayOfTerritories[60].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[70], 70);
            arrayOfTerritories[60].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[61], 61);
            arrayOfTerritories[60].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[51], 51);
            arrayOfTerritories[60].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[52], 52);
            arrayOfTerritories[60].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[53], 53);

            //61 Yugoslavia adjacent list 60 70 69 68 62 50 51 
            arrayOfTerritories[61].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[60], 60);
            arrayOfTerritories[61].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[70], 70);
            arrayOfTerritories[61].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[69], 69);
            arrayOfTerritories[61].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[68], 68);
            arrayOfTerritories[61].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[62], 62);
            arrayOfTerritories[61].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[50], 50);
            arrayOfTerritories[61].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[51], 51);

            //62 Adriatic Sea adjacent list 61 68 67 63 50 
            arrayOfTerritories[62].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[61], 61);
            arrayOfTerritories[62].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[68], 68);
            arrayOfTerritories[62].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[67], 67);
            arrayOfTerritories[62].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[63], 63);
            arrayOfTerritories[62].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[50], 50);

            //63 Southern Italy adjacent list 62 67 65 33 50 
            arrayOfTerritories[63].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[62], 62);
            arrayOfTerritories[63].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[67], 67);
            arrayOfTerritories[63].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[65], 65);
            arrayOfTerritories[63].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[33], 33);
            arrayOfTerritories[63].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[50], 50);

            //64 Sicily adjacent list 65
            arrayOfTerritories[64].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[65], 65);

            //65 Sicilian Sea adjacent list 63 67 64 66 86 34 30 33
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[63], 63);
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[67], 67);
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[64], 64);
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[66], 66);
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[86], 86);
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[34], 34);
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[30], 30);
            arrayOfTerritories[65].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[33], 33);

            //66 Malta adjacent list 65
            arrayOfTerritories[66].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[65], 65);

            //67 Central Mediterranean adjacent list 62 68 83 85 86 66 63 
            arrayOfTerritories[67].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[62], 62);
            arrayOfTerritories[67].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[68], 68);
            arrayOfTerritories[67].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[83], 83);
            arrayOfTerritories[67].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 85);
            arrayOfTerritories[67].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[86], 86);
            arrayOfTerritories[67].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[66], 66);
            arrayOfTerritories[67].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[63], 63);

            //68 Greece adjacent list 69 83 67 62 61
            arrayOfTerritories[68].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[69], 69);
            arrayOfTerritories[68].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[83], 83);
            arrayOfTerritories[68].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[67], 67);
            arrayOfTerritories[68].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[62], 62);
            arrayOfTerritories[68].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[61], 61);

            //69 Bulgaria adjacent list 70 81 68 61 
            arrayOfTerritories[69].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[70], 70);
            arrayOfTerritories[69].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[81], 81);
            arrayOfTerritories[69].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[68], 68);
            arrayOfTerritories[69].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[61], 61);

            //70 Rumania  adjacent list 71 79 81 69 61 60
            arrayOfTerritories[70].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[71], 71);
            arrayOfTerritories[70].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[79], 79);
            arrayOfTerritories[70].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[81], 81);
            arrayOfTerritories[70].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[69], 69);
            arrayOfTerritories[70].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[61], 61);
            arrayOfTerritories[70].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[60], 60);

            //71 Bessarabia adjacent list 59 79 70 60 
            arrayOfTerritories[71].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[59], 59);
            arrayOfTerritories[71].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[79], 79);
            arrayOfTerritories[71].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[70], 70);
            arrayOfTerritories[71].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[60], 60);

            //72 leningrad adjacent list 73 74 76 58 57 54 
            arrayOfTerritories[72].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[73], 73);
            arrayOfTerritories[72].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[74], 74);
            arrayOfTerritories[72].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);
            arrayOfTerritories[72].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[58], 58);
            arrayOfTerritories[72].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[57], 57);
            arrayOfTerritories[72].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[54], 54);

            //73 Karelia adjacent list10 11 74 72 57 56
            arrayOfTerritories[73].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[11], 11);
            arrayOfTerritories[73].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[74], 74);
            arrayOfTerritories[73].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[72], 72);
            arrayOfTerritories[73].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[57], 57);            
            arrayOfTerritories[73].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[56], 56);

            //74 Russia adjacent list 11 12 75 78 76 72 73
            arrayOfTerritories[74].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[11], 11);
            arrayOfTerritories[74].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[12], 12);
            arrayOfTerritories[74].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[75], 75);
            arrayOfTerritories[74].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[78], 78);
            arrayOfTerritories[74].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);
            arrayOfTerritories[74].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[72], 72);
            arrayOfTerritories[74].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[73], 73);

            //75 Moscow adjacent list 74 78 76 
            arrayOfTerritories[75].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[74], 74);
            arrayOfTerritories[75].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[78], 78);
            arrayOfTerritories[75].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);

            //76 Belorussia adjacent list 74 75 78 77 79 59 58 72 
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[74], 74);
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[75], 75);
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[78], 78);
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[77], 77);
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[79], 79);
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[59], 59);
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[58], 58);
            arrayOfTerritories[76].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[72], 72);

            //77 Stalingrad adjacent list 78 80 79 76
            arrayOfTerritories[77].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[78], 78);
            arrayOfTerritories[77].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[80], 80);
            arrayOfTerritories[77].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[79], 79);
            arrayOfTerritories[77].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);

            //78 Turkestan adjacent list12 75 74 76 77 80
            arrayOfTerritories[78].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[12], 12);
            arrayOfTerritories[78].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[75], 75);
            arrayOfTerritories[78].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[74], 74);
            arrayOfTerritories[78].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);
            arrayOfTerritories[78].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[77], 77);
            arrayOfTerritories[78].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[80], 80);

            //79 Ukraine S.S.R adjacent list 76 77 80 81 70 71 59  
            arrayOfTerritories[79].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[76], 76);
            arrayOfTerritories[79].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[77], 77);
            arrayOfTerritories[79].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[80], 80);
            arrayOfTerritories[79].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[81], 81);
            arrayOfTerritories[79].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[70], 70);
            arrayOfTerritories[79].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[71], 71);
            arrayOfTerritories[79].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[59], 59);

            //80 Caucasus adjacent list 78 82 81 79 77
            arrayOfTerritories[80].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[78], 78);
            arrayOfTerritories[80].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[82], 82);
            arrayOfTerritories[80].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[81], 81);
            arrayOfTerritories[80].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[79], 79);
            arrayOfTerritories[80].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[77], 77);

            //81 BlackSea adjacent list 80 82 69 70 79            
            arrayOfTerritories[81].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[80], 80);
            arrayOfTerritories[81].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[82], 82);
            arrayOfTerritories[81].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[69], 69);
            arrayOfTerritories[81].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[70], 70);
            arrayOfTerritories[81].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[79], 79);

            //82 Turkey adjacent list 81 93 91 85 83 
            arrayOfTerritories[82].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[81], 81);
            arrayOfTerritories[82].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[93], 93);
            arrayOfTerritories[82].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[91], 91);
            arrayOfTerritories[82].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 85);
            arrayOfTerritories[82].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[83], 83);

            //83 Aegean Sea adjacent list 82 84 85 67 68
            arrayOfTerritories[83].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[82], 82);
            arrayOfTerritories[83].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[84], 84);
            arrayOfTerritories[83].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 85);
            arrayOfTerritories[83].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[67], 67);
            arrayOfTerritories[83].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[68], 68);

            //84 Crete adjacent list 83 85
            arrayOfTerritories[84].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[83], 83);
            arrayOfTerritories[84].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 65);

            //85 Eastern Mediterranean adjacent list 84 91 88 87 86 67 83
            arrayOfTerritories[85].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[84], 84);
            arrayOfTerritories[85].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[91], 91);
            arrayOfTerritories[85].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[88], 88);
            arrayOfTerritories[85].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[87], 87);
            arrayOfTerritories[85].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[86], 86);
            arrayOfTerritories[85].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[67], 67);
            arrayOfTerritories[85].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[83], 83);

            //86 Libya adjacent list 85 87 67 66 34 
            arrayOfTerritories[86].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 85);
            arrayOfTerritories[86].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[87], 87);
            arrayOfTerritories[86].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[67], 67);
            arrayOfTerritories[86].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[66], 66);
            arrayOfTerritories[86].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[34], 34);

            //87 Egypt adjacent list 85 88 89 90 86
            arrayOfTerritories[87].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 85);
            arrayOfTerritories[87].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[88], 88);
            arrayOfTerritories[87].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[89], 89);
            arrayOfTerritories[87].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[90], 90);
            arrayOfTerritories[87].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[86], 86);

            //88 Palestine adjacent list 91 92 89 90 87 85 
            arrayOfTerritories[88].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[91], 91);
            arrayOfTerritories[88].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[92], 92);
            arrayOfTerritories[88].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[89], 89);
            arrayOfTerritories[88].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[90], 90);
            arrayOfTerritories[88].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[87], 87);
            arrayOfTerritories[88].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 85);

            //89 Trans-Jordan adjacent list 91 92 90 87 88
            arrayOfTerritories[89].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[91], 91);
            arrayOfTerritories[89].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[92], 92);
            arrayOfTerritories[89].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[90], 90);
            arrayOfTerritories[89].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[87], 87);
            arrayOfTerritories[89].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[88], 88);

            //90 Saudi Arabia adjacent list 89 87
            arrayOfTerritories[90].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[89], 89);
            arrayOfTerritories[90].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[87], 87);

            //91 Syria adjacent list 93 92 88 89 85 82
            arrayOfTerritories[91].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[93], 93);
            arrayOfTerritories[91].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[92], 92);
            arrayOfTerritories[91].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[88], 88);
            arrayOfTerritories[91].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[89], 89);
            arrayOfTerritories[91].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[85], 85);
            arrayOfTerritories[91].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[82], 82);

            //92 Irag adjacent list 91 89
            arrayOfTerritories[92].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[91], 91);
            arrayOfTerritories[92].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[89], 89);

            //93 Iran adjacent list 82 91
            arrayOfTerritories[93].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[82], 82);
            arrayOfTerritories[93].addTerritoryToDictionaryOfAdjacentTerritories(arrayOfTerritories[91], 91);

/*ADD UNITS*/
            //UNITED STATES
            // add units to United States
            arrayOfTerritories[1].addUnitToArrayListOfUnits(new Infantry("United States"));
            arrayOfTerritories[1].addUnitToArrayListOfUnits(new Infantry("United States"));
            arrayOfTerritories[1].addUnitToArrayListOfUnits(new AntiAircraftGun("United States"));
            arrayOfTerritories[1].addUnitToArrayListOfUnits(new Fighter("United States"));
            arrayOfTerritories[1].addUnitToArrayListOfUnits(new Bomber("United States"));
            arrayOfTerritories[1].addUnitToArrayListOfUnits(new IndustrialComplex("United States"));
            //add units to U.S East Coast
            arrayOfTerritories[3].addUnitToArrayListOfUnits(new Destroyer("United States"));
            arrayOfTerritories[3].addUnitToArrayListOfUnits(new Transport("United States"));

            //GREAT BRITAIN
            //add units to United Kingdom
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Artillery ("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Armor ("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new AntiAircraftGun ("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Fighter ("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Fighter ("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new Bomber ("Great Britain"));
            arrayOfTerritories[38].addUnitToArrayListOfUnits(new IndustrialComplex ("Great Britain"));
            // add units to Canada
            arrayOfTerritories[0].addUnitToArrayListOfUnits(new Armor("Great Britain"));
            arrayOfTerritories[0].addUnitToArrayListOfUnits(new AntiAircraftGun("Great Britain"));
            arrayOfTerritories[0].addUnitToArrayListOfUnits(new IndustrialComplex("Great Britain"));           
            //add units to Egypts
            arrayOfTerritories[87].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            arrayOfTerritories[87].addUnitToArrayListOfUnits(new Artillery ("Great Britain"));
            arrayOfTerritories[87].addUnitToArrayListOfUnits(new Armor ("Great Britain"));
            //add units to Palestine
            arrayOfTerritories[88].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            //add units to Syria
            arrayOfTerritories[91].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            //add units to Iraq
            arrayOfTerritories[92].addUnitToArrayListOfUnits(new Infantry("Great Britain"));
            //add units to Malta
            arrayOfTerritories[66].addUnitToArrayListOfUnits(new Fighter("Great Britain"));
            //add units to Eastern Mediterranean
            arrayOfTerritories[85].addUnitToArrayListOfUnits(new Destroyer ("Great Britain"));
            arrayOfTerritories[85].addUnitToArrayListOfUnits(new Transport ("Great Britain"));
            //add units to Strait of Gibraltar
            arrayOfTerritories[26].addUnitToArrayListOfUnits(new Destroyer("Great Britain"));
            //add units to North Sea
            arrayOfTerritories[41].addUnitToArrayListOfUnits(new Battleship("Great Britain"));
            arrayOfTerritories[41].addUnitToArrayListOfUnits(new Destroyer ("Great Britain"));
            arrayOfTerritories[41].addUnitToArrayListOfUnits(new Transport ("Great Britain"));
            //add units to English Channel
            arrayOfTerritories[37].addUnitToArrayListOfUnits(new Destroyer ("Great Britain"));
            arrayOfTerritories[37].addUnitToArrayListOfUnits(new Destroyer("Great Britain"));
            //add units to Davis State
            arrayOfTerritories[2].addUnitToArrayListOfUnits(new Destroyer("Great Britain"));
            arrayOfTerritories[2].addUnitToArrayListOfUnits(new Transport("Great Britain"));
            //add units to Celtic Sea
            arrayOfTerritories[39].addUnitToArrayListOfUnits(new Destroyer("Great Britain"));
            arrayOfTerritories[39].addUnitToArrayListOfUnits(new Destroyer("Great Britain"));
            arrayOfTerritories[39].addUnitToArrayListOfUnits(new Destroyer("Great Britain"));
            
            //SOVIET UNION      
            //add units to Moscow
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new Armor("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new AntiAircraftGun ("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new Fighter ("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new Bomber ("Soviet Union"));
            arrayOfTerritories[75].addUnitToArrayListOfUnits(new IndustrialComplex("Soviet Union"));
            //add units to Russia
            arrayOfTerritories[74].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[74].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[74].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[74].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[74].addUnitToArrayListOfUnits(new Artillery("Soviet Union"));
            //add units to Siberia
            arrayOfTerritories[12].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[12].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[12].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[12].addUnitToArrayListOfUnits(new Artillery("Soviet Union"));
            arrayOfTerritories[12].addUnitToArrayListOfUnits(new Armor("Soviet Union"));
            //add units to Archangel
            arrayOfTerritories[11].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[11].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[11].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[11].addUnitToArrayListOfUnits(new Armor("Soviet Union"));
            arrayOfTerritories[11].addUnitToArrayListOfUnits(new AntiAircraftGun("Soviet Union"));
            arrayOfTerritories[11].addUnitToArrayListOfUnits(new IndustrialComplex("Soviet Union"));
            //add units to Karelia
            arrayOfTerritories[73].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[73].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            //add units to Leningrad
            arrayOfTerritories[72].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[72].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[72].addUnitToArrayListOfUnits(new AntiAircraftGun ("Soviet Union"));
            arrayOfTerritories[72].addUnitToArrayListOfUnits(new IndustrialComplex ("Soviet Union"));
            //add units to Belorussia
            arrayOfTerritories[76].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[76].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[76].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[76].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[76].addUnitToArrayListOfUnits(new Artillery("Soviet Union"));
            //add units to Turkestan
            arrayOfTerritories[78].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[78].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[78].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[78].addUnitToArrayListOfUnits(new Armor("Soviet Union"));
            //add units to Stalingrad
            arrayOfTerritories[77].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[77].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[77].addUnitToArrayListOfUnits(new Artillery ("Soviet Union"));
            arrayOfTerritories[77].addUnitToArrayListOfUnits(new AntiAircraftGun ("Soviet Union"));
            arrayOfTerritories[77].addUnitToArrayListOfUnits(new IndustrialComplex ("Soviet Union"));
            //add units to Ukraine S.S.R
            arrayOfTerritories[79].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[79].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[79].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[79].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[79].addUnitToArrayListOfUnits(new Artillery("Soviet Union"));
            //add units to Caucasus
            arrayOfTerritories[80].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[80].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[80].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[80].addUnitToArrayListOfUnits(new Armor("Soviet Union"));
            //add units to Iran
            arrayOfTerritories[93].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            //add units to East Poland
            arrayOfTerritories[59].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[59].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[59].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[59].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            //add units to Baltic States
            arrayOfTerritories[58].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[58].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[58].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            //add units to Vyborg
            arrayOfTerritories[57].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            //add units to White Sea
            arrayOfTerritories[10].addUnitToArrayListOfUnits(new Transport("Soviet Union"));
            arrayOfTerritories[10].addUnitToArrayListOfUnits(new Submarine("Soviet Union"));
            //add units to Bessarabia
            arrayOfTerritories[71].addUnitToArrayListOfUnits(new Infantry("Soviet Union"));
            arrayOfTerritories[71].addUnitToArrayListOfUnits(new Armor("Soviet Union"));
            
            //GERMANY
            //add units to Germany
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Artillery  ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Artillery  ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Armor  ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new  Armor ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new  AntiAircraftGun ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Fighter  ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Fighter  ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new Bomber ("Germany"));
            arrayOfTerritories[48].addUnitToArrayListOfUnits(new IndustrialComplex  ("Germany"));
            //add units to NetherLand Belgium
            arrayOfTerritories[42].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[42].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[42].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[42].addUnitToArrayListOfUnits(new Armor ("Germany"));
            arrayOfTerritories[42].addUnitToArrayListOfUnits(new Armor ("Germany"));
            //add units to Denmark
            arrayOfTerritories[47].addUnitToArrayListOfUnits(new Infantry  ("Germany"));
            //add units to Poland
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Artillery ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Armor ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Armor ("Germany"));
            arrayOfTerritories[53].addUnitToArrayListOfUnits(new Fighter ("Germany"));
            //add units to Norway
            arrayOfTerritories[45].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[45].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[45].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[45].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[45].addUnitToArrayListOfUnits(new Fighter ("Germany"));
            //add units to Finland
            arrayOfTerritories[56].addUnitToArrayListOfUnits(new Infantry("Germany"));
            arrayOfTerritories[56].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[56].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            //add units to Czechslovakia
            arrayOfTerritories[52].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[52].addUnitToArrayListOfUnits(new Artillery ("Germany"));
            //add units to Austria
            arrayOfTerritories[51].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[51].addUnitToArrayListOfUnits(new Artillery ("Germany"));
            //add units to Northern Italy
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new Artillery  ("Germany"));
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new Armor  ("Germany"));
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new  Armor ("Germany"));
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new  AntiAircraftGun ("Germany"));
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new Fighter  ("Germany"));
            arrayOfTerritories[50].addUnitToArrayListOfUnits(new  IndustrialComplex ("Germany"));
            //add units to Sounthern Italy
            arrayOfTerritories[63].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[63].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            //add units to Yogoslavia
            arrayOfTerritories[61].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[61].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            //add units to Hungary
            arrayOfTerritories[60].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[60].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[60].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[60].addUnitToArrayListOfUnits(new Artillery ("Germany"));
            arrayOfTerritories[60].addUnitToArrayListOfUnits(new Armor ("Germany"));
            //add units to Rumania
            arrayOfTerritories[70].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[70].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[70].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[70].addUnitToArrayListOfUnits(new Armor ("Germany"));
            //add units to Bulgaria
            arrayOfTerritories[69].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[69].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            //add units to Greece
            arrayOfTerritories[68].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[68].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[68].addUnitToArrayListOfUnits(new Artillery ("Germany"));
            //add units to Crete
            arrayOfTerritories[84].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            //add units to Tunisia
            arrayOfTerritories[34].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[34].addUnitToArrayListOfUnits(new Armor ("Germany"));
            arrayOfTerritories[34].addUnitToArrayListOfUnits(new Artillery ("Germany"));
            //add units to Morocco
            arrayOfTerritories[28].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            //add units to France
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Artillery ("Germany"));
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Armor ("Germany"));
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Armor ("Germany"));
            arrayOfTerritories[35].addUnitToArrayListOfUnits(new Fighter ("Germany"));
            //add units to Eastern France
            arrayOfTerritories[36].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[36].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[36].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            arrayOfTerritories[36].addUnitToArrayListOfUnits(new Infantry ("Germany"));
            //add units to tyrrhenian Sea
            arrayOfTerritories[33].addUnitToArrayListOfUnits(new Battleship ("Germany"));
            arrayOfTerritories[33].addUnitToArrayListOfUnits(new Destroyer ("Germany"));
            arrayOfTerritories[33].addUnitToArrayListOfUnits(new Transport ("Germany"));
            //add units to South Atlantic
            arrayOfTerritories[5].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            //add units to Azores Sea
            arrayOfTerritories[23].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            //add units to Mid-atlantic
            arrayOfTerritories[22].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            //add units to bay of Biscay
            arrayOfTerritories[24].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            arrayOfTerritories[24].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            //add units to Halifax
            arrayOfTerritories[21].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            //add units to Denmark Strait
            arrayOfTerritories[6].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            //add units to Barent Sea
            arrayOfTerritories[9].addUnitToArrayListOfUnits(new Submarine ("Germany"));
            //add units to Danish Sea
            arrayOfTerritories[46].addUnitToArrayListOfUnits(new Transport ("Germany"));
            arrayOfTerritories[46].addUnitToArrayListOfUnits(new Submarine("Germany"));
            arrayOfTerritories[46].addUnitToArrayListOfUnits(new Submarine("Germany"));
            

            currentMouseState = new MouseState();
        }

        public Territory[] getArrayOfTerritories()
        {
            return arrayOfTerritories;
        }

        public void selectTerritory(ContentManager theContent, SpriteBatch spriteBatch, Game1 game, MouseState previousMouseState, ref Player player)
        {
            currentMouseState = Mouse.GetState();

            if (currentMouseState.LeftButton.Equals(ButtonState.Pressed)
                && previousMouseState.LeftButton.Equals(ButtonState.Released))
            {
                for (int i = 0; i < arrayOfTerritories.Length; ++i)
                {
                    if (arrayOfTerritories[i] == null) break;

                    Vector2 pos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                    if (arrayOfTerritories[i].getRectangleofTerritory().Intersects(getRectangleOfMousePointerSprite(ref player)))
                    {
                        if (movement == Movement.PlaceNewUnits)
                        {
                            if (arrayOfTerritories[i].getContainsIndustrialComplex())
                            {
                                game.mCurrentScreen = Game1.ScreenState.MoveUnits;
                                indexOfSelectedTerritory = i;
                            }
                        }
                        else
                        {
                            game.mCurrentScreen = Game1.ScreenState.MoveUnits;
                            indexOfSelectedTerritory = i;
                        }
                    }
                }
            }

            viewUnitsInTerritory(theContent, spriteBatch, game, previousMouseState, player);
        }

        private void viewUnitsInTerritory(ContentManager theContent, SpriteBatch spriteBatch, Game1 game, MouseState previousMouseState, Player player)
        {
            currentMouseState = Mouse.GetState();

            if (currentMouseState.RightButton.Equals(ButtonState.Pressed) &&
                previousMouseState.LeftButton.Equals(ButtonState.Released))
            {
                for (int i = 0; i < arrayOfTerritories.Length; ++i)
                {
                    if (arrayOfTerritories[i] == null) break;

                    Vector2 pos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                    if (arrayOfTerritories[i].getRectangleofTerritory().Intersects(getRectangleOfMousePointerSprite(ref player)))
                    {
                        indexOfHighlightedTerritory = i;
                    }
                }
            }
        }
        public void moveMap(Game1 game, ref Player player, KeyboardState previousKeyboardState)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if ((keyboardState.IsKeyDown(Keys.Left)))
            {
                if (positionOfGameBoard.X == 0)
                    return;
                positionOfGameBoard.X = positionOfGameBoard.X + 10;
                foreach (Territory territory in this.getArrayOfTerritories())
                {
                    if (territory == null) break;
                    territory.setPosition((territory.getPosition().X + 10), (territory.getPosition().Y));
                }
            }

            if ((keyboardState.IsKeyDown(Keys.Right)))
            {
                if (positionOfGameBoard.X == -1400)
                    return;
                positionOfGameBoard.X = positionOfGameBoard.X - 10;
                foreach (Territory territory in this.getArrayOfTerritories())
                {
                    if (territory == null) break;
                    territory.setPosition((territory.getPosition().X - 10), (territory.getPosition().Y));
                }

            }

            if ((keyboardState.IsKeyDown(Keys.Up)))
            {
                if (positionOfGameBoard.Y == 0)
                    return;
                positionOfGameBoard.Y = positionOfGameBoard.Y + 10;
                foreach (Territory territory in this.getArrayOfTerritories())
                {
                    if (territory == null) break;
                    territory.setPosition((territory.getPosition().X), (territory.getPosition().Y + 10));
                }


            }

            if ((keyboardState.IsKeyDown(Keys.Down)))
            {
                if (positionOfGameBoard.Y == -900)
                    return;
                positionOfGameBoard.Y = positionOfGameBoard.Y - 10;
                foreach (Territory territory in this.getArrayOfTerritories())
                {
                    if (territory == null) break;
                    territory.setPosition((territory.getPosition().X), (territory.getPosition().Y - 10));
                }

            }

            if ((keyboardState.IsKeyDown(Keys.F5)) && previousKeyboardState.IsKeyUp(Keys.F5))
            {
                if (movement == Movement.NonCombat)
                {
                    movement = Movement.PlaceNewUnits;
                    return;
                }

                if (movement == Movement.PlaceNewUnits)
                {
                    player.nextWorldPower(game, this);
                    movement = Movement.Combat;
                    if (game.mCurrentScreen == Game1.ScreenState.Victory)
                        return;
                    game.mCurrentScreen = Game1.ScreenState.Purchase;
                    resetUnitMovement();
                    return;
                }
                game.mCurrentScreen = Game1.ScreenState.Battleboard;
            }
        }



        public void Draw(SpriteBatch spriteBatch, ContentManager theContent, ref Player player)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                SpriteSortMode.Immediate, SaveStateMode.None);

            spriteBatch.Draw(gameBoard, positionOfGameBoard, Color.White);

            SpriteFont font;
            SpriteFont fontHighLighted;

            font = theContent.Load<SpriteFont>("Fonts\\GameFont");
            fontHighLighted = theContent.Load<SpriteFont>("Fonts\\GameFontHighLighted");

            spriteBatch.DrawString(font,
            "Click F5 to end the phase. " + movement + " " + player.getWhosTurn(), (
            new Vector2(0, 0)),
            Color.White);

            spriteBatch.End();

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
                SpriteSortMode.Immediate, SaveStateMode.None);

            foreach (Territory territory in arrayOfTerritories)
            {
                if (territory == null) break;
                spriteBatch.Draw(territory.getSprite(), territory.getPosition(), Color.White);
            }

            spriteBatch.End();

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
            SpriteSortMode.Immediate, SaveStateMode.None);

            Vector2 pos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            spriteBatch.Draw(player.getMousePointerSprite(), pos, Color.White);

            Texture2D unitsInHighlightedTerritory;

            unitsInHighlightedTerritory = theContent.Load<Texture2D>("UnitSelection\\Units in Highlighted Territory");

            spriteBatch.Draw(unitsInHighlightedTerritory, new Vector2(0, 636), Color.White);

            spriteBatch.End();

            drawUnitsInHighlightedTerritory(spriteBatch, theContent, "Germany", 0);
            drawUnitsInHighlightedTerritory(spriteBatch, theContent, "Great Britain", 417);
            drawUnitsInHighlightedTerritory(spriteBatch, theContent, "United States", 846);
            drawUnitsInHighlightedTerritory(spriteBatch, theContent, "Soviet Union", 1265);

        }
        public Rectangle getRectangleOfMousePointerSprite(ref Player player)
        {
            return new Rectangle
                (
                      ((int)Mouse.GetState().X),
                      ((int)Mouse.GetState().Y),
                      player.getMousePointerSprite().Width,
                      player.getMousePointerSprite().Height);
                
        }

        private void drawUnitsInHighlightedTerritory(SpriteBatch spriteBatch, ContentManager theContent, string worldPower, int x)
        {
            int numberOfInfanty = 0;
            foreach (Unit unit in ((arrayOfTerritories[indexOfHighlightedTerritory]).getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Infantry", true)) == 0)
                        numberOfInfanty++;
                }
            }

            int numberOfArmor = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Armor", true)) == 0)
                        numberOfArmor++;
                }
            }



            int numberOfArtillery = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Artillery", true)) == 0)
                        numberOfArtillery++;
                }
            }



            int numberOfAntiAircraftGun = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "AntiAircraftGun", true)) == 0)
                        numberOfAntiAircraftGun++;
                }
            }

  

            int numberOfFighter = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Fighter", true)) == 0)
                        numberOfFighter++;
                }
            }



            int numberOfBomber = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Bomber", true)) == 0)
                        numberOfBomber++;
                }
            }


            int numberOfBattleship = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Battleship", true)) == 0)
                        numberOfBattleship++;
                }
            }


            int numberOfAirCraftCarrier = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "AirCraftCarrier", true)) == 0)
                        numberOfAirCraftCarrier++;
                }
            }



            int numberOfTransport = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Transport", true)) == 0)
                    {
                        numberOfTransport++;
                        foreach (Unit unitInTransport in ((Transport)unit).getArrayListOfLoadedUnits())
                        {
                            if (unitInTransport.getType() == "Infantry")
                                numberOfInfanty++;

                            if (unitInTransport.getType() == "Armor")
                                numberOfArmor++;

                            if (unitInTransport.getType() == "Artillery")
                                numberOfArtillery++;

                            if (unitInTransport.getType() == "AntiAircraftGun")
                                numberOfAntiAircraftGun++;
                        }
                    }
                }
            }

 

            int numberOfDestroyer = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfHighlightedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Destroyer", true)) == 0)
                        numberOfDestroyer++;
                }
            }



            int numberofSubmarine = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getWorldPower(), worldPower, true)) == 0)
                {
                    if ((string.Compare(unit.getType(), "Submarine", true)) == 0)
                        numberofSubmarine++;
                }
            }


            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
   SpriteSortMode.Immediate, SaveStateMode.None);

            SpriteFont MapFont;
            // Texture2D unitsInHighlightedTerritory;

            MapFont = theContent.Load<SpriteFont>("Fonts\\MapFont");

            // unitsInHighlightedTerritory = theContent.Load<Texture2D>("UnitSelection\\Units in Highlighted Territory");

            // spriteBatch.Draw(unitsInHighlightedTerritory, new Vector2(0, 636), Color.White);

            spriteBatch.DrawString(MapFont,
                "Name of territory: " + arrayOfTerritories[indexOfHighlightedTerritory].getNameOfTerritory(), (
                new Vector2(790, 636 - 50)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfInfanty ,
                (new Vector2(68 + x, 636 + 125)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfBomber,
                (new Vector2(68 + x, 636 + 200)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfTransport,
                (new Vector2(68 + x, 636 + 272)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfArtillery,
                (new Vector2(156 + x, 636 + 125)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfBattleship,
                (new Vector2(156 + x, 636 + 200)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberofSubmarine,
                (new Vector2(156 + x, 636 + 272)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfArmor,
                (new Vector2(253 + x, 636 + 125)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfAirCraftCarrier,
                (new Vector2(253 + x, 636 + 200)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfAntiAircraftGun,
                (new Vector2(253 + x, 636 + 272)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfFighter,
                (new Vector2(343 + x, 636 + 125)),
                Color.Black);

            spriteBatch.DrawString(MapFont,
                "" + numberOfDestroyer,
                (new Vector2(343 + x, 636 + 200)),
                Color.Black);



            spriteBatch.End();

        }

        public void drawUnitSelectionScreen(Game1 game, ContentManager theContent, SpriteBatch spriteBatch, Player player, MouseState previousMouseState)
        {
            if (movement == Movement.PlaceNewUnits)
            {
                drawPlaceNewUnits(theContent, spriteBatch, player, previousMouseState);
                return;
            }

            int numberOfInfanty = 0;
            foreach (Unit unit in ((arrayOfTerritories[indexOfSelectedTerritory]).getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Infantry", true)) == 0)
                    numberOfInfanty++;
            }

            int numberOfInfantySelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Infantry", true)) == 0)
                    numberOfInfantySelected++;
            }

            int numberOfArmor = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Armor", true)) == 0)
                    numberOfArmor++;
            }

            int numberOfArmorSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Armor", true)) == 0)
                    numberOfArmorSelected++;
            }

            int numberOfArtillery = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Artillery", true)) == 0)
                    numberOfArtillery++;
            }

            int numberOfArtillerySelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Artillery", true)) == 0)
                    numberOfArtillerySelected++;
            }

            int numberOfAntiAircraftGun = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "AntiAircraftGun", true)) == 0)
                    numberOfAntiAircraftGun++;
            }

            int numberOfAntiAircraftGunSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "AntiAircraftGun", true)) == 0)
                    numberOfAntiAircraftGunSelected++;
            }

            int numberOfFighter = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Fighter", true)) == 0)
                    numberOfFighter++;
            }

            int numberOfFighterSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Fighter", true)) == 0)
                    numberOfFighterSelected++;
            }

            int numberOfBomber = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Bomber", true)) == 0)
                    numberOfBomber++;
            }

            int numberOfBomberSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Bomber", true)) == 0)
                    numberOfBomberSelected++;
            }

            int numberOfBattleship = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Battleship", true)) == 0)
                    numberOfBattleship++;
            }

            int numberOfBattleshipSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Battleship", true)) == 0)
                    numberOfBattleshipSelected++;
            }

            int numberOfAirCraftCarrier = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "AirCraftCarrier", true)) == 0)
                    numberOfAirCraftCarrier++;
            }

            int numberOfAirCraftCarrierSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "AirCraftCarrier", true)) == 0)
                    numberOfAirCraftCarrierSelected++;
            }

            int numberOfTransport = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Transport", true)) == 0)
                {
                    numberOfTransport++;
                    foreach (Unit unitInTransport in ((Transport)unit).getArrayListOfLoadedUnits())
                    {
                        if (unitInTransport.getType() == "Infantry")
                            numberOfInfanty++;

                        if (unitInTransport.getType() == "Armor")
                            numberOfArmor++;

                        if (unitInTransport.getType() == "Artillery")
                            numberOfArtillery++;

                        if (unitInTransport.getType() == "AntiAircraftGun")
                            numberOfAntiAircraftGun++;
                    }
                }
            }

            int numberOfTransportSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Transport", true)) == 0)
                    numberOfTransportSelected++;
            }

            int numberOfDestroyer = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Destroyer", true)) == 0)
                    numberOfDestroyer++;
            }

            int numberOfDestroyerSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Destroyer", true)) == 0)
                    numberOfDestroyerSelected++;
            }

            int numberofSubmarine = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits()))
            {
                if ((string.Compare(unit.getType(), "Submarine", true)) == 0)
                    numberofSubmarine++;
            }

            int numberOfSubmarineSelected = 0;
            foreach (Unit unit in (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits()))
            {
                if ((string.Compare(unit.getType(), "Submarine", true)) == 0)
                    numberOfSubmarineSelected++;
            }



            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
   SpriteSortMode.Immediate, SaveStateMode.None);

            SpriteFont font;
            SpriteFont fontHighLighted;

            font = theContent.Load<SpriteFont>("Fonts\\GameFont");
            fontHighLighted = theContent.Load<SpriteFont>("Fonts\\GameFontHighLighted");

            spriteBatch.DrawString(font,
           "Name of territory: " + arrayOfTerritories[indexOfSelectedTerritory].getNameOfTerritory() + "     click on space to exit", (
           new Vector2(0, 0)),
           Color.Black);

            #region Lists # of units in selected Territory

            if (highLightedUnits != 0)
            {
                spriteBatch.DrawString(font,
            "Number of Infantry:                    " + numberOfInfanty + "     " + numberOfInfantySelected,
            (new Vector2(0, 25)),
            Color.Black);
            }

            if (highLightedUnits != 1)
            {
                spriteBatch.DrawString(font,
                "Number of Armor:                   " + numberOfArmor + "     " + numberOfArmorSelected,
                (new Vector2(0, 50)),
                Color.Black);
            }

            if (highLightedUnits != 2)
            {
                spriteBatch.DrawString(font,
                "Number of Artillery:               " + numberOfArtillery + "     " + numberOfArtillerySelected,
                (new Vector2(0, 75)),
                Color.Black);
            }

            if (highLightedUnits != 3)
            {
                spriteBatch.DrawString(font,
                "Number of AntiAircraftGun:                   " + numberOfAntiAircraftGun + "     " + numberOfAntiAircraftGunSelected,
                (new Vector2(0, 100)),
                Color.Black);
            }


            if (highLightedUnits != 4)
            {
                spriteBatch.DrawString(font,
                "Number of Fighter:                   " + numberOfFighter + "     " + numberOfFighterSelected,
                (new Vector2(0, 125)),
                Color.Black);
            }

            if (highLightedUnits != 5)
            {
                spriteBatch.DrawString(font,
                "Number of Bomber:                   " + numberOfBomber + "     " + numberOfBomberSelected,
                (new Vector2(0, 150)),
                Color.Black);
            }


            if (highLightedUnits != 6)
            {
                spriteBatch.DrawString(font,
                "Number of Battleship:                   " + numberOfBattleship + "     " + numberOfBattleshipSelected,
                (new Vector2(0, 175)),
                Color.Black);
            }

            if (highLightedUnits != 7)
            {
                spriteBatch.DrawString(font,
                "Number of AirCraftCarrier:                   " + numberOfAirCraftCarrier + "     " + numberOfAirCraftCarrierSelected,
                (new Vector2(0, 200)),
                Color.Black);
            }

            if (highLightedUnits != 8)
            {
                spriteBatch.DrawString(font,
                "Number of Transport:                   " + numberOfTransport + "     " + numberOfTransportSelected,
                (new Vector2(0, 225)),
                Color.Black);
            }

            if (highLightedUnits != 9)
            {
                spriteBatch.DrawString(font,
                "Number of Destroyer:                   " + numberOfDestroyer + "     " + numberOfDestroyerSelected,
                (new Vector2(0, 250)),
                Color.Black);
            }

            if (highLightedUnits != 10)
            {
                spriteBatch.DrawString(font,
                "Number of Submarine:                   " + numberofSubmarine + "     " + numberOfSubmarineSelected,
                (new Vector2(0, 275)),
                Color.Black);
            }

            if (highLightedUnits != 11)
            {
                spriteBatch.DrawString(font,
                "Territory to move to:                   " +
                ((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory)).Key),
                (new Vector2(0, 300)),
                Color.Black);
            }

            #endregion
            #region switch to highlight the selected type of unit.

            switch (highLightedUnits)
            {
                case 0:



                    spriteBatch.DrawString(fontHighLighted,
                    "Number of Infantry:                " + numberOfInfanty + "     " + numberOfInfantySelected,
                    (new Vector2(0, 25)),
                    Color.Black);
                    break;

                case 1:
                    spriteBatch.DrawString(fontHighLighted,
                    "Number of Armor:                   " + numberOfArmor + "     " + numberOfArmorSelected,
                    (new Vector2(0, 50)),
                    Color.Black);
                    break;

                case 2:
                    spriteBatch.DrawString(fontHighLighted,
                    "Number of Artillery:               " + numberOfArtillery + "     " + numberOfArtillerySelected,
                    (new Vector2(0, 75)),
                    Color.Black);
                    break;

                case 3:
                    spriteBatch.DrawString(fontHighLighted,
                    "Number of AntiAircraftGun:                   " + numberOfAntiAircraftGun + "     " + numberOfAntiAircraftGunSelected,
                    (new Vector2(0, 100)),
                    Color.Black);
                    break;
                
                case 4:
                spriteBatch.DrawString(fontHighLighted,
                "Number of Fighter:                   " + numberOfFighter + "     " + numberOfFighterSelected,
                (new Vector2(0, 125)),
                Color.Black);
                    break;

                case 5:
                spriteBatch.DrawString(fontHighLighted,
                "Number of Bomber:                   " + numberOfBomber + "     " + numberOfBomberSelected,
                (new Vector2(0, 150)),
                Color.Black);
                    break;


                case 6:
                spriteBatch.DrawString(fontHighLighted,
                "Number of Battleship:                   " + numberOfBattleship + "     " + numberOfBattleshipSelected,
                (new Vector2(0, 175)),
                Color.Black);
                    break;

                case 7:
                spriteBatch.DrawString(fontHighLighted,
                "Number of AirCraftCarrier:                   " + numberOfAirCraftCarrier + "     " + numberOfAirCraftCarrierSelected,
                (new Vector2(0, 200)),
                Color.Black);
                    break;

                case 8:
                spriteBatch.DrawString(fontHighLighted,
                "Number of Transport:                   " + numberOfTransport + "     " + numberOfTransportSelected,
                (new Vector2(0, 225)),
                Color.Black);
                    break;


                case 9:
                spriteBatch.DrawString(fontHighLighted,
                "Number of Destroyer:                   " + numberOfDestroyer + "     " + numberOfDestroyerSelected,
                (new Vector2(0, 250)),
                Color.Black);
                    break;

                case 10:
                    spriteBatch.DrawString(fontHighLighted,
                "Number of Submarine:                   " + numberofSubmarine + "     " + numberOfSubmarineSelected,
                (new Vector2(0, 275)),
                Color.Black);
                    break;

                case 11:
                    spriteBatch.DrawString(fontHighLighted,
                    "Territory to move to:                   " +
                    ((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory)).Key),
                    (new Vector2(0, 300)),
                    Color.Black);
                    break;
            }
            #endregion




            spriteBatch.End();

        }

        public void unitSelectionScreen(Game1 game, KeyboardState previousKeyboardState, ref Player player, MouseState previousMouseState)
        {          
            if (movement == Movement.PlaceNewUnits)
            {
                placeNewUnits(game, previousMouseState, player);
                return;
            }

            KeyboardState keyboardState = Keyboard.GetState();

            if ((keyboardState.IsKeyDown(Keys.Down)) && previousKeyboardState.IsKeyUp(Keys.Down))
            {
                highLightedUnits++;
                if (highLightedUnits > 11)
                    highLightedUnits = 0;
            }

            if ((keyboardState.IsKeyDown(Keys.Up)) && previousKeyboardState.IsKeyUp(Keys.Up))
            {
                highLightedUnits--;
                if (highLightedUnits < 0)
                    highLightedUnits = 11;
            }

            // selected units to move to another territory

            if ((keyboardState.IsKeyDown(Keys.Right)) && previousKeyboardState.IsKeyUp(Keys.Right))
            {
                switch (highLightedUnits)
                {
                    case 0:
                        // unloading from a transport check for sea territory
                        if (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).isSeaTerritory())
                        {
                            foreach (Unit unit in (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).getArrayListOfUnits()))
                            {
                                if (unit.getType() == "Transport")
                                {
                                    for (int i = 0;i < ((Transport)unit).getArrayListOfLoadedUnits().Count; ++i )
                                    {
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(((Transport)unit).unLoadLandUnit(((Unit)(((Transport)unit).getArrayListOfLoadedUnits()[i]))));
                                    }
                                }
                            }
                        }


                        if (!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory()))
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Infantry", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                            // checking for transport.
                        else
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Infantry", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).containsTransport())))
                                            break;
                                        if ((arrayOfTerritories.ElementAt(arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory).Value))
                                            .addUnitToEmptyTransport(unit))
                                        {
                                            arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 1:
                        // unloading from a transport check for sea territory
                        if (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).isSeaTerritory())
                        {
                            foreach (Unit unit in (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).getArrayListOfUnits()))
                            {
                                if (unit.getType() == "Transport")
                                {
                                    for (int i = 0; i < ((Transport)unit).getArrayListOfLoadedUnits().Count; ++i)
                                    {
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(((Transport)unit).unLoadLandUnit(((Unit)(((Transport)unit).getArrayListOfLoadedUnits()[i]))));
                                    }
                                }
                            }
                        }

                        if (!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory()))
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Armor", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        // checking for transport.
                        else
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Armor", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).containsTransport())))
                                            break;
                                        if ((arrayOfTerritories.ElementAt(arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory).Value))
                                            .addUnitToEmptyTransport(unit))
                                        {
                                            arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 2:
                        // unloading from a transport check for sea territory
                        if (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).isSeaTerritory())
                        {
                            foreach (Unit unit in (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).getArrayListOfUnits()))
                            {
                                if (unit.getType() == "Transport")
                                {
                                    for (int i = 0; i < ((Transport)unit).getArrayListOfLoadedUnits().Count; ++i)
                                    {
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(((Transport)unit).unLoadLandUnit(((Unit)(((Transport)unit).getArrayListOfLoadedUnits()[i]))));
                                    }
                                }
                            }
                        }

                        if (!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory()))
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Artillery", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        // checking for transport.
                        else
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Artillery", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).containsTransport())))
                                            break;
                                        if ((arrayOfTerritories.ElementAt(arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory).Value))
                                            .addUnitToEmptyTransport(unit))
                                        {
                                            arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 3:
                        // unloading from a transport check for sea territory
                        if (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).isSeaTerritory())
                        {
                            foreach (Unit unit in (arrayOfTerritories.ElementAt(indexOfSelectedTerritory).getArrayListOfUnits()))
                            {
                                if (unit.getType() == "Transport")
                                {
                                    for (int i = 0; i < ((Transport)unit).getArrayListOfLoadedUnits().Count; ++i)
                                    {
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(((Transport)unit).unLoadLandUnit(((Unit)(((Transport)unit).getArrayListOfLoadedUnits()[i]))));
                                    }
                                }
                            }
                        }

                        if (!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory()))
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "AntiAircraftGun", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        // checking for transport.
                        else
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "AntiAircraftGun", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((!(arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).containsTransport())))
                                            break;
                                        if ((arrayOfTerritories.ElementAt(arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory).Value))
                                            .addUnitToEmptyTransport(unit))
                                        {
                                            arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 4:
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Fighter", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        break;

                    case 5:

                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Bomber", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        break;


                    case 6:
                        if (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory())
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Battleship", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 7:
                        if (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory())
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "AirCraftCarrier", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 8:
                        if (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory())
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Transport", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 9:
                        if (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory())
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Destroyer", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 10:
                        if (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).isSeaTerritory())
                        {
                            foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfUnits())
                            {
                                if (string.Compare(unit.getType(), "Submarine", true) == 0)
                                {
                                    if (unit.canMove())
                                    {
                                        if (!(player.getWhosTurn() == unit.getWorldPower()))
                                            break;
                                        if ((movement == Movement.NonCombat) && (!(unit.getWorldPower() == (arrayOfTerritories.ElementAt(((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory))).Value).getWhoControlsTerritory()))))
                                            break;
                                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfSelectedUnits(unit);
                                        arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfUnits(unit);
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 11:
                        if (indexOfSelectedAdjacentTerritory < ((arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().Count - 1)))
                            if (arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits().Count == 0)
                        indexOfSelectedAdjacentTerritory++;
                        break;
                }
            }

#region KeyBoard Left

            if ((keyboardState.IsKeyDown(Keys.Left)) && previousKeyboardState.IsKeyUp(Keys.Left))
            {
                switch (highLightedUnits)
                {
                    case 0:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Infantry", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 1:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Armor", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 2:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Artillery", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 3:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "AntiAircraftGun", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 4:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Fighter", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 5:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Bomber", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 6:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Battleship", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 7:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "AirCraftCarrier", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 8:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Transport", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 9:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Destroyer", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;

                    case 10:
                        foreach (Unit unit in arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits())
                        {
                            if (string.Compare(unit.getType(), "Submarine", true) == 0)
                            {
                                arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(unit);
                                arrayOfTerritories[indexOfSelectedTerritory].removeUnitFromArrayListOfSelectedUnits(unit);
                                break;
                            }
                        }
                        break;


                    case 11:
                        if (indexOfSelectedAdjacentTerritory > 0)
                            indexOfSelectedAdjacentTerritory--;
                        break;
                }
            }

#endregion

            if ((keyboardState.IsKeyDown(Keys.Enter)) && previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                arrayOfTerritories[indexOfSelectedTerritory].reduceMovementLeft();

                arrayOfTerritories[
                arrayOfTerritories[indexOfSelectedTerritory].getDictionaryOfAdjacentTerritories().ElementAt(indexOfSelectedAdjacentTerritory).Value].addUnitToArrayListOfUnits(
                arrayOfTerritories[indexOfSelectedTerritory].getArrayListOfSelectedUnits());

                arrayOfTerritories[indexOfSelectedTerritory].clearArrayListOfSelectedUnits();
            }

            if ((keyboardState.IsKeyDown(Keys.Space)) && previousKeyboardState.IsKeyUp(Keys.Space))
            {
                indexOfSelectedAdjacentTerritory = 0;
                game.mCurrentScreen = Game1.ScreenState.Map;
            }
        }

        public Texture2D getMousePointerSprite(ref Player player)
        {
            return player.getMousePointerSprite();
        }

        public void removeUnitFromSelectedTerritory(Unit unit, int indexOfTerritory)
        {
            arrayOfTerritories[indexOfTerritory].removeUnitFromArrayListOfUnits(unit);
        }

        public void setWhoControlsTerritory(string worldPower, int indexOfTerritory, ContentManager theContent)
        {
            arrayOfTerritories[indexOfTerritory].externalSetWhoControlsTerritory(worldPower, theContent);
        }
        private void resetUnitMovement()
        {
            for (int i = 0; i < arrayOfTerritories.Length; ++i)
            {
                arrayOfTerritories[i].resetUnitMovement();
            }
        }

        private void drawPlaceNewUnits(ContentManager theContent, SpriteBatch spriteBatch, Player player, MouseState previousMouseState)
        {
            int numberOfInfanty = 0;
            int numberOfArmor = 0;
            int numberOfArtillery = 0;
            int numberOfAntiAircraftGun = 0;
            int numberOfFighter = 0;
            int numberOfBomber = 0;
            int numberOfBattleship = 0;
            int numberOfAirCraftCarrier = 0;
            int numberOfTransport = 0;
            int numberOfDestroyer = 0;
            int numberofSubmarine = 0;

            if (!(player.getListOfPurchasedUnits() == null))
                foreach (Unit unit in player.getListOfPurchasedUnits())
                {
                    if ((string.Compare(unit.getType(), "Infantry", true)) == 0)
                        numberOfInfanty++;
                    if ((string.Compare(unit.getType(), "Armor", true)) == 0)
                        numberOfArmor++;
                    if ((string.Compare(unit.getType(), "Artillery", true)) == 0)
                        numberOfArtillery++;
                    if ((string.Compare(unit.getType(), "AntiAircraftGun", true)) == 0)
                        numberOfAntiAircraftGun++;
                    if ((string.Compare(unit.getType(), "Fighter", true)) == 0)
                        numberOfFighter++;
                    if ((string.Compare(unit.getType(), "Bomber", true)) == 0)
                        numberOfBomber++;
                    if ((string.Compare(unit.getType(), "Battleship", true)) == 0)
                        numberOfBattleship++;
                    if ((string.Compare(unit.getType(), "AirCraftCarrier", true)) == 0)
                        numberOfAirCraftCarrier++;
                    if ((string.Compare(unit.getType(), "Transport", true)) == 0)
                        numberOfTransport++;
                    if ((string.Compare(unit.getType(), "Destroyer", true)) == 0)
                        numberOfDestroyer++;
                    if ((string.Compare(unit.getType(), "Submarine", true)) == 0)
                        numberofSubmarine++;
                }

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend,
            SpriteSortMode.Immediate, SaveStateMode.None);

            Texture2D sprite;

            sprite = theContent.Load<Texture2D>("UnitSelection\\PurchaseUnitSelectionScreen");

            spriteBatch.Draw(sprite, (new Vector2(0, 0)), Color.White);

            SpriteFont font;
            SpriteFont fontHighLighted;
            int highLightedUnits = 0;

            font = theContent.Load<SpriteFont>("Fonts\\GameFont");
            fontHighLighted = theContent.Load<SpriteFont>("Fonts\\GameFontHighLighted");

            spriteBatch.DrawString(font,
                "" + numberOfInfanty,
                (new Vector2(163, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfBomber,
                (new Vector2(163, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfTransport,
                (new Vector2(163, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfArtillery,
                (new Vector2(322, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfBattleship,
                (new Vector2(322, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberofSubmarine,
                (new Vector2(322, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfArmor,
                (new Vector2(503, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfAntiAircraftGun,
                (new Vector2(503, 541)),
                Color.Black);

            spriteBatch.DrawString(font,
               "" + numberOfAirCraftCarrier,
               (new Vector2(503, 398)),
               Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfFighter,
                (new Vector2(684, 255)),
                Color.Black);

            spriteBatch.DrawString(font,
                "" + numberOfDestroyer,
                (new Vector2(684, 398)),
                Color.Black);

            spriteBatch.DrawString(font,
            "" + player.getWhosTurn(), (
            new Vector2(665, 687)),
            Color.Black);

            spriteBatch.DrawString(font,
            "Number of IPC: " + player.getIPC(), (
            new Vector2(665, 787)),
            Color.Black);

            spriteBatch.DrawString(font,
            "Hit Enter when done", (
            new Vector2(665, 887)),
            Color.Black);

            Vector2 pos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            spriteBatch.Draw(this.getMousePointerSprite(ref player), new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);

            spriteBatch.End();
        }

        private void placeNewUnits(Game1 game, MouseState previousMouseState, Player player)
        {
            if (Mouse.GetState().LeftButton.Equals(ButtonState.Pressed)
    && previousMouseState.LeftButton.Equals(ButtonState.Released))
            {

                // add Infantry
                if (new Rectangle(54, 141, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    Infantry infantry = new Infantry(player.getWhosTurn());
                    if (player.getListOfPurchasedUnits().Contains(infantry))
                    {
                        arrayOfTerritories[indexOfSelectedTerritory].addUnitToArrayListOfUnits(new Infantry(player.getWhosTurn()));
                        player.removeUnitFromPurchaseList(new Infantry(player.getWhosTurn()));
                    }
                }

                // purchase Bomber
                if (new Rectangle(54, 285, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Bomber(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Bomber(player.getWhosTurn()));
                        player.reduceIPC(new Bomber(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Transport
                if (new Rectangle(54, 426, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Transport(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Transport(player.getWhosTurn()));
                        player.reduceIPC(new Transport(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Artillery
                if (new Rectangle(230, 141, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Artillery(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Artillery(player.getWhosTurn()));
                        player.reduceIPC(new Artillery(player.getWhosTurn()).getCost());
                    }
                }

                // purhcase Battleship
                if (new Rectangle(230, 285, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Battleship(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Battleship(player.getWhosTurn()));
                        player.reduceIPC(new Battleship(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Submarine
                if (new Rectangle(230, 426, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Submarine(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Submarine(player.getWhosTurn()));
                        player.reduceIPC(new Submarine(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Armor
                if (new Rectangle(417, 141, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Armor(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Armor(player.getWhosTurn()));
                        player.reduceIPC(new Armor(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Carrier
                if (new Rectangle(417, 285, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new AirCraftCarrier(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new AirCraftCarrier(player.getWhosTurn()));
                        player.reduceIPC(new AirCraftCarrier(player.getWhosTurn()).getCost());
                    }
                }

                // purchase AntiAircraftGun.
                if (new Rectangle(417, 426, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new AntiAircraftGun(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new AntiAircraftGun(player.getWhosTurn()));
                        player.reduceIPC(new AntiAircraftGun(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Fighter
                if (new Rectangle(599, 141, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Fighter(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Fighter(player.getWhosTurn()));
                        player.reduceIPC(new Fighter(player.getWhosTurn()).getCost());
                    }
                }

                // purchase Destroyer
                if (new Rectangle(599, 285, 171, 141).Intersects(getRectangleOfMousePointerSprite(ref player)))
                {
                    if ((new Destroyer(player.getWhosTurn()).getCost()) <= player.getIPC())
                    {
                        player.addUnitToPurchaseList(new Destroyer(player.getWhosTurn()));
                        player.reduceIPC(new Destroyer(player.getWhosTurn()).getCost());
                    }

                }
            }

            KeyboardState keyboardState = Keyboard.GetState();
            if ((keyboardState.IsKeyDown(Keys.Enter)) && previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                game.mCurrentScreen = Game1.ScreenState.Map;
            } 
        }
    }
    }


