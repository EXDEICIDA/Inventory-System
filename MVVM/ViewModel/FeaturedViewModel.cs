using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using ModernDesign.MVVM.ViewModel; // Ensure this is added if not already

namespace ModernDesign.MVVM.ViewModel
{
    public class FeaturedViewModel
    {
        public PlotModel PieChartModel { get; private set; }

        public FeaturedViewModel(IEnumerable<Product> products)
        {
            CreatePieChart(products.ToList());
        }

        private void CreatePieChart(List<Product> products)
        {
            PieChartModel = new PlotModel { Title = "Category Distribution" };

            var categoryDistribution = products
                .GroupBy(p => p.Category)
                .Select(group => new { Category = group.Key, Count = group.Count() })
                .ToList();

            var pieSeries = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            foreach (var category in categoryDistribution)
            {
                pieSeries.Slices.Add(new PieSlice(category.Category, category.Count) { IsExploded = false });
            }

            PieChartModel.Series.Add(pieSeries);
        }
    }
}
