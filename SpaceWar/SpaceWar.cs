
namespace SpaceWarLib
{
    public class SpaceMove
    {
        public static double[] Movement(double[] SpaceShipCoordinates,double[] SpaceShipWay, bool opportunityMove ){

        if (opportunityMove == false) {
            throw new System.Exception();
        }
        
        foreach(double x in SpaceShipCoordinates.Concat(SpaceShipWay).ToArray()){
            if (double.IsNaN(x) | double.IsInfinity(x)){
             throw new System.Exception();
            }    
        }
        

        for (int i = 0; i < SpaceShipCoordinates.Length;i++){
            SpaceShipCoordinates[i] += SpaceShipWay[i];
        }

        return  SpaceShipCoordinates;
        }

    }
}
