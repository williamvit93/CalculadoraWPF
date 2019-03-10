using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnUm_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "1)" : "1";
        }

        private void BtnDois_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "2)" : "2";
        }

        private void BtnTres_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "3)" : "3";
        }

        private void BtnQuatro_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "4)" : "4";
        }

        private void BtnCinco_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "5)" : "5";
        }

        private void BtnSeis_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "6)" : "6";
        }

        private void BtnSete_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "7)" : "7";
        }

        private void BtnOito_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "8)" : "8";
        }

        private void BtnNove_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "9)" : "9";
        }

        private void BtnZero_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhMultiplicaoOuDivisao() ? "0)" : "0";
        }

        private void BtnApagar_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = txtResultado.Text.Remove(txtResultado.Text.Count() - 1);
        }

        private void BtnApagarTudo_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text = "";
        }

        private void BtnAdicao_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhNumero() || EhParenteses() ? " + " : "";
        }

        private void BtnSubtracao_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.Text += EhNumero() || EhParenteses() ? " - " : "";
        }

        private void BtnMultiplicacao_Click(object sender, RoutedEventArgs e)
        {
            AbrirParentesesExpressao();
            txtResultado.Text += EhNumero() ? "x" : "";
        }

        private void BtnDivisao_Click(object sender, RoutedEventArgs e)
        {
            AbrirParentesesExpressao();
            txtResultado.Text += EhNumero() ? "/" : "";
        }

        private void BtnPontuacao_Click(object sender, RoutedEventArgs e)
        {
            if (!char.IsDigit(txtResultado.Text.LastOrDefault())) return;
            txtResultado.Text += ',';
        }

        private void BtnResultado_Click(object sender, RoutedEventArgs e)
        {
            double? resultado = null;
            if (txtResultado.Text.Contains("("))
            {
                var array = txtResultado.Text.Split(' ').ToList();
                do
                {
                    var indiceExpressao = array.FindIndex(x => x.Contains("("));
                    var expressao = array[indiceExpressao];
                    double? resultadoExpressao = null;

                    if (expressao.Split('x').Length > 1)
                    {
                        var arrEx = expressao.Split('x', '(', ')').ToList();
                        arrEx.RemoveAll(x => x == "");
                        resultadoExpressao = Convert.ToDouble(arrEx[0]) * Convert.ToDouble(arrEx[1]);
                    }
                    else
                    {
                        var arrEx = expressao.Split('/', '(', ')').ToList();
                        arrEx.RemoveAll(x => x == "");
                        resultadoExpressao = Convert.ToDouble(arrEx[0]) / Convert.ToDouble(arrEx[1]);
                    }

                    array[indiceExpressao] = resultadoExpressao.ToString();
                } while (array.FindIndex(x => x.Contains("(")) != -1);

                txtResultado.Text = String.Join(" ", array);
                resultado = SomarEOuSubtrair();

            }
            else
            {
                resultado = SomarEOuSubtrair();
            }


            txtResultado.Text = resultado.ToString();
        }

        private double? SomarEOuSubtrair()
        {
            var array = txtResultado.Text.Split(' ');
            var listaNumeros = new List<double>();
            var listaOperadores = new List<char>();
            double? resultado = null;

            foreach (var item in array)
            {
                try
                {
                    double numero = Convert.ToDouble(item);
                    listaNumeros.Add(numero);
                }
                catch (Exception)
                {
                    var operador = item.ToCharArray()[0];
                    listaOperadores.Add(operador);
                }
            }

            int j = 0;
            double termo;

            for (int i = 0; i < listaNumeros.Count(); i++)
            {
                termo = listaNumeros[i];

                if (resultado == null)
                {
                    resultado = termo;
                    continue;
                }

                resultado = listaOperadores[j] == '+' ? resultado += termo : resultado -= termo;
                j++;
            }

            return resultado;
        }

        private bool EhMultiplicaoOuDivisao()
        {
            if (txtResultado.Text.Count() == 0) return false;

            return txtResultado.Text.ElementAt(txtResultado.Text.Count() - 1) == 'x'
                      || txtResultado.Text.ElementAt(txtResultado.Text.Count() - 1) == '/';
        }

        private bool EhNumero()
        {
            return char.IsDigit(txtResultado.Text.LastOrDefault());
        }

        private bool EhParenteses()
        {
            return txtResultado.Text.LastOrDefault() == ')';
        }

        private void AbrirParentesesExpressao()
        {

            var indiceNumeroAnterior = txtResultado.Text.LastIndexOf(" ") == -1 ? 0 : txtResultado.Text.LastIndexOf(" ") + 1;
            var resultado = txtResultado.Text.ToList();
            if (resultado[indiceNumeroAnterior] == ')') return;
            resultado.Insert(indiceNumeroAnterior, '(');
            txtResultado.Text = String.Join("", resultado);
        }
    }
}
