using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Task_3
{
    public class Map
    {
        int roundCheck = 0;

        GameEngine gE;
        Random r = new Random();
        public int test = 0;
        int xMapSize = 20;
        int yMapSize = 20;

        List<Unit> units;
        private List<Building> buildings = new List<Building>();
        int buildingRd = 0;

        public List<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }

        public List<Building> Buildings
        {
            get { return buildings; }
            set { buildings = value; }
        }

        public Map()
        {
            units = new List<Unit>();
            //SpawnUnitAtFactory();
            GenerateUnits(5);
            //Buildings = new List<Building>();
        }

        int[] listOfPositions = new int[100];
        int nearestDistance;
        int positionInList;

        public void GenerateUnits(int numOfBuildingsToSpawn)//Gets Called Only Once
        {
            //buildings = new List<Building>();
        
            //Spawns Buildings
            for (int b = 0; b < numOfBuildingsToSpawn; b++)
            {
                buildingRd = r.Next(0, 5);

                if(buildingRd >= 3)
                {
                    ResourceBuilding rb = new ResourceBuilding(r.Next(0, 20),
                                                            r.Next(0, 20),
                                                            200,
                                                            r.Next(0, 5),
                                                            "R");

                    buildings.Add(rb);
                }
                else
                {
                    FactoryBuilding fb = new FactoryBuilding(r.Next(0, 20),
                                                            r.Next(0, 20),
                                                            100,
                                                            r.Next(0, 5),
                                                            "F");

                    buildings.Add(fb);
                }


            }


            //Generates Melee Units
            //for (int i = 0; i < 20; i++)
            //{
            //    Melee_Unit mU = new Melee_Unit();
            //    mU.xPos = r.Next(2, xMapSize);
            //    mU.yPos = r.Next(2, yMapSize);
            //    mU.symbol = "{:}";
            //    mU.health = 200;

            //    units.Add(mU);
            //}

            ////Generates Ranged Units
            //for (int i = 0; i < 20; i++)
            //{
            //    Ranged_Unit rU = new Ranged_Unit();
            //    rU.xPos = r.Next(0, xMapSize);
            //    rU.yPos = r.Next(0, yMapSize);
            //    rU.symbol = "[!]";
            //    rU.health = 100;

            //    units.Add(rU);
            //}

        }


        public void SpawnUnitAtFactory()
        {
            //for(int i = 0; i < 20; i++)
            //{
            //    Melee_Unit m = new Melee_Unit(r.Next(0, 20), r.Next(0, 20) + 5, 200, 1, 20, 1, "{:}", "Red");
            //    units.Add(m);
            //}

            //for (int i = 0; i < 20; i++)
            //{
            //    Ranged_Unit rU = new Ranged_Unit(r.Next(0, 20), r.Next(0, 20) + 5, 100, 1, 20, 5, "[!]", "Blue");
            //    units.Add(rU);
            //}



            foreach (Building bd in buildings)
            {

                if (bd is FactoryBuilding)
                {
                    if (bd.symbol == "F" && bd.team < 3)
                    {
                            Ranged_Unit rU = new Ranged_Unit(bd.xPosition, bd.yPosition + 5, 100, 1, 20, 5, "[!]", "Red");
                            //rU.Xpos = bd.xPosition;
                            //rU.Ypos = bd.yPosition + 5;
                            //rU.Symbol = "[!]";
                            //rU.Health = 100;
                            //rU.Attack = 20;
                            //rU.AttackRange = 5;
                            //rU.IsAttacking = false;
                            //rU.IsDead = false;
                            //rU.Team = "Red";

                            units.Add(rU);
                       
                            Melee_Unit mU = new Melee_Unit(bd.xPosition, bd.yPosition + 5, 200, 1, 20, 1, "{:}", "Red");
                            //mU.Xpos = bd.xPosition;
                            //mU.Ypos = bd.xPosition + 5;
                            //mU.Symbol = "{:}";
                            //mU.Health = 200;
                            //mU.Attack = 20;
                            //mU.AttackRange = 1;
                            //mU.IsAttacking = false;
                            //mU.IsDead = false;
                            //mU.Team = "Red";

                            units.Add(mU);
                        

                    }
                    else if (bd.symbol == "F" && bd.team >= 3)
                    {
                      
                            Ranged_Unit rU = new Ranged_Unit(bd.xPosition, bd.yPosition + 5, 100, 1, 20, 5, "[!]", "Red");
                            //rU.Xpos = bd.xPosition;
                            //rU.Ypos = bd.yPosition + 5;
                            //rU.Symbol = "[!]";
                            //rU.Health = 100;
                            //rU.Team = "Blue";

                            units.Add(rU);
                      
                            Melee_Unit mU = new Melee_Unit(bd.xPosition, bd.yPosition + 5, 200, 1, 20, 1, "{:}", "Blue");
                            //mU.Xpos = bd.xPosition;
                            //mU.Ypos = bd.xPosition + 5;
                            //mU.Symbol = "{:}";
                            //mU.Health = 200;
                            //mU.Team = "Blue";

                            units.Add(mU);
                        

                    }
                }

            }

        }


        public void DisplayUnits(GroupBox groupBox)
        {
            //Num_Of_Rounds.Text = "Round : " + gE.round.ToString();

            groupBox.Controls.Clear();

            foreach (Unit u in units)
            {
                Button newLabel = new Button();

                if (u is Melee_Unit)
                {
                    Melee_Unit mu = (Melee_Unit)u;
                    newLabel.Width = 25;
                    newLabel.Height = 25;
                    newLabel.Location = new Point(mu.Xpos * 20, mu.Ypos * 15);
                    newLabel.Text = mu.Symbol;
                    if (mu.Team == "Blue")
                    {
                        newLabel.ForeColor = Color.Blue;
                    }
                    else
                    {
                        newLabel.ForeColor = Color.Red;
                    }
                }
                else if(u is Ranged_Unit)
                {
                    Ranged_Unit ru = (Ranged_Unit)u;
                    newLabel.Width = 25;
                    newLabel.Height = 25;
                    newLabel.Location = new Point(ru.Xpos * 20, ru.Ypos * 15);
                    newLabel.Text = ru.Symbol;
                    if (ru.Team == "Blue")
                    {
                        newLabel.ForeColor = Color.Blue;
                    }
                    else
                    {
                        newLabel.ForeColor = Color.Red;
                    }
                }

                groupBox.Controls.Add(newLabel);

            }
            


            foreach (Building bd in buildings)
            {
                Button newLabel = new Button();
                newLabel.Width = 30;
                newLabel.Height = 30;
                newLabel.Location = new Point(bd.xPosition * 20, bd.yPosition * 15);
                newLabel.Text = bd.symbol;
                if (bd.team >= 3)
                {
                    newLabel.ForeColor = Color.Blue;
                }
                else
                {
                    newLabel.ForeColor = Color.Red;
                }

                groupBox.Controls.Add(newLabel);
            }

            //test = groupBox.Controls.Count;
        }


        public void UpdateUnitMovement()
        {
            //First Checks For Closest Unit

            //foreach (Unit unit in units)//This finds the closest unit to "this" unit
            //{
            //    //unit.Move();
            //    //unit.ClosestUnit();

            //    for (int i = 0; i < units.Count; i++)
            //    {
            //        if(unit.team != units.ElementAt(i).team)
            //        {
            //            double xSqr = Math.Pow(unit.xPos - units.ElementAt(i).xPos, 2);
            //            double ySqr = Math.Pow(unit.yPos - units.ElementAt(i).yPos, 2);
            //            double distance = Math.Round(Math.Sqrt(xSqr + ySqr));

            //            listOfPositions[i] = Convert.ToInt32(distance);
            //        }

            //    }


            //    if (listOfPositions.Min() == 0)
            //    {
            //        for (int u = 0; u < listOfPositions.GetLength(0); u++)
            //        {
            //            if (listOfPositions[u] == 0)
            //            {
            //                listOfPositions[u] = 1000;
            //            }
            //        }

            //        nearestDistance = listOfPositions.Min();


            //    }
            //    else if (listOfPositions.Min() != 0)
            //    {
            //        nearestDistance = listOfPositions.Min();
            //    }




            //    for (int b = 0; b < listOfPositions.GetLength(0); b++)
            //    {
            //        if (listOfPositions[b] == nearestDistance)
            //        {
            //            positionInList = b;
            //        }
            //    }

            //    int newXPos;
            //    int newYPos;

            //    newXPos = units.ElementAt(positionInList).xPos;
            //    newYPos = units.ElementAt(positionInList).yPos;

            //    double xNew = newXPos - unit.xPos;
            //    double yNew = newYPos - unit.yPos;

            //    int subXPos = 0;
            //    int subYPos = 0;

            //    if (xNew < 0)
            //    {
            //        //unit.xPos += -1;
            //        subXPos += -1;
            //    }
            //    else if (xNew > 0)
            //    {
            //        //unit.xPos += 1;
            //        subXPos += 1;
            //    }


            //    if (yNew < 0)
            //    {
            //        //unit.yPos += -1;
            //        subYPos += -1;
            //    }
            //    else if (yNew > 0)
            //    {
            //        subYPos += 1;
            //    }

            //    unit.ClosestUnit(subXPos, subYPos);

            //}

        }


    }
}
