
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

    public class ShipFuel{
        public static double fuelCalculate(double fuelBalance ,double fuelNeed){
            if(fuelNeed>fuelBalance){
                throw new System.Exception();
            }
            return fuelBalance-fuelNeed;
        }
    }

    public class ShipRotate{
        public static double Rotation (double degX,double instantSpeed,bool rotate){
            foreach(double x in new double[]{degX, instantSpeed})
        {
            
            if (double.IsNaN(x) | double.IsInfinity(x)) {
                throw new System.Exception();
            }
        }
        if (rotate == false) {
            throw new System.Exception();
        }

        return degX + instantSpeed;
        }
    }

}
