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
                    ABotones[x].BOTON.Height = 40;
                    ABotones[x].BOTON.Width = 80;
                    ABotones[x].BOTON.Left = 100;
                }
                else
                {
                    ABotones[x].BOTON.Top = k * (ABotones.Length-x);
                    ABotones[x].BOTON.BackColor = Color.Red;
                    ABotones[x].BOTON.Height = 40;
                    ABotones[x].BOTON.Width = 80;

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
            /*Trabajando con Programacion Orientada a Objetos->Podemos crear objetos de clases,
             * 
             * si guardamos en una clase un boton, su velocidad y su valor cuando creamos el objeto tendra esos atributos
             * Podemos acceder a los atributos Objeto.Atributo.blabla
             * Como guardamos el boton podemos usar los metodos y propiedades del boton normalmente
             * Objeto.Boton.Text por ejemplo, seria como usar el boton normal, pero siendo parte de algo
             * Asi que para manejar los botones, usamos arreglos ya sabemos trabajar con ellos asi, nos sera mas sencillo para las colisiones y eso:b
             * Pero si agregamos una lista podemos controlar cuando se crean botones, cuando se eliminan y podemos ahorrar lineas de codigo, las listas tienen una propiedad para copiar lo que hay una lista en un arreglo
             * si queremos borrar algo de un arreglo como le hacemos? batallariamos mas, con las listas es mas facil usando el comando remove, y actualizamos el arreglo con CopyTo(), asi tenemos el arreglo actualizado cada que se creen o eliminen
               Y pues lo de los casos en los que se eliminan, se tragan y esas cosas son lo facil
             * P.e. Choque entre 2 botones de diferente color -> se elimina el menor y se le suma al mayor
             * if(ABotones[x].BOTON.BackColor!=ABotones[j].BackColor)
             * ABotones[x].VAL+=ABotones[j].VAL
             * ListaB.Remove(ABotones[j])
             * 
                 */
        }
    }
    class Botones 
    {//Atributos del objeto
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
