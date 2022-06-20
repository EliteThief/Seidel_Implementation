using Seidel_Implementation;
using System.Linq;
using System.Numerics;
using System.Text;

List<double> Seidel(List<List<double>> A, List<double> b, List<double> c, List<string> condition) 
{
    var helper = A.Select((z => z.FindAllIndex(x => x!=0))).ToList();
    var isWorking = helper.SelectMany(x => x).Distinct().ToList();

    if( isWorking.Count == 1)
    {
         SolveOneArgumentEquation(A, b, c);
    }
    else if(A.Max(x => x.Count ) == b.Count)
    {
       return SolveGauss(A, b).ToList();
    }
    int randomConstraint = condition.IndexOf("<=");
    var result = Seidel(A.Where((x,index) => index != randomConstraint).ToList(),
            b.Where((x, index) => index != randomConstraint).ToList(),
            c,
            condition.Where((x, index) => index != randomConstraint).ToList());

    if(Satisfy(result, A[randomConstraint], b[randomConstraint], condition[randomConstraint]))
    {
        Console.WriteLine("Satysfy");
        return result;
    }
    else
    {
        Console.WriteLine("FUCK IT!");
        condition[randomConstraint] = "=";
        return Seidel(A, b, c, condition);
    }
}

bool Satisfy(List<double> result, List<double> equation , double value , string condition)
{
   if(condition == "=")
    {
        double solution = 0;
        for(int i=0; i < result.Count;i++)
        {
            solution += result[i] * equation[i];
        }
        return solution == value;
    }
    else if(condition == "<=")
    {
        double solution = 0;
        for (int i = 0; i < result.Count; i++)
        {
            solution += result[i] * equation[i];
        }
        return solution <= value;
    }
    else
    {
        double solution = 0;
        for (int i = 0; i < result.Count; i++)
        {
            solution += result[i] * equation[i];
        }
        return solution >= value;
    }
}

double[] SolveGauss(List<List<double>> a, List<double> b)
{
    for (int i = 0; i < a.Count; i++)
    {
        a[i].Add(b[i]);
    }
    var result = a.Select(x => x.ToArray()).ToArray();
    return Gauss.SolveLinearEquations(result);

}

void SolveOneArgumentEquation(List<List<double>> a, List<double> b, List<double> c)
{
    for(int i = 0; i < a.Count; i++)
    {
        b[i]=(b[i]/a[i][0]);
    }
    var solution = new List<double>();
    foreach(var digit in b)
    {
        double result = 0;
        foreach(var argument in c)
        {
            result += digit * argument;
        }
        solution.Add(result);
    }
    Console.WriteLine(solution.Max());
}


List<List<double>> equations = new List<List<double>>()
{
   
   new List<double>(){4,-3},
   new List<double>(){1,2},

};
List<string> condition = new List<string>() { "<=", "<=" };
List<double> values = new List<double>() {2,3};
List<double> objectiveFunction = new List<double>() { 2,1 };

var result = Seidel(equations, values, objectiveFunction, condition);
foreach(var item in result)
{
    Console.WriteLine(item);
}
