Imports MySql.Data.MySqlClient

Public Class CustomerGeneratedReport
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

    Public Sub New()
        InitializeComponent()

        ' Display data from SharedVariables
        Label5.Text = $"{SharedVariables.discount:P}"
        Label7.Text = $"₱{SharedVariables.totalSalesPrice:N2}"
        Label9.Text = $"₱{SharedVariables.cashValue:N2}"
        Label11.Text = $"₱{SharedVariables.Result:N2}"
        Label13.Text = $"₱{SharedVariables.vatAmount:N2}"
    End Sub
End Class
