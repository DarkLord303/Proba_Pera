namespace Proba__2
{
    internal class Program
    {
        public class Circle 
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double R { get; set; }
            public Circle(double x, double y, double r)
            {
                X = x;
                Y = y;
                R = r;
            }
            public static List<Circle> CreateCircleList()
            {
                return new List<Circle>
                {
                    new Circle(0,0,3),
                    new Circle(4,0,3),
                    new Circle(4,2,3),
                    new Circle(8,0,3),
                    new Circle(12,0,3),
                    new Circle(100,2,2),
                    new Circle(20,5,5),
                    new Circle(99,4,2),
                };
            }
            public static bool Intersects(Circle A,Circle B)
            {
                //double dist = Math.Pow(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2), 1 / 2);
                double dist = Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));

                return dist <= A.R + B.R;
            }

        }
        static void Main(string[] args)
        {


            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Use the predefined list of circles");
            Console.WriteLine("2. Enter your own circles");
            string choice = Console.ReadLine();

            List<Circle> circles = new List<Circle>();

            if (choice == "1")
            {
                circles = Circle.CreateCircleList();
            }
            else if (choice == "2")
            {
                Console.WriteLine("How many circles would you like to enter?");
                int numberOfCircles = int.Parse(Console.ReadLine());

                for (int i = 0; i < numberOfCircles; i++)
                {
                    Console.WriteLine($"Enter details for circle {i + 1}:");

                    Console.Write("Enter X: ");
                    double x = double.Parse(Console.ReadLine());

                    Console.Write("Enter Y: ");
                    double y = double.Parse(Console.ReadLine());

                    Console.Write("Enter R: ");
                    double r = double.Parse(Console.ReadLine());

                    circles.Add(new Circle(x, y, r));
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            //List<Circle> circles = Circle.CreateCircleList();
            List<Circle>[] CircleGraph = new List<Circle>[circles.Count];
            for(int i = 0; i < circles.Count; i++) { CircleGraph[i] = new List<Circle>(); }
            for (int i = 0; i < circles.Count; i++) 
            {
                CircleGraph[i].Add(circles[i]);
            }
            for (int i = 0; i < circles.Count; i++) 
            {
                for(int j=i+1; j<circles.Count; j++) 
                {
                    if (Circle.Intersects(circles[i], circles[j])==true && circles[i] != circles[j]) 
                    {
                        CircleGraph[i].Add(circles[j]);
                    }
                }
            }

            for (int i = 0; i < CircleGraph.Length; i++)
            {
                for (int j = i + 1; j < CircleGraph.Length; j++)
                {
                    bool ShouldMergeCircles = false;


                    foreach (var left_circle in CircleGraph[i])
                    {
                        foreach (var right_circle in CircleGraph[j])
                        {
                            if (Circle.Intersects(left_circle, right_circle))
                            {
                                ShouldMergeCircles = true;
                                break;
                            }
                        }
                        if (ShouldMergeCircles) break;
                    }

                    
                    if (ShouldMergeCircles)
                    {
                        HashSet<Circle> MergedCircles = new HashSet<Circle>(CircleGraph[i]); 
                        MergedCircles.UnionWith(CircleGraph[j]); 

                        CircleGraph[i] = MergedCircles.ToList(); 
                        CircleGraph[j].Clear(); 
                    }
                }
            }

            
            CircleGraph = CircleGraph.Where(g => g.Count > 0).ToArray();
            foreach (var i in CircleGraph) 
            {
                foreach(var j in i) 
                {
                    Console.WriteLine($"x={j.X} y={j.Y} r={j.R}");
                }
                Console.WriteLine("----------------------");
            }
        }
    }
}