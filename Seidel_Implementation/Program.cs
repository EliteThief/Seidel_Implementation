using Seidel_Implementation;
using System.Linq;
using System.Numerics;
using System.Text;
int i = 0;
List<List<double>> final = new List<List<double>>();
List<double> Seidel(List<List<double>> A, List<double> b, List<double> c, List<string> condition) 
{
    Console.WriteLine($"Iteration {i}");
    i++;
    var helper = A.Select((z => z.FindAllIndex(x => x!=0))).ToList();
    var isWorking = helper.SelectMany(x => x).Distinct().ToList();

    if( isWorking.Count == 1)
    {
        Console.WriteLine("LINIOWA!");
        SolveOneArgumentEquation(A, b, c);
    }
    else if(A.Max(x => x.Count ) == b.Count)
    {
        Console.WriteLine("GALSS!");
        return SolveGauss(A, b).ToList();
    }
    int randomConstraint = condition.IndexOf("<=");
    var result = Seidel(A.Where((x,index) => index != randomConstraint).ToList(),
            b.Where((x, index) => index != randomConstraint).ToList(),
            c,
            condition.Where((x, index) => index != randomConstraint).ToList());
    if(Satisfy(result, A[randomConstraint], b[randomConstraint], condition[randomConstraint]))
    {
        Console.WriteLine("SATYSFAKSZYN!");
        final.Add(result);
        return result;
    }
    else
    {
        Console.WriteLine("NIE SATYSFAKSZYN!");
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

        return solution <= value;
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
    var solution = Gauss.SolveLinearEquations(result);
    for (int i = 0; i < a.Count; i++)
    {
        a[i].Remove(b[i]);
    }
    return solution;

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
   new List<double>(){-1,0},
   new List<double>(){0, -1 },
   new List<double>(){-3,1},
   new List<double>(){2,1},
   new List<double>(){6,2},
   new List<double>(){-2,-2},
   new List<double>(){-2,1}
};
List<string> condition = new List<string>() { "<=", "<=", "<=", "<=","<=", "<=", "<="
};
List<double> values = new List<double>() {0,0,4,4,24,-3,0
};
List<double> objectiveFunction = new List<double>() { -1, 1};

var result = Seidel(equations, values, objectiveFunction, condition);
//foreach(var item in result)
//{
//    Console.WriteLine(item);
//}
if(final.Count()==0)
{
    if(objectiveFunction[1]>0)
    {
        Console.WriteLine("Infinity+");
    }
    else
    {
        Console.WriteLine("0 0");
    }
}
else
{
    foreach (var item in final)
    {
        foreach(var equation in item)
        {
            Console.Write(equation+" ");
        }
        Console.WriteLine("");
    }
}
