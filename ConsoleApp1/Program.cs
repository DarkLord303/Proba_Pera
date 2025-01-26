using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
    internal class Program
    {
        public class Circle
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int R { get; set; }
            public string Name { get; set; }
            public Circle(int x, int y, int r, string name) 
            {
                X = x;
                Y = y;
                R = r;
                Name = name;
                
            }
            public static List<Circle> CircleMasive() 
            {
                return new List<Circle>
                {
                    new Circle(1,6,1,"A"),
                    new Circle(2,5,3,"B"),
                    new Circle(3,4,5,"C"),
                    new Circle(4,3,7,"D"),
                    new Circle(5,2,9,"E"),
                    new Circle(6,1,11,"F"),
                };
            }

            public static List<List<Circle>> new_circles = new List<List<Circle>>();

            public static int n = 0;

            public static void Distance(Circle A,Circle B) //відстань між точкою A і B
            {
                double AB = Math.Pow(Math.Pow(A.X - B.X,2)+ Math.Pow(A.Y - B.Y, 2),1/2);
                if(A.R +B.R>=AB)
                {
                    if (new_circles.Count <= n)
                    {
                        new_circles.Add(new List<Circle>()); // Створюємо новий список
                    }
                    new_circles[n].Add(A);
                    new_circles[n].Add(B);
                    n++;
                }
            }
            public static void FinalDistance(Circle A, Circle B, List<List<Circle>> new_circles) //відстань між точкою A і B
            {
                double AB = Math.Pow(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2), 1 / 2);
                if (A.R + B.R >= AB)
                {
                    if (new_circles.Count <= n)
                    {
                        new_circles.Add(new List<Circle>()); // Створюємо новий список
                    }
                    new_circles[n].Add(A);
                    new_circles[n].Add(B);
                    n++;
                }
            }
            public static void Step1(List<Circle> c) 
            {
                int lengh=c.Count;
                for (int i = 0; i < lengh; i++) 
                {
                    for (int j = 0; j < lengh; j++) 
                    {
                        Circle.Distance(c[i],c[j]);
                    }
                }
                //return new Circle(0, 0, 0, "0");
            }
            public static void Step2(List<Circle> c)
            {
                List<List<Circle>> new_circles2 = new_circles;
                int new_n = n;
                n = 0;
                foreach (var i in new_circles) 
                {
                    foreach (Circle j in i) 
                    {
                        Circle.FinalDistance(j, c[n], new_circles2);
                    }
                }
                //for (int i = 0; i <new_n; i++) 
                //{
                //    //var L = new_circles[n];
                //    foreach (var j in new_circles[n]) 
                //    {
                //        Circle.FinalDistance(j, c[i],new_circles2);
                //    }
                //    n++;
                //}
                int max = 0;
                int pos = 0;
                int l = new_circles2.Count;
                for (int i = 0; i < l; i++) 
                {
                    if (max<new_circles2[i].Count) { max = new_circles2[i].Count; pos = i; }
                }
                Console.WriteLine($"Lengh = {max}");
                foreach (var i in new_circles2[pos]) 
                {
                    //Console.Write(i.Name+',');
                    Console.WriteLine(i.Name);
                }
            }


        }
        static void Main(string[] args)
        {
            List<Circle> c=Circle.CircleMasive();
            Circle.Step1(c);
            Circle.Step2(c);
        }
    }
}