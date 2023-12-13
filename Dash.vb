Imports MySql.Data.MySqlClient

Public Class Dash
    Dim random As New Random()
    Private formClosingByButton As Boolean = False
    Private formClosingBySystem As Boolean = False
    Private userEmail As String

    Public Property UserEmailProperty As String
        Get
            Return userEmail
        End Get
        Set(value As String)
            userEmail = value
            Label2.Text = userEmail
        End Set
    End Property

    Private Sub LoadMedicines()
        Try
            Dim connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Modify the query to retrieve rows where @item_qty is less than or equal to 50
                Dim query As String = "SELECT item_name, item_qty, item_dosage, category, item_price FROM Inventory WHERE item_qty <= 50"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        If dataTable.Rows.Count > 0 Then
                            DataGridView1.DataSource = dataTable
                        Else
                            'MessageBox.Show("No medicines stock alert at the moment.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ' Optionally, you can clear the DataGridView if no rows are found
                            DataGridView1.DataSource = Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading medicines: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        LoadMedicines()
        'I add this because of fatigue for nothing.
        Me.AcceptButton = Nothing
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard active
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sales
        Dim sales As New Sales()
        sales.Show()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        'idk this
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory
        Dim inv As New Inventory()
        inv.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Assuming you have a function to retrieve order details, replace it with the actual code
        Dim orderDetails As DataTable = GetOrderDetails()

        ' Open SalesReport form and pass the order details
        Dim salesReportForm As New SalesReport(orderDetails)
        salesReportForm.Show()

        ' Close the current Dash form
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        'settings
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Function GetOrderDetails() As DataTable
        ' Replace this with the actual code to retrieve order details from your database or another source
        Try
            Dim connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM OrderDetails"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        Return dataTable
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving order details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs)
        'search box???
    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs)
        'data grid view
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Generate syempre live count haha
        Dim randomNumber As Integer = random.Next(1, 301)
        Dim randomNumber1 As Integer = random.Next(1, 401)
        Dim randomNumber2 As Integer = random.Next(1, 501)
        Dim randomNumber3 As Integer = random.Next(1, 201)

        Label7.Text = randomNumber.ToString()
        Label8.Text = randomNumber1.ToString()
        Label9.Text = randomNumber2.ToString()
        Label10.Text = randomNumber3.ToString()
    End Sub

    Private Sub DataGridView1_CellContentClick_2(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'I need you to display on load the existing rows who does have an few @item_qty VALUE left starting from 50 in exact.
        'Use the inventory table declaration in my database to fetch and display it into DataGridView1
    End Sub
End Class