using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assessment{
    public partial class Form1 : Form{

        public Button[,] lights = new Button[5, 5];
        public bool[,] GridLights = new bool[5, 5];

        public MessageBoxButtons MB_OK { get; private set; }

        public Form1(){
            InitializeComponent();
            randomStart();
        }

        public void randomStart(){
            for (int i = 0; i < lights.GetLength(1); i++){
                for (int j = 0; j < lights.GetLength(0); j++){
                    lights[i, j] = new Button();
                    lights[i, j].Size = new Size(50, 50);
                    lights[i, j].Name = i.ToString() + j.ToString();
                    lights[i, j].Click += light_Click;
                    lights[i, j].Location = new Point(30 + (j * 50), 20 + (i * 50));
                    lights[i, j].BackColor = Color.Gray;
                    GridLights[i, j] = false;
                    this.Controls.Add(lights[i, j]);
                }
            }

            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(1, 10); i++){
                int x = rnd.Next(0, lights.GetLength(1));
                int y = rnd.Next(0, lights.GetLength(0));
                invertButton(lights[x, y], x, y);
            }

            if (checkStatus() == true){
                int x = rnd.Next(0, lights.GetLength(1));
                int y = rnd.Next(0, lights.GetLength(0));
                invertButton(lights[x, y], x, y);
            }
        }

        public void light_Click(object sender, EventArgs e){
            Button b = sender as Button;
            int i = (int)Char.GetNumericValue(b.Name[0]);
            int j = (int)Char.GetNumericValue(b.Name[1]);

            invertHandler(lights[i, j], i, j);

            checkEnd();
        }

        public void invertHandler(object sender, int i, int j){
            invertButton(lights[i, j], i, j);

            if (i > 0){
                invertButton(lights[i - 1, j], i - 1, j);
            }
            if (i < (lights.GetLength(1) - 1)){
                invertButton(lights[i + 1, j], i + 1, j);
            }
            if (j > 0){
                invertButton(lights[i, j - 1], i, j - 1);
            }
            if (j < (lights.GetLength(1) - 1)){
                invertButton(lights[i, j + 1], i, j + 1);
            }

        }

        public void invertButton(object sender, int i, int j){
            Button b = sender as Button;

            GridLights[i, j] = !GridLights[i, j];

            if (GridLights[i, j] == true){
                b.BackColor = Color.Red;
            }
            else{
                b.BackColor = Color.Gray;
            }

        }

        public bool checkStatus(){
            for (int i = 0; i < GridLights.GetLength(1); i++){
                for (int j = 0; j < GridLights.GetLength(0); j++){
                    if (GridLights[i, j] == true){
                        return false;
                    }
                }
            }

            return true;
        }

        public void checkEnd(){
            if (checkStatus() == true)
            {
                MessageBox.Show("You Win!",
                    "Congratulations!",
                     MB_OK);
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}