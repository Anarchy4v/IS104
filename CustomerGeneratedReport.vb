Imports MySql.Data.MySqlClient

Public Class CustomerGeneratedReport
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

    Public Sub New()
        InitializeComponent()

        Dim currentDateAndTime As DateTime = DateTime.Now
        Label2.Text = $"{currentDateAndTime}"

        'auto closes fucking salesWindow
        If Not IsNothing(Application.OpenForms("SalesWindow")) Then
            CType(Application.OpenForms("SalesWindow"), SalesWindow).Close()
        End If

        LoadDataIntoDataGridView()

        ' Display data from SharedVariables
        Label5.Text = $"{SharedVariables.discount:P}"
        Label7.Text = $"₱{SharedVariables.totalSalesPrice:N2}"
        Label9.Text = $"₱{SharedVariables.cashValue:N2}"
        Label11.Text = $"₱{SharedVariables.Result:N2}"
        Label13.Text = $"₱{SharedVariables.vatAmount:N2}"
    End Sub

    Private Sub LoadDataIntoDataGridView()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT sales_name, sales_qty, sales_price FROM compute_sales"
                Using adapter As New MySqlDataAdapter(query, connection)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)

                    ' Bind data to DataGridView
                    DataGridView1.DataSource = dataTable
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions that might occur during data retrieval
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' this will delete all existing rows in my compute_sales
    Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)

        If e.CloseReason = CloseReason.UserClosing Then
            Try
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim deleteQuery As String = "DELETE FROM compute_sales"
                    Using command As New MySqlCommand(deleteQuery, connection)
                        command.ExecuteNonQuery()
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show($"Error executing SQL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

End Class
