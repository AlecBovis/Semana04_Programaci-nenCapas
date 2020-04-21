using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Business;
using Entity;

namespace Semana04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            BPedido bPedido = null;
            try
            {
                bPedido = new BPedido();
                dgvPedido.ItemsSource = bPedido.GetPedidosEntreFechas(Convert.ToDateTime(txtFechaInicio.Text),
                                                                      Convert.ToDateTime(txtFechaFin.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Comunicarse con el Administrador");
            }
            finally
            {
                bPedido = null;
            }
        }

        private void dgvPedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BDetallePedido bDetallePedido = null;
            try
            {
                bDetallePedido = new BDetallePedido();
                int idPedido;
                var item = (Pedido)dgvPedido.SelectedItem;
                if (null == item) return;
                idPedido = Convert.ToInt32(item.IdPedido);               
                dgvDetallePedido.ItemsSource = bDetallePedido.GetDetallePedidosPorId(idPedido);
                txtTotal.Text = Convert.ToString(bDetallePedido.GetDetalleTotalPorId(idPedido));
            }
            catch (Exception)
            {
                MessageBox.Show("Comunicarse con el Administrador");
            }
            finally
            {
                bDetallePedido = null;
            }
        }
    }
}
