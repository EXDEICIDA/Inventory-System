using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace ModernDesign.MVVM.ViewModel
{
    internal class VisualsViewModel
    {
        public PlotModel PieChartModel { get; private set; }
        public PlotModel BarChartModel { get; private set; }



        public VisualsViewModel()
        {
            // Fetch categories and their quantities from SQLite database
            var categoryDistribution = FetchCategoryDistribution();

          
            CreatePieChart(categoryDistribution);

          
            CreateBarChart(categoryDistribution);
        }

        private Dictionary<string, int> FetchCategoryDistribution()
        {
            Dictionary<string, int> categoryDistribution = new Dictionary<string, int>();

            string dbFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataLayer", "InventorySystem.db");
            string connectionString = $"Data Source={dbFilePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Category, COUNT(*) AS Quantity FROM Products GROUP BY Category";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader["Category"].ToString(); // Changed from "CategoryName" to "Category"
                            int quantity = Convert.ToInt32(reader["Quantity"]); // Parse method replaced with Convert.ToInt32
                            categoryDistribution.Add(categoryName, quantity);
                        }
                    }
                }
            }

            return categoryDistribution;
        }

        private void CreatePieChart(Dictionary<string, int> categoryDistribution)
        {
            PieChartModel = new PlotModel { Title = "Category Distribution" };

            var pieSeries = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.7, AngleSpan = 360, StartAngle = 0 };

            // Sorting categories by quantity
            var sortedCategories = categoryDistribution.OrderByDescending(x => x.Value).ToList();

            // Calculating total quantity
            int totalQuantity = sortedCategories.Sum(x => x.Value);

            // Adding categories with quantity >= 5% of total to the pie chart
            foreach (var category in sortedCategories)
            {
                double percentage = (double)category.Value / totalQuantity * 100;
                if (percentage >= 5)
                {
                    pieSeries.Slices.Add(new PieSlice(category.Key, category.Value) { IsExploded = false });
                }
            }

            // Calculate total quantity for "Other" category
            int otherQuantity = sortedCategories.Where(x => (double)x.Value / totalQuantity * 100 < 5).Sum(x => x.Value);

            // Add "Other" category to the pie chart
            pieSeries.Slices.Add(new PieSlice("Other", otherQuantity) { IsExploded = false });

            PieChartModel.Series.Add(pieSeries);
        }


         //bar chart method is creted here for us to use 

        private void CreateBarChart(Dictionary<string, int> categoryDistribution)
        {
            BarChartModel = new PlotModel { Title = "Quantity Distribution" };

           
            var barSeries = new BarSeries { StrokeColor = OxyColors.Black, StrokeThickness = 1 };

            var sortedCategories = categoryDistribution.OrderByDescending(x => x.Value).ToList();

            // Initialize a list to store the displayed categories
            List<string> displayedCategories = new List<string>();

            // Add categories with quantity >= 5% of total to the bar chart -->this will help us reduce the useless infos on the charts 
            foreach (var category in sortedCategories)
            {
                double percentage = (double)category.Value / categoryDistribution.Values.Sum() * 100;
                if (percentage >= 5)
                {
                    barSeries.Items.Add(new BarItem { Value = category.Value });

                    displayedCategories.Add(category.Key);
                }
            }

            // Add the "Other" category with its total quantity
            int otherQuantity = sortedCategories.Where(x => (double)x.Value / categoryDistribution.Values.Sum() * 100 < 5).Sum(x => x.Value);
            if (otherQuantity > 0)
            {
                // Add the bar item for "Other"
                barSeries.Items.Add(new BarItem { Value = otherQuantity });

                // Add "Other" to the displayed categories list
                displayedCategories.Add("Other");
            }

            // Add the bar series to the plot model
            BarChartModel.Series.Add(barSeries);

            // Add labels to the axis here
            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "CategoryAxis",
                LabelField = "Category"
            };
            BarChartModel.Axes.Add(categoryAxis);

            // Add labels to the bars
            for (int i = 0; i < barSeries.Items.Count; i++)
            {
                var annotation = new TextAnnotation
                {
                    Text = $"{barSeries.Items[i].Value}",
                    TextPosition = new DataPoint(i, barSeries.Items[i].Value),
                    TextHorizontalAlignment = HorizontalAlignment.Center,
                    TextVerticalAlignment = VerticalAlignment.Middle,
                    FontWeight = FontWeights.Bold,
                    FontSize = 12,
                    Padding = new OxyThickness(4) // Corrected assignment here
                };
                BarChartModel.Annotations.Add(annotation);
            }

            // Assign categories to axis labels
            categoryAxis.Labels.AddRange(displayedCategories);
        }




    }
}
