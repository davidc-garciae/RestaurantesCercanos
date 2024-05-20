using GeoCoordinatePortable;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace SpecFlowRestaurantes.specs.StepDefinitions
{
        

        [Binding]
    public sealed class DistanciaStepDefinitions
    {

        private readonly string connectionString = "Server=MANGELPV;Database=Proyecto;User Id=testUser3;Password=12345;";
        private DataTable dataTable = new DataTable();

        private void CargarDatosDesdeBaseDeDatos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM worldcitiesproject";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        dataTable.Clear();
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos desde la base de datos: " + ex.Message);
                }
            }
        }

        private void OrdenarInsercion()
        {
            int n = dataTable.Rows.Count;
            DataTable dataTableOrdenado = dataTable.Clone();

            for (int i = 0; i < n; i++)
            {
                DataRow filaActual = dataTable.Rows[i];
                double distanciaActual = Convert.ToDouble(filaActual["distancia"]);
                int j = i - 1;

                while (j >= 0 && Convert.ToDouble(dataTableOrdenado.Rows[j]["distancia"]) > distanciaActual)
                {
                    j--;
                }

                DataRow nuevaFila = dataTableOrdenado.NewRow();
                nuevaFila.ItemArray = filaActual.ItemArray;
                dataTableOrdenado.Rows.InsertAt(nuevaFila, j + 1);
            }

            dataTable = dataTableOrdenado;
        }

        public void CalcularDistancias(GeoCoordinate currentCoord)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                double latDB = Convert.ToDouble(row["lat"]);
                double lngDB = Convert.ToDouble(row["lng"]);

                var dbCoord = new GeoCoordinate(latDB, lngDB);

                double distancia = currentCoord.GetDistanceTo(dbCoord);

                if (!dataTable.Columns.Contains("distancia"))
                {
                    dataTable.Columns.Add("distancia", typeof(double));
                }

                row["distancia"] = distancia;
            }
        }

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly GeoCoordinate currentCoord = new GeoCoordinate();
        private double num_result;
        private string str_result = "";



        [Given("the latitude is (.*)")]
        public void GivenTheLatitudeIs(double number)
        {
            CargarDatosDesdeBaseDeDatos();
            currentCoord.Latitude = number;
        }

        [Given("the longitude is (.*)")]
        public void GivenTheLongitudeIs(double number)
        {
            currentCoord.Longitude = number;   
        }

        [When("the distances are calculated")]
        public void WhenTheDistancesAreCalculated()
        {
            CalcularDistancias(currentCoord);
        }

        [When("the distances are sorted")]
        public void WhenTheDistancesAreSorted()
        {
            OrdenarInsercion();
        }

        [Then("the closest one should be (.*)")]
        public void ThenTheClosestOneShouldBe(double result)
        {
            num_result = Convert.ToDouble(dataTable.Rows[0][6]);
            num_result.Should().Be(result);
        }

        [Then("the closest city should be (.*)")]
        public void ThenTheClosestCityShouldBe(string result)
        {
            str_result = (string)dataTable.Rows[0][0];
            str_result.Should().Be(result);
        }

        [Then("the closest country should be (.*)")]
        public void ThenTheClosestCountryShouldBe(string result)
        {
            str_result = (string)dataTable.Rows[0][3];
            str_result.Should().Be(result);
        }

        [Then("the fifth closest should be (.*)")]
        public void ThenTheFifthClosestShouldBe(double result)
        {
            num_result = Convert.ToDouble(dataTable.Rows[4][6]);
            num_result.Should().Be(result);
        }
    }
}
