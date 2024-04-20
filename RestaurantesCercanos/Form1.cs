using System.Data;
using System.Data.SqlClient;
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


        public Form1()
        {
            InitializeComponent();
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
                    string query = "SELECT * FROM merged_data";

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
            marker.ToolTipText = string.Format("Ubicación: \n Latitud {0} \n Longitud: {1}", LatInicial,LogInicial);

            gMapControl1.Overlays.Add(overlay);
        }

    }
}
