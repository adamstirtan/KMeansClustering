namespace KMeansClustering
{
    internal class Program
    {
        public class Point
        {
            public double X { get; set; }

            public double Y { get; set; }

            public int Cluster { get; set; }
        }

        static readonly Random random = new();

        static void Main(string[] args)
        {
            List<Point> dataPoints = GetFlowerData();

            int k = 3; // Number of clusters
            List<Point> centroids = InitializeRandomCentroids(dataPoints, k);

            int maxIterations = 100;
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                // Assign data points to the nearest cluster
                foreach (Point dataPoint in dataPoints)
                {
                    double minDistance = double.MaxValue;
                    int closestCluster = -1;

                    for (int i = 0; i < k; i++)
                    {
                        double distance = CalculateDistance(dataPoint, centroids[i]);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestCluster = i;
                        }
                    }

                    dataPoint.Cluster = closestCluster;
                }

                // Update centroids
                for (int i = 0; i < k; i++)
                {
                    List<Point> clusterPoints = dataPoints.Where(p => p.Cluster == i).ToList();
                    if (clusterPoints.Count > 0)
                    {
                        double meanX = clusterPoints.Select(p => p.X).Average();
                        double meanY = clusterPoints.Select(p => p.Y).Average();
                        centroids[i] = new Point { X = meanX, Y = meanY };
                    }
                }
            }

            // Print the final clusters
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine($"Cluster {i}:");
                foreach (Point dataPoint in dataPoints.Where(p => p.Cluster == i))
                {
                    Console.WriteLine($"X: {dataPoint.X}, Y: {dataPoint.Y}");
                }
                Console.WriteLine();
            }
        }

        private static List<Point> GetFlowerData()
        {
            return new List<Point>()
            {
                new Point { X = 0, Y = 5 },
                new Point { X = 0, Y = 6 },
                new Point { X = 1, Y = 3 },
                new Point { X = 1, Y = 3 },
                new Point { X = 1, Y = 6 },
                new Point { X = 1, Y = 8 },
                new Point { X = 2, Y = 3 },
                new Point { X = 2, Y = 7 },
                new Point { X = 2, Y = 8 }
            };
        }

        static List<Point> GenerateRandomData(int count)
        {
            List<Point> dataPoints = new();

            for (int i = 0; i < count; i++)
            {
                dataPoints.Add(new Point
                {
                    X = random.NextDouble() * 100,
                    Y = random.NextDouble() * 100,
                    Cluster = -1
                });
            }
            return dataPoints;
        }

        static List<Point> InitializeRandomCentroids(List<Point> dataPoints, int k)
        {
            List<Point> centroids = new();

            for (int i = 0; i < k; i++)
            {
                int randomIndex = random.Next(dataPoints.Count);

                centroids.Add(new Point
                {
                    X = dataPoints[randomIndex].X,
                    Y = dataPoints[randomIndex].Y,
                    Cluster = i
                });
            }
            return centroids;
        }

        static double CalculateDistance(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}