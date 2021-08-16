using System;

namespace Projekt1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Choose your shape : \n1.Press 1 if your shape is rectangle\n2.Press 2 if your shape is square\n3.Press 3 if your shape is triangle\n4.Press 4 if your shape is elipse\n");


            string number = Console.ReadLine();

            if (number == "1") {

                Rectangle myRectangle = new Rectangle(10, 5);
                Rectangle yourRectangle = new Rectangle();

                Console.WriteLine("Enter height of Rectangle: ");
                string heihgt = Console.ReadLine();
                yourRectangle.Height = Double.Parse(heihgt);

                Console.WriteLine("Enter width of Rectangle: ");
                string width = Console.ReadLine();
                yourRectangle.Width = Double.Parse(width);

                AreaComapration(myRectangle, yourRectangle);
            }

            if (number == "2") {  

                Square mySquare = new Square(5);

                Console.WriteLine("Area od square is " +mySquare.Area());
                Console.WriteLine("Perimeter of square is " + mySquare.Perimeter());
            }

            if (number == "3")
            {
                Console.WriteLine("Enter first side od triangle: ");
                string firstSide = Console.ReadLine();
                Console.WriteLine("Enter second side of triangle: ");
                string secondSide = Console.ReadLine();
                Console.WriteLine("Enter third side of triangle: ");
                string thirdSide = Console.ReadLine();

                if(firstSide==secondSide && secondSide == thirdSide)
                {
                    Triangle myTriangle = new EquilateralTriangle();
                    myTriangle.PrintTypeOfTriangle();
               
                }
                else if(firstSide!=secondSide && firstSide!=thirdSide && thirdSide != secondSide)
                {
                    Triangle myTriangle = new ScaleneTriangle();
                    myTriangle.PrintTypeOfTriangle();
                }
                else
                {
                    Triangle myTriangle = new IsoscelesTriangle();
                    myTriangle.PrintTypeOfTriangle();
                }  

            }

            if (number == "4")
            {
                Circle circle = new Circle();
                Console.WriteLine("Enter radius:");
                string radius = Console.ReadLine();
                circle.Radius = Double.Parse(radius);

                Console.WriteLine("Area of circle is " + circle.Area() + ", perimeter of circle is " + circle.Perimetar());

            }

        }

        static void AreaComapration(Rectangle myRectangle, Rectangle yourRectangle)
        {
            if (myRectangle.Area() > yourRectangle.Area())
            {
                Console.WriteLine("My rectangle has bigger area than your");
            }
            else if (myRectangle.Area() < yourRectangle.Area())
            {
                Console.WriteLine("Your rectangle has bigger area than my");
            }
            else
            {
                Console.WriteLine("Your and my ractangle have the same area");
            }
        }
    }

    //ENKAPSULACIJA, NASLJEĐIVANJE, SRP,DRY

   public class Rectangle
    {  
        public double Width
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }
        public double Area()
        {
            return Width * Height;
        }

        public double Perimeter()
        {
            return (2 * Width) +( 2* Height);
        }

        public Rectangle(double mWidth,double mHeight) 
        {
            Width = mWidth;
            Height = mHeight;
        }

        public Rectangle()
        {

        }  
    }

    public class Square : Rectangle
    {
        
        public double Side
        {
            get { return Width; }
            set { Width = Height = value; }
        }

       public Square(double mSide)
        {
            Side = mSide;
        }

    }

    //POLIMORFIZAM

    public class Triangle
    {
        public virtual void PrintTypeOfTriangle()
        {
            Console.WriteLine("It is a triangle!");
        }
    }

    class EquilateralTriangle : Triangle
    {
        public override void PrintTypeOfTriangle()
        {
            Console.WriteLine("It is a equilateral triangle!");
        }
    }

    class IsoscelesTriangle : Triangle
    {
        public override void PrintTypeOfTriangle()
        {
            Console.WriteLine("It is a isosceles triangle!");
        }
    }

    class ScaleneTriangle : Triangle
    {
        public override void PrintTypeOfTriangle()
        {
            Console.WriteLine("It is a scalene triangle!");
        }
    }

    //ABSTRAKTNA KLASA

    abstract class CircleShape
    {
        public abstract double Perimetar();
        public abstract double Area();
    }

    class Circle:CircleShape
    {

        private double radius;

        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        public override double Perimetar()
        {
            return 2 * 3.14 * radius;
        }

        public override double Area()
        {
            return radius * radius * 3.14;
        }
    }

    //INTERFACE

    interface IShape
    {
        double Perimetar();
    }

    class Rhombus : IShape
    {

        private double side;

        public double Side
        {
            get { return side; }
            set { side = value; }
        }
        public double Perimetar()
        {
            return 4 * side;
        }
    }

    class Trapeze : IShape
    {
        

        public double Width
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }

        public double Perimetar()
        {
            return (Height * 2) + (Width * 2);
        }
    }

}
