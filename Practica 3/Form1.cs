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
        List<Botones> ListaB = new List<Botones>();//Lista de botones
        Botones[] ABotones = new Botones[6];//Empieza con 6
        Random ran = new Random();
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            int k = 110;
            for (int x = 0; x < ABotones.Length; x++)//Para trabajar con objeto boton 
            {
                //ABotones[].PROPIEDAD
                ABotones[x] = new Botones(new Point(ran.Next(-10, 10), ran.Next(-10, 10)), ran.Next(1, 6), new Button());//Primer point velocidad, segundo el valor, tercero el boton
                ABotones[x].BOTON.Text = ABotones[x].VAL.ToString();
                if (x < ABotones.Length / 2)//3 de un color, 3 de otro
                {
                    ABotones[x].BOTON.Top = k * (x + 1);
                    ABotones[x].BOTON.BackColor = Color.Blue;
                    ABotones[x].BOTON.ForeColor = Color.White;
                    ABotones[x].BOTON.Height = 40;
                    ABotones[x].BOTON.Width = 80;
                    ABotones[x].BOTON.Left = 100;
                }
                else
                {
                    ABotones[x].BOTON.Top = k * (ABotones.Length - x);
                    ABotones[x].BOTON.BackColor = Color.Red;
                    ABotones[x].BOTON.Height = 40;
                    ABotones[x].BOTON.ForeColor = Color.White;
                    ABotones[x].BOTON.Width = 80;
                    ABotones[x].BOTON.Left = 300;
                }
                this.Controls.Add(ABotones[x].BOTON);
                ListaB.Add(ABotones[x]);//Se gregan los botones a una lista para manipular el arreglo posteriormente aprovechando sus propiedades de la lista
            }
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Botones b in ListaB)
            {
                b.Movimiento(this.ClientRectangle);//Movimiento normal
            }

            for (int i = 0; i < ABotones.Length; i++)
            {
                for (int x = 0; x < ABotones.Length; x++)
                {
                    if (i != x && ABotones[i].BOTON.Bounds.IntersectsWith(ABotones[x].BOTON.Bounds))
                    {
                        Colision(ABotones[i], ABotones[x]);//Si hay colision manda
                    }
                }
            }
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();
            Refresh();
        }
        private void Colision(Botones a, Botones b)
        {
            //Choque entre 2 botones de diferente color -> se elimina el menor y se le suma al mayor
            if (a.BOTON.BackColor != b.BOTON.BackColor)
            {
                if (a.VAL > b.VAL)
                {
                    a.VAL += b.VAL;
                    a.BOTON.Text = a.VAL.ToString();
                    this.Controls.Remove(b.BOTON);
                    ListaB.Remove(b);//Elimina el menor
                    Array.Resize(ref ABotones, ABotones.Length - 1);//Redimensiona
                    ListaB.CopyTo(ABotones);//Copia
                }
                else
                {
                    b.VAL += a.VAL;
                    b.BOTON.Text = b.VAL.ToString();
                    this.Controls.Remove(a.BOTON);
                    ListaB.Remove(a);//Elimina el valor
                    Array.Resize(ref ABotones, ABotones.Length - 1);//Redimensiona
                    ListaB.CopyTo(ABotones);//Copia
                }
            }

            //En caso de ser pares y del mismo color se suma la cantidadde ambos y sera el numero de botones a generar, con su respectivo numero aleatorio
            else if ((a.VAL % 2 == 0 && b.VAL % 2 == 0) && a.BOTON.BackColor == b.BOTON.BackColor)
            {
                this.Controls.Remove(a.BOTON);
                this.Controls.Remove(b.BOTON);
                ListaB.Remove(a);
                ListaB.Remove(b);
                ListaB.CopyTo(ABotones);
                int T = a.VAL + b.VAL;
                Array.Resize(ref ABotones, ABotones.Length + T - 2);

                for (int x = ABotones.Length - T; x < ABotones.Length; x++)//Para trabajar con objeto boton 
                {
                    ABotones[x] = new Botones(new Point(ran.Next(-10, 10), ran.Next(-10, 10)), ran.Next(1, 6), new Button());//Primer point velocidad, segundo el valor, tercero el boton
                    ABotones[x].BOTON.Text = ABotones[x].VAL.ToString();
                    ABotones[x].BOTON.Top = ran.Next(0, this.ClientRectangle.Height - 40);
                    ABotones[x].BOTON.BackColor = a.BOTON.BackColor;
                    ABotones[x].BOTON.ForeColor = Color.White;
                    ABotones[x].BOTON.Height = 40;
                    ABotones[x].BOTON.Width = 80;
                    ABotones[x].BOTON.Left = ran.Next(0, this.ClientRectangle.Width - 80);

                    this.Controls.Add(ABotones[x].BOTON);
                    ListaB.Add(ABotones[x]);
                }
            }
            //Choque entre 2 de un mismo color, si es impar se eliminan ambos
            else if (a.BOTON.BackColor == b.BOTON.BackColor && (a.VAL % 2 != 0 && b.VAL % 2 != 0))
            {
                this.Controls.Remove(a.BOTON);
                this.Controls.Remove(b.BOTON);
                ListaB.Remove(a);
                ListaB.Remove(b);
                Array.Resize(ref ABotones, ABotones.Length - 2);
                ListaB.CopyTo(ABotones);//Copia
            }

               //si uno es par y el otro impar de el mismo color, se toma el de mayor numeraciony se resta el otro, que debera desaparecer
            else if (a.BOTON.BackColor == b.BOTON.BackColor && ((a.VAL % 2 == 0 && b.VAL % 2 != 0) || (a.VAL % 2 != 0 && b.VAL % 2 == 0)))
            {
                if (a.VAL > b.VAL)
                {
                    a.VAL -= b.VAL;
                    a.BOTON.Text = a.VAL.ToString();
                    this.Controls.Remove(b.BOTON);
                    ListaB.Remove(b);
                    ListaB.CopyTo(ABotones);
                    Array.Resize(ref ABotones, ABotones.Length - 1);
                }
                else
                {
                    b.VAL -= a.VAL;
                    b.BOTON.Text = b.VAL.ToString();
                    this.Controls.Remove(a.BOTON);
                    ListaB.Remove(a);
                    ListaB.CopyTo(ABotones);
                    Array.Resize(ref ABotones, ABotones.Length - 1);
                }
            }
        }
    }
    class Botones
    {
        public int VAL;//Valor que tiene el boton 1-6
        public Point MOV;
        public Button BOTON;//Recibe el boton
        public Botones(Point mov, int val, Button Boton) { MOV = mov; VAL = val; BOTON = Boton; }//Recibe botón

        public void Movimiento(Rectangle Pantalla)
        {
            BOTON.Left += MOV.X;
            BOTON.Top += MOV.Y;//Movimiento general

            //Rebotes con las paredes
            if (BOTON.Left < 0 || BOTON.Left + BOTON.Width > Pantalla.Width)
                MOV.X *= -1;
            if (BOTON.Top + BOTON.Height > Pantalla.Height || BOTON.Top < 0)
                MOV.Y *= -1;
        }
    }
}
