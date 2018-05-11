using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String imie, nazwisko, adres, telefon;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ekran1();
        }

        private void Dalej_Click(object sender, RoutedEventArgs e)
        {
            string pattern="";
            Match match;
            switch (strona.Content) {
                case "1/5":
                    pattern = @"([A-Z]?[À-Ú])?[a-z à-ú]{1,29}$";
                    match = Regex.Match(tekst.Text, pattern, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        imie = zamianaWielkosci(tekst.Text);
                        ekran2();
                        if (nazwisko != "") tekst.Text = nazwisko;
                        else tekst.Text = "";
                        uwaga.Content = "";
                    }
                    else if (tekst.Text == "")
                        uwaga.Content = "Aby przejść do drugiego ekranu wpisz imię.";
                    else
                        uwaga.Content = "Aby przejść do drugiego ekranu wpisz poprawnie imię.";

                    break;
                case "2/5":
                    pattern = @"[A-Z]?[À-Ú]?[a-z à-ú]{1,29}$";
                    match = Regex.Match(tekst.Text, pattern, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        nazwisko = zamianaWielkosci(tekst.Text);
                        ekran3();
                        if (adres != "") tekst.Text = adres;
                        else tekst.Text = "";
                        uwaga.Content = "";
                    }
                    else if (tekst.Text == "")
                        uwaga.Content = "Aby przejść do trzeciego ekranu wpisz nazwisko.";
                    else
                        uwaga.Content = "Aby przejść do trzeciego ekranu wpisz poprawnie nazwisko (minimum 2 znaki).";

                    break;
                case "3/5": 
                    pattern = @"([A-Z]|[À-ÚŁ]){1}[a-zà-úł]{4,40}\ (([0-9]+\/{1}[0-9]+)|([0-9]+))(\ |\,\ ){1}([0-9]{2}\-{1}[0-9]{3})\ ([A-Z]|[À-ÚŁ]){1}[a-zà-úł]{4,40}$";
                    match = Regex.Match(tekst.Text, pattern, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        adres = zamianaZeSpacjami(tekst.Text);
                        ekran4();
                        if (telefon != "") tekst.Text = telefon;
                        else tekst.Text = "";
                        uwaga.Content = "";
                    }
                    else if (tekst.Text == "")
                        uwaga.Content = "Aby przejść do czwartego ekranu wpisz adres zamieszkania.";
                    else
                        uwaga.Content = "Aby przejść do czwartego ekranu wpisz poprawnie adres (patrz na podpowiedź).";
                    break;
                case "4/5":
                    pattern = @"((^[1-9]{1}[0-9]{8})|(^\+{1}[1-9]{1}[0-9]{10}))$";
                    match = Regex.Match(tekst.Text, pattern, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        telefon = tekst.Text;
                        ekran5();
                        tekst.Text = "";
                        uwaga.Content = "";
                    }
                    else if (tekst.Text == "")
                        uwaga.Content = "Aby przejść do piątego ekranu wpisz numer telefonu.";
                    else
                        uwaga.Content = "Aby przejść do piątego ekranu wpisz poprawnie numer telefonu - 123456789 lub +48123456789";
                    break;
            }
        }

        private void Wstecz_Click(object sender, RoutedEventArgs e)
        {
            switch (strona.Content)
            {
                case "2/5":
                    ekran1();
                    tekst.Text = imie;
                    uwaga.Content = "";
                    break;
                case "3/5":
                    ekran2();
                    tekst.Text = nazwisko;
                    uwaga.Content = "";
                    break;
                case "4/5":
                    ekran3();
                    tekst.Text = adres;
                    uwaga.Content = "";
                    break;

            }
        }

        private void ekran1() {
            textLabel.Content = "Wpisz swoje imię:";
            ToolTipService.SetToolTip(toolTip, "Imię powinno składać się z conajmniej 2 znaków. \nDozwolone są tylko litery, w tym znaki diakrytyczne.");
            tytul.Title = "Dane użytkownika - imię";
            wstecz.Visibility = Visibility.Hidden;
            strona.Content = "1/5";
        }

        private void ekran2() {
            textLabel.Content = "Wpisz swoje nazwisko:";
            ToolTipService.SetToolTip(toolTip, "Nazwisko powinno składać się z conajmniej 2 znaków. \nDozwolone są tylko litery, w tym znaki diakrytyczne.");
            tytul.Title = "Dane użytkownika - nazwisko";
            wstecz.Visibility = Visibility.Visible;
            strona.Content = "2/5";
        }

        private void ekran3() {
            textLabel.Content = "Wpisz swój adres zamieszkania:";
            ToolTipService.SetToolTip(toolTip, "Przykładowe poprawne adres:\n 'Testowa 3, 35-040 Rzeszów' lub 'Testowa 3 35-040 Rzeszów'.");
            tytul.Title = "Dane użytkownika - adres zamieszkania";
            wstecz.Visibility = Visibility.Visible;
            strona.Content = "3/5";
        }

        private void ekran4() {
            textLabel.Content = "Wpisz swój numer telefonu:";
            ToolTipService.SetToolTip(toolTip, "Numer telefonu powinien być zapisany jako '123456789' lub '+48123456789'.");
            tytul.Title = "Dane użytkownika - numer telefonu";
            wstecz.Visibility = Visibility.Visible;
            strona.Content = "4/5";

        }

        private void ekran5()
        {
            Window1 oknowynik = new Window1();
            oknowynik.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            oknowynik.Show();
            this.Close();
            oknowynik.imieWpisane.Content = imie;
            oknowynik.nazwiskoWpisane.Content = nazwisko;
            oknowynik.adresWpisany.Text = adres;
            oknowynik.telefonWpisany.Content = telefon;
        }

        private string zamianaWielkosci(string nazwa) {
            nazwa = nazwa.ToLower();
            int dlugosc = nazwa.Length;
            char[] pomocnicza = nazwa.ToCharArray();
            nazwa = "";
            nazwa += pomocnicza[0].ToString().ToUpper();
            for (int i = 1; i < dlugosc; i++)
                nazwa += pomocnicza[i];

            return nazwa;
        }

        private string zamianaZeSpacjami(string adres)
        {
            adres = adres.ToLower();
            int dlugosc = adres.Length;
            char[] pomocnicza = adres.ToCharArray();
            adres = "";
            adres += pomocnicza[0].ToString().ToUpper();
            for(int i=1; i < dlugosc; i++)
            {
                if (pomocnicza[i] == ' ' && pomocnicza[i + 1] >= 97 && pomocnicza[i + 1] <= 122)
                {
                    adres += pomocnicza[i];
                    adres += pomocnicza[i + 1].ToString().ToUpper();
                    i++;
                }
                else adres += pomocnicza[i].ToString();
            }
                
            return adres;
        }
    }

}
