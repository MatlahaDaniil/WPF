using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace KeyboardTrainer
{
    internal sealed partial class MainWindow : Window
    {
        int fails , correct_chars , time;
        Keyboard keyboard = new Keyboard();
        bool Caps = false;
        bool Shift = false;
        bool Game_is_Started = false;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            difficult_txtBlock.Text = Difficult_slider.Value.ToString();
            Stop_btn.IsEnabled = false;

            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += Timer_Tick;


        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            ++time;
            Update_Speed();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Shift)
            {
                Lower_Registr_Key();
                Shift = false;
            }
              
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.KeyboardDevice.IsKeyDown(Key.LeftShift))
            {
                Upper_Registr_Key();
                Shift = true;

            }


            if (Game_is_Started)
            {
                string key = e.Key.ToString();

                bool Check = Check_Key(ref key);

                if (key == "Capital" && !Caps)
                    Caps = true;
                else if (key == "Capital" && Caps)
                    Caps = false;

                if (Caps && key == "Capital")
                    Upper_Registr_Key();
                else if (!Caps && key == "Capital")
                    Lower_Registr_Key();

                if (key == "LeftShift")
                    Upper_Registr_Key();


                if (Check)
                {
                    if (!Caps && !Shift)
                    {
                        TxtBlock_for_keys.Text = key.ToLower();
                        key = key.ToLower();
                    }
                    else
                    {
                        TxtBlock_for_keys.Text = key.ToUpper();
                        key.ToUpper();
                    }

                    if (key[0] == TxtBlock_for_str.Text[0])
                    {
                        TxtBlock_for_str.Text = TxtBlock_for_str.Text.Remove(0, 1);
                        ++correct_chars;
                    }
                    else
                    {
                        fails++;
                    }


                    TxtBlock_for_fails.Text = fails.ToString();

                    if (TxtBlock_for_str.Text.Length < 70)
                        TxtBlock_for_str.Text += keyboard.generateSymbols((int)Difficult_slider.Value, (bool)Upper_checkBox.IsChecked);
                }
            }
        }

        void Upper_Registr_Key()
        {      
            Lower_Key_Registr.Visibility = Visibility.Collapsed;
            Upper_Key_Registr.Visibility = Visibility.Visible;
        }
        void Lower_Registr_Key()
        {
            Lower_Key_Registr.Visibility = Visibility.Visible;
            Upper_Key_Registr.Visibility = Visibility.Collapsed;
        }

        private void Start_btn_Click(object sender, RoutedEventArgs e)
        {
            Game_is_Started = true;
            if (Game_is_Started)
            {
                timer.Start();
                TxtBlock_for_str.Text = keyboard.generateSymbols((int)Difficult_slider.Value, (bool)Upper_checkBox.IsChecked);
                Start_btn.IsEnabled = false;
                Difficult_slider.IsEnabled = false;
                Upper_checkBox.IsEnabled = false;
                Stop_btn.IsEnabled = true;
            }
        }

        private void Stop_btn_Click_1(object sender, RoutedEventArgs e)
        {
            string result_game = Get_Result_String();
            Caps = false;
            Game_is_Started = false;
            Start_btn.IsEnabled = true;
            Difficult_slider.IsEnabled = true;
            Upper_checkBox.IsEnabled = true;
            Stop_btn.IsEnabled = false;
            TxtBlock_for_keys.Text = "";
            TxtBlock_for_str.Text = "";
            timer.Stop();
            fails = 0;
            correct_chars = 0;
            time = 0;

            TxtBlock_for_fails.Text = fails.ToString();
            MessageBox.Show(result_game, "END" , MessageBoxButton.OK , MessageBoxImage.Asterisk);
        }

        private bool Check_Key( ref string key)
        {
            switch (key)
            {

                case "Capital"   : return false;
                case "LeftCtrl"  : return false;
                case "Decimal"   : return false;
                case "Return"    : return false;
                case "Tab"       : return false;
                
                case "Shift"     : return false;
                case "System"    : return false;
                case "LeftShift" : return false;
                case "RWin"      : return false;
                case "LWin"      : return false;
                case "Back"      : return false;
                case "Escape"    : return false;
                case "Oem3"      : return false;
                case "Add"       : return false;
                case "Subtract"  : return false;
                case "Multiply"  : return false;
                case "Divide"    : return false;
                case "NumLock"   : return false;

                case "D0"        : key = "0"; return true;
                case "D1"        : key = "1"; return true;
                case "D2"        : key = "2"; return true;
                case "D3"        : key = "3"; return true;
                case "D4"        : key = "4"; return true;
                case "D5"        : key = "5"; return true;
                case "D6"        : key = "6"; return true;
                case "D7"        : key = "7"; return true;
                case "D8"        : key = "8"; return true;
                case "D9"        : key = "9"; return true;
                case "Space"     : key = " "; return true;
                default          : return true;
            }
        }

        private void Difficult_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            difficult_txtBlock.Text = ((int)Difficult_slider.Value).ToString();
        }

        void Update_Speed()
        {
            TxtBlock_for_Speed.Text = ((correct_chars * 60) / time).ToString();
        }

        string Get_Result_String()
        {
            string res = "";
            res += $"fails                 :             {fails}\n";
            res += $"Correct Chars :             {correct_chars}\n";
            res += $"time                 :             {time} c\n";
            res += $"speed              :             {TxtBlock_for_Speed.Text} chars/min\n";

            return res;
        }

    }

    class Keyboard
    {
        string chars = "abcdefghijklmnopqrstuvwxz1234567890";

        public string generateSymbols(int difficult , bool Upper)
        {                        
            Random rand_for_Upper = new Random();
            Random rand_for_space = new Random();
            Random random = new Random();    
            string str = "";

            switch (difficult)
            {
                case 1:       
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 25; j++)
                        {
                            int index = random.Next(0, 25) , length = random.Next(0,10);
                            for (int q = 0; q < length; q++)
                            {
                                str += chars[index];
                                if(random.Next(0,5) == 1)
                                {
                                    str += " ";
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 25; j++)
                        {
                            int index = random.Next(0, 26), length = random.Next(0, 5);
                            for (int q = 0; q < length; q++)
                            {
                                str += chars[index];
                                if (random.Next(0, 3) == 1)
                                {
                                    str += " ";
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 25; j++)
                        {
                            int index = random.Next(0, chars.Length), length = random.Next(0, 3);
                            for (int q = 0; q < length; q++)
                            {
                                str += chars[index];
                                if (random.Next(0, 3) == 1)
                                {
                                    str += " ";
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    for (int i = 0; i < 100; i++)
                    {
                        string temp = "";
                        str += chars[random.Next(0, chars.Length)];
                        if (rand_for_space.Next(0, 3) == 1) str += " ";
                        if (rand_for_Upper.Next(0, 5) == 1 && Upper)
                        {
                            temp += chars[random.Next(0, chars.Length)];
                            str += temp.ToUpper();
                        }
                    }
                    break;
                    break;
                case 5:
                    for (int i = 0; i < 100; i++)
                    {
                        string temp = "";
                        str += chars[random.Next(0, chars.Length)];
                        if (rand_for_space.Next(0, 3) == 1) str += " ";
                        if (rand_for_Upper.Next(0, 3) == 1 && Upper)
                        {
                            temp += chars[random.Next(0, chars.Length)];
                            str += temp.ToUpper();
                        }
                    }
                    break;
            }
            return str;
        }

    }
}