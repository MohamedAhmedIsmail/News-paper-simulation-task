using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerTesting;
using NewspaperSellerModels;
using System.IO;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {

        List<DayTypeDistribution> Day = new List<DayTypeDistribution>();


        List<DemandDistribution> DemandDist = new List<DemandDistribution>();

        List<DayTypeDistribution> DemandGood = new List<DayTypeDistribution>();
        List<DayTypeDistribution> DemandFair = new List<DayTypeDistribution>();
        List<DayTypeDistribution> DemandPoor = new List<DayTypeDistribution>();

        List<DayTypeDistribution> dayTypeDist = new List<DayTypeDistribution>();

        PerformanceMeasures PM = new PerformanceMeasures();
        List<int> RandomDemandNumber = new List<int>();
        List<int> DemandNumbers = new List<int>();
        List<int> RandomNumbers = new List<int>();
        string[] type = new string[3];
        Random rnd = new Random();
        Random rnd2 = new Random();
        int i = 0;
        SimulationCase simulation = new SimulationCase();
        List<SimulationCase> Simulations = new List<SimulationCase>();
        NewspaperSellerModels.Systems sys = new NewspaperSellerModels.Systems();
        public Form1()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader(@"C:\Users\runmd\Desktop\College 4th yera\Simulation and modeling\Lab 5\newspapersellersimulation_students\NewspaperSellerSimulation\TestCases\TestCase1.txt"))

            {
                string line = sr.ReadToEnd();
                MessageBox.Show(line);
            }

            dataGridView1.Rows.Add(7);
            dataGridView1.Rows[0].Cells[0].Value = 40;
            dataGridView1.Rows[1].Cells[0].Value = 50;
            dataGridView1.Rows[2].Cells[0].Value = 60;
            dataGridView1.Rows[3].Cells[0].Value = 70;
            dataGridView1.Rows[4].Cells[0].Value = 80;
            dataGridView1.Rows[5].Cells[0].Value = 90;
            dataGridView1.Rows[6].Cells[0].Value = 100;

            dataGridView1.Rows[0].Cells[1].Value = 0.03;
            dataGridView1.Rows[1].Cells[1].Value = 0.05;
            dataGridView1.Rows[2].Cells[1].Value = 0.15;
            dataGridView1.Rows[3].Cells[1].Value = 0.20;
            dataGridView1.Rows[4].Cells[1].Value = 0.35;
            dataGridView1.Rows[5].Cells[1].Value = 0.15;
            dataGridView1.Rows[6].Cells[1].Value = 0.07;

            dataGridView1.Rows[0].Cells[2].Value = 0.10;
            dataGridView1.Rows[1].Cells[2].Value = 0.18;
            dataGridView1.Rows[2].Cells[2].Value = 0.40;
            dataGridView1.Rows[3].Cells[2].Value = 0.20;
            dataGridView1.Rows[4].Cells[2].Value = 0.08;
            dataGridView1.Rows[5].Cells[2].Value = 0.04;
            dataGridView1.Rows[6].Cells[2].Value = 0.00;

            dataGridView1.Rows[0].Cells[3].Value = 0.44;
            dataGridView1.Rows[1].Cells[3].Value = 0.22;
            dataGridView1.Rows[2].Cells[3].Value = 0.16;
            dataGridView1.Rows[3].Cells[3].Value = 0.12;
            dataGridView1.Rows[4].Cells[3].Value = 0.06;
            dataGridView1.Rows[5].Cells[3].Value = 0.00;
            dataGridView1.Rows[6].Cells[3].Value = 0.00;

        }



        public void GetDataFromGrid3()
        {
            int i = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;
                if (i >= 3) break;
                Day.Add(new DayTypeDistribution());
                
                Day[i++].Probability = double.Parse(row.Cells[0].Value.ToString());
            }
            Day[0].DayType = Enums.DayType.Good;
            Day[1].DayType = Enums.DayType.Fair;
            Day[2].DayType = Enums.DayType.Poor;
            MessageBox.Show(Day.Count.ToString());
        }
        public void CumDataAndRanges()
        {
            Day[0].CummProbability = Day[0].Probability;
            for (i = 0; i <=1; i++)
            {
                Day[i + 1].CummProbability += Day[i].CummProbability + Day[i + 1].Probability;

            }
            double max1 = Day[0].Probability * 100;
            Day[0].MinRange = 1;
            Day[0].MaxRange = (int)max1;
            Day[1].MinRange = Day[0].MaxRange + 1;
            double max2 = Day[1].CummProbability * 100;
            Day[1].MaxRange = (int)max2;
            Day[2].MinRange = Day[1].MaxRange + 1;
            Day[2].MaxRange = 100;
        }
        public void GenerateRandomNumbers()
        {
            dataGridView2.Rows.Add(simulation.DayNo);
            for (i = 0; i < simulation.DayNo; i++)
            {
                simulation.RandomNewsDayType = rnd.Next(1, 100);
                RandomNumbers.Add(simulation.RandomNewsDayType);
                dataGridView2.Rows[i].Cells[0].Value = i + 1;
                dataGridView2.Rows[i].Cells[1].Value = RandomNumbers[i];
                int rando = rnd2.Next(1, 100);
                simulation.RandomDemand = rnd2.Next(1, 100);
                RandomDemandNumber.Add(rando);
                dataGridView2.Rows[i].Cells[3].Value = RandomDemandNumber[i];
            }
        }
        public void PutTheTypeOfNewsPaper()
        {
            
            for (i = 0; i < RandomNumbers.Count; i++)
            {
                if (RandomNumbers[i] >= Day[0].MinRange && RandomNumbers[i] <= Day[0].MaxRange)
                {
                    simulation.NewsDayType += 0;
                    type[0] = "Good";
                    dataGridView2.Rows[i].Cells[2].Value = type[0];
                }
                if (RandomNumbers[i] >= Day[1].MinRange && RandomNumbers[i] <= Day[1].MaxRange)
                {
                    simulation.NewsDayType += 1;
                    type[1] = "Fair";
                    dataGridView2.Rows[i].Cells[2].Value = type[1];
                }
                if (RandomNumbers[i] >= Day[2].MinRange && RandomNumbers[i] <= Day[2].MaxRange)
                {
                    simulation.NewsDayType += 2;
                    type[2] = "Poor";
                    dataGridView2.Rows[i].Cells[2].Value = type[2];
                }
            }
            
        }
        public void ReadDataFromGrid3()
        {
            List<double> Good = new List<double>();
            List<double> Fair = new List<double>();
            List<double> Poor = new List<double>();
            i = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value == null)
                    break;

                DemandDist.Add(new DemandDistribution());
                Good.Add(new double());
                Fair.Add(new double());
                Poor.Add(new double());
                
                DemandDist[i].Demand = int.Parse(row.Cells[0].Value.ToString());

                Good[i] = double.Parse(row.Cells[1].Value.ToString());
                Fair[i] = double.Parse(row.Cells[2].Value.ToString());
                Poor[i] = double.Parse(row.Cells[3].Value.ToString());
                DemandDist[i].DayTypeDistributions.Add(new DayTypeDistribution());
                DemandDist[i].DayTypeDistributions.Add(new DayTypeDistribution());
                DemandDist[i].DayTypeDistributions.Add(new DayTypeDistribution());

                DemandDist[i].DayTypeDistributions[0].Probability = Good[i];
                DemandDist[i].DayTypeDistributions[1].Probability = Fair[i];
                DemandDist[i].DayTypeDistributions[2].Probability = Poor[i];
                i++;
            }
            for (i = 1; i < Good.Count - 1; i++)
            {
                
                Good[i] = Good[i - 1] + Good[i];
                Fair[i] = Fair[i - 1] + Fair[i];
                Poor[i] = Poor[i - 1] + Poor[i];
            }
            
            for (i = 0; i < Good.Count - 1; i++)
            {
                DemandGood.Add(new DayTypeDistribution());
                
                if (i == 0)
                {
                    DemandGood[i].MinRange = 1;
                    double res = Good[i] * 100;
                    DemandGood[i].MaxRange = (int)res;
                }
                else
                {
                    double res1 = (Good[i - 1] * 100) + 1;
                    DemandGood[i].MinRange = (int)res1;
                    double res2 = Good[i] * 100;
                    DemandGood[i].MaxRange = (int)res2;
                }
                if (Good[i] == 1 && Good[i + 1] == 1)
                {
                    break;
                }
             //   DemandDist[i].DayTypeDistributions.Add(new DayTypeDistribution());
               // DemandDist[i].DayTypeDistributions[0] = new DayTypeDistribution();
                DemandDist[i].DayTypeDistributions[0].DayType = Enums.DayType.Good;
                DemandDist[i].DayTypeDistributions[0].CummProbability = Good[i];

                DemandDist[i].DayTypeDistributions[0].MinRange = DemandGood[i].MinRange;
                DemandDist[i].DayTypeDistributions[0].MaxRange = DemandGood[i].MaxRange;
            }
            for (i = 0; i < Fair.Count - 1; i++)
            {
                DemandFair.Add(new DayTypeDistribution());

                if (i == 0)
                {
                    DemandFair[i].MinRange = 1;
                    double res = Fair[i] * 100;
                    DemandFair[i].MaxRange = (int)res;
                }
                else
                {
                    double res1 = (Fair[i - 1] * 100) + 1;
                    DemandFair[i].MinRange = (int)res1;
                    double res2 = Fair[i] * 100;
                    DemandFair[i].MaxRange = (int)res2;
                }
                if (Fair[i] == 1 && Fair[i + 1] == 1)
                {
                    break;
                }
               // DemandDist[i].DayTypeDistributions.Add(new DayTypeDistribution());
                //DemandDist[i].DayTypeDistributions[1] = new DayTypeDistribution();
                DemandDist[i].DayTypeDistributions[1].DayType = Enums.DayType.Fair;

                DemandDist[i].DayTypeDistributions[1].CummProbability = Fair[i];
                DemandDist[i].DayTypeDistributions[1].MinRange = DemandFair[i].MinRange;
                DemandDist[i].DayTypeDistributions[1].MaxRange = DemandFair[i].MaxRange;
            }
            for (i = 0; i < Poor.Count - 1; i++)
            {
                DemandPoor.Add(new DayTypeDistribution());
                if (i == 0)
                {
                    DemandPoor[i].MinRange = 1;
                    double res = Poor[i] * 100;
                    DemandPoor[i].MaxRange = (int)res;
                }
                else
                {
                    double res1 = (Poor[i - 1] * 100) + 1;
                    DemandPoor[i].MinRange = (int)res1;
                    double res2 = Poor[i] * 100;
                    DemandPoor[i].MaxRange = (int)res2;
                }
                if (Poor[i] == 1 && Poor[i + 1] == 1)
                {
                    break;
                }
              //  DemandDist[i].DayTypeDistributions.Add(new DayTypeDistribution());
                //DemandDist[i].DayTypeDistributions[2] = new DayTypeDistribution();
                DemandDist[i].DayTypeDistributions[2].DayType = Enums.DayType.Poor;
                DemandDist[i].DayTypeDistributions[2].CummProbability = Poor[i];
                DemandDist[i].DayTypeDistributions[2].MinRange = DemandPoor[i].MinRange;
                DemandDist[i].DayTypeDistributions[2].MaxRange = DemandPoor[i].MaxRange;
            }
        }
        public void CalculateTheDemand()
        {
            for (i = 0; i < RandomDemandNumber.Count; i++)
            {

                if (dataGridView2.Rows[i].Cells[2].Value == "Good")
                {
                    for (int j = 0; j < DemandGood.Count; j++)
                    {
                        if (RandomDemandNumber[i] >= DemandGood[j].MinRange && RandomDemandNumber[i] <= DemandGood[j].MaxRange)
                        {
                            DemandNumbers.Add(DemandDist[j].Demand);
                            dataGridView2.Rows[i].Cells[4].Value = DemandDist[j].Demand;
                        }

                    }
                }
                if (dataGridView2.Rows[i].Cells[2].Value == "Fair")
                {
                    for (int j = 0; j < DemandFair.Count; j++)
                    {
                        if (RandomDemandNumber[i] >= DemandFair[j].MinRange && RandomDemandNumber[i] <= DemandFair[j].MaxRange)
                        {
                            DemandNumbers.Add(DemandDist[j].Demand);

                            dataGridView2.Rows[i].Cells[4].Value = DemandDist[j].Demand;
                        }
                    }
                }
                if (dataGridView2.Rows[i].Cells[2].Value == "Poor")
                {
                    for (int j = 0; j < DemandPoor.Count; j++)
                    {
                        if (RandomDemandNumber[i] >= DemandPoor[j].MinRange && RandomDemandNumber[i] <= DemandPoor[j].MaxRange)
                        {
                            DemandNumbers.Add(DemandDist[j].Demand);
                            dataGridView2.Rows[i].Cells[4].Value = DemandDist[j].Demand;
                        }
                    }
                }

            }
        }
        public void RevenueAndScrap()
        {
            for (i = 0; i < DemandNumbers.Count; i++)
            {
                Simulations.Add(new SimulationCase());
                Simulations[i].DayNo = i + 1;
               // Simulations[i].
                Simulations[i].Demand = DemandNumbers[i];
                Simulations[i].SalesProfit = Math.Min(sys.NumOfNewspapers, Simulations[i].Demand) * sys.SellingPrice;
                dataGridView2.Rows[i].Cells[5].Value = Simulations[i].SalesProfit;

                if (DemandNumbers[i] > sys.NumOfNewspapers)
                {
                    Simulations[i].CalcLostProfit(DemandNumbers[i], sys.NumOfNewspapers, sys.PurchasePrice, sys.SellingPrice);
                    dataGridView2.Rows[i].Cells[6].Value = Simulations[i].LostProfit;
                    PM.DaysWithMoreDemand++;
                }
                else
                {
                    int res = sys.NumOfNewspapers - DemandNumbers[i];
                    double calc = res * sys.ScrapPrice;
                    Simulations[i].ScrapProfit = calc;
                    dataGridView2.Rows[i].Cells[7].Value = Simulations[i].ScrapProfit;
                    if (Simulations[i].ScrapProfit != 0)
                        PM.DaysWithUnsoldPapers++;
                }

            }
        }
        public void FinalCalculations()
        {
            for(int i=Simulations.Count;i<simulation.DayNo;++i)
            {
                Simulations.Add(new SimulationCase());
            }
            for (i = 0; i < simulation.DayNo; i++)
            {
                double res = Simulations[i].SalesProfit - (sys.NumOfNewspapers * sys.PurchasePrice) - (Simulations[i].LostProfit) + Simulations[i].ScrapProfit;
                Simulations[i].DailyNetProfit = res;
                dataGridView2.Rows[i].Cells[8].Value = Simulations[i].DailyNetProfit;
                PM.TotalSalesProfit += Simulations[i].SalesProfit;
                PM.TotalNetProfit += Simulations[i].DailyNetProfit;
                PM.TotalLostProfit += Simulations[i].LostProfit;
                PM.TotalScrapProfit += Simulations[i].ScrapProfit;
            }
            PM.TotalCost = simulation.DayNo * sys.NumOfNewspapers * sys.PurchasePrice;
        }
        public void Final()
        {
            GetDataFromGrid3();
            CumDataAndRanges();
            GenerateRandomNumbers();
            PutTheTypeOfNewsPaper();
            ReadDataFromGrid3();
            CalculateTheDemand();
            RevenueAndScrap();
            FinalCalculations();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            sys.NumOfNewspapers = int.Parse(textBox4.Text);
            sys.PurchasePrice = double.Parse(textBox1.Text);
            sys.SellingPrice = double.Parse(textBox2.Text);
            sys.ScrapPrice = double.Parse(textBox3.Text);
            simulation.DayNo = int.Parse(textBox7.Text);

            Final();

            textBox6.Text = PM.TotalSalesProfit.ToString();
            textBox5.Text = PM.TotalCost.ToString();
            textBox8.Text = PM.TotalLostProfit.ToString();
            textBox9.Text = PM.TotalScrapProfit.ToString();
            textBox10.Text = PM.TotalNetProfit.ToString();
            textBox11.Text = PM.DaysWithMoreDemand.ToString();
            textBox12.Text = PM.DaysWithUnsoldPapers.ToString();
            sys.SimulationCases = Simulations;
            sys.PerformanceMeasures = PM;
            sys.DemandDistributions = DemandDist;
            sys.UnitProfit = sys.SellingPrice - sys.PurchasePrice;
            sys.DayTypeDistributions = Day;
            //string res = TestingManager.Test(sys, Constants.FileNames.TestCase1);
            //MessageBox.Show(res);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

