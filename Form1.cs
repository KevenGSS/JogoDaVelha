using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaVelha
{
  public partial class Form1 : Form
  {
    private char jogadorAtual = 'X';
    private Button[] botoes;

    public Form1()
    {
      InitializeComponent();
      InicializarJogo();
    }

    private void InicializarJogo()
    {
      // Lista de botões para facilitar o controle
      botoes = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9};

      // Adicionar evento de clique a todos os botões do tabuleiro
      foreach (var botao in botoes)
      {
        botao.Text = string.Empty;
        botao.Enabled = true;
        botao.Click += Botao_Click;
      }

      lblstatus.Text = "Vez do jogador X";
      jogadorAtual = 'X';
    }

    private void Botao_Click(object sender, EventArgs e)
    {
      Button botaoClicado = (Button)sender;
      botaoClicado.Text = jogadorAtual.ToString();
      botaoClicado.Enabled = false;

      // Verificar vitória
      if (VerificarVitoria())
      {
        lblstatus.Text = $"Jogador {jogadorAtual} venceu!";
        DesabilitarBotoes();
        return;
      }

      // Verificar empate
      if (VerificarEmpate())
      {
        lblstatus.Text = "Empate!";
        return;
      }

      // Trocar de jogador
      jogadorAtual = jogadorAtual == 'X' ? 'O' : 'X';
      lblstatus.Text = $"Vez do jogador {jogadorAtual}";
    }

    private bool VerificarVitoria()
{
    // Possíveis combinações vencedoras
    int[,] combinacoes = {
        { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, // Linhas
        { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, // Colunas
        { 0, 4, 8 }, { 2, 4, 6 }              // Diagonais
    };

    for (int i = 0; i < combinacoes.GetLength(0); i++) // Iterar pelas combinações
    {
        if (botoes[combinacoes[i, 0]].Text == jogadorAtual.ToString() &&
            botoes[combinacoes[i, 1]].Text == jogadorAtual.ToString() &&
            botoes[combinacoes[i, 2]].Text == jogadorAtual.ToString())
        {
            return true;
        }
    }

    return false;
}


    private bool VerificarEmpate()
    {
      foreach (var botao in botoes)
      {
        if (string.IsNullOrEmpty(botao.Text))
        {
          return false;
        }
      }
      return true;
    }

    private void DesabilitarBotoes()
    {
      foreach (var botao in botoes)
      {
        botao.Enabled = false;
      }
    }

    private void btnReiniciar_Click(object sender, EventArgs e)
    {
      InicializarJogo();
    }

    private void btnReiniciar_Click_1(object sender, EventArgs e)
    {
      this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
    }
  }
}
