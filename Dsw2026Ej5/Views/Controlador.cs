using Dsw2026Ej5.Data;
using Dsw2026Ej5.Domain;

namespace Dsw2026Ej5.Views;

public class Controlador
{
    public static List<VehiculoViewModel> GetVehiculos()
    {
        List<VehiculoViewModel> vehiculos = new List<VehiculoViewModel>();
        foreach (Vehiculo vehiculo in Persistencia.GetVehiculos())
        {
            vehiculos.Add(new VehiculoViewModel(vehiculo));
        }
        return vehiculos;
    }

    public static (double, double) CalcularConsumos(List<VehiculoViewModel> vehiculosVM)
    {
        double consumoElectricos = 0;
        double consumoCombustible = 0;

        foreach (var vm in vehiculosVM)
        {
            Vehiculo vehiculo = Persistencia.GetVehiculo(vm.GetPatente());

            if (vehiculo != null)
            {
                double consumo = vehiculo.CalcularConsumo(vm.GetKmARecorrer());

                if (vehiculo.EsDe(VehiculoTipo.Electrico))
                {
                    consumoElectricos += consumo;
                }
                else if (vehiculo.EsDe(VehiculoTipo.Combustible))
                {
                    consumoCombustible += consumo;
                }
            }
        }

        return (consumoElectricos, consumoCombustible);
    }
}
