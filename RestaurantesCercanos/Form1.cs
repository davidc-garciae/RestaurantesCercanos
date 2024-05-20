using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using GeoCoordinatePortable;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace RestaurantesCercanos
{
    public partial class Form1 : Form
    {
        GMarkerGoogle marker;
        GMapOverlay overlay;
        double LatInicial = 6.25184;
        double LogInicial = -75.56359;
        private readonly string connectionString = "Server=DAVIDGARCIA;Database=Proyecto;User Id=testUser3;Password=12345;";
        private readonly DataTable dataTable = new DataTable();
        private Stopwatch stopwatch;


        public Form1()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            dataGrid.DataSource = dataTable;
            Color backgroundColor = ColorTranslator.FromHtml("#323232");
            dataGrid.DefaultCellStyle.BackColor = backgroundColor;
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = backgroundColor;
            dataGrid.DefaultCellStyle.ForeColor = Color.White;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CargarDatosDesdeBaseDeDatos();
        }

        private void CargarDatosDesdeBaseDeDatos()
        {
            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abre la conexión
                    connection.Open();

                    // Consulta SQL para seleccionar todos los datos de la tabla "merged_data"
                    string query = "SELECT * FROM worldcitiesproject";

                    // Crea un adaptador de datos para ejecutar la consulta y llenar el DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Limpia los datos existentes en el DataTable
                        dataTable.Clear();

                        // Llena el DataTable con los datos de la consulta
                        adapter.Fill(dataTable);
                    }

                    // Asigna el DataTable como la fuente de datos del DataGridView
                    dataGrid.DataSource = dataTable;
                    dataGrid.Columns[1].Visible = false;
                    dataGrid.Columns[2].Visible = false;
                    dataGrid.Columns[4].Visible = false;
                    dataGrid.Columns[5].Visible = false;
                    dataGrid.Columns[6].Visible = false;

                }
                catch (Exception ex)
                {
                    // Maneja cualquier error que pueda ocurrir durante la conexión o la consulta
                    MessageBox.Show("Error al cargar los datos desde la base de datos: " + ex.Message);
                }
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LogInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            overlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LogInicial), GMarkerGoogleType.blue);

            overlay.Markers.Add(marker);

            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicación: \n Latitud {0} \n Longitud: {1}", LatInicial, LogInicial);

            gMapControl1.Overlays.Add(overlay);
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            //Creamos el marcador
            marker.Position = new PointLatLng(lat, lng);
            //tooltip
            marker.ToolTipText = string.Format("Ubicación: \n Latitud {0} \n Longitud: {1}", lat, lng);

            // Calcular la distancia entre la posición actual y las coordenadas del DataFrame
            var currentCoord = new GeoCoordinate(lat, lng);
            CalcularDistancias(currentCoord);
        }



        private void CalcularDistancias(GeoCoordinate currentCoord)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                double latDB = Convert.ToDouble(row["lat"]);
                double lngDB = Convert.ToDouble(row["lng"]);


                var dbCoord = new GeoCoordinate(latDB, lngDB);

                double distancia = currentCoord.GetDistanceTo(dbCoord);

                // Verificar si la columna "distancia" existe en el DataFrame
                if (!dataTable.Columns.Contains("distancia"))
                {
                    // Si la columna no existe, crearla
                    dataTable.Columns.Add("distancia", typeof(double));
                }
                //dataGrid.Columns[5].Visible = false;

                // Actualizar o añadir el valor de la distancia en la columna "distancia"
                row["distancia"] = distancia;
            }


        }

        private void AgregarMarcadoresDesdeDataFrame(DataTable dataTable)
        {
            // Limpiar los marcadores existentes en el mapa
            //overlay.Markers.Clear();

            // Recorrer cada fila del DataTable
            for (int i = 0; i < Math.Min(dataTable.Rows.Count, 100); i++)
            {
                DataRow row = dataTable.Rows[i]; // Obtener la fila actual

                double lat = Convert.ToDouble(row["lat"]);
                double lng = Convert.ToDouble(row["lng"]);

                // Crear un marcador para cada fila y agregarlo a la capa de marcadores
                GMarkerGoogle marcador = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red);
                overlay.Markers.Add(marcador);
            }
        }


        private void OrdenarIntercambio()
        {
            stopwatch.Reset(); // Reinicia el Stopwatch
            stopwatch.Start(); // Inicia la medición del tiempo

            int n = dataTable.Rows.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    // Obtener las distancias de las filas en las posiciones j y j+1
                    double distancia1 = Convert.ToDouble(dataTable.Rows[j]["distancia"]);
                    double distancia2 = Convert.ToDouble(dataTable.Rows[j + 1]["distancia"]);

                    // Si la distancia de la fila en la posición j es mayor que la distancia de la fila en la posición j+1, intercambiarlas
                    if (distancia1 > distancia2)
                    {
                        object[] temp = dataTable.Rows[j].ItemArray;
                        dataTable.Rows[j].ItemArray = dataTable.Rows[j + 1].ItemArray;
                        dataTable.Rows[j + 1].ItemArray = temp;
                    }
                }
            }
            dataGrid.DataSource = dataTable;

            stopwatch.Stop(); // Detiene la medición del tiempo
                              // Actualiza la etiqueta con el tiempo de ejecución
            labelTime.Text = "Tiempo de ordenamiento (Intercambio): " + stopwatch.ElapsedMilliseconds + " ms";

            AgregarMarcadoresDesdeDataFrame(dataTable);
        }

        // Método para ordenar el DataFrame utilizando el algoritmo de Selección
        private void OrdenarSeleccion()
        {
            stopwatch.Reset(); // Reinicia el Stopwatch
            stopwatch.Start(); // Inicia la medición del tiempo

            int n = dataTable.Rows.Count;
            for (int i = 0; i < n - 1; i++)
            {
                // Encuentra el índice del elemento más pequeño en el subarray no ordenado
                int indiceMinimo = i;
                for (int j = i + 1; j < n; j++)
                {
                    double distanciaActual = Convert.ToDouble(dataTable.Rows[j]["distancia"]);
                    double distanciaMinima = Convert.ToDouble(dataTable.Rows[indiceMinimo]["distancia"]);
                    if (distanciaActual < distanciaMinima)
                    {
                        indiceMinimo = j;
                    }
                }

                // Crear nuevas filas y copiar los datos de las filas existentes
                DataRow filaTemporal1 = dataTable.NewRow();
                filaTemporal1.ItemArray = dataTable.Rows[i].ItemArray;
                DataRow filaTemporal2 = dataTable.NewRow();
                filaTemporal2.ItemArray = dataTable.Rows[indiceMinimo].ItemArray;

                // Reemplazar las filas en el DataTable
                dataTable.Rows[i].ItemArray = filaTemporal2.ItemArray;
                dataTable.Rows[indiceMinimo].ItemArray = filaTemporal1.ItemArray;
            }

            dataGrid.DataSource = dataTable;
            stopwatch.Stop(); // Detiene la medición del tiempo
                              // Actualiza la etiqueta con el tiempo de ejecución
            labelTime.Text = "Tiempo de ordenamiento (Selección): " + stopwatch.ElapsedMilliseconds + " ms";
            AgregarMarcadoresDesdeDataFrame(dataTable);
        }



        // Método para ordenar el DataFrame utilizando el algoritmo de Inserción
        private void OrdenarInsercion()
        {
            stopwatch.Reset(); // Reinicia el Stopwatch
            stopwatch.Start(); // Inicia la medición del tiempo

            int n = dataTable.Rows.Count;
            DataTable dataTableOrdenado = dataTable.Clone(); // Clonar la estructura del DataTable original

            for (int i = 0; i < n; i++)
            {
                DataRow filaActual = dataTable.Rows[i];
                double distanciaActual = Convert.ToDouble(filaActual["distancia"]);
                int j = i - 1;

                // Encontrar la posición correcta para la fila actual en el nuevo DataTable ordenado
                while (j >= 0 && Convert.ToDouble(dataTableOrdenado.Rows[j]["distancia"]) > distanciaActual)
                {
                    j--;
                }

                // Crear una nueva fila y copiar los valores de la fila actual
                DataRow nuevaFila = dataTableOrdenado.NewRow();
                nuevaFila.ItemArray = filaActual.ItemArray;

                // Insertar la nueva fila en la posición correcta en el nuevo DataTable ordenado
                dataTableOrdenado.Rows.InsertAt(nuevaFila, j + 1);
            }

            // Asignar el nuevo DataTable ordenado como origen de datos del DataGridView
            dataGrid.DataSource = dataTableOrdenado;

            stopwatch.Stop(); // Detiene la medición del tiempo
                              // Actualiza la etiqueta con el tiempo de ejecución
            labelTime.Text = "Tiempo de ordenamiento (Inserción): " + stopwatch.ElapsedMilliseconds + " ms";
            AgregarMarcadoresDesdeDataFrame(dataTableOrdenado);
        }




        private void buttonSort_Click(object sender, EventArgs e)
        {
            string metodoSeleccionado;
            // Verificar qué método de ordenamiento se seleccionó en el comboBox

            if (comboBox1.SelectedItem == null)
            {
                metodoSeleccionado = "Método Inserción";
                OrdenarInsercion();
            }
            else
            {
                metodoSeleccionado = comboBox1.SelectedItem.ToString();

                // Llamar al método de ordenamiento correspondiente
                switch (metodoSeleccionado)
                {
                    case "Método Intercambio":
                        OrdenarIntercambio();
                        break;
                    case "Método Selección":
                        OrdenarSeleccion();
                        break;
                    case "Método Inserción":
                        OrdenarInsercion();
                        break;
                    default:
                        // Manejar caso no válido (opcional)
                        break;
                }
            }
        }

    }
}
