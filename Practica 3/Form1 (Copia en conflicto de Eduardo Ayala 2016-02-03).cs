using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//Se invocacan 6 botones, 3 de un color 3 de otro
//Se asignan numeros del 1-6 (aleatorios) a los botones
//Choque entre 2 botones de diferente color -> se elimina el menor y se le suma al mayor
//Choque entre 2 de un mismo color, si es impar se eliminan ambos
//En caso de ser pares y del mismo color se suma la cantidadde ambos y sera el numero de botones a generar, con su respectivo numero aleatorio
//si uno es par y el otro impar de el mismo color, se toma el de mayor numeraciony se resta el otro, que debera desaparecer

namespace Practica_3
{
    public partial class frmPrincipal : Form
    {
        List<Botones>ListaB=new List<Botones>();//Lista de botones
        Botones[]ABotones=new Botones[6];//Empieza con 6
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            Random ran=new Random();
            int k=110;
            for(int x=0;x<ABotones.Length;x++)//Para trabajar con objeto boton 
            {
                //ABotones[].PROPIEDAD
                ABotones[x] = new Botones(new Point(ran.Next(-10, 10), ran.Next(-10, 10)), ran.Next(1, 6), new Button());//Primer point velocidad, segundo el valor, tercero el boton
                ABotones[x].BOTON.Text=ABotones[x].VAL.ToString();
                if (x < ABotones.Length/2)//3 de un color, 3 de otro
                {
                    ABotones[x].BOTON.Top = k * (x + 1);
                    ABotones[x].BOTON.BackColor = Color.Blue;
                    ABotones[x].BOTON.Left = 100;
                }
                else
                {
                    ABotones[x].BOTON.Top = k * (ABotones.Length-x);
                    ABotones[x].BOTON.BackColor = Color.Red;
                    ABotones[x].BOTON.Left = 300;
                }
                this.Controls.Add(ABotones[x].BOTON);
                ListaB.Add(ABotones[x]);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Botones b in ListaB)
            {
                b.Movimiento(this.ClientRectangle);//Movimiento normal
            }

            for (int i = 0; i < ABotones.Length - 1; i++)
            {
                for (int x = 1; x < ABotones.Length; x++)
                {
                    if (ABotones[i] != ABotones[x] && ABotones[i].BOTON.Bounds.IntersectsWith(ABotones[x].BOTON.ClientRectangle))
                    {
                        Colision(ABotones[i], ABotones[x]);//Si hay colision manda
                    }
                }
            }
        }
        private void Colision(Botones a, Botones b)
        {
           //Casos

        }
    }
    class Botones 
    {
        public int VAL;//Valor que tiene el boton 1-6
        public Point MOV;
        public Button BOTON;//Recibe el boton
        public Botones(Point mov,int val, Button Boton) { MOV = mov;VAL=val; BOTON = Boton; }//Recibe botón

        public void Movimiento(Rectangle Pantalla) 
        {
            BOTON.Left += MOV.X; 
            BOTON.Top += MOV.Y;//Movimiento general

            //Rebotes con las paredes
            if (BOTON.Left < 0 || BOTON.Left+BOTON.Width > Pantalla.Width)
                MOV.X *= -1;
            if (BOTON.Top+BOTON.Height > Pantalla.Height || BOTON.Top < 0)
                MOV.Y *= -1;
        }
    }
}
