namespace SquareEquationLib;

public class SquareEquation
{
    public static double[] Solve(double a, double b, double c)
    {
        double eps = 0.000000001;
        double[] answer;

        if (Math.Abs(a) < eps)
        {
            throw new System.ArgumentException();
        }

        foreach (double x in new double[] { a, b, c })
        {
            if (double.IsNaN(x) || double.IsInfinity(x))
            {
                throw new System.ArgumentException();
            }
        }

        double d = Math.Pow(b, 2) - 4 * a * c;

        if (d < 0 && !(Math.Abs(d) < eps))
        {
            answer = new double[0];

        }
        else if (d < eps)
        {
            answer = new double[1];
            if(b!=0){
                answer[0] = -(b + Math.Sign(b) * Math.Sqrt(d)) / 2;
            }
            else{
                answer[0] = -(b +  Math.Sqrt(d)) / 2;
            }

        }
        else
        {
            answer = new double[2];
           if(b!=0){
                answer[0] = -(b + Math.Sign(b) * Math.Sqrt(d)) / 2;
            }
            else{
                answer[0] = -(b +  Math.Sqrt(d)) / 2;
            }
            answer[1] = c / answer[0];

        }

        return answer;

    }
}
