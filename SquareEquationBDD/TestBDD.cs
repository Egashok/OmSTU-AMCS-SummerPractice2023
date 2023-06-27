namespace SquareEquationBDD;


using TechTalk.SpecFlow;
using SquareEquationLib;


[Binding]
public class TestBDD
{
    private double a, b, c;
    private double[] action = new double[0]; 
    private Exception actualException = new Exception();

	[Given(@"^Квадратное уравнение с коэффициентами \((.*), (.*), (.*)\)")]
    public void КвадратноеУравнение(string p0, string p1, string p2)
    {
        string[] numb = new string[]{p0, p1, p2};
        double[] numbDouble = new double[numb.Length];
        for (int i=0; i < numb.Length; i++){
            if(numb[i] == "NaN"){
                numbDouble[i] = double.NaN;
            }
            else if(numb[i] == "Double.NegativeInfinity"){
                numbDouble[i] = double.NegativeInfinity;
            }
            else if(numb[i] == "Double.PositiveInfinity"){
                numbDouble[i] = double.PositiveInfinity;
            }
            else{
                numbDouble[i] = double.Parse(numb[i]);
            }
        } 
        a = numbDouble[0]; b = numbDouble[1]; c = numbDouble[2];
    }

    [When(@"^вычисляются корни квадратного уравнения")]
    public void НахождениеКорней(){
        try{
            action = SquareEquation.Solve(a, b, c);
        }
        catch(Exception e){
            actualException = e;
        }
    }

    [Then(@"квадратное уравнение имеет два корня \((.*), (.*)\) кратности один")]
    public void ТоКвадратноеУравнениеИмеетДваКорняКратностиОдин(double r1, double r2)
    {
        double[] expected = new double[]{r1, r2};

        Array.Sort(expected); Array.Sort(action);
        for (int i = 0; i < expected.Length;i++){
                Assert.Equal(expected[i], action[i], 6);
            }
    }

    [Then(@"квадратное уравнение имеет один корень (.*) кратности два")]
    public void ТоКвадратноеУравнениеИмеетОдинКореньКратностиДва(double expectedRoot)
    {
        double[] expected = new double[]{expectedRoot};

        Array.Sort(expected); Array.Sort(action);
        for (int i = 0; i < expected.Length;i++){
                Assert.Equal(expected[i], action[i], 6);
            }
    }

    [Then(@"множество корней квадратного уравнения пустое")]
    public void ТоКвадратноеУравнениеНеИмеетКорней()
    {
        Assert.Empty(action);
    }

    [Then(@"выбрасывается исключение ArgumentException")]
    public void ТоВыбрасываетсяИсключениеArgumentException()
    {
        Assert.ThrowsAsync<ArgumentException>(() => throw actualException);
    }
}